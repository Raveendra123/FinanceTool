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
    public class ProjectController : Controller
    {
        public FinanceToolEntities db = new FinanceToolEntities();
        // GET: Project
        public ActionResult ProjectDetails()
        {
            return View();
        }


        public JsonResult GetProjects(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            #region MyRegion
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            // FinanceToolEntities2 db = new FinanceToolEntities2();
            var ProjectResults = db.Usp_GetProjectMasterDetails().Select(
                    project => new
                    {
                        project.ProjectID,
                        project.OpportunityName,
                        project.ProjectName,
                        project.poavilable,
                        project.pobalance,
                        project.SOWStatus,
                        project.Note,
                        project.Createdby,
                        project.Createddate,
                        project.Modifiedby,
                        project.ModifiedDate,
                        project.ProjectCode
                    }).ToList();



            int totalRecords = ProjectResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var customerResults2 = ProjectResults.OrderByDescending(s => s.ProjectName);

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
        public string Create([Bind(Exclude = "ProjectID")]  FinaceTool.Models.ProjectTemp projecttemp)
        {
            string msg;
            if (projecttemp.OpportunityName != 0 && projecttemp.SOWStatus != 0)
            {
                Project project = new Project();
                project.Createddate = System.DateTime.Now;
                project.IsActive = true;
                project.SowStatusID = projecttemp.SOWStatus;
                project.OpportunityID = projecttemp.OpportunityName;
                project.Createdby = Session["UserName"].ToString();
                project.Modifiedby = Session["UserName"].ToString();
                project.ProjectName = projecttemp.ProjectName;
                project.poavilable = projecttemp.poavilable;
                project.pobalance = projecttemp.pobalance;
                project.Note = projecttemp.Note;
                project.ProjectCode = projecttemp.ProjectCode;
                db.Projects.Add(project);
                db.SaveChanges();
                int i = db.Usp_Update_Opportunity_IsMapped(projecttemp.OpportunityName);
                // GetOpportunityNames();
                msg = "Saved Successfully";
            }
            else
            {
                msg = "Opportunity Name Should provide to create a Project";
            }

            return msg;
        }


        public string Edit(FinaceTool.Models.ProjectTemp projecttemp)
        {
            var result = db.Projects.SingleOrDefault(b => b.ProjectID == projecttemp.ProjectID);
            string msg;
            if (ModelState.IsValid && result != null)
            {
                result.Note = projecttemp.Note;
                result.OpportunityID = projecttemp.OpportunityName;
                result.poavilable = projecttemp.poavilable;
                result.pobalance = projecttemp.pobalance;
                result.ProjectName = projecttemp.ProjectName;
                result.SowStatusID = projecttemp.SOWStatus;
                result.Createdby = Session["UserName"].ToString();
                result.Createddate = System.DateTime.Now;
                result.ModifiedDate = System.DateTime.Now;
                result.Modifiedby = Session["UserName"].ToString();
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        //public string GetOpportunityNames()
        //{
        //    System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
        //    strBuilder.Append("<select>");
        //    foreach (var item in db.Opportunities.ToList<Opportunity>())
        //    {
        //        strBuilder.Append("<option value = '" + item.OpportunityID + "'>" + item.OpportunityName + "</option>");
        //    }
        //    strBuilder.Append("</select>");
        //    return strBuilder.ToString();
        //}

        public string GetOpportunityNames()
        {
            int a = 0;
            string sel = "--Select Opportunity--";
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            strBuilder.Append("<select>");
            strBuilder.Append("<option value = '" + a + "'>" + sel + "</option>");
            var OpportunityResults = db.Usp_GetOpportunityDetails().Select(
                    opportunity => new
                    {
                        opportunity.OpportunityID,
                        opportunity.OpportunityName

                    }).ToList();
            //strBuilder.Append("<option value= '" + a + "'>" + sel + "</option>");
            foreach (var item in OpportunityResults)
            {
                strBuilder.Append("<option value = '" + item.OpportunityID + "'>" + item.OpportunityName + "</option>");
            }
            strBuilder.Append("</select>");
            return strBuilder.ToString();

        }



        public string Delete(Project project)
        {
            string msg;
            if (ModelState.IsValid)

            {
                db.Projects.Add(project);
                db.SaveChanges();
            }
            msg = "Saved Successfully";
            return msg;
        }

        public string GetSOWstatus()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<select>");

            foreach (var item in db.SOWStatus.ToList<SOWStatu>())
            {
                strBuilder.Append("<option value = '" + item.SOWStatusID + "'>" + item.SOWStatus + "</option>");
            }
            strBuilder.Append("</select>");

            return strBuilder.ToString();
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
