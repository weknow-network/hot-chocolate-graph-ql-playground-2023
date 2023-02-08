using HotChocolate;
using HotChocolate.Types;

namespace Weknow.HotChocolatePlayground;

/// <summary>
/// extends the Author type with a resolver
/// </summary>
[ExtendObjectType(typeof(Author))]
public sealed class AuthorExtension
{
    // Avatar generators: https://www.stefanjudis.com/blog/apis-to-generate-random-user-avatars/
    private const string BASE_URL_1 = "https://api.multiavatar.com/";
    private const string BASE_URL_2 = "https://robohash.org/";
    //private const string BASE_URL = "https://api.dicebear.com/5.x/fun-emoji/svg?seed=";

    public Uri AvatarUrl([Parent] Author autor)
    {
        string baseUrl = autor.Id.GetHashCode() % 2 == 0 ? BASE_URL_1 : BASE_URL_2;
        return new Uri($"{baseUrl}{autor.Name}");
    }
}
