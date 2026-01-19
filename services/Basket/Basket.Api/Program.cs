using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using System.Reflection;
//using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddAutoMapper(typeof(BasketMappingProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(CreateShoppingCartCommand))!
    ));


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Basket API",
        Version = "v1",
        Description = "This is API for Basket Microservices in Ecommerce Application",
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Entsar Ahmed",
            Email = "eentsaraahmed@gmail.com",
            Url = new Uri("https://www.facebook.com/entesar.ahmed.902")
        }
    });
});


//Configure Redis Cache
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
