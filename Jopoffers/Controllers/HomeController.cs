using Jopoffers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
          
            return View(db.Categories.ToList());
        }
    
        
        public ActionResult Details(int jopId)
        {
            var jop = db.Jops.Find(jopId);

            if (jop == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jopId;
            return View(jop);
        }
        [Authorize]
        public ActionResult Apply()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Apply(string message)
        {
            var userId = User.Identity.GetUserId();
            var jopId = (int)Session["JobId"];

            var check = db.ApplyForJops.Where(a => a.JopId == jopId && a.UserId == userId).ToList();

            if (check.Count() < 1)
            {
                var jop = new ApplyForJop();

                jop.UserId = userId;
                jop.JopId = jopId;
                jop.ApplyDate = DateTime.Now;
                jop.message = message;

                db.ApplyForJops.Add(jop);
                db.SaveChanges();
                ViewBag.Result = "تم التقدم الى الوظيفة بنجاح";

                return RedirectToAction("GetJopsByUser");

            } else
            {
                ViewBag.Result = "لقد سبق وتقدمت الى هذه الوظيفة";
            }
            return View();
   
        }

        [Authorize]
        public ActionResult GetJopsByUser()
        {
            var userid = User.Identity.GetUserId();

            var jops = db.ApplyForJops.Where(a => a.UserId == userid);
            return View(jops.ToList());
        }


        public ActionResult GetJopsByPublisher()
        {
            var UserId = User.Identity.GetUserId();

            var jops = from app in db.ApplyForJops
                       join jop in db.Jops
                       on app.JopId equals jop.Id
                       where jop.UserID == UserId
                       select app;
            var groubed = from j in jops
                          group j by j.jop.title
                          into grou
                          select new JopsView
                          {
                              JopTitle = grou.Key,
                              Items= grou

                          };


            return View(groubed.ToList());
        }

        [Authorize]
        public ActionResult Detailsofjop(int id)
        {
            var jop = db.ApplyForJops.Find(id);
            if(jop == null)
            {
                return HttpNotFound();
            }

            return View(jop);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             var jop = db.ApplyForJops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplyForJop jop)
        {
            if (ModelState.IsValid)
            {
                jop.ApplyDate = DateTime.Now;
                
                db.Entry(jop).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("GetJopsByUser");
            }
            return View(jop);

        }

        public ActionResult Search()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jops.Where(a => a.title.Contains(searchName)
            || a.content.Contains(searchName)
            || a.Category.Name.Contains(searchName)
            || a.Category.Description.Contains(searchName));

            return View(result);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jop = db.ApplyForJops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var jop= db.ApplyForJops.Find(id);

            db.ApplyForJops.Remove(jop);
            db.SaveChanges();
            return RedirectToAction("GetJopsByUser");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]

        public ActionResult Contact(ContactModel contact)
        {
            /*
            var mail = new MailMessage();
            var login = new NetworkCredential("mekhailmaged45@gmail.com", "beshoy93@");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("mekhailmaged45@gmail.com"));
            mail.Subject = contact.Subject;
            mail.Body = contact.Message;

            var smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.EnableSsl = true;
            smtp.Credentials = login;
            smtp.Send(mail);
            */
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "mekhailmaged45@gmail.com";
            WebMail.Password = "";
            WebMail.From = "mekhailmaged45@gmail.com";
            WebMail.SmtpUseDefaultCredentials = true;
            string body = "From" + contact.Name +"<br>" +
                           "Email :" + contact.Email +"<br>" +
                            "content :" + contact.Message; 
            WebMail.Send("mekhailmaged45@gmail.com","contact us ",body);
            return RedirectToAction("Index");
        }

        public ActionResult test()
        {
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "mekhailmaged45@gmail.com";
            WebMail.Password = "beshoy93@";
            WebMail.From = "mekhailmaged45@gmail.com";
            WebMail.SmtpUseDefaultCredentials = true;
            WebMail.Send("mekhailmaged45@gmail.com", "test", "will attend");
            return RedirectToAction("Index");
        }
    }
}