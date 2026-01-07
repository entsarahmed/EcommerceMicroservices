using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts;
public static class ProductContextSeed
{
   public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
    {
        // 1️⃣ Prevent duplicate seeding
        var hasType = await productCollection.Find(_ => true).AnyAsync();
        if (hasType)
            return;

        // 2️⃣ Locate seed file
        var basePath = AppContext.BaseDirectory;
        var filePath = Path.Combine(basePath, "Data", "SeedData", "products.json");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Seed file not found: {filePath}");

        // 3️⃣ Read JSON
        var productJson = await File.ReadAllTextAsync(filePath);

        // 4️⃣ Deserialize (IMPORTANT PART)
        var products = JsonSerializer.Deserialize<List<Product>>(//productJson);  
            productJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        // 5️⃣ Insert into MongoDB
        if (products?.Any() is true)
            await productCollection.InsertManyAsync(products);
    }

    
}
