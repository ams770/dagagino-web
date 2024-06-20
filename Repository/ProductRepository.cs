using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Data;
using Dagagino.Interfaces;
using Dagagino.Models;
using MongoDB.Driver;

namespace Dagagino.Repository
{
    public class ProductRepository(AppDBContext dbContext) : IProductRepository
    {
        /* -------------------------------------------------------------------------- */
        /*                              Collection Reader                             */
        /* -------------------------------------------------------------------------- */
        private readonly IMongoCollection<Product> Products = dbContext.Products;

        /* -------------------------------------------------------------------------- */
        /*                              Get All Products                              */
        /* -------------------------------------------------------------------------- */
        public async Task<List<Product>> GetAllAsync() => await Products.Find(p => true).ToListAsync();
    }
}