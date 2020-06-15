using Mr_MissSeeks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mr_MissSeeks.Controllers
{
    public class HomeController : Controller
    {
        private Tables database;


        public ActionResult Index()
        {
           
    
            
            Tables database = new Tables();

            return View();
        }


        [HttpGet]
        public ActionResult NearestItems()
        {
            Tables database = new Tables();
            List<Item> itemList = database.Items.ToList<Item>();
            List<float> locationLatt = new List<float>();
            List<string> itemDetails = new List<string>();
            List<float> locationLong = new List<float>();
            List<int> indexArray = new List<int>();
            int i = 0;
            foreach (Item item in itemList)
            {
                string[] itemLocation = item.location.Split(',');
                locationLatt.Add(float.Parse(itemLocation[0]));
                locationLong.Add(float.Parse(itemLocation[1]));
                indexArray.Add(i);
                i++;
                string itemDescription = "";
                itemDescription = item.name + " " + item.color + " " + item.type + " " +
                item.description;
                itemDetails.Add(itemDescription);     
            }




            TempData["locationLatt"] = locationLatt;
            TempData["locationLong"] = locationLong;
            TempData["indexArray"] = indexArray;
            TempData["itemDetails"] = itemDetails;
            return View();

        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}