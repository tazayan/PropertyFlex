namespace PropertyFlex.Tests;

/// <summary>
/// Unit tests for <see cref="ValueContainer"/> ref struct.
/// </summary>
public class ValueContainerTests
{
    [Fact]
    public void ValueContainer_WithNoneType_ShouldNotBeValueType()
    {
        // Arrange & Act
        var container = new ValueContainer(null, (0L, 0L), ValueContainerType.None);

        // Assert
        container.IsValueType.Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_WithValueType_ShouldBeValueType()
    {
        // Arrange & Act
        var container = new ValueContainer(null, (42L, 0L), ValueContainerType.ValueType);

        // Assert
        container.IsValueType.Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_WithReferenceType_ShouldNotBeValueType()
    {
        // Arrange
        var referenceValue = "test string";

        // Act
        var container = new ValueContainer(referenceValue, (0L, 0L), ValueContainerType.ReferenceType);

        // Assert
        container.IsValueType.Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_Equals_SameNoneType_ShouldBeEqual()
    {
        // Arrange
        var container1 = new ValueContainer(null, (0L, 0L), ValueContainerType.None);
        var container2 = new ValueContainer(null, (0L, 0L), ValueContainerType.None);

        // Assert
        container1.Equals(container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_Equals_SameValueType_ShouldBeEqual()
    {
        // Arrange
        var container1 = new ValueContainer(null, (42L, 100L), ValueContainerType.ValueType);
        var container2 = new ValueContainer(null, (42L, 100L), ValueContainerType.ValueType);

        // Assert
        container1.Equals(container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_Equals_DifferentValueType_ShouldNotBeEqual()
    {
        // Arrange
        var container1 = new ValueContainer(null, (42L, 100L), ValueContainerType.ValueType);
        var container2 = new ValueContainer(null, (43L, 100L), ValueContainerType.ValueType);

        // Assert
        container1.Equals(container2).Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_Equals_SameReferenceType_ShouldBeEqual()
    {
        // Arrange
        var sharedReference = "shared";
        var container1 = new ValueContainer(sharedReference, (0L, 0L), ValueContainerType.ReferenceType);
        var container2 = new ValueContainer(sharedReference, (0L, 0L), ValueContainerType.ReferenceType);

        // Assert
        container1.Equals(container2).Should().BeTrue();
    }

    [Fact]
    public void ValueContainer_Equals_DifferentReferenceType_ShouldNotBeEqual()
    {
        // Arrange
        var container1 = new ValueContainer("value1", (0L, 0L), ValueContainerType.ReferenceType);
        var container2 = new ValueContainer("value2", (0L, 0L), ValueContainerType.ReferenceType);

        // Assert
        container1.Equals(container2).Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_Equals_DifferentTypes_ShouldNotBeEqual()
    {
        // Arrange
        var container1 = new ValueContainer(null, (42L, 0L), ValueContainerType.ValueType);
        var container2 = new ValueContainer("test", (42L, 0L), ValueContainerType.ReferenceType);

        // Assert
        container1.Equals(container2).Should().BeFalse();
    }

    [Fact]
    public void ValueContainer_GetHashCode_SameValueType_ShouldBeSame()
    {
        // Arrange
        var container1 = new ValueContainer(null, (42L, 100L), ValueContainerType.ValueType);
        var container2 = new ValueContainer(null, (42L, 100L), ValueContainerType.ValueType);

        // Assert
        container1.GetHashCode().Should().Be(container2.GetHashCode());
    }

    [Fact]
    public void ValueContainer_GetHashCode_NoneType_ShouldBeZero()
    {
        // Arrange
        var container = new ValueContainer(null, (0L, 0L), ValueContainerType.None);

        // Assert
        container.GetHashCode().Should().Be(0);
    }

    [Fact]
    public void ValueContainer_GetHashCode_ReferenceType_WithNull_ShouldBeZero()
    {
        // Arrange
        var container = new ValueContainer(null, (0L, 0L), ValueContainerType.ReferenceType);

        // Assert
        container.GetHashCode().Should().Be(0);
    }
}
