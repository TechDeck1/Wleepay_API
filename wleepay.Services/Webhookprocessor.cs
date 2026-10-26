using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class WebhookProcessor : BackgroundService
    {
        private const int MaxAttempts = 3;

        private readonly IWebhookQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<WebhookProcessor> _logger;

        public WebhookProcessor(
            IWebhookQueue queue,
            IServiceScopeFactory scopeFactory,
            ILogger<WebhookProcessor> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WebhookProcessor started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                WebhookJob job;

                try
                {
                    job = await _queue.DequeueAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                await ProcessWithRetryAsync(job, stoppingToken);
            }

            _logger.LogInformation("WebhookProcessor stopped.");
        }

        private async Task ProcessWithRetryAsync(WebhookJob job, CancellationToken ct)
        {
            for (var attempt = 1; attempt <= MaxAttempts; attempt++)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var webhookService = scope.ServiceProvider.GetRequiredService<IWebhookService>();
                    await webhookService.ProcessWebhookAsync(job.Json);
                    return;
                }
                catch (Exception ex) when (!(ex is OperationCanceledException))
                {
                    _logger.LogError(ex, "Webhook {EventId} failed (attempt {Attempt}/{Max}).",
                        job.EventId, attempt, MaxAttempts);

                    if (attempt == MaxAttempts)
                    {
                        _logger.LogCritical("Webhook {EventId} gave up after {Max} attempts. Manual review required.",
                            job.EventId, MaxAttempts);
                        return;
                    }

                    try { await Task.Delay(TimeSpan.FromSeconds(2 * attempt), ct); }
                    catch (OperationCanceledException) { return; }
                }
            }
        }
    }
}