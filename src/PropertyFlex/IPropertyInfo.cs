namespace PropertyFlexLib;

public interface IPropertyInfo
{
    PropertyId Id { get; }

    Type PropertyType { get; }

    string Name { get; }

    ValueComparer? ValueComparer { get; }

    string? Description { get; }

    string? DisplayName { get; }
}
