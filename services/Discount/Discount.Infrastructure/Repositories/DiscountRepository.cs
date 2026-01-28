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
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
