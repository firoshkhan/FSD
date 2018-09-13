using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment20.Models;

namespace Assignment20.Controllers
{
    public class TWEETsController : Controller
    {
        private Assignment20EntitiesNew db = new Assignment20EntitiesNew();

        //private Assignment20_1Entities db = new Assignment20_1Entities();

        // GET: TWEETs
        public ActionResult Index()
        {
           // var tWEETs = db.TWEETs.Include(t => t.PERSON);
            //return View(tWEETs.ToList());
            string uid = Session["UserID"].ToString();

            var p4 = db.People.AsNoTracking().Include("PERSON1").Where(i => i.User_Id == uid);
            var peoples1 = p4.ToArray();
            IEnumerable<PERSON> FollowUSer = null;


            foreach (var people in peoples1)
            {
                FollowUSer = people.People;
            }
            var FollowsId = FollowUSer.Select(l => l.User_Id).ToList();
            FollowsId.Add(uid);

            var tWEETs = db.TWEETs.Include(t => t.PERSON).Where(o => FollowsId.Contains(o.user_id)).OrderByDescending(o => o.created).ThenByDescending(o => o.user_id == uid )  ;


            

            return View(tWEETs.ToList());
        }
        public ActionResult Index1()
        {
            var tWEETs = db.TWEETs.Include(t => t.PERSON);
            string s = HttpContext.User.Identity.Name;
            return View(tWEETs.ToList());
        }

        // GET: TWEETs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWEET tWEET = db.TWEETs.Find(id);
            if (tWEET == null)
            {
                return HttpNotFound();
            }
            return View(tWEET);
        }

        // GET: TWEETs/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.People, "User_Id", "password");
            return View();
        }

        // POST: TWEETs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tweet_id,user_id,message,created")] TWEET tWEET)
        {
            if ((ModelState.IsValid)  && (tWEET.message != null))
            {
                tWEET.created = DateTime.Today;
                tWEET.user_id = Session["UserID"].ToString();
               // pERSON.active = true;
                db.TWEETs.Add(tWEET);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.People, "User_Id", "password", tWEET.user_id);
            return View(tWEET);
        }

        // GET: TWEETs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWEET tWEET = db.TWEETs.Find(id);
            if (tWEET == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.People, "User_Id", "password", tWEET.user_id);
            return View(tWEET);
        }

        // POST: TWEETs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tweet_id,user_id,message,created")] TWEET tWEET)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tWEET).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.People, "User_Id", "password", tWEET.user_id);
            return View(tWEET);
        }

        // GET: TWEETs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWEET tWEET = db.TWEETs.Find(id);
            if (tWEET == null)
            {
                return HttpNotFound();
            }
            return View(tWEET);
        }

        // POST: TWEETs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TWEET tWEET = db.TWEETs.Find(id);
            db.TWEETs.Remove(tWEET);
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

        public ActionResult GetFollows()
        {
            string id= Session["UserID"].ToString();
            var p4 = db.People.AsNoTracking().Include("PERSON1").Where(i => i.User_Id == id);

            var peoples1 = p4.ToArray();
            foreach (var people in peoples1)
            {

                ViewBag.Followers = people.PERSON1.Count().ToString();
                ViewBag.Following = people.People.Count().ToString();
                ViewBag.TWEETs = people.TWEETs.Count().ToString();


            }
            return View();

        }
    }
}
