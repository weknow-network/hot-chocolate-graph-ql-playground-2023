using System.Text;

using HotChocolate;
using HotChocolate.Types;


namespace Weknow.HotChocolatePlayground;

/// <summary>
/// extends the Book's related type with a resolver
/// </summary>
[ExtendObjectType(typeof(IBookPart))]
public sealed class IBookPartExtension
{
    // Avatar generators: https://www.stefanjudis.com/blog/apis-to-generate-random-user-avatars/
    private const string BASE_URL_1 = "https://api.multiavatar.com/";
    private const string BASE_URL_2 = "https://robohash.org/";
    //private const string BASE_URL = "https://api.dicebear.com/5.x/fun-emoji/svg?seed=";

    public string Base64([Parent] IBookPart marker) => marker switch
    {
        Book b => Convert.ToBase64String(Encoding.UTF8.GetBytes(b.Title)),
        Author a => Convert.ToBase64String(Encoding.UTF8.GetBytes(a.Name)),
        _ => string.Empty
    };
}
