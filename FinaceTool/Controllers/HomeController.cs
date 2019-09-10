using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinaceTool.Models.ViewModels;


namespace FinaceTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProjectDetails()
        {
            List<ProjectDetails> Projectdetails = new List<ProjectDetails>();

            FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
            var datacontent = FinanceToolEntities2.AMTable().ToList();
            foreach (var item in datacontent)
            {
                ProjectDetails ProjectDetailitem = new ProjectDetails();
                ProjectDetailitem.SNo = item.S_No;
                ProjectDetailitem.CustomerName = item.CustomerName;
                ProjectDetailitem.AMName = item.AMName;
                ProjectDetailitem.LOB = item.LOBName;
                ProjectDetailitem.DUName = item.DUName;
                ProjectDetailitem.DBBLDU = item.DBBLDU;
                ProjectDetailitem.OpportunityID = item.OpportunityID;
                ProjectDetailitem.OpportunityName = item.OpportunityName;
                ProjectDetailitem.ProjectID = item.ProjectID;
                ProjectDetailitem.ProgramName = item.ProgramName;
                ProjectDetailitem.ServiceLine = item.ServiceLine;
                ProjectDetailitem.ProductGroup = item.ProductGroup;
                ProjectDetailitem.TCV = item.TCV;
                ProjectDetailitem.ACV = item.acv;
                ProjectDetailitem.SOWValue = item.sowvalue;
                ProjectDetailitem.DealStage = item.DealStage;
                ProjectDetailitem.BillingStartDate = item.BillingStratDate;
                ProjectDetailitem.Note = item.Note;

               
                Projectdetails.Add(ProjectDetailitem);
            }
            return View(Projectdetails);
            

            //return View(ProjectDetails);
        }
        
        public ActionResult GetInsertRecord()
        {
            int roleid = 10;
            ProjectDetails ProjectDetails = new ProjectDetails();
            FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
            ProjectDetails.CustomerNames = FinanceToolEntities2.customers.Select(x=>x.CustomerName).ToList();
            ProjectDetails.AMNames = FinanceToolEntities2.Roles.Where(x=>x.RoleID== roleid).Select(x => x.RoleName).ToList();
            ProjectDetails.LOBs = FinanceToolEntities2.LOBs.Select(x => x.LOBName).ToList();
            ProjectDetails.OppurtunityIDs = FinanceToolEntities2.Opportunities.Select(x => x.OpportunityID).ToList();
            ProjectDetails.OpportunityNames = FinanceToolEntities2.Opportunities.Select(x => x.OpportunityName).ToList();
            ProjectDetails.ProjectIDs = FinanceToolEntities2.Projects.Select(x => x.ProjectID).ToList();
            ProjectDetails.ProgramNames = FinanceToolEntities2.Opportunities.Select(x => x.ProgramName).ToList();
            ProjectDetails.ServiceLines = FinanceToolEntities2.ServiceLines.Select(x => x.ServiceLineName).ToList();
            ProjectDetails.DUNames = FinanceToolEntities2.DUs.Select(x => x.DUName).ToList();
            ProjectDetails.ProductGroups = FinanceToolEntities2.ProductGroups.Select(x => x.ProductGroupName).ToList();

            return PartialView("_InsertRecord", ProjectDetails);
        }
     
        public ActionResult PostInsertRecord()
        {
            return PartialView();
        }
        public ActionResult GetCustomer()
        {
            return View();
        }
        public ActionResult PostCustomerRecord(customer customerrec)
        {
            FinanceToolEntities FinanceToolEntities2 = new FinanceToolEntities();
            FinanceToolEntities2.customers.Add(customerrec);
            FinanceToolEntities2.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            FinanceToolEntities financetoolentities = new FinanceToolEntities();

            var data = financetoolentities.Usp_GetForecastValues().ToList();
            List<string> lstforecastvalues = new List<string>();
            List<string> QuaterName = new List<string>();
            
            //ViewBag.Message = "Your application description page.";
            foreach (var item in data)
            {
                //var value = item.GetType().GetProperty(item.QuaterName);
                //var Quartervalue = value.GetValue(item, null).ToString();
                var qvalue = MyCustomFormat(Convert.ToDouble(item.ForecastValues));
                lstforecastvalues.Add(qvalue);
                QuaterName.Add(item.QuaterName);

              

            }
           
            string names =   QuaterName[0] +  ","  + QuaterName[1]  + "," + QuaterName[2]  + ","  + QuaterName[3] ;
          var quarternames=  names;
            Session["QuarterNames"] = quarternames;
            string forecastvalues= lstforecastvalues[0]  + "," +  lstforecastvalues[1]  + "," +  lstforecastvalues[2] + ","  + lstforecastvalues[3] ;
            //var forecastvaluesarr = forecastvalues.Split(',');
            Session["forecastvalues"] = forecastvalues;
          
            return View();
        }
        public static string MyCustomFormat(double myNumber)
        {
            var str = (string.Format("{0:0.00}", myNumber));
            return (str.EndsWith(".00") ? str.Substring(0, str.LastIndexOf(".00")) : str);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}