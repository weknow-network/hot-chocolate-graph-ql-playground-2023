using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

using Microsoft.Extensions.Logging;

using static Weknow.HotChocolatePlayground.Query;

namespace Weknow.HotChocolatePlayground;

[ExtendObjectType(OperationType.Query)]
public class QueryWithSorting
{
    private readonly ILogger _logger;

    public QueryWithSorting(ILogger<QueryWithSorting> logger)
    {
        _logger = logger;
    }

    //[UseFiltering]
    //[UseFiltering(typeof(BookFilterType))]
    [UseSorting]
    public async Task<IQueryable<Book>> GetBooksWithSorting(
        [Service]IBookRepository repository)
    {
        int count = 100;

        IEnumerable<Book> data = await repository.GetBooksAsync(count);

        return data.AsQueryable();
    }

}
