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
   public static async Task DataSeedAsync(IMongoCollection<Product> productCollection)
    {
        // 1️⃣ Prevent duplicate seeding
        if (await productCollection.CountDocumentsAsync(_ => true) > 0)
            return;

        // 2️⃣ Locate seed file
        var filePath = Path.Combine("Data", "SeedData", "products.json");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Seed file not found: {filePath}");

        // 3️⃣ Read JSON
        var productJson = await File.ReadAllTextAsync(filePath);

        // 4️⃣ Deserialize (IMPORTANT PART)
        var products = JsonSerializer.Deserialize<List<Product>>(
            productJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        // 5️⃣ Insert into MongoDB
        if (products is { Count: > 0 })
            await productCollection.InsertManyAsync(products);
    }
}
