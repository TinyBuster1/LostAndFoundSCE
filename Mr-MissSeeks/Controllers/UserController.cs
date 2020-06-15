using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mr_MissSeeks.Models;
namespace Mr_MissSeeks.Controllers
{
    public class UserController : Controller
    {

        private Models.Tables database;
        public UserController()
        {
            database = new Models.Tables();
        }
        // GET: User
        public ActionResult Search()
        {
            return View();
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/itemImages"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }


                Item newRecord = new Item();
                newRecord.name = Request.Form["name"];
                newRecord.type = Request.Form["type"];
                newRecord.color = Request.Form["color"];
                newRecord.status = Request.Form["status"];
                newRecord.location = Request.Form["location"];
                newRecord.userID = Request.Form["userID"];
                newRecord.description = Request.Form["description"];
                newRecord.imagePath = pic;


                int maxId;
                if (database.Items.Count() == 0)
                {
                    maxId = 0;
                }
                else
                {
                    maxId = database.Items.Max(u => u.Id);
                }


                newRecord.Id = maxId + 1;
                database.Items.Add(newRecord);
                database.SaveChanges();

            }





            // after successfully uploading redirect the user
            return RedirectToAction("/Search");
        }

        public ActionResult GetLostData()
        {
            using (database = new Tables())
            {
                List<Item> userList = database.Items.Where(t => t.status.Equals("Lost")).ToList<Item>();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetFoundData()
        {
            using (database = new Tables())
            {
                List<Item> userList = database.Items.Where(t => t.status.Equals("Found")).ToList<Item>();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ItemDetails(int id)
        {
            Tables database = new Tables();
            var model = database.Items.FirstOrDefault(t => t.Id == id);



            if (model == null)
            {
                TempData["ItemDetails"] = "";
            }
            else
            {
                List<float> location = new List<float>();
                string[] words = model.location.Split(',');

                foreach (var word in words)
                {
                    location.Add(float.Parse(word));
                }
                List<string> itemDetails = new List<string>();

                itemDetails.Add(model.name);
                itemDetails.Add(model.type);
                itemDetails.Add(model.color);
                itemDetails.Add(model.description);
                itemDetails.Add(model.userID);
                itemDetails.Add(model.imagePath);


                TempData["itemLocation"] = location;
                TempData["itemDetails"] = itemDetails;

            }

            return View(new Item());

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




    }
}