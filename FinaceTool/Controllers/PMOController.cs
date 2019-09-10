using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    public class PMOController : Controller
    {
        // GET: PMO
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            QuaterGenerator.UpdateActiveQuaters();
            FinanceToolEntities financetoolentities = new FinanceToolEntities();
            var PMOGriddetails = financetoolentities.Usp_GetPMOOpportunityDetails().ToList();
            return View(PMOGriddetails);
        }
    }
}