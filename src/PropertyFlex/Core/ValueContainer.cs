#pragma warning disable CA1051

namespace FlexLib;

public readonly ref struct ValueContainer
{
    private readonly ValueContainerType type;

    public readonly object? ReferenceValue;
    public readonly (long, long) Value;

    public ValueContainer(object? referenceValue, (long, long) value, ValueContainerType type)
    {
        this.ReferenceValue = referenceValue;
        this.Value = value;
        this.type = type;
    }

    public bool IsValueType => this.type == ValueContainerType.ValueType;

    public bool Equals(ValueContainer other)
    {
        return this.type == other.type &&
            this.type switch
            {
                ValueContainerType.None => true,
                ValueContainerType.ValueType => this.Value == other.Value,
                ValueContainerType.ReferenceType => Equals(this.ReferenceValue, other.ReferenceValue),
                _ => false,
            };
    }

    public override int GetHashCode()
    {
        return this.type switch
        {
            ValueContainerType.None => 0,
            ValueContainerType.ValueType => this.Value.GetHashCode(),
            ValueContainerType.ReferenceType => this.ReferenceValue?.GetHashCode() ?? 0,
            _ => 0,
        };
    }
}

public enum ValueContainerType : int
{
    None,
    ValueType,
    ReferenceType
}

#pragma warning restore CA1051
