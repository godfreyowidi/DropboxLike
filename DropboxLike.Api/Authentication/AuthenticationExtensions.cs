namespace DropboxLike.Api.Authentication;

public static class AuthenticationExtensions
{
    public static IApplicationBuilder UseCustomClaimValidation(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }

            await next.Invoke();
        });
    }
}