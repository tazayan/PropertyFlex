namespace PropertyFlexLib;

public class PropertyRegistry
{
    private readonly FrozenDictionary<PropertyId, (IPropertyInfo, uint index)> propertiesMap;

    public PropertyRegistry(IEnumerable<IPropertyInfo> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        if (!properties.TryGetNonEnumeratedCount(out var count))
        {
            count = properties.Count();
        }

        var seenIds = new HashSet<PropertyId>(count);
        var seenNameTypes = new HashSet<(string Name, Type PropertyType)>(count);
        var tempPropertiesMap = new Dictionary<PropertyId, (IPropertyInfo, uint index)>(count);

        uint index = 0;

        foreach (IPropertyInfo property in properties)
        {
            if (property is null)
            {
                continue;
            }

            if (!seenIds.Add(property.Id))
            {
                throw new ArgumentException($"Duplicate property ID found: {property.Id}", nameof(properties));
            }

            if (!seenNameTypes.Add((property.Name, property.PropertyType)))
            {
                throw new ArgumentException($"Duplicate property name and type combination found: ({property.Name}, {property.PropertyType.Name})", nameof(properties));
            }

            tempPropertiesMap.Add(property.Id, (property, index++));
        }

        this.propertiesMap = tempPropertiesMap.ToFrozenDictionary();
    }

    public bool TryGetConfiguration(PropertyId id, out (IPropertyInfo Property, uint Index) configuration)
    {
        if (this.propertiesMap.TryGetValue(id, out configuration))
        {
            return true;
        }

        configuration = default;
        return false;
    }
}
