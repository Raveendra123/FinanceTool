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
    public class DUController : Controller
    {
        private FinanceToolEntities db = new FinanceToolEntities();
        // GET: DU
        
        public ActionResult DUDetails()
        {
            return View();
        }
        public ActionResult MyEditDetails(DU duobj)
        {

            return View();
        }
        public JsonResult GetDUs(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            // FinanceToolEntities2 db = new FinanceToolEntities2();
            var duResults = db.Usp_GetDUMasterDetails().Select(
                    du => new
                    {
                        du.DUID,
                        du.DUName,
                        du.DUHeadID,
                        du.DUHeadName,
                        du.AMName,
                        du.AMID,
                        du.SDUID,
                        du.SDUName,
                        du.Createdby,
                        du.Createddate,
                        du.Modifiedby,
                        du.ModifiedDate

                    }).ToList();
            int totalRecords = duResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            var customerResults2 = duResults.OrderByDescending(s => s.DUName);
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
        public string Create([Bind(Exclude = "DUID")]  FinaceTool.Models.DUTemp dutemp)
        {
            
            string msg;
            if (dutemp.AMName != 0 && dutemp.SDUName != 0)
            {
                DU Dbdu = new DU();
                Dbdu.AMID = dutemp.AMName;
                Dbdu.SDUID = dutemp.SDUName;
                Dbdu.DUName = dutemp.DUName;
                Dbdu.DUHeadID = Convert.ToInt16(dutemp.DUHeadName);
                Dbdu.Createdby = Session["UserName"].ToString();
                Dbdu.Modifiedby = Session["UserName"].ToString();
                Dbdu.Createddate = System.DateTime.Now;
                Dbdu.IsActive = true;
                db.DUs.Add(Dbdu);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        public string Edit(FinaceTool.Models.DUTemp dutemp)
        {
            var result = db.DUs.SingleOrDefault(b => b.DUID == dutemp.DUID);
            string msg;
            if (ModelState.IsValid && result != null)
            {
                result.AMID = dutemp.AMName;
                result.SDUID = dutemp.SDUName;
                result.DUHeadID = Convert.ToInt16(dutemp.DUHeadName);
                //result.AMID = du.AMID;
                result.DUName = dutemp.DUName;
                //result.SDUID = du.SDUID;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        [HttpPost]
        public string Delete(int Id)
        {
            var result = db.DUs.SingleOrDefault(b => b.DUID == Id);
            string msg;
            if (ModelState.IsValid &&  result!=null)
            {
                result.IsActive = false;
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }


        public string Deletee(int duid)
        {
            var result = db.DUs.SingleOrDefault(b => b.DUID == duid);
            string msg;
            if (ModelState.IsValid && result != null)
            {
                result.IsActive = false;
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        public string GetSDUNames()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<select>");
            foreach (var item in db.SDUs.ToList<SDU>())
            {
                strBuilder.Append("<option value = '" + item.SDUID + "'>" + item.SDUName + "</option>");
            }
            strBuilder.Append("</select>");
            return strBuilder.ToString();
        }

        public string GetAMNames()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<select>");
            foreach (var item in db.Users.Where(i=>i.RoleID == 10).ToList<User>())
            {
                strBuilder.Append("<option value = '" + item.UserID + "'>" + item.UserName + "</option>");
            }
            strBuilder.Append("</select>");
            return strBuilder.ToString();
        }

        public string GetDUHeadNames()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<select>");
            foreach (var item in db.Users.Where(i => i.RoleID == 7).ToList<User>())
            {
                strBuilder.Append("<option value = '" + item.UserID + "'>" + item.UserName + "</option>");
            }
            strBuilder.Append("</select>");
            return strBuilder.ToString();
        }

    }
}
