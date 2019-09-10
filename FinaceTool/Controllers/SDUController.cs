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
    public class SDUController : Controller
    {
        private FinanceToolEntities db = new FinanceToolEntities();
        // GET: SDU
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSDUs(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            // FinanceToolEntities2 db = new FinanceToolEntities2();
            LOB oB = new LOB();
            var SduResults = db.Usp_GetSDUMasterDetails().Select(
                    sdu => new
                    {
                        sdu.SDUID,
                        sdu.SDUName,
                        sdu.LOBName,
                        sdu.Modifiedby,
                        sdu.Createdby,
                        sdu.ModifiedDate,
                        sdu.Createddate

                    }).ToList();
            int totalRecords = SduResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            
            var customerResults2 = SduResults.OrderByDescending(s => s.SDUName);

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
        public string Create([Bind(Exclude = "SDUID")]  FinaceTool.Models.SDUTemp sdurec)
        {
            string msg;
            SDU sdu = new SDU();
            if (sdurec.LOBName != 0)
            {
                sdu.LOBID = sdurec.LOBName;
                sdu.Createdby = Session["UserName"].ToString();
                sdu.Modifiedby = Session["UserName"].ToString();
                db.SDUs.Add(sdu);
                db.SaveChanges();
            }
            msg = "SDU added Successfully";
            return msg;
        }


        public string Edit(FinaceTool.Models.SDUTemp sdutemp)
        {
            var result = db.SDUs.SingleOrDefault(b => b.SDUID == sdutemp.SDUID);
            string msg;
            if (ModelState.IsValid && result != null)
            {
                result.LOBID = sdutemp.LOBName;
                result.SDUName = sdutemp.SDUName;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Modified Successfully";
            return msg;
        }
        public string Delete(SDU sdu)
        {
            string msg;
            if (ModelState.IsValid)
            {
                db.SDUs.Add(sdu);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        public string Getlobs()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<select>");

            foreach (var item in db.LOBs.ToList<LOB>())
            {
                strBuilder.Append("<option value = '" + item.LOBID + "'>" + item.LOBName + "</option>");
            }
            strBuilder.Append("</select>");

            return strBuilder.ToString();
        }


    }
}
    