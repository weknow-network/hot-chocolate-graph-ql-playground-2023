using GreenDonut;

using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;

using Microsoft.Extensions.Logging;

using static Weknow.HotChocolatePlayground.Query;

namespace Weknow.HotChocolatePlayground;

[ExtendObjectType(OperationType.Query)]
public class QueryWithDataLoader
{
    private readonly ILogger<Query> _logger;

    public QueryWithDataLoader(ILogger<Query> logger)
    {
        _logger = logger;
    }

    public async Task<Person[]> GetPersonByIds(
        int[] ids,
        //[Service] IPersonBatchDataLoader dataLoader)
        [Service] PersonBatchDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(ids);
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

    public async Task<Book[]> GetBookByRank(
        int id,
        [Service] BookByRankDataloader dataLoader)
    {
        Book[] results = await dataLoader.LoadAsync(id);
        return results;
    }

    public async Task<IReadOnlyList<Book[]>> GetBookByRanks(
        int[] ids,
        [Service] BookByRankDataloader dataLoader)
    {
        IReadOnlyList<Book[]> results = await dataLoader.LoadAsync(ids);
        return results;
    }

    public async Task<IReadOnlyList<Book[]>> GetBookByRanksContext(
        int[] ids,
        IResolverContext context,
        [Service] BookByRankDataloader dataLoader)
    {
        IDataLoader<int, Book[]> loader = context.GroupDataLoader<int, Book>(async (keys, ct) =>
        {
            IReadOnlyList<Book[]> raw = await dataLoader.LoadAsync(keys);
            // Ugly yet demo the idea
            return raw.SelectMany(m => m).ToLookup(m => m.Author.Rank);
        });
        IReadOnlyList<Book[]> results = await loader.LoadAsync(ids);
        return results;
    }
}