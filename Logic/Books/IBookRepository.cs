namespace Weknow.HotChocolatePlayground
{
    public interface IBookRepository
    {
        Task<Book[]> GetBooksAsync();
    }
}