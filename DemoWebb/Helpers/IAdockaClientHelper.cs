using System.Net.Http;
using System.Threading.Tasks;

namespace DemoWebb.Helpers
{
    public interface IAdockaClientHelper
    {
        Task<HttpClient> GetClient();
    }
}