using FinaceTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    [CustomAuthorize(Roles = "14,8,9")]
    public class RoleController : Controller
    {
        private FinanceToolEntities db = new FinanceToolEntities();
        // GET: Role
        public ActionResult GetRoleDetils()
        {
            return View();
        }

        public JsonResult GetRoles(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {

            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            // FinanceToolEntities2 db = new FinanceToolEntities2();
            var RolesResults = db.Roles.Select(
                    role => new
                    {
                        role.RoleID,
                        role.RoleName,
                        role.Createdby,
                        role.Createddate,
                        role.Modifiedby,
                        role.ModifiedDate
                        
                    }).ToList();
            
            //if ((IsActive == "true") || (IsActive == "false"))
            //{
            //    bool IsActiveValue = IsActive == "true";
            //    customerResults = customerResults.Where(p => p.IsActive.Equals(IsActiveValue));
            //}


            int totalRecords = RolesResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            //if (string.IsNullOrEmpty(sidx))
            //{
            //    sidx = "CustomerAccountName";
            //}
            //if (string.IsNullOrEmpty(sord))
            //{
            //    sord = "asc";
            //}
            var customerResults2 = RolesResults.OrderByDescending(s => s.RoleName);

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
        public string Create([Bind(Exclude = "RoleID")]  Role rolerec)
        {
            string msg;
            if (ModelState.IsValid)

            {
                rolerec.Createdby = Session["UserName"].ToString();
                rolerec.Modifiedby = Session["UserName"].ToString();
                db.Roles.Add(rolerec);
                db.SaveChanges();
            }
            msg = "Role added Successfully";
            return msg;
        }


        public string Edit(Role role)
        {
            var result = db.Roles.SingleOrDefault(b => b.RoleID == role.RoleID);
            string msg;
            if (ModelState.IsValid && role!=  null)
            {
                result.RoleName = role.RoleName;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Modified Successfully";
            return msg;
        }
        public string Delete(Role role)
        {

            string msg;
            if (ModelState.IsValid)

            {
                db.Roles.Add(role);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

    }
}