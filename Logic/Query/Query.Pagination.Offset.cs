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
}