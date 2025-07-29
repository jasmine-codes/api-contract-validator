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
        [Fact]
        public void GetUser_ShouldMatchContract()
        {
            var api = new ApiService("https://jsonplaceholder.typicode.com");

            //grab raw response JSON
            var rawJson = api.GetRaw("users/1");

            //pretty-print
            var prettyJson = JToken.Parse(rawJson).ToString(Newtonsoft.Json.Formatting.Indented);

            Console.WriteLine("Raw JSON Response:");
            Console.WriteLine(prettyJson);

            var user = api.Get<UserResponseContract>("/users/1");

            user.Should().NotBeNull();
            user.id.Should().BeGreaterThan(0);
            user.name.Should().NotBeNullOrEmpty();
            user.email.Should().Contain("@");
        }
    }
}