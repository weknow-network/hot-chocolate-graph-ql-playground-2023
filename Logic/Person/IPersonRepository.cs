namespace Weknow.HotChocolatePlayground
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonById(int id, CancellationToken cancellationToken = default);
        Task<Person[]> GetPersonByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    }
}