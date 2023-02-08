using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

using Microsoft.Extensions.Logging;

using static Weknow.HotChocolatePlayground.Query;

namespace Weknow.HotChocolatePlayground;

[ExtendObjectType(OperationType.Query)]
public class QueryWithPagination
{
    private readonly ILogger _logger;

    public QueryWithPagination(ILogger<QueryWithPagination> logger)
    {
        _logger = logger;
    }

    [UsePaging(
        IncludeTotalCount = true,
        MaxPageSize = 20 /* hard limit (will throw when asked for larger size)*/)]
    public async Task<Connection<Book>> GetBooksWithPagination(
        IResolverContext context)
    {
        int count = 100;
        int f = context.ArgumentValue<int>("first");
        int? first = context.ArgumentOptional<int>("first");
        int? last = context.ArgumentOptional<int>("last");
        string? before = context.ArgumentOptional<string>("before");
        string? after = context.ArgumentOptional<string>("after");

        _logger.LogInformation("Pagination arguments {first} {last} {before} {after}", first, last, before, after);

        IEnumerable<Book> data = GetBooksData(count);

        Connection<Book> result = await data.ApplyCursorPaginationAsync(
                context,
                defaultPageSize: 2,
                totalCount: count
                //, cancellationToken: cancellationToken
                );

        return result;
    }

    [UsePaging(IncludeTotalCount = true)]
    public async Task<Connection<Book>> GetBooksWithCustomPagination(string? after, int? first) //, string sortBy)
    {
        int total = 1000;
        // get users using the above arguments
        IEnumerable<Book> books = GetBooksData(total);

        var edges = books
                         .SkipWhile(b => after != null && b.Id != after)
                         .Skip(after == null ? 0 : 1)
                         .Take(first ?? 10)
                            .Select(book => new Edge<Book>(book, book.Id))
                            .ToList();
        var pageInfo = new ConnectionPageInfo(
                                    true /* not really */,
                                    after != null,
                                    edges[0].Node.Id,
                                    edges[^1].Node.Id);

        var connection = new Connection<Book>(edges, pageInfo,
                            ct => ValueTask.FromResult(total));

        return connection;
    }
}