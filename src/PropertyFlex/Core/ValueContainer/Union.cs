using System.Runtime.InteropServices;

namespace FlexLib;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct Union : IEquatable<Union>, IComparable<Union>
{
    //128 bits
    [FieldOffset(0)]
    public readonly decimal Decimal;

    //128 bits
    [FieldOffset(0)]
    public readonly Guid Guid;

    //128 bits
    [FieldOffset(0)]
    public readonly DateTimeOffset DateTimeOffset;

    [FieldOffset(0)]
    public readonly double Double;

    [FieldOffset(0)]
    public readonly long Long;

    [FieldOffset(0)]
    public readonly ulong ULong;

    [FieldOffset(0)]
    public readonly DateTime DateTime;

    [FieldOffset(0)]
    public readonly DateOnly DateOnly;

    [FieldOffset(0)]
    public readonly TimeOnly TimeOnly;

    [FieldOffset(0)]
    public readonly TimeSpan TimeSpan;

    [FieldOffset(0)]
    public readonly float Float;

    [FieldOffset(0)]
    public readonly int Int;

    [FieldOffset(0)]
    public readonly uint UInt;

    [FieldOffset(0)]
    public readonly short Short;

    [FieldOffset(0)]
    public readonly ushort UShort;

    [FieldOffset(0)]
    public readonly byte Byte;

    [FieldOffset(0)]
    public readonly sbyte SByte;

    [FieldOffset(0)]
    public readonly char Char;

    [FieldOffset(0)]
    public readonly bool Bool;

    [FieldOffset(16)]
    private readonly UnionValueType Type;

    public Union(decimal value)
    {
        this.Decimal = value;
        this.Type = UnionValueType.DecimalNumber;
    }

    public Union(Guid value)
    {
        this.Guid = value;
        this.Type = UnionValueType.GuidValue;
    }

    public Union(DateTimeOffset value)
    {
        this.DateTimeOffset = value;
        this.Type = UnionValueType.DateTimeOffset;
    }

    public Union(double value)
    {
        this.Double = value;
        this.Type = UnionValueType.DoubleNumber;
    }

    public Union(long value)
    {
        this.Long = value;
        this.Type = UnionValueType.LongNumber;
    }

    public Union(ulong value)
    {
        this.ULong = value;
        this.Type = UnionValueType.ULongNumber;
    }

    public Union(DateTime value)
    {
        this.DateTime = value;
        this.Type = UnionValueType.DateTime;
    }

    public Union(DateOnly value)
    {
        this.DateOnly = value;
        this.Type = UnionValueType.DateOnly;
    }

    public Union(TimeOnly value)
    {
        this.TimeOnly = value;
        this.Type = UnionValueType.TimeOnly;
    }

    public Union(TimeSpan value)
    {
        this.TimeSpan = value;
        this.Type = UnionValueType.TimeSpan;
    }

    public Union(float value)
    {
        this.Float = value;
        this.Type = UnionValueType.FloatNumber;
    }

    public Union(int value)
    {
        this.Int = value;
        this.Type = UnionValueType.IntNumber;
    }

    public Union(uint value)
    {
        this.UInt = value;
        this.Type = UnionValueType.UIntNumber;
    }

    public Union(short value)
    {
        this.Short = value;
        this.Type = UnionValueType.ShortNumber;
    }

    public Union(ushort value)
    {
        this.UShort = value;
        this.Type = UnionValueType.UShortNumber;
    }

    public Union(byte value)
    {
        this.Byte = value;
        this.Type = UnionValueType.ByteNumber;
    }

    public Union(sbyte value)
    {
        this.SByte = value;
        this.Type = UnionValueType.SByteNumber;
    }

    public Union(char value)
    {
        this.Char = value;
        this.Type = UnionValueType.CharValue;
    }

    public Union(bool value)
    {
        this.Bool = value;
        this.Type = UnionValueType.BooleanValue;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Type, this.Decimal);
    }

    public override bool Equals(object? obj)
    {
        return obj is Union other && Equals(other);
    }

    public override string ToString()
    {
        return this.Type switch
        {
            UnionValueType.DecimalNumber => this.Decimal.ToString(),
            UnionValueType.GuidValue => this.Guid.ToString(),
            UnionValueType.DateTimeOffset => this.DateTimeOffset.ToString(),
            UnionValueType.DoubleNumber => this.Double.ToString(),
            UnionValueType.LongNumber => this.Long.ToString(),
            UnionValueType.ULongNumber => this.ULong.ToString(),
            UnionValueType.DateTime => this.DateTime.ToString(),
            UnionValueType.DateOnly => this.DateOnly.ToString(),
            UnionValueType.TimeOnly => this.TimeOnly.ToString(),
            UnionValueType.TimeSpan => this.TimeSpan.ToString(),
            UnionValueType.FloatNumber => this.Float.ToString(),
            UnionValueType.IntNumber => this.Int.ToString(),
            UnionValueType.UIntNumber => this.UInt.ToString(),
            UnionValueType.ShortNumber => this.Short.ToString(),
            UnionValueType.UShortNumber => this.UShort.ToString(),
            UnionValueType.ByteNumber => this.Byte.ToString(),
            UnionValueType.SByteNumber => this.SByte.ToString(),
            UnionValueType.CharValue => this.Char.ToString(),
            UnionValueType.BooleanValue => this.Bool.ToString(),
            _ => throw new NotImplementedException()
        };
    }

    public bool Equals(Union other)
    {
        return this == other;
    }

    public int CompareTo(Union other)
    {
        return this.Type switch
        {
            UnionValueType.DecimalNumber => this.Decimal.CompareTo(other.Decimal),
            UnionValueType.GuidValue => this.Guid.CompareTo(other.Guid),
            UnionValueType.DateTimeOffset => this.DateTimeOffset.CompareTo(other.DateTimeOffset),
            UnionValueType.DoubleNumber => this.Double.CompareTo(other.Double),
            UnionValueType.LongNumber => this.Long.CompareTo(other.Long),
            UnionValueType.ULongNumber => this.ULong.CompareTo(other.ULong),
            UnionValueType.DateTime => this.DateTime.CompareTo(other.DateTime),
            UnionValueType.DateOnly => this.DateOnly.CompareTo(other.DateOnly),
            UnionValueType.TimeOnly => this.TimeOnly.CompareTo(other.TimeOnly),
            UnionValueType.TimeSpan => this.TimeSpan.CompareTo(other.TimeSpan),
            UnionValueType.FloatNumber => this.Float.CompareTo(other.Float),
            UnionValueType.IntNumber => this.Int.CompareTo(other.Int),
            UnionValueType.UIntNumber => this.UInt.CompareTo(other.UInt),
            UnionValueType.ShortNumber => this.Short.CompareTo(other.Short),
            UnionValueType.UShortNumber => this.UShort.CompareTo(other.UShort),
            UnionValueType.ByteNumber => this.Byte.CompareTo(other.Byte),
            UnionValueType.SByteNumber => this.SByte.CompareTo(other.SByte),
            UnionValueType.CharValue => this.Char.CompareTo(other.Char),
            UnionValueType.BooleanValue => this.Bool.CompareTo(other.Bool),
            _ => throw new NotImplementedException()
        };
    }

    public string ToString(IFormatProvider formatProvider)
    {
        return this.Type switch
        {
            UnionValueType.DecimalNumber => this.Decimal.ToString(formatProvider),
            UnionValueType.GuidValue => this.Guid.ToString(),
            UnionValueType.DateTimeOffset => this.DateTimeOffset.ToString(formatProvider),
            UnionValueType.DoubleNumber => this.Double.ToString(formatProvider),
            UnionValueType.LongNumber => this.Long.ToString(formatProvider),
            UnionValueType.ULongNumber => this.ULong.ToString(formatProvider),
            UnionValueType.DateTime => this.DateTime.ToString(formatProvider),
            UnionValueType.DateOnly => this.DateOnly.ToString(formatProvider),
            UnionValueType.TimeOnly => this.TimeOnly.ToString(formatProvider),
            UnionValueType.TimeSpan => this.TimeSpan.ToString(),
            UnionValueType.FloatNumber => this.Float.ToString(formatProvider),
            UnionValueType.IntNumber => this.Int.ToString(formatProvider),
            UnionValueType.UIntNumber => this.UInt.ToString(formatProvider),
            UnionValueType.ShortNumber => this.Short.ToString(formatProvider),
            UnionValueType.UShortNumber => this.UShort.ToString(formatProvider),
            UnionValueType.ByteNumber => this.Byte.ToString(formatProvider),
            UnionValueType.SByteNumber => this.SByte.ToString(formatProvider),
            UnionValueType.CharValue => this.Char.ToString(formatProvider),
            UnionValueType.BooleanValue => this.Bool.ToString(formatProvider),
            _ => throw new NotImplementedException()
        };
    }

    public static bool operator ==(Union left, Union right)
    {
        return left.Type == right.Type && left.Decimal == right.Decimal;
    }

    public static bool operator !=(Union left, Union right)
    {
        return !(left == right);
    }
}
