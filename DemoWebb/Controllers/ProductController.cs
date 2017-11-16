using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoWebb.Models;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json;
using DemoWebb.Helpers;

namespace DemoWebb.Controllers
{
    public class ProductController : Controller
    {
        private IAdockaClientHelper _helper;
        public ProductController(IAdockaClientHelper helper)
        {
            _helper = helper;
        }
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _helper.GetClient();

            var response = await client.GetAsync("open/Product/details?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(json);

                return View(product);
            }



            return View();
        }
        
    }
}
