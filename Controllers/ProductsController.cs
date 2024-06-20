using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dagagino.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        private readonly IProductRepository repository = repository;

        /* -------------------------------------------------------------------------- */
        /*                              Get All Products                              */
        /* -------------------------------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Get() {
            var products = await repository.GetAllAsync();
            return Ok(products);
        }
    }
}

