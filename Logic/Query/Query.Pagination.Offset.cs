using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading;

using GreenDonut;

using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using HotChocolate.Types.Pagination.Extensions;

using Microsoft.Extensions.Logging;

namespace Weknow.HotChocolatePlayground;

partial class Query
{
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
