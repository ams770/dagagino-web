using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Models;

namespace Dagagino.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
    }
}