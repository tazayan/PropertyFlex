namespace PropertyFlexLib;

public interface IPropertyFlex
{
    object GetValue(PropertyId propertyId);
    object SetValue(PropertyId propertyId, object value);
}
