namespace PropertyFlex.Tests;

/// <summary>
/// Unit tests for <see cref="FlexPropertyId"/> struct.
/// </summary>
public class FlexPropertyIdTests
{
    [Fact]
    public void FlexPropertyId_ShouldInitializeWithDefaultValue()
    {
        // Arrange & Act
        var propertyId = new FlexPropertyId();

        // Assert
        propertyId.Id.Should().Be(0u);
    }

    [Fact]
    public void FlexPropertyId_ShouldInitializeWithSpecifiedValue()
    {
        // Arrange & Act
        var propertyId = new FlexPropertyId { Id = 42u };

        // Assert
        propertyId.Id.Should().Be(42u);
    }

    [Theory]
    [InlineData(0u)]
    [InlineData(1u)]
    [InlineData(100u)]
    [InlineData(uint.MaxValue)]
    public void FlexPropertyId_ShouldAcceptVariousValues(uint value)
    {
        // Arrange & Act
        var propertyId = new FlexPropertyId { Id = value };

        // Assert
        propertyId.Id.Should().Be(value);
    }

    [Fact]
    public void FlexPropertyId_WithSameId_ShouldBeEqual()
    {
        // Arrange
        var propertyId1 = new FlexPropertyId { Id = 10u };
        var propertyId2 = new FlexPropertyId { Id = 10u };

        // Assert
        propertyId1.Should().Be(propertyId2);
    }

    [Fact]
    public void FlexPropertyId_WithDifferentId_ShouldNotBeEqual()
    {
        // Arrange
        var propertyId1 = new FlexPropertyId { Id = 10u };
        var propertyId2 = new FlexPropertyId { Id = 20u };

        // Assert
        propertyId1.Should().NotBe(propertyId2);
    }
}
