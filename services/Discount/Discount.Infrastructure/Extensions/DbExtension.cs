using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDataBase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount DB Migration started");
                    ApplyMigrations(config);
                    logger.LogInformation("Discount DB Migration Completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Cannot create DB Migration");
                    throw;
                }

            }
            return host;
        }  
    private static void ApplyMigrations(IConfiguration config)
        {
            var retry = 5;
            while(retry > 0)
            {
                try {
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var cmd = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Coupon(ID SERIAL PRIMARY KEY, 
                                                                  ProductName VARCHAR(500) NOT NULL,
                                                                  Description TEXT,
                                                                   Amount INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText= "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Double Caramel Frappuccino', 'Rich Discount', 600);";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('White Chocolate Mocha Frappuccino', 'Powerfit Discount', 700);";
                    cmd.ExecuteNonQuery();
                    break;
                }
                catch (Exception)
                {
                    retry--;
                    if(retry == 0)
                    {
                        throw;
                    }
                    Thread.Sleep(2000);
                }
            }
        }
    }

}
