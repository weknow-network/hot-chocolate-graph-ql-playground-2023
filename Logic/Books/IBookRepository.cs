namespace Weknow.HotChocolatePlayground
{
    public interface IBookRepository
    {
        Task<Book[]> GetBooksAsync(int limit = 400);
    }
}