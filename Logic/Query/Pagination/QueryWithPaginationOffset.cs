using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

using Microsoft.Extensions.Logging;

using static Weknow.HotChocolatePlayground.Query;

namespace Weknow.HotChocolatePlayground;

[ExtendObjectType(OperationType.Query)]
public class QueryWithPaginationOffset
{
    private readonly ILogger _logger;

    public QueryWithPaginationOffset(ILogger<QueryWithPaginationOffset> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// https://chillicream.com/docs/hotchocolate/v12/fetching-data/filtering
    /// Note: If you use more than one middleware, keep in mind that ORDER MATTERS. The correct order is UsePaging > UseProjections > UseFiltering > UseSorting
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <returns></returns>
    [UseOffsetPaging(
        IncludeTotalCount = true,
        MaxPageSize = 20 /* hard limit (will throw when asked for larger size)*/)]
    public async Task<Book[]> GetBooksWithOffset(
        [Service] IBookRepository repository)
    {
        var result = await repository.GetBooksAsync();
        return result;
    }

    [UseOffsetPaging(
        IncludeTotalCount = true,
        MaxPageSize = 20 /* hard limit (will throw when asked for larger size)*/)]
    public async Task<CollectionSegment<Book>> GetBooksWithCustomOffset(
        int? skip, int? take, SortBook sortBy,
        [Service] IBookRepository repository)
    {
        int limit = 1000;
        Book[] source = await repository.GetBooksAsync(limit);
        IEnumerable<Book> data = source.OrderBy(m => sortBy switch
        {
            SortBook.Rank => m.Author.Rank.ToString().PadLeft(4, '0'),
            SortBook.Author => m.Author.Name,
            _ => m.Title
        }).ToArray();
        if (skip != null)
            data = data.Skip(skip ?? 0);
        if (take != null)
            data = data.Take(take ?? 0);

        var pageInfo = new CollectionSegmentInfo((skip ?? 0 + take ?? 20) < limit, (skip ?? 0) > 0);

        CollectionSegment<Book> collectionSegment = new CollectionSegment<Book>(
            data.ToList(),
            pageInfo,
            ct => ValueTask.FromResult(limit));
        return collectionSegment;
    }
}
