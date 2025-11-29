var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Minimal API with filter
app.MapGet("/products", GetProducts)
   .AddEndpointFilter<MyValidationFilter>();

app.Run();

// Function
List<string> GetProducts()
{
    return new List<string> { "Mouse", "Keyboard", "Monitor" };
}

// Validation Filter
public class MyValidationFilter : IEndpointFilter
{
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // Example validation: token required
        if (!context.HttpContext.Request.Query.ContainsKey("token"))
        {
            return Results.BadRequest("Token is missing");
        }

        return await next(context);
    }
}
