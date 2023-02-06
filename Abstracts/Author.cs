using Weknow.TypesUtility;

namespace Weknow.HotChocolatePlayground;

[Nullable]

public record Author
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public int Rank { get; init; }
}