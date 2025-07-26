using Xunit;
using FluentAssertions;
using ApiContractValidator.Models;
using ApiContractValidator.Services;

namespace ApiContractValidator.Tests
{
    public class UserEndpointTests
    {
        [Fact]
        public void GetUser_ShouldMatchContract()
        {
            var api = new ApiService("https://jsonplaceholder.typicode.com");
            var user = api.Get<UserResponseContract>("/users/1");
        }
    }
}