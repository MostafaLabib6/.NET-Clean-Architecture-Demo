using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Tests.Users;

// [TestSubject(typeof(CurrentUser))]
public class CurrentUserTest
{
    [Theory]
	[InlineData(nameof(RolesEnum.ADMIN))]
    [InlineData(nameof(RolesEnum.USER))]
    //METHODNAME_SCENARIO_RESULT
    public void IsInRole_WithMatchingRole_ShouldBeTrue(string roleName)
    {
        // Arrange
        var user = new CurrentUser("1", "test@test.com", new[] { nameof(RolesEnum.ADMIN), nameof(RolesEnum.USER) });

        // Act
        var isInRole = user.IsInRole(roleName);

        // Assert
        isInRole.Should().BeTrue();
    }


    [Fact]
    public void IsInRole_WithNoMatchingRole_ShouldBeTrue()
    {
        // Arrange
        var user = new CurrentUser("1", "test@test.com", new[] { nameof(RolesEnum.ADMIN), nameof(RolesEnum.USER) });

        // Act
        var isInRole = user.IsInRole(nameof(RolesEnum.OWNER));

        // Assert
        isInRole.Should().BeFalse();
    }
    
    [Fact]
    public void IsInRole_WithNoMatchingRoleCase_ShouldBeTrue()
    {
        // Arrange
        var user = new CurrentUser("1", "test@test.com", new[] { nameof(RolesEnum.ADMIN), nameof(RolesEnum.USER) });

        // Act
        var isInRole = user.IsInRole(nameof(RolesEnum.ADMIN).ToLower());

        // Assert
        isInRole.Should().BeFalse();
    }
}