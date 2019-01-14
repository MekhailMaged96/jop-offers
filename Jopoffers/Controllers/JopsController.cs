using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Jopoffers.Models;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace Jopoffers.Controllers
{
    [Authorize]
    public class JopsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jops
        public ActionResult Index()
        {
            var jops = db.Jops.Include(j => j.Category);
            return View(jops.ToList());
        }

        // GET: Jops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jop jop = db.Jops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
           
            return View(jop);
        }

        // GET: Jops/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Jops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jop jop,HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {

                string path = Path.Combine(Server.MapPath("~/Uploads"), img.FileName);


                img.SaveAs(path);
                
                jop.image =img.FileName;
                jop.UserID= User.Identity.GetUserId();
                db.Jops.Add(jop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", jop.CategoryId);
            return View(jop);
        }

        // GET: Jops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jop jop = db.Jops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", jop.CategoryId);
            return View(jop);
        }

        // POST: Jops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jop jop, HttpPostedFileBase img)
        {
            string oldpath = Path.Combine(Server.MapPath("~/Uploads"), jop.image);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    System.IO.File.Delete(oldpath);
                   // string path = Path.Combine(Server.MapPath("~/Uploads"), img.FileName);
                    string filenames = Path.GetFileNameWithoutExtension(img.FileName);
                    string extins = Path.GetExtension(img.FileName);
                    string ReNameFile = filenames + DateTime.Now.ToString("yymmssff") + extins;
                    string path = Path.Combine(Server.MapPath("~/Uploads"), ReNameFile);
                    img.SaveAs(path);
                    jop.image = ReNameFile;
                }
                db.Entry(jop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", jop.CategoryId);
            return View(jop);
        }

        // GET: Jops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jop jop = db.Jops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }

        // POST: Jops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jop jop = db.Jops.Find(id);
            db.Jops.Remove(jop);
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
