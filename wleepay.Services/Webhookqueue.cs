using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class WebhookQueue : IWebhookQueue
    {
        private readonly Channel<WebhookJob> _channel =
            Channel.CreateUnbounded<WebhookJob>(new UnboundedChannelOptions
            {
                SingleReader = true,   
                SingleWriter = false
            });

        public void Enqueue(WebhookJob job)
        {
           
            if (!_channel.Writer.TryWrite(job))
                throw new InvalidOperationException("Webhook queue is closed.");
        }

        public ValueTask<WebhookJob> DequeueAsync(CancellationToken cancellationToken)
            => _channel.Reader.ReadAsync(cancellationToken);
    }
}