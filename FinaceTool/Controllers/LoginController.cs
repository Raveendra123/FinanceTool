using FinaceTool.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace FinaceTool.Controllers
{
    public class LoginController : Controller
    {
        private FinanceToolEntities db = new FinanceToolEntities(); // GET: Login
        public ActionResult Index()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                //new Logger().Log(ex, "Login", "Index");
                Response.Write("<script>alert('Interal Server Error while loading the page. Please contact system administrator.')</script>");
            }
            return null;
        }


        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl = "adsad")
        {
            string rolename = string.Empty;

            if (ModelState.IsValid)
            {
                var uservariable = from users in db.Users
                                   join roles in db.Roles on new { users.RoleID } equals new { roles.RoleID }
                                   where users.ISActive == true && users.UserName == model.Username && users.Password == model.Password
                                   select new
                                   {
                                       users.UserID,
                                       users.UserName,
                                       users.RoleID ,
                                       roles.RoleName
                                   };
                var user = uservariable.FirstOrDefault();
                
                if (user == null)
                {
                    Response.Write("<script>alert('Incorrect UserName or Password')</script>");
                }
                else
                {
                    rolename = user.RoleID.ToString();
                    HttpContext.Session.Add("ISUserActive", true);
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.UserID;
                    serializeModel.UserName = user.UserName;
                    serializeModel.RoleID = user.RoleID;
                    Session["UserName"] = user.UserName;
                    Session["RoleName"] = user.RoleName;
                    Session["UserId"] = user.UserID;
                    Session["RoleId"] = user.RoleID;


                    string userData = JsonConvert.SerializeObject(serializeModel);
                    System.Web.Security.FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    user.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    false, //pass here true, if you want to implement remember me functionality
                    userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);
                    if (rolename == "14" || rolename == "8" || rolename =="9")
                    {
                      //  return RedirectToAction("DisplayAmDetails", "AdminData");
                        return RedirectToAction("About", "Home");
                    }
                    else if(rolename=="10")
                    {
                        return RedirectToAction("ViewOpportunities", "AMData");
                    }
                    else if(rolename == "7")
                    {
                        return RedirectToAction("Index", "DUHData");
                    }
                    else
                    {
                        return RedirectToAction("Index","PMO");
                    }
                }
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return null;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            // replace with username if this is the wrong cookie name
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            // send an expired cookie back to the browser
            var ticketExpiration = DateTime.Now.AddDays(-7);
            var ticket = new FormsAuthenticationTicket(
                1,
                //replace with username if this is the wrong cookie name
                FormsAuthentication.FormsCookieName,
                DateTime.Now,
                ticketExpiration,
                false,
                String.Empty);
            var cookie = new System.Web.HttpCookie("user")
            {
                Expires = ticketExpiration,
                Value = FormsAuthentication.Encrypt(ticket),
                HttpOnly = true
            };
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Login");
        }
    }
    public class CustomPrincipalSerializeModel
        {
            public string SapId { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public int RoleID { get; set; }
            public string RoleName { get; set; }
        }

    }
