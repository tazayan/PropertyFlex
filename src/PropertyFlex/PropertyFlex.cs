namespace PropertyFlexLib;

public abstract class PropertyFlex : IPropertyFlex
{
    private readonly PropertyRegistry registry;

    public PropertyFlex(PropertyRegistry registry)
    {
        this.registry = registry;
    }

    public T GetValue<T>(PropertyId propertyId)
    {
        return this.registry.TryGetConfiguration(propertyId, out (IPropertyInfo Property, uint Index) config)
            ? GetValue<T>(config.Index)
            : throw new InvalidOperationException("Property is not definde on the type");
    }


    public T SetValue<T>(PropertyId propertyId, T value)
    {
        if (this.registry.TryGetConfiguration(propertyId, out (IPropertyInfo Property, uint Index) config))
        {
            if (!config.Property.PropertyType.IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException("Invalid property type");
            }

            return SetValue(config.Index, value);
        }

        throw new InvalidOperationException("Property is not definde on the type");
    }

    protected abstract T GetValue<T>(uint index);
    protected abstract T SetValue<T>(uint index, T value);


    object IPropertyFlex.GetValue(PropertyId propertyId) => this.GetValue<object>(propertyId);

    object IPropertyFlex.SetValue(PropertyId propertyId, object value) => this.SetValue<object>(propertyId, value);
}
