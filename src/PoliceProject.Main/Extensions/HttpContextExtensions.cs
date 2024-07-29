using DAL.Entities;

namespace MainService.Extensions;

public static class HttpContextExtensions
{
    public static int? GetUserLocalIdentifier(this HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        if (httpContext.Items.TryGetValue(nameof(User), out var userObj))
        {
            if (userObj is User user)
            {
                return user.Id;
            }
        }

        return null;
    }
}
