using System;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using PersonalSite.UI.MVC.Models;


namespace PersonalSite.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }//end Action

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            //sending message...
            string body = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";
            //Message Object from site to my gmail
            MailMessage m = new MailMessage("admin@benslenker.com", "slenkerbenjamin@gmail.com", cvm.Subject, body);

            //allow HTML in email
            m.IsBodyHtml = true;

            //high priority
            m.Priority = MailPriority.High;

            //reply to visitor who emailed the admin@benslenker.com
            m.ReplyToList.Add(cvm.Email);

            //configure the mail client
            SmtpClient client = new SmtpClient("mail.benslenker.com");

            //Configure your email credentials
            client.Credentials = new NetworkCredential("admin@benslenker.com", "Chewy13@");
            
            try
            {
                //send email
                client.Send(m);
            }//end try
            catch (Exception e)
            {
                //log error in viewbag to be seen by admins
                ViewBag.Message = e.StackTrace;
            }//end catch
            return Json(cvm);
        }//end Action
    }//end class
}//end namespace
