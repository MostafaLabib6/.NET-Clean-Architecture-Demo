using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Restaurants.API.Controllers;
using Restaurants.Application.Queries.GetAllRestaurants;
using Xunit;

namespace Restaurants.API.Tests.Controllers;

[TestSubject(typeof(RestaurantsController))]
public class RestaurantsControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RestaurantsControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>(); });
            }
        );
    }

    [Fact]
    public async Task GetAll_GetAllRestaurantWithValidQuery_ShouldReturn200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();
        var query = new GetAllRestaurantsQuery
        {
            Page = 1,
            PageSize = 10,
            SortBy = "Name",
            SortOrder = 0
        };

        // Act
        var response = await client.GetAsync(
            $"/api/restaurants?page={query.Page}&pageSize={query.PageSize}&sortBy={query.SortBy}&sortOrder={query.SortOrder}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("restaurants");
        content.Should().Contain("totalCount");
    }
}

