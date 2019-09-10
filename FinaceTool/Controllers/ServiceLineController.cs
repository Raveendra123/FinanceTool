using FinaceTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    [CustomAuthorize(Roles = "14,8,9")]
    public class ServiceLineController : Controller
    {
        private FinanceToolEntities db = new  FinanceToolEntities();
        // GET: ServiceLine
        public ActionResult Serviceline()
        {
            return View();
        }

        public JsonResult GetServices(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {

            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var serviceResults = db.ServiceLines.Select(
                    service => new
                    {
                        service.ServiceLineID,
                        service.ServiceLineName,
                        service.Createdby,
                        service.Createddate,
                        service.Modifiedby,
                        service.ModifiedDate
                    }).ToList();
            int totalRecords = serviceResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var customerResults2 = serviceResults.OrderByDescending(s => s.ServiceLineName);

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
        public string Create([Bind(Exclude = "ServiceLineID")]  ServiceLine serviceLine)
        {
            string msg;
            if (ModelState.IsValid)

            {
                serviceLine.Createdby = Session["UserName"].ToString();
                serviceLine.Modifiedby = Session["UserName"].ToString();
                db.ServiceLines.Add(serviceLine);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }


        public string Edit(ServiceLine serviceLine)
        {
            var result = db.ServiceLines.SingleOrDefault(b => b.ServiceLineID == serviceLine.ServiceLineID);
            string msg;
            if (ModelState.IsValid && result!=null)
            {
                result.ServiceLineName = serviceLine.ServiceLineName;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }
        public string Delete(ServiceLine serviceLine)
        {
            string msg;
            if (ModelState.IsValid)
            {
                db.ServiceLines.Add(serviceLine);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}