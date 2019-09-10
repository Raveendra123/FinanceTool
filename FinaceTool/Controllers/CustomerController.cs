using FinaceTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    [CustomAuthorize(Roles = "14,8,9")]
    public class CustomerController : Controller
    {
        private FinanceToolEntities db = new FinanceToolEntities();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomers(string sidx, string sord, int page, int rows )  //Gets the todo Lists.
        {
           
                #region MyRegion
                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
               // FinanceToolEntities2 db = new FinanceToolEntities2();
                var customerResults = db.customers.Select(
                        customer => new
                        {
                            customer.CustomerID,
                            customer.CustomerName,
                            customer.Createdby,
                            customer.Createddate,
                            customer.Modifiedby,
                            customer.ModifiedDate
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



            int totalRecords = customerResults.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

                //if (string.IsNullOrEmpty(sidx))
                //{
                //    sidx = "CustomerAccountName";
                //}
                //if (string.IsNullOrEmpty(sord))
                //{
                //    sord = "asc";
                //}
                var customerResults2 = customerResults.OrderByDescending(s=> s.CustomerName);

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
        public string Create([Bind(Exclude = "CustomerID")]  customer customerec)
        {
            string msg;
            if (ModelState.IsValid)

            {
                customerec.Createdby = Session["UserName"].ToString();
                customerec.Modifiedby = Session["UserName"].ToString();
                db.customers.Add(customerec);
                db.SaveChanges();
            }
                        msg = "Saved Successfully";
                return msg;
            }
           

        public string Edit(customer customer)
        {
            var result = db.customers.SingleOrDefault(b => b.CustomerID == customer.CustomerID);
            string msg;
            if (ModelState.IsValid && result!= null)
            {
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.CustomerName = customer.CustomerName;
                result.Modifiedby = Session["UserName"].ToString();
                result.ModifiedDate = System.DateTime.Now;
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }
        public string Delete(customer customer)
        {

            string msg;
            if (ModelState.IsValid)

            {
                db.customers.Add(customer);
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
