namespace Weknow.HotChocolatePlayground
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonById(int id);
    }
}