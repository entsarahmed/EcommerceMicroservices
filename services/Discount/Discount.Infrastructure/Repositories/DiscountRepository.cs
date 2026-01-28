using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            //Using Npgsql to connect to PostgreSQL
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
         
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                // Writting SQL Statement in Dapper
                ("SELECT * FROM Coupon WHERE ProductName = @productName",
                new
                {
                    ProductName = productName
                });
            if (coupon == null)
            {
                return new Coupon { Amount = 0, Description = "No Discount Available for this Product", ProductName = "No Discount"};
            }
            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName, @Description,@Amountt)",
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
                new
                {
                    ProductName=coupon.ProductName,
                    Description=coupon.Description,
                    Amount=coupon.Amount,
                    Id=coupon.Id
                }
                );
            if(affected == 0)
            {
                return false;
            }
            return true;
            
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductNam=@ProductName",
                new
                {
                    ProductName =productName
                }
                );
            if(affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
