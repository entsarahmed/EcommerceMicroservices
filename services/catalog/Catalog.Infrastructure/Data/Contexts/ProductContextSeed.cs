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
        var hasProduct = await productCollection.Find(_ => true).AnyAsync();
        if (hasProduct)
            return;
        var filePath = Path.Combine("Data", "DataSeed", "products.json");
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"this filePath Not exist {filePath}");
        var productJson = await File.ReadAllTextAsync(filePath);
        var products = JsonSerializer.Deserialize<List<Product>>(productJson);
        if (products?.Any() is true)
            await productCollection.InsertManyAsync(products);

    }
}
