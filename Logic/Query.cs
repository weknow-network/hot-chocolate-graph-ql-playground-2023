using System;

using GreenDonut;

using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace Weknow.HotChocolatePlayground;

public class Query
{
    public IEnumerable<Book> GetBooks() =>
        Enumerable.Range(0, 100).Select(i =>
        new Book
        {
            Id = (i * 1000).ToString(),
            Title = $"Book {i}",

            Author = new Author
            {
                Id = i.ToString(),
                Name = i % 3 == 0 ? "Jon Skeet" : "Bnaya Eshet",
                Rank = i % 7 + 3
            }
        });
    public Book? GetBook(int index) => GetBooks().Skip(index).Take(1).FirstOrDefault();


    public async Task<Person> GetPerson(
        int id,
        [Service] IPersonRepository repository)
    {
        return await repository.GetPersonById(id);
    }

    public async Task<Person[]> GetPersonByIds(
        int[] ids,
        [Service] IPersonBatchDataLoader repository)
        //[Service] PersonBatchDataLoader repository)
    {
        var results = await repository.LoadAsync(ids);
        return results.ToArray();
    }

    public async Task<Person[]> GetPeopleByIds(
        int[] ids,
        IResolverContext context,
        [Service] IPersonRepository repository)
    {
        IDataLoader<int, Person> dataLoader = context.BatchDataLoader<int, Person>(
            async (keys, ct) =>
            {
                Person[] result = await repository.GetPersonByIds(keys, ct);
                return result.ToDictionary(m => m.Id);

            });
        IReadOnlyList<Person> results = await dataLoader.LoadAsync(ids.ToList());
        return results.ToArray();
    }
}