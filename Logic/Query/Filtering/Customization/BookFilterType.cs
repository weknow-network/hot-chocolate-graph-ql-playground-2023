using HotChocolate.Data.Filters;

namespace Weknow.HotChocolatePlayground;

public class BookFilterType : FilterInputType<Book>
{
    protected override void Configure(
        IFilterInputTypeDescriptor<Book> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(f => f.Title).Name("caption");
        descriptor.Field(f => f.Author.Rank).Name("rank");
    }
}
