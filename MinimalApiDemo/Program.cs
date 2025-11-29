var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.MapGet("/products", () =>
{
    return new List<string> { "Mouse", "Keyboard", "Monitor" };
});

app.MapGet("/products/{id}", (int id) =>
{
    return $"Your Product ID is: {id}";
});
app.MapPost("/products", (Product product) =>
{
    return Results.Ok(new { Message = "Product Added", product });
});

public record Product(int Id, string Name, double Price);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
