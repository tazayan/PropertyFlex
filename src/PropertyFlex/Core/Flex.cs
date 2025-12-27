namespace FlexLib;

public abstract class Flex : IFlex
{
    private readonly FlexPropertyRegistry registry;

    public Flex(FlexPropertyRegistry registry)
    {
        this.registry = registry;
    }

    public T GetValue<T>(FlexPropertyId propertyId) where T : allows ref struct
    {
        return this.registry.TryGetConfiguration(propertyId, out (IFlexProperty Property, uint Index) config)
            ? GetValue<T>(config.Index)
            : throw new InvalidOperationException("Property is not definde on the type");
    }


    public T SetValue<T>(FlexPropertyId propertyId, T value) where T : allows ref struct
    {
        if (this.registry.TryGetConfiguration(propertyId, out (IFlexProperty Property, uint Index) config))
        {
            if (!config.Property.PropertyType.IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException("Invalid property type");
            }

            return SetValue(config.Index, value);
        }

        throw new InvalidOperationException("Property is not definde on the type");
    }

    protected abstract T GetValue<T>(uint index) where T : allows ref struct;
    protected abstract T SetValue<T>(uint index, T value) where T : allows ref struct;


    ValueContainer IFlex.GetValue(FlexPropertyId propertyId) => this.GetValue<ValueContainer>(propertyId);

    ValueContainer IFlex.SetValue(FlexPropertyId propertyId, ValueContainer value) => this.SetValue<ValueContainer>(propertyId, value);
}
