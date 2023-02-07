using System;
using System.Buffers.Text;
using System.Collections.Generic;
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

public partial class Query
{
    private readonly ILogger<Query> _logger;

    public Query(ILogger<Query> logger)
    {
        _logger = logger;
    }

    public Book? GetBook(int index) => GetBooksData().Skip(index).Take(1).FirstOrDefault();


    public async Task<Person> GetPerson(
        int id,
        [Service] IPersonRepository repository)
    {
        return await repository.GetPersonById(id);
    }


    #region GetBooksData

    private static IEnumerable<Book> GetBooksData(int count = 100)
    {
        return Enumerable.Range(0, count).Select(i =>
                new Book
                {
                    Id = (i + 1000).ToString(),
                    Title = $"Book {i}",

                    Author = new Author
                    {
                        Id = i.ToString(),
                        Name = i % 3 == 0 ? "Jon Skeet" : "Bnaya Eshet",
                        Rank = i % 7 + 3
                    }
                });
    }

    #endregion // GetBooksData

}