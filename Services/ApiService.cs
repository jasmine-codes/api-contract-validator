using RestSharp;
using Newtonsoft.Json;

namespace ApiContractValidator.Services
{
    public class ApiService
    {
        private readonly RestClient _client;

        public ApiService(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }
    }
}