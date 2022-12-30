using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServerAPI.Models;

namespace WebServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController() { }
        [HttpGet("list")]
        public ActionResult Index()
        {
            List<RollItemVM> model = new List<RollItemVM>
            {
                new RollItemVM
                {
                    Id = 1,
                    Name = "Спайсі рол з окунем і лососем",
                    Grams = "210г",
                    Description = new List<string> {"Рол з окунем", "лососем, листом салата", "японським омлетом", "спайсі соусом і ікрою Тобіко" },
                    Img = "/menu/spajsi-roll-s-okunem-i-lososem.jpg",
                    Discount = false,
                    Price = 99,
                    DiscountPrice = 70
                }
            };
            return Ok(model);
        }
    }
}
