namespace Weknow.HotChocolatePlayground;

internal class BookRepository : IBookRepository
{
    async Task<Book[]> IBookRepository.GetBooksAsync(int limit)
    {
        await Task.Delay(limit);
        var range = Enumerable.Range(0, 100);
        var books = range.Select(i =>
        new Book
        {
            Id = (i * 1000).ToString(),
            Title = $"Book {i}",

            Author = new Author
            {
                Id = i.ToString(),
                Name = i % 3 == 0 ? "Jon Skeet" : "Bnaya Eshet",
                Rank = i % 10
            }
        });
        return books.ToArray();
    }
}
