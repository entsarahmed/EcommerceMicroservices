using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts;
public static class BrandContextSeed
{
    public static async Task SeedDataAsync(IMongoCollection<ProductBrand> brandCollection)
    {
        var hasBrands = await brandCollection.Find(_ => true).AnyAsync();
       //Check on find Brands
        if (hasBrands)
            return;
        //Get File Path as string
        var basePath = AppContext.BaseDirectory;
        var filePath = Path.Combine(basePath, "Data", "SeedData", "brands.json");

        //Check on file Path Do you Find?
        if (!File.Exists(filePath))
        {
            Console.WriteLine($" Seed file Not Exists:{ filePath}");
            return;
        }
        //Read Data from file
        var brandData = await File.ReadAllTextAsync(filePath);
        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
        //Check on Data
        if(brands?.Any() is true)
        {
            await brandCollection.InsertManyAsync(brands);
        }
    }
}
