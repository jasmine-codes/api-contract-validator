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

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        public void GetUser_ShouldMatchContract(int userId)
        {
            var api = new ApiService("https://jsonplaceholder.typicode.com");
            var endpoint = $"/users/{userId}";

            var rawJson = api.GetRaw(endpoint);
            var prettyJson = JToken.Parse(rawJson).ToString(Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine($"Raw JSON for User {userId}:\n{prettyJson}");

            var user = api.Get<UserResponseContract>(endpoint);

            user.Should().NotBeNull();
            user.id.Should().Be(userId);
            user.name.Should().NotBeNullOrEmpty();
            user.email.Should().Contain("@");
        }
    }
}