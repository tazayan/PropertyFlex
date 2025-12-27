namespace PropertyFlex.Tests;

/// <summary>
/// Unit tests for <see cref="FlexPropertyRegistry"/> class.
/// </summary>
public class FlexPropertyRegistryTests
{
    [Fact]
    public void FlexPropertyRegistry_Constructor_WithNullProperties_ShouldThrowArgumentNullException()
    {
        // Arrange & Act
        var action = () => new FlexPropertyRegistry(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("properties");
    }

    [Fact]
    public void FlexPropertyRegistry_Constructor_WithEmptyProperties_ShouldCreateEmptyRegistry()
    {
        // Arrange
        var properties = Array.Empty<IFlexProperty>();

        // Act
        var registry = new FlexPropertyRegistry(properties);

        // Assert
        registry.Should().BeEmpty();
    }

    [Fact]
    public void FlexPropertyRegistry_Constructor_WithValidProperties_ShouldCreateRegistry()
    {
        // Arrange
        var property = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 1u },
            Name = "TestProperty",
            PropertyType = typeof(string)
        };

        // Act
        var registry = new FlexPropertyRegistry([property]);

        // Assert
        registry.Should().ContainSingle();
    }

    [Fact]
    public void FlexPropertyRegistry_Constructor_WithDuplicateIds_ShouldThrowArgumentException()
    {
        // Arrange
        var property1 = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 1u },
            Name = "Property1",
            PropertyType = typeof(string)
        };
        var property2 = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 1u },
            Name = "Property2",
            PropertyType = typeof(int)
        };

        // Act
        var action = () => new FlexPropertyRegistry([property1, property2]);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("*Duplicate property ID*");
    }

    [Fact]
    public void FlexPropertyRegistry_Constructor_WithDuplicateNameAndType_ShouldThrowArgumentException()
    {
        // Arrange
        var property1 = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 1u },
            Name = "TestProperty",
            PropertyType = typeof(string)
        };
        var property2 = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 2u },
            Name = "TestProperty",
            PropertyType = typeof(string)
        };

        // Act
        var action = () => new FlexPropertyRegistry([property1, property2]);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("*Duplicate property name and type*");
    }

    [Fact]
    public void FlexPropertyRegistry_Constructor_ShouldSkipNullProperties()
    {
        // Arrange
        var property = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 1u },
            Name = "TestProperty",
            PropertyType = typeof(string)
        };
        var properties = new IFlexProperty?[] { null, property, null };

        // Act
        var registry = new FlexPropertyRegistry(properties!);

        // Assert
        registry.Should().ContainSingle();
    }

    [Fact]
    public void FlexPropertyRegistry_TryGetConfiguration_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var propertyId = new FlexPropertyId { Id = 1u };
        var property = new TestFlexProperty
        {
            Id = propertyId,
            Name = "TestProperty",
            PropertyType = typeof(string)
        };
        var registry = new FlexPropertyRegistry([property]);

        // Act
        var result = registry.TryGetConfiguration(propertyId, out var configuration);

        // Assert
        result.Should().BeTrue();
        configuration.Property.Should().Be(property);
        configuration.Index.Should().Be(0u);
    }

    [Fact]
    public void FlexPropertyRegistry_TryGetConfiguration_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var existingPropertyId = new FlexPropertyId { Id = 1u };
        var nonExistingPropertyId = new FlexPropertyId { Id = 999u };
        var property = new TestFlexProperty
        {
            Id = existingPropertyId,
            Name = "TestProperty",
            PropertyType = typeof(string)
        };
        var registry = new FlexPropertyRegistry([property]);

        // Act
        var result = registry.TryGetConfiguration(nonExistingPropertyId, out var configuration);

        // Assert
        result.Should().BeFalse();
        configuration.Should().Be(default);
    }

    [Fact]
    public void FlexPropertyRegistry_GetEnumerator_ShouldEnumerateAllProperties()
    {
        // Arrange
        var property1 = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 1u },
            Name = "Property1",
            PropertyType = typeof(string)
        };
        var property2 = new TestFlexProperty
        {
            Id = new FlexPropertyId { Id = 2u },
            Name = "Property2",
            PropertyType = typeof(int)
        };
        var registry = new FlexPropertyRegistry([property1, property2]);

        // Act & Assert
        registry.Should().HaveCount(2);
        registry.Should().Contain(property1);
        registry.Should().Contain(property2);
    }

    [Fact]
    public void FlexPropertyRegistry_From_WithNullType_ShouldThrowArgumentNullException()
    {
        // Arrange & Act
        var action = () => FlexPropertyRegistry.From(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("type");
    }

    /// <summary>
    /// Test implementation of <see cref="IFlexProperty"/> for testing purposes.
    /// </summary>
    private sealed class TestFlexProperty : IFlexProperty
    {
        public FlexPropertyId Id { get; init; }
        public Type PropertyType { get; init; } = typeof(object);
        public string Name { get; init; } = string.Empty;
        public ValueComparer? ValueComparer { get; init; }
        public string? Description { get; init; }
        public string? DisplayName { get; init; }
    }
}
