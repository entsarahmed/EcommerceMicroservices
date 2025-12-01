using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts;
public class CatalogContext : ICatalogContext
{


    public IMongoCollection<Product> Products { get; }

    public IMongoCollection<ProductBrand> Brands { get; }

    public IMongoCollection<ProductType> Types { get; }


    public CatalogContext(IConfiguration configuration)
    {
        //---(Get things that is need ==> Read from DatabaseSettings that is find inside Docker as image  -- Make Seeding and return things for need it)
       //<--  No build Mongo But Created inside Docker  -->
        //throw MongoClient that inside Docker => Using Get Connection String that in AppDbContext
        //Get Database Name
        //from DatabaseName ==> Get Brands, Types and products
        // see Seeding ==> send it brands , Types and products that its read
        //brands, types and products make seeding if its find Data
        var Client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]); // ==> Get Client
        //Get DatabaseName
        var Database = Client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]); //Get DatabaseName

        //Make Configurations
        //Get NameOfCollection Binding on Product ==> give NameOfCollection in appSettings
        Products = Database.GetCollection<Product>(configuration["DatabaseSettings:ProductsCollection"]);
        Brands = Database.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandsCollection"]);
        Types = Database.GetCollection<ProductType>(configuration["DatabaseSettings:TypesCollection"]);

        //Make Seed to Check on Data
        // _=    -> run and no need return
           _=ProductContextSeed.DataSeedAsync(Products);
           _=BrandContextSeed.SeedDataAsync(Brands);
           _=TypeContextSeed.SeedDataAsync(Types);

    
    }


}
