#pragma warning disable CA1051

namespace FlexLib;

public readonly struct ValueContainer
{
    private readonly ValueContainerType type;

    private readonly object? ReferenceValue;
    private readonly Union PrimitiveValue;

    public static ValueContainer FromReference(object? referenceValue)
    {
        return new ValueContainer(referenceValue, default, ValueContainerType.ReferenceValue);
    }

    public static ValueContainer FromDecimal(decimal decimalValue)
    {
        return new ValueContainer(null, new Union(decimalValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromGuid(Guid guidValue)
    {
        return new ValueContainer(null, new Union(guidValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromDateTimeOffset(DateTimeOffset dateTimeOffsetValue)
    {
        return new ValueContainer(null, new Union(dateTimeOffsetValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromDouble(double doubleValue)
    {
        return new ValueContainer(null, new Union(doubleValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromLong(long longValue)
    {
        return new ValueContainer(null, new Union(longValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromULong(ulong ulongValue)
    {
        return new ValueContainer(null, new Union(ulongValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromDateTime(DateTime dateTimeValue)
    {
        return new ValueContainer(null, new Union(dateTimeValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromDateOnly(DateOnly dateOnlyValue)
    {
        return new ValueContainer(null, new Union(dateOnlyValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromTimeOnly(TimeOnly timeOnlyValue)
    {
        return new ValueContainer(null, new Union(timeOnlyValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromTimeSpan(TimeSpan timeSpanValue)
    {
        return new ValueContainer(null, new Union(timeSpanValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromFloat(float floatValue)
    {
        return new ValueContainer(null, new Union(floatValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromInt(int intValue)
    {
        return new ValueContainer(null, new Union(intValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromUInt(uint uintValue)
    {
        return new ValueContainer(null, new Union(uintValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromShort(short shortValue)
    {
        return new ValueContainer(null, new Union(shortValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromUShort(ushort ushortValue)
    {
        return new ValueContainer(null, new Union(ushortValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromByte(byte byteValue)
    {
        return new ValueContainer(null, new Union(byteValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromSByte(sbyte sbyteValue)
    {
        return new ValueContainer(null, new Union(sbyteValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromChar(char charValue)
    {
        return new ValueContainer(null, new Union(charValue), ValueContainerType.PrimitiveValue);
    }

    public static ValueContainer FromBool(bool boolValue)
    {
        return new ValueContainer(null, new Union(boolValue), ValueContainerType.PrimitiveValue);
    }

    private ValueContainer(object? referenceValue, Union value, ValueContainerType type)
    {
        this.ReferenceValue = referenceValue;
        this.PrimitiveValue = value;
        this.type = type;
    }

    public bool IsValueType => this.type == ValueContainerType.PrimitiveValue;

    public bool Equals(ValueContainer other)
    {
        return this.type == other.type &&
            this.type switch
            {
                ValueContainerType.None => true,
                ValueContainerType.PrimitiveValue => this.PrimitiveValue == other.PrimitiveValue,
                ValueContainerType.ReferenceValue => Equals(this.ReferenceValue, other.ReferenceValue),
                _ => false,
            };
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueContainer other && Equals(other);
    }

    public override int GetHashCode()
    {
        return this.type switch
        {
            ValueContainerType.None => 0,
            ValueContainerType.PrimitiveValue => HashCode.Combine(this.type, this.PrimitiveValue),
            ValueContainerType.ReferenceValue => HashCode.Combine(this.type, this.ReferenceValue?.GetHashCode() ?? 0),
            _ => 0,
        };
    }

    public override string ToString()
    {
        return this.type switch
        {
            ValueContainerType.None => "None",
            ValueContainerType.PrimitiveValue => this.PrimitiveValue.ToString() ?? "null",
            ValueContainerType.ReferenceValue => this.ReferenceValue?.ToString() ?? "null",
            _ => "Unknown",
        };
    }

    public static bool operator ==(ValueContainer left, ValueContainer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueContainer left, ValueContainer right)
    {
        return !(left == right);
    }
}

public enum ValueContainerType : int
{
    None,
    PrimitiveValue,
    ReferenceValue
}

#pragma warning restore CA1051
