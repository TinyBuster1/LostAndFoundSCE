using Mr_MissSeeks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        //
        private Tables database;
        // GET: /User/
        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult GetUserData()
        {
            using (database = new Tables())
            {
                List<User> userList = database.Users.ToList<User>();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetItemData()
        {
            using (database = new Tables())
            {
                List<Item> userList = database.Items.ToList<Item>();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddItem(int itemId = 0)
        {

            return View(new Item());

        }

        [HttpPost]
        public ActionResult AddItem(Item item)
        {
            using (database = new Tables())
            {
                int maxId;
                if (database.Items.Count() == 0)
                {
                    maxId = 0;
                }
                else
                {
                    maxId = database.Items.Max(u => u.Id);
                }


                item.Id = maxId + 1;
                database.Items.Add(item);
                database.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            Tables database = new Tables();
            var model = database.Items.FirstOrDefault(t => t.Id == id);



            if (model == null)
            {
                TempData["location"] = "";
            }
            else
            {
                List<float> location = new List<float>();
                string[] words = model.location.Split(',');

                foreach (var word in words)
                {
                    location.Add(float.Parse(word));
                }

                TempData["location"] = location;
            }

            return View(new Item());

        }



        [HttpPost]
        public ActionResult EditItem(Item item)
        {
            database = new Tables();
            database.Entry(item).State = EntityState.Modified;
            database.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditUser()
        {

            return View(new User());


        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            Tables database = new Tables();
            database.Entry(user).State = EntityState.Modified;
            database.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public ActionResult DeleteUser(string Id)
        {
            using (database = new Tables())
            {
                User user = database.Users.Where(x => x.Id.ToString().Equals(Id)).FirstOrDefault<User>();
                database.Users.Remove(user);
                database.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteItem(string itemId)
        {
            using (database = new Tables())
            {

                Item item = database.Items.Where(x => x.Id.ToString().Equals(itemId)).FirstOrDefault<Item>();
                database.Items.Remove(item);
                database.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Statistics()
        {
            Tables databases = new Tables();
            List<int> data = new List<int>();

            int totalUsers = databases.Users.Count();
            int totalItems = databases.Items.Count();
            int totalLostItems = databases.Items.Where(t => t.status.Equals("Lost")).Count();
            int totalFoundItems = databases.Items.Where(t => t.status.Equals("Found")).Count();
            data.Add(totalUsers);
            data.Add(totalItems);
            data.Add(totalLostItems);
            data.Add(totalFoundItems);
            TempData["Statistics"] = data;
            return View();

        }

        [HttpPost]
        public ActionResult Statistics(User user)
        {

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CalculateStatistics()
        {
            Tables database = new Tables();
            var users = database.Users;

            ViewData["MyData"] = users; // Send this list to the view

            return View();
        }


    }

}