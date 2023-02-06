using Weknow.TypesUtility;

namespace Weknow.HotChocolatePlayground;

[Nullable]
public record Person
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}