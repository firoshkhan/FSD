using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment20.Models;
using System.Web.Security;
using System.Threading.Tasks;

namespace Assignment20.Controllers
{
    public class PeopleController : Controller
    {
        private Assignment20EntitiesNew db = new Assignment20EntitiesNew();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        public ActionResult Search()
        {
           // ViewBag.user_id = new SelectList(db.People, "User_Id", "password");
            return View();
        }


        // GET: People/Details/5
        [HttpPost]
        public ActionResult Search(string txtsearchUserName)       
        {
            if (txtsearchUserName == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pERSON = db.People.Where(m => (m.fullname == txtsearchUserName)).ToList();
            if (pERSON == null)
            {
                return HttpNotFound();
            }
          //  Assignment20.Models.TWEET tweet = new Assignment20.Models.TWEET();
           // tweet.PERSON= pERSON;
            return View(pERSON);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,password,fullname,email,joined,active")] PERSON pERSON)
        {
            if (ModelState.IsValid)
            {
                pERSON.joined = DateTime.Today;
               // pERSON.User_Id = Session["UserID"].ToString();
                Session["UserID"] = pERSON.User_Id;
               Session["fullname"] = pERSON.fullname;

                db.People.Add(pERSON);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }
            return Redirect("~/people/Login");

           
        }

        // GET: People/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

       

        public bool UserNotExists(string id, string FollowUserid)
        {
            var p4 = db.People.AsNoTracking().Include("PERSON1").Where(i => i.User_Id == id);

            var peoples1 = p4.ToArray();
            IEnumerable<PERSON> FollowUSer = null;


            foreach (var people in peoples1)
            {

                ViewBag.Followers = people.PERSON1.Count().ToString();
                ViewBag.Following = people.People.Count().ToString();
                ViewBag.TWEETs = people.TWEETs.Count().ToString();

                FollowUSer = people.People.Where(p => p.User_Id == FollowUserid);
            }

            peoples1 = null;


            if (FollowUSer.Count() == 0)
            {
                FollowUSer = null;

               // var p4 = db.People.AsNoTracking()
                return true;
                }

            else return false;

        }
      
        public ActionResult AddFollows(string id, string FollowUserid)
        {

            // Make dummy objects for records existing in your database
            /* var user = new User() { Id = 1 };
             var product = new Product() { Id = 1 };
             // Ensure that your context knows instances and does not track them as new or modified
             context.Users.Attach(user);
             context.Products.Attach(product);
             // Again make relation and save changes
             user.Products.Add(product);
             ctx.SaveChanges();*/


            if (UserNotExists(id, FollowUserid))

            { 

   
                // var p5 = db.People.Include("People").Where(i => i.User_Id == "test");

                PERSON p1 = new PERSON() { User_Id = FollowUserid   };
                PERSON p2 = new PERSON() { User_Id = id };
                db.People.Attach(p1);
                db.People.Attach(p2);
                p1.PERSON1.Add(p2);
                // PERSON pERSON = db.People.Find("test");
                //pERSON.People.Add(p1);
                //pERSON.PERSON1.Add(p1);
                db.SaveChanges();

      }

            return Redirect("~/TWEETs/Index1");
           // return View(db.People);

        }


        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,password,fullname,email,active")] PERSON pERSON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERSON);
        }

        public ActionResult Login()
     {  
          return View();  
      }

    [HttpPost]
        public ActionResult Login([Bind(Include = "User_Id,password,fullname")] PERSON pERSON)
        {
          //  DataContext db = new DataContext();
            var output = db.People.FirstOrDefault(m => (m.User_Id == pERSON.User_Id) && (m.password == pERSON.password));
            if (output != null)
            {
                //ViewBag.msg = "Success full Login";
                FormsAuthentication.SetAuthCookie(pERSON.User_Id,true);
                Session["UserID"] = output.User_Id;
                Session["fullname"] = output.fullname;
                return RedirectToAction("index1","TWEETs");
                
            }
            else
            {
                ViewBag.msg = "UnSuccess full Login";
            }
        
            return View();
        }
 

    // GET: People/Delete/5
    public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERSON pERSON = db.People.Find(id);
            db.People.Remove(pERSON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

