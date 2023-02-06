namespace Weknow.HotChocolatePlayground
{
    public class PersonRepository: IPersonRepository
    {
        async Task<Person> IPersonRepository.GetPersonById(int id)
        {
            await Task.Delay(id * 200 % 2000);
            return new Person { Id = id, Name = $"Person {id}" };
        }
    }
}