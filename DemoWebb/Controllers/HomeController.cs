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
    public class HomeController : Controller
    {
        private IAdockaClientHelper _helper;
        public HomeController(IAdockaClientHelper helper)
        {
            _helper = helper;
        }
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> Index()
        {
            var client = await _helper.GetClient();

            var response = await client.GetAsync("open/Product/All");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(json);

                return View(products);
            }



            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
