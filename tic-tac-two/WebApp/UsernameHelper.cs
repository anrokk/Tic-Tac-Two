namespace WebApp;

public static class UsernameHelper
{
    public static string? GetUsername(HttpContext context, string? usernameFromQuery)
    {
        // Attempt to retrieve from session
        var username = context.Session.GetString("Username");

        if (!string.IsNullOrEmpty(username)) return username;
        // Fallback to query string
        if (string.IsNullOrEmpty(usernameFromQuery)) return username;
        username = usernameFromQuery;
        context.Session.SetString("Username", username);

        return username;
    }
}