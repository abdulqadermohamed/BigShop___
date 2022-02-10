using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using BigShop___.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace BigShop___.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            var products = db.Products.Where(p => p.isdeleted == false).Include(p => p.Category);
            ViewBag.categories = db.Categories.ToList();
            return View(products.ToList());

        }

        public ActionResult aboutus()
        {

            return View();
        }
        /// <summary>
        public ActionResult Form()
        {
            return View("contactus");
        }

        [HttpPost]
        public ActionResult Form(string reciverEmail, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderemail = new MailAddress("shopbig037@gmail.com", "Demo Test");
                    var reciveremail = new MailAddress(reciverEmail, "Receiver");
                    var password = "A@bigshop037";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(senderemail.Address, password)
                    };

                    using (var mess = new MailMessage(senderemail, reciveremail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View("contactus");

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "There are some problems in sending email";
            }

            return View("contactus");
        }


        /// <returns></returns>
        public ActionResult contactus()
        {
            return View();
        }
        public ActionResult search(string id)
        {
            Product product = db.Products.FirstOrDefault(n => n.ProductName.Equals(id));

            if (product == null)
            {
                return View("PageNotFound");
            }
            return RedirectToAction("productdetails", "product", new { id = product.ProductID });
        }


        public ActionResult paymentpage()
        {
            string currentuser = User.Identity.GetUserId();
            List<CartProducts> cartproducts = db.CartProducts.Where(c => c.Cart.User.Id == currentuser).ToList();

            return View(cartproducts);
        }
    }
}