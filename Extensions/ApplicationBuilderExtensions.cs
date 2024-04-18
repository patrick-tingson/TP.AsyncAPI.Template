using Microsoft.Extensions.Primitives;

public static class ApplicationBuilderExtentions
{
    public static void AddRequiredBuilder(this IApplicationBuilder app, WebApplicationBuilder builder)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}