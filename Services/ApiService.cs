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

        public T Get<T>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = _client.Execute(request);

            if (!response.IsSuccessful) throw new Exception($"API call failed: {response.StatusCode}");

            if (string.IsNullOrWhiteSpace(response.Content)) throw new Exception("Empty API response");

            var result = JsonConvert.DeserializeObject<T>(response.Content);
            if (result == null) throw new Exception("Deserialization failed");

            return result;
        }
    }
}