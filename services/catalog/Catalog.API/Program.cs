//Program.cs ===> File API Configuration( Handling MiddleWares & PipeLines)
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog API",
        Version= "v1",
        Description="This is API for Catalog Microservices in Ecommerce Application",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name =  "Entsar Ahmed",
            Email="eentsaraahmed@gmail.com",
            Url =new Uri("https://www.facebook.com/entesar.ahmed.902")
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
