using FinaceTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    [CustomAuthorize(Roles = "14,8,9")]
    public class UserController : Controller
    {
        // GET: User
        private FinanceToolEntities db = new FinanceToolEntities();
        public ActionResult GetUsers()
        {
            return View();
        }

        public JsonResult GetUserDetails(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            // FinanceToolEntities2 db = new FinanceToolEntities2();
            var duResults = db.Usp_GetUserMasterDetails().Select(
                    userdata => new
                    {
                       userdata.UserID,
                       userdata.UserName,
                       userdata.ISActive,
                       userdata.RoleID,
                       userdata.RoleName,
                       userdata.Createdby,
                       userdata.Createddate,
                       userdata.Modifiedby,
                       userdata.ModifiedDate,
                       userdata.password 

                    }).ToList();
            int totalRecords = duResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            var customerResults2 = duResults.OrderByDescending(s => s.UserName);
            var customerResults3 = customerResults2.Skip(pageIndex * pageSize).Take(pageSize);
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = customerResults3
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
            #endregion
        }
        [HttpPost]
       public string Create([Bind(Exclude = "UserID")]  FinaceTool.Models.UserTemp userrec)
        {
            string rolename = Session["RoleName"].ToString();
            string msg;
            User user = new User();
            if (userrec.RoleName != 0 )
            {
                user.RoleID= userrec.RoleName;
                user.Createdby = rolename;
                user.Modifiedby = rolename;
                user.Createddate = System.DateTime.Now;
                user.Password = userrec.Password;
                user.ISActive = userrec.ISActive;
                db.Users.Add(user);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }


        public string Edit(FinaceTool.Models.UserTemp  userrec)
        {
            var result = db.Users.SingleOrDefault(b => b.UserID == userrec.UserID);
            string msg;
            if (userrec.RoleName != 0)
            {
                result.RoleID = userrec.RoleName;
                result.UserName = userrec.UserName;
                result.ISActive = userrec.ISActive;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        public string GetRoleNames()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<select>");
            foreach (var item in db.Roles.ToList<Role>())
            {
                strBuilder.Append("<option value = '" + item.RoleID + "'>" + item.RoleName + "</option>");
            }
            strBuilder.Append("</select>");
            return strBuilder.ToString();
        }

    }
}