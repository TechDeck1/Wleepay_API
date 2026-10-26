using System.Threading;
using System.Threading.Tasks;

namespace wleepay.Core.Services
{

    public class WebhookJob
    {
        public WebhookJob(string json, string eventId)
        {
            Json = json;
            EventId = eventId;

        }

        public string Json { get; }
        public string EventId { get; }

    }


    public interface IWebhookQueue
    {
        void Enqueue(WebhookJob job);
        ValueTask<WebhookJob> DequeueAsync(CancellationToken cancellationToken);
    }
}
