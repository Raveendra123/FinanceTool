using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FinaceTool.Controllers;
using FinaceTool.Common;

namespace FinaceTool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        FinanceToolEntities obj = new FinanceToolEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
       
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                if (serializeModel != null)
                {
                    newUser.UserId = serializeModel.UserId;
                    newUser.UserName = serializeModel.UserName;
                    newUser.UserEmail = serializeModel.UserEmail;
                    newUser.RoleID = serializeModel.RoleID;
                    newUser.RoleName = serializeModel.RoleName;
                    newUser.SapId = serializeModel.SapId;
                }
                HttpContext.Current.User = newUser;
            }
        }
    }
}
