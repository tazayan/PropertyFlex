using System.Collections;
using System.Reflection;

namespace FlexLib;

public class FlexPropertyRegistry : IEnumerable<IFlexProperty>
{
    private readonly FrozenDictionary<FlexPropertyId, (IFlexProperty, uint index)> propertiesMap;

    public static FlexPropertyRegistry From(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        IEnumerable<IFlexProperty> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(CreatePropertyInfo);

        return properties.Any() ? new FlexPropertyRegistry(properties)
            : throw new ArgumentException($"Type '{type.FullName}' does not contain any public instance properties.", nameof(type));
    }

    private static IFlexProperty CreatePropertyInfo(PropertyInfo propertyInfo)
    {
        return new FlexProperty();
    }

    public FlexPropertyRegistry(IEnumerable<IFlexProperty> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        if (!properties.TryGetNonEnumeratedCount(out var count))
        {
            count = properties.Count();
        }

        var seenIds = new HashSet<FlexPropertyId>(count);
        var seenNameTypes = new HashSet<(string Name, Type PropertyType)>(count);
        var tempPropertiesMap = new Dictionary<FlexPropertyId, (IFlexProperty, uint index)>(count);

        uint index = 0;

        foreach (IFlexProperty property in properties)
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

    public IEnumerator<IFlexProperty> GetEnumerator()
    {
        return this.propertiesMap.Values.Select(x => x.Item1).GetEnumerator();
    }

    public bool TryGetConfiguration(FlexPropertyId id, out (IFlexProperty Property, uint Index) configuration)
    {
        if (this.propertiesMap.TryGetValue(id, out configuration))
        {
            return true;
        }

        configuration = default;
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
