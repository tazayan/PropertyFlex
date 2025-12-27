namespace FlexLib;

public interface IFlexProperty
{
    FlexPropertyId Id { get; }

    Type PropertyType { get; }

    string Name { get; }

    ValueComparer? ValueComparer { get; }

    string? Description { get; }

    string? DisplayName { get; }
}
