using Job_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View( db.Categories.ToList());
        }

        public ActionResult Details (int jobid)

        {
            var job = db.Jobs.Find(jobid);
            if (job == null)
            {
                return HttpNotFound();


            }
            Session["Jobid"] = jobid;
            return View(job);


        }

        [Authorize]
        public ActionResult Applay()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Applay(string Message)
        {


            var Userid = User.Identity.GetUserId();
            var Jobid = (int)Session["Jobid"];
            var job = new ApplayForJob();
            var check = db.ApplayForJobs.Where(a => a.jobid == Jobid && a.userid == Userid).ToList();
            if (check.Count < 1)
            {
                job.userid = Userid;
                job.jobid = Jobid;
                job.Message = Message;
                job.ApplayDate = DateTime.Now;
                db.ApplayForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = " تمت الاضافة بنجاح|";

                              
            }
            else
            {
                ViewBag.Result = "المعذرة,  لقد سبق وتقدمت الي نفس هذه الوظيفة";

            }
            

            return  View();
        }
        [Authorize]
        public ActionResult GetJobsbyUsers()
        {

            var Userid = User.Identity.GetUserId();
            var Jobs = db.ApplayForJobs.Where(a => a.userid == Userid);

            return View(Jobs.ToList());

        }
        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplayForJobs.Find(id);
            if (job==null)
            {

                return HttpNotFound();

            }


            return View(job);

        }


        [Authorize]
        public ActionResult GetJobsbyPublisher()

        {
            var UserID = User.Identity.GetUserId();


            var jobs = from app in db.ApplayForJobs

                       join job in db.Jobs
                       on app.jobid equals job.Id
                       where job.User.Id == UserID
                       select app;


            var groubed = from j in jobs
                          group j by j.job.Jobtitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key
                              , item = gr

                          };
            //jobs
            return View(groubed.ToList());
        }


        public ActionResult Search()
        {


            return View();
        }


        [HttpPost]
        public ActionResult Search(string SearchName)
        {


            var Result = db.Jobs.Where(a => a.Jobtitle.Contains(SearchName) || a.JobContent.Contains(SearchName) || a.category.CategoryName.Contains(SearchName)
            
            
            || a.category.CategoryDescription.Contains(SearchName)
            );


            return View(Result);

        }

        public ActionResult Edit(int id)
        {
            var job = db.ApplayForJobs.Find(id);
            if(job==null)
            {
                return HttpNotFound();
 
            }
             
            return View(job); 

        }

        [HttpPost]
        public ActionResult Edit(ApplayForJob job)

        {
            if(ModelState.IsValid)
            {
                job.ApplayDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                                  
                RedirectToAction("GetJobsbyUsers");

            }

            return View(job);
        }



        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplayForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete( ApplayForJob job)
        {

            if (ModelState.IsValid)
            {


                db.Entry(job).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("GetJobsbyUsers");
            }

            return View(job);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Contact(contectmodelview contect)
        {
            var mail = new MailMessage();

            var loginfo = new NetworkCredential("momalik665@gmail.com", "pesmost000");
            mail.From = new MailAddress(contect.Email);
            mail.To.Add("momalik665@gmail.com");
            mail.IsBodyHtml = true;
            mail.Subject = contect.Object;

            string body = "اسم المرسل: " + contect.Name + "<br>" +
                 " بريد المرسل  : " + contect.Email + "<br>" +
                " عنوان الرسالة : " + contect.Object + "<br>" +
                " نص الرسالة :" + contect.Message;

            mail.Body = body;

            var smtpclient = new SmtpClient("smtp.gmail.com",587);
            smtpclient.EnableSsl = true;

            smtpclient.Credentials = loginfo;
            smtpclient.Send(mail);

           return RedirectToAction("Index");
             
        }
    }
}