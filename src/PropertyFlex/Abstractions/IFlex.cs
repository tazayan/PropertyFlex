namespace FlexLib;

public interface IFlex
{
    ValueContainer GetValue(FlexPropertyId propertyId);
    ValueContainer SetValue(FlexPropertyId propertyId, ValueContainer value);
}
