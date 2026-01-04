namespace PropertyFlex.Tests;

/// <summary>
/// Unit tests for <see cref="ValueContainer"/> struct.
/// </summary>
public class ValueContainerTests
{
    [Fact]
    public void ValueContainer_Default_ShouldNotBeValueType()
    {
        // Arrange & Act
        var container = default(ValueContainer);

        // Assert
        container.IsValueType.Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_WithPrimitiveValue_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromLong(42L);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_WithReferenceValue_ShouldNotBeValueType()
    {
        // Arrange
        var referenceValue = "test string";

        // Act
        var container = ValueContainer.FromReference(referenceValue);

        // Assert
        container.IsValueType.Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_Equals_SameDefaultValue_ShouldBeEqual()
    {
        // Arrange
        var container1 = default(ValueContainer);
        var container2 = default(ValueContainer);

        // Assert
        container1.Equals(container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_Equals_SamePrimitiveValue_ShouldBeEqual()
    {
        // Arrange
        var container1 = ValueContainer.FromLong(42L);
        var container2 = ValueContainer.FromLong(42L);

        // Assert
        container1.Equals(container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_Equals_DifferentPrimitiveValue_ShouldNotBeEqual()
    {
        // Arrange
        var container1 = ValueContainer.FromLong(42L);
        var container2 = ValueContainer.FromLong(43L);

        // Assert
        container1.Equals(container2).Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_Equals_SameReferenceValue_ShouldBeEqual()
    {
        // Arrange
        var sharedReference = "shared";
        var container1 = ValueContainer.FromReference(sharedReference);
        var container2 = ValueContainer.FromReference(sharedReference);

        // Assert
        container1.Equals(container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_Equals_DifferentReferenceValue_ShouldNotBeEqual()
    {
        // Arrange
        var container1 = ValueContainer.FromReference("value1");
        var container2 = ValueContainer.FromReference("value2");

        // Assert
        container1.Equals(container2).Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_Equals_DifferentTypes_ShouldNotBeEqual()
    {
        // Arrange
        var container1 = ValueContainer.FromLong(42L);
        var container2 = ValueContainer.FromReference("test");

        // Assert
        container1.Equals(container2).Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_GetHashCode_SamePrimitiveValue_ShouldBeSame()
    {
        // Arrange
        var container1 = ValueContainer.FromLong(42L);
        var container2 = ValueContainer.FromLong(42L);

        // Assert
        container1.GetHashCode().Should().Be(container2.GetHashCode());
    }

    [Fact]
    public void ValueContainer_GetHashCode_DefaultValue_ShouldBeZero()
    {
        // Arrange
        var container = default(ValueContainer);

        // Assert
        container.GetHashCode().Should().Be(0);
    }

    [Fact]
    public void ValueContainer_GetHashCode_ReferenceValue_WithNull_ShouldNotThrow()
    {
        // Arrange
        var container = ValueContainer.FromReference(null);

        // Act
        var hashCode = container.GetHashCode();

        // Assert - should not throw and return a consistent value
        hashCode.Should().Be(ValueContainer.FromReference(null).GetHashCode());
    }

    [Fact]
    public void ValueContainer_FromDouble_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromDouble(3.14);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_FromInt_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromInt(123);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_FromBool_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromBool(true);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_FromGuid_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromGuid(Guid.NewGuid());

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_FromDateTime_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromDateTime(DateTime.Now);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_FromDecimal_ShouldBeValueType()
    {
        // Arrange & Act
        var container = ValueContainer.FromDecimal(123.45m);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_EqualityOperator_SameValue_ShouldBeTrue()
    {
        // Arrange
        var container1 = ValueContainer.FromInt(42);
        var container2 = ValueContainer.FromInt(42);

        // Assert
        (container1 == container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_InequalityOperator_DifferentValue_ShouldBeTrue()
    {
        // Arrange
        var container1 = ValueContainer.FromInt(42);
        var container2 = ValueContainer.FromInt(43);

        // Assert
        (container1 != container2).Should().BeTrue();
    }
}
