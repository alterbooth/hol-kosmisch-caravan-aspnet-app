using MyWebApp.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    public class UsersController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Age,ProfileFileName")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            HttpPostedFileBase file = Request.Files["profile"];
            if (file != null && file.ContentLength > 0)
            {
                user.ProfileFileName = SaveFile(file);
            }

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,ProfileFileName")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            HttpPostedFileBase file = Request.Files["profile"];
            if (file != null && file.ContentLength > 0)
            {
                user.ProfileFileName = SaveFile(file);
            }

            db.Entry(user).State = EntityState.Modified;
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

        private string SaveFile(HttpPostedFileBase file)
        {
            string dirPath = HttpContext.Server.MapPath("~/temp/");
            Directory.CreateDirectory(dirPath);

            string fileName = new FileInfo(file.FileName).Name;
            file.SaveAs($"{dirPath}{fileName}");
            return $"/temp/{fileName}";
        }
    }
}
