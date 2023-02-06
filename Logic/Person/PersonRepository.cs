using System.Threading;
using System.Threading.Tasks;

using Weknow.HotChocolatePlayground;

namespace Weknow.HotChocolatePlayground;

public class PersonRepository : IPersonRepository
{
    async Task<Person> IPersonRepository.GetPersonById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(id * 200 % 2000);
        return new Person { Id = id, Name = $"Person {id}" };
    }

    async Task<Person[]> IPersonRepository.GetPersonByIds(
        IEnumerable<int> ids,
        CancellationToken cancellationToken)
    {
        await Task.Delay(500);
        return ids.Select(id => new Person { Id = id, Name = $"Person {id}" }).ToArray();
    }
}
