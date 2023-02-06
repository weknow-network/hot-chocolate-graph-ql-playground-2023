using Weknow.TypesUtility;

namespace Weknow.HotChocolatePlayground;

[Nullable]
public record Book
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public required Author Author { get; init; }
}
