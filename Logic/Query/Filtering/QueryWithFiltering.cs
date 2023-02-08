using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

using Microsoft.Extensions.Logging;

using static Weknow.HotChocolatePlayground.Query;

namespace Weknow.HotChocolatePlayground;

[ExtendObjectType(OperationType.Query)]
public class QueryWithFiltering
{
    private readonly ILogger _logger;

    public QueryWithFiltering(ILogger<QueryWithFiltering> logger)
    {
        _logger = logger;
    }

    [UseFiltering]
    //[UseFiltering(typeof(BookFilterType))]
    [UseSorting]
    public async Task<IQueryable<Book>> GetBooksWithFiltering(
        [Service] IBookRepository repository)
    {
        int count = 100;

        IEnumerable<Book> data = await repository.GetBooksAsync(count);

        return data.AsQueryable();
    }


    [UsePaging(MaxPageSize = 20, DefaultPageSize = 5, IncludeTotalCount = true)]
    //[UseProjection]
    [UseFiltering]
    //[UseFiltering(typeof(BookFilterType))]
    [UseSorting]
    public async Task<Connection<Book>> GetBooksWithFilteringAndPagination(
        [Service] IBookRepository repository,
        IResolverContext context)
    {
        int count = 1000;
        int f = context.ArgumentValue<int>("first");
        int? first = context.ArgumentOptional<int>("first");
        int? last = context.ArgumentOptional<int>("last");
        string? before = context.ArgumentOptional<string>("before");
        string? after = context.ArgumentOptional<string>("after");

        _logger.LogInformation("Pagination arguments {first} {last} {before} {after}", first, last, before, after);

        IEnumerable<Book> data = await repository.GetBooksAsync(count);

        Connection<Book> result = await data.ApplyCursorPaginationAsync(
                context,
                defaultPageSize: 2,
                totalCount: count
                //, cancellationToken: cancellationToken
                );

        return result;
    }

}
