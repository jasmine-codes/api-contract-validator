using Xunit;
using FluentAssertions;
using ApiContractValidator.Models;
using ApiContractValidator.Services;
using Newtonsoft.Json.Linq; //for pretty-printing JSON
using FluentAssertions.Equivalency.Steps; 

namespace ApiContractValidator.Tests
{
    public class UserEndpointTests
    {
        private string LoadExpectedJson(string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Contracts", fileName);
            return File.ReadAllText(path);
        }

        [Theory]
        [InlineData(1, "user_1.json")]
        [InlineData(2, "user_2.json")]
        [InlineData(3, "user_3.json")]

        public void GetUser_ShouldMatchContract(int userId, string expectedFile)
        {
            var api = new ApiService("https://jsonplaceholder.typicode.com");
            var endpoint = $"/users/{userId}";

            var actualJson = api.GetRaw(endpoint); //live API response
            var expectedJson = LoadExpectedJson(expectedFile); //stored expectations

            var actual = JToken.Parse(actualJson);
            var expected = JToken.Parse(expectedJson);

            Console.WriteLine($"Comparing User {userId}");

            actual.Should().BeEquivalentTo(expected);
        }
    }
}