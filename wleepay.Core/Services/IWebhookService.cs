using System.Threading.Tasks;

namespace wleepay.Core.Services
{
    public interface IWebhookService
    {
        Task ProcessWebhookAsync(string json );
    }
}
