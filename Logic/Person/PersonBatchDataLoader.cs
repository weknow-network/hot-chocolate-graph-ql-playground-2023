
using GreenDonut;

namespace Weknow.HotChocolatePlayground;

internal class PersonBatchDataLoader : 
                        BatchDataLoader<int, Person> ,
                        IPersonBatchDataLoader
{
    private readonly IPersonRepository _repository;

    public PersonBatchDataLoader(
        IPersonRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyDictionary<int, Person>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        // instead of fetching one person, we fetch multiple persons
        Person[] persons = await _repository.GetPersonByIds(keys, cancellationToken);
        return persons.ToDictionary(x => x.Id);
    }
}
