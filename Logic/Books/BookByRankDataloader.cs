
using GreenDonut;

namespace Weknow.HotChocolatePlayground;

public class BookByRankDataloader
    : GroupedDataLoader<int, Book> //, IBookByRankBatchDataLoader
{
    private readonly IBookRepository _repository;

    public BookByRankDataloader(
        IBookRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _repository = repository;
    }

    protected override async Task<ILookup<int, Book>> LoadGroupedBatchAsync(
        IReadOnlyList<int> ranks,
        CancellationToken cancellationToken)
    {
        var books = await _repository.GetBooksAsync();
        return books.ToLookup(x => x.Author.Rank);
    }

}