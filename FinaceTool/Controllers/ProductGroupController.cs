using FinaceTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    [CustomAuthorize(Roles = "14,8,9")]
    public class ProductGroupController : Controller
    {
        private FinanceToolEntities db = new FinanceToolEntities();

        // GET: ProductGroup
        public ActionResult ProductGroupDetails()
        {
            return View();
        }

        public JsonResult GetProducts(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {

            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            // FinanceToolEntities21 db = new FinanceToolEntities21();
            var productResults = db.ProductGroups.Select(
                    product => new
                    {
                        product.ProductGroupID,
                        product.ProductGroupName,
                        product.Modifiedby,
                        product.ModifiedDate,
                        product.Createdby,
                        product.Createddate


                    }).ToList();
            // db.customers.Add(customer);
            //if (!string.IsNullOrEmpty(CustomerAccountCode))
            //{
            //    customerResults = customerResults.Where(p => p.CustomerAccountCode.StartsWith(CustomerAccountCode));
            //}

            //if (!string.IsNullOrEmpty(CustomerAccountName))
            //{
            //    customerResults = customerResults.Where(p => p.CustomerAccountName.StartsWith(CustomerAccountName));
            //}


            //if ((IsActive == "true") || (IsActive == "false"))
            //{
            //    bool IsActiveValue = IsActive == "true";
            //    customerResults = customerResults.Where(p => p.IsActive.Equals(IsActiveValue));
            //}



            int totalRecords = productResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            //if (string.IsNullOrEmpty(sidx))
            //{
            //    sidx = "CustomerAccountName";
            //}
            //if (string.IsNullOrEmpty(sord))
            //{
            //    sord = "asc";
            //}
            var customerResults2 = productResults.OrderByDescending(s => s.ProductGroupName);

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
        public string Create([Bind(Exclude = "ProductGroupID")]  ProductGroup productrec)
        {
            string msg;
            if (ModelState.IsValid)

            {
                productrec.Createdby = Session["UserName"].ToString();
                productrec.Modifiedby = Session["UserName"].ToString();
                db.ProductGroups.Add(productrec);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }
      


        public string Edit(ProductGroup product)
        {
            var result = db.ProductGroups.SingleOrDefault(b => b.ProductGroupID == product.ProductGroupID);
            string msg;
            if (ModelState.IsValid && result!= null)
            {
                result.ProductGroupName = product.ProductGroupName;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }
        public string Delete(ProductGroup product)
        {

            string msg;
            if (ModelState.IsValid)

            {
                db.ProductGroups.Add(product);
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
