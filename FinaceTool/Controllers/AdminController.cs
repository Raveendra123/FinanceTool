using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult GetCustomer()
        {
            return View();
        }
        public ActionResult AddCustomer(customer customerrec)
        {
            FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
            customerrec.Createddate = System.DateTime.Now;
            FinanceToolEntities2.customers.Add(customerrec);
            FinanceToolEntities2.SaveChanges();
            return View("AddCustomer");
        }
        public ActionResult GetLOB()
        {
            return View();
        }
        public ActionResult AddLOB(LOB lob)
        {
            FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
            lob.Createddate = System.DateTime.Now;
            FinanceToolEntities2.LOBs.Add(lob);
            FinanceToolEntities2.SaveChanges();
            return View("GetLOB");
        }
        public ActionResult GetSDU()
        {
            FinanceToolEntities entities = new FinanceToolEntities();
            ViewBag.Organisations = new SelectList(entities.LOBs.ToList(), "LOBID", "LOBName");
            return View();
        }


        public ActionResult AddSDU(SDU sdu)
        {
            if (ModelState.IsValid)
            {
                sdu.Createddate = System.DateTime.Now;
                FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
                FinanceToolEntities2.SDUs.Add(sdu);
                FinanceToolEntities2.SaveChanges();
            }
            return View("GetSDU");
        }
        public ActionResult GetRole()
        {
            return View();
        }
        public ActionResult AddRole(Role role)
        {
            FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
            FinanceToolEntities2.Roles.Add(role);
            FinanceToolEntities2.SaveChanges();
            return View("GetRole");
        }
    }
}