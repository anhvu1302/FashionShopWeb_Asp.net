using FashionShopWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FashionShopWeb.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public ActionResult WeatherApp()
        {
            return View();
        }
        [HttpGet]
        public ActionResult WeatherApp(string location)
        {
            string key = "d87953e229d5cfeb15b51541485cb789";
            string apiUrl = string.Format("http://api.weatherstack.com/current?access_key={0}&query={1}",key , location);

            using (WebClient client = new WebClient())
            {
                string data = client.DownloadString(apiUrl);
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(data);

                return View(weatherData);
            }
        }
    }
}