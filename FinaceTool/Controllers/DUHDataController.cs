using FinaceTool.Common;
using FinaceTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{

    [CustomAuthorize(Roles = "7,9")]
    public class DUHDataController : Controller
    {
        FinanceToolEntities financetoolentities = new FinanceToolEntities();
        // GET: DUHData
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            QuaterGenerator.UpdateActiveQuaters();
            var DUHGriddetails = financetoolentities.Usp_GetDUHMainDetailsByUserID_V2(Session["UserId"].ToString()).Where(i => i.IsUpdated == false).ToList();
           // var DUHGriddetails = financetoolentities.Usp_GetDUHMainDetails().ToList();
            int Iteration = 0;

            List<DUHMainDetailsResult> DUHMainDetailsResultList = new List<DUHMainDetailsResult>();
            foreach (var DUHdbvalues in DUHGriddetails)
            {
                DUHMainDetailsResult duhmaindetail = new DUHMainDetailsResult();
                duhmaindetail.AMID = DUHdbvalues.AMID;
                duhmaindetail.AMName = DUHdbvalues.AMName;
                duhmaindetail.CustomerID = DUHdbvalues.CustomerID;
                duhmaindetail.Customername = DUHdbvalues.Customername;
                duhmaindetail.DBBLDU = DUHdbvalues.DBBLDU;
                duhmaindetail.DBBLDUID = DUHdbvalues.DBBLDUID;
                duhmaindetail.DealStageID = DUHdbvalues.DealStageID;
                duhmaindetail.DUID = DUHdbvalues.DUID;
                duhmaindetail.DUName = DUHdbvalues.DUName;
                duhmaindetail.LOBID = DUHdbvalues.LOBID;
                duhmaindetail.LOBName = DUHdbvalues.LOBName;
                duhmaindetail.Note = DUHdbvalues.Note;
                duhmaindetail.OpportunityID = DUHdbvalues.OpportunityID;
                duhmaindetail.OpportunityKeyID = DUHdbvalues.OpportunityKeyID;
                duhmaindetail.OpportunityLobId = DUHdbvalues.OpportunityLobId;
                duhmaindetail.OpportunityLobName = DUHdbvalues.OpportunityLobName;
                duhmaindetail.OpportunityName = DUHdbvalues.OpportunityName;              
                duhmaindetail.pobalance = DUHdbvalues.pobalance;
                duhmaindetail.ProductGroup = DUHdbvalues.ProductGroup;
                duhmaindetail.ProductGroupID = DUHdbvalues.ProductGroupID;
                duhmaindetail.ProgramName = DUHdbvalues.ProgramName;
                duhmaindetail.ProjectID = DUHdbvalues.ProjectID;
                duhmaindetail.ProjectName = DUHdbvalues.ProjectName;
                duhmaindetail.SDUID = DUHdbvalues.SDUID;
                duhmaindetail.ServiceLine = DUHdbvalues.ServiceLine;
                duhmaindetail.ServiceLineID = DUHdbvalues.ServiceLineID;
                duhmaindetail.SowStatus = DUHdbvalues.SOWStatus1;
                duhmaindetail.S_No = DUHdbvalues.S_No;
                if (DUHdbvalues.poavilable == "1")
                {
                    duhmaindetail.poavilable = "Yes";
                }
                else if (DUHdbvalues.poavilable == "0")
                {
                    duhmaindetail.poavilable = "No";
                }

                List<string> Objquater = new List<string>();
                List<string> QuaterName = new List<string>();
                List<string> Operations = new List<string>();
                Operations.Add("_FC");
                var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();
                var dbQuater_Actuallist = financetoolentities.quater_Actual.Where(i => i.IsActive == true).ToList();
                foreach (var quater in dbQuaterlist)
                {
                    foreach (var Obj in Operations)
                    {
                        var value = DUHdbvalues.GetType().GetProperty(quater.QuaterName + Obj);
                        var Quartervalue = value.GetValue(DUHdbvalues, null).ToString();
                        var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                        Objquater.Add(qvalue);
                    }
                    QuaterName.Add(quater.QuaterName + "_FC($K)");
                    // QuaterName.Add(quater.QuaterName + "_ACT($K)");
                }
                Operations.Clear();
                Operations.Add("_ACT");
                foreach (var quater in dbQuater_Actuallist)
                {
                    foreach (var Obj in Operations)
                    {
                        var value = DUHdbvalues.GetType().GetProperty(quater.QuaterName + Obj);
                        var Quartervalue = value.GetValue(DUHdbvalues, null).ToString();
                        var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                        Objquater.Add(qvalue);
                    }
                    //QuaterName.Add(quater.QuaterName + "_FC($K)");
                    QuaterName.Add(quater.QuaterName + "_ACT($K)");
                }

                if (Iteration == 0)
                {
                    duhmaindetail.QuaterName = QuaterName;
                }
                duhmaindetail.Quaterlist = Objquater;
                DUHMainDetailsResultList.Add(duhmaindetail);
                Iteration++;
            }
            return View(DUHMainDetailsResultList);
        }
        public static string MyCustomFormat(double myNumber)
        {
            var str = (string.Format("{0:0.00}", myNumber));
            return (str.EndsWith(".00") ? str.Substring(0, str.LastIndexOf(".00")) : str);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult EditDUHData(FinaceTool.Usp_GetDUHMainDetails_Result _duhSelctedObj)
        {
            bool result = QuaterGenerator.EditRestrictionByRole(Session["RoleId"].ToString());
            if (result == false)
            {
                TempData["Message"] = "You do not have permissions to Edit in this Time Slab.";
                return View("Index", Display());
            }
            else
            {
              
                Usp_GetDUHMainDetailsByUserID_V2_Result[] myresult = financetoolentities.Usp_GetDUHMainDetailsByUserID_V2(Session["UserId"].ToString())
                    .Where(i => i.OpportunityID == _duhSelctedObj.OpportunityID && i.IsUpdated == false &&
                  i.CustomerID == _duhSelctedObj.CustomerID &&
                  i.AMID == _duhSelctedObj.AMID &&
                  i.OpportunityLobId == _duhSelctedObj.LOBID && i.DUID == _duhSelctedObj.DUID && i.ServiceLineID == _duhSelctedObj.ServiceLineID)
                  .ToArray();
                FinaceTool.Models.DUHModel _DuhDetails = new FinaceTool.Models.DUHModel();

                _DuhDetails.dealstageId = myresult[0].DealStageID.ToString();
                _DuhDetails.Customername = _duhSelctedObj.Customername;
                _DuhDetails.CustomerList = BindDropdowncustomerDetails();
                _DuhDetails.duList = BindDropdownDUDetails();
                _DuhDetails.lobList = BindDropdownLobDetails();
                _DuhDetails.ProjectList = BindDropdownProjectDetails();
                _DuhDetails.serviceList = BindServiceLineDetails();
                _DuhDetails.ProductGroupList = BindDdlProductGroupDetails();
                _DuhDetails.DealStageList = BindDealStageDetails();
                _DuhDetails.SowStatusList = BindSowStatusDetails();
                _DuhDetails.AmUserList = BindAmUserDetails();
                _DuhDetails.poAvailablelist = BindPoAvailableDetails();
                _DuhDetails.OpportunityList = BindDropdownOpportunityDetails();
                //drop down binding ended
                _DuhDetails.OpportunityName = myresult[0].OpportunityName;
                _DuhDetails.S_No = myresult[0].S_No;
                _DuhDetails.selectedcustomerText = myresult[0].Customername;
                _DuhDetails.AMName = myresult[0].AMName;
                _DuhDetails.AMID = myresult[0].AMID;
                _DuhDetails.selectedcustomerId = myresult[0].CustomerID;
                _DuhDetails.OpportunityID = myresult[0].OpportunityID;
                _DuhDetails.DBBLDU = myresult[0].DBBLDU;
                _DuhDetails.DBBLDUID = myresult[0].DBBLDUID;
                _DuhDetails.DUID = myresult[0].DUID;
                _DuhDetails.DUName = myresult[0].DUName;
                _DuhDetails.LOBName = myresult[0].LOBName;
                _DuhDetails.LOBID = Convert.ToInt16(myresult[0].OpportunityLobId);
                _DuhDetails.OpportunityKeyID = myresult[0].OpportunityKeyID;
                _DuhDetails.poavilable = myresult[0].poavilable;
                _DuhDetails.pobalance = myresult[0].pobalance;
                if (string.IsNullOrEmpty(myresult[0].ProjectID.ToString()))
                {
                    _DuhDetails.ProjectID = 0;
                    _DuhDetails.ProjectName = "Please Select Project";
                }
                else
                {
                    _DuhDetails.ProjectID = myresult[0].ProjectID;
                    _DuhDetails.ProjectName = myresult[0].ProjectName;
                }
                _DuhDetails.ServiceLine = myresult[0].ServiceLine;
                _DuhDetails.ServiceLineID = myresult[0].ServiceLineID; ;
                _DuhDetails.ProgramName = myresult[0].ProgramName;
                _DuhDetails.ProductGroup = myresult[0].ProductGroup;
                _DuhDetails.ProductGroupID = myresult[0].ProductGroupID;
                if (string.IsNullOrEmpty(myresult[0].SowStatus.ToString()))
                {
                    _DuhDetails.SowStatus = 0;
                }
                else
                {
                    _DuhDetails.SowStatus = myresult[0].SowStatus;
                }
                _DuhDetails.Note = myresult[0].Note;



                //List<Quater> QuaterList = new List<Quater>();
                var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();
                var dbQuater_Actuallist = financetoolentities.quater_Actual.Where(i => i.IsActive == true).ToList();
                List<string> Operations = new List<string>();
                Operations.Add("_FC"); 
                List<string> QuaterList = new List<string>();
                foreach (var quater in dbQuaterlist)
                {
                    foreach (var Obj in Operations)
                    {
                        var value = myresult[0].GetType().GetProperty(quater.QuaterName.Substring(0, 5) + Obj);
                        var Quartervalue = value.GetValue(myresult[0], null).ToString();
                        var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                        string QuaterName = quater.QuaterName.Substring(0, 5) + Obj + "($K)" + ":" + qvalue;
                        QuaterList.Add(QuaterName);
                    }
                }
                Operations.Clear();
                Operations.Add("_ACT");
                foreach (var quater in dbQuater_Actuallist)
                {
                    foreach (var Obj in Operations)
                    {
                        var value = myresult[0].GetType().GetProperty(quater.QuaterName.Substring(0, 5) + Obj);
                        var Quartervalue = value.GetValue(myresult[0], null).ToString();
                        var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                        string QuaterName = quater.QuaterName.Substring(0, 5) + Obj + "($K)" + ":" + qvalue;
                        QuaterList.Add(QuaterName);
                    }
                }
                _DuhDetails.QuaterListData = QuaterList;
                return View(_DuhDetails);
            }
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult UpdateDUHData(FormCollection formcollection)
        {
            var UiFCQuaterobj = formcollection.AllKeys.Where(i => i.Contains("_FC"));
            var UiActQuaterobj = formcollection.AllKeys.Where(i => i.Contains("_AC"));
            int ProjectId =Convert.ToInt32(formcollection["ProjectID"]);
            string projectname = string.IsNullOrEmpty(formcollection["hdnProjectName"]) ? formcollection["ProjectName"] : formcollection["hdnProjectName"];
            string OpportunityName = string.IsNullOrEmpty(formcollection["hdnOpportunityName"]) ? formcollection["OpportunityName"] : formcollection["hdnOpportunityName"];
            int OpportunityId = Convert.ToInt16(formcollection["OpportunityID"]);
            int DealStageID = 0;
            decimal? TCV;
            decimal? ACV;
            DateTime? BillingStratDate;
            string Note = string.Empty;
            string CreatedBy = string.Empty;
            int SalesForceCastID = 0;
            bool? IsActive = true ;
            long? SalesForceGuid;
            decimal? SowStatusValue;
            bool? IsMapped;
            string OpportunityCategory = string.Empty;
            if (!string.IsNullOrEmpty(formcollection["OpportunityID"].ToString()))
            {
                var opportunityresult = financetoolentities.Opportunities.Where(i => i.OpportunityID == OpportunityId && i.IsUpdated == false).SingleOrDefault();

                var opportunitylatestresult = financetoolentities.Opportunity_Latest
                    .SingleOrDefault(b => b.OpportunityID == OpportunityId && b.IsUpdated == false);
                if (opportunityresult != null)
                {
                    financetoolentities.Usp_UpdateOpportunity(opportunityresult.OpportunityID);
                    TCV = opportunityresult.TCV;
                    ACV = opportunityresult.acv;
                    BillingStratDate = opportunityresult.BillingStratDate;
                    Note = opportunityresult.Note;
                    CreatedBy = opportunityresult.Createdby;
                    SalesForceCastID = opportunityresult.SalesForceCastID;
                    SalesForceGuid = opportunityresult.SalesForceGuid;
                    SowStatusValue = opportunityresult.SowStatusValue;
                    IsMapped = opportunityresult.IsMapped;
                    OpportunityCategory = opportunityresult.OpportunityCategory;
                    if (projectname != "Please Select Project" && !string.IsNullOrEmpty(OpportunityId.ToString()))
                    {
                       // IsActive = false;
                        DealStageID = 4;
                        IsMapped = true;
                    }
                    else
                    {
                       // IsActive = true;
                        DealStageID = Convert.ToInt16(formcollection["dealstageId"]);
                        IsMapped = false;
                    }

                }
                else
                {
                    financetoolentities.Usp_UpdateOpportunityLatest(opportunitylatestresult.OpportunityID);
                    TCV = opportunitylatestresult.TCV;
                    ACV = opportunitylatestresult.acv;
                    BillingStratDate = opportunitylatestresult.BillingStratDate;
                    Note = opportunitylatestresult.Note;
                    CreatedBy = opportunitylatestresult.Createdby;
                    SalesForceCastID = opportunitylatestresult.SalesForceCastID;
                    SalesForceGuid = opportunitylatestresult.SalesForceGuid;
                    SowStatusValue = opportunitylatestresult.SowStatusValue;
                    IsMapped = opportunitylatestresult.IsMapped;
                    OpportunityCategory = opportunitylatestresult.OpportunityCategory;
                    if (projectname != "Please Select Project" && !string.IsNullOrEmpty(OpportunityId.ToString()))
                    {
                        //IsActive = false;
                        DealStageID = 4;
                        IsMapped = true;
                    }
                    else
                    {
                       // IsActive = true;
                        DealStageID = Convert.ToInt16(formcollection["dealstageId"]);
                        IsMapped = false;
                    }
                }                         
               

                financetoolentities.Usp_InsertOpportunityLatest(Convert.ToInt16(formcollection["OpportunityID"]), OpportunityName,
                DealStageID, Convert.ToInt16(formcollection["SowStatus"]), Convert.ToInt16(formcollection["AMID"]),
                Convert.ToInt16(formcollection["selectedcustomerId"]), formcollection["ProgramName"], Convert.ToInt16(formcollection["ServiceLineID"]),
                Convert.ToInt16(formcollection["ProductGroupID"]), Convert.ToInt16(formcollection["DBBLDUID"]), TCV,
                ACV, Convert.ToInt16(formcollection["SowStatus"]), BillingStratDate,
                Note, CreatedBy, System.DateTime.Now, Session["UserName"].ToString(), System.DateTime.Now, SalesForceCastID,
                IsActive, SalesForceGuid, Convert.ToInt16(formcollection["DUID"]), Convert.ToInt16(formcollection["LOBID"]),
              SowStatusValue, Convert.ToInt16(Session["UserId"]), Convert.ToInt16(Session["RoleId"]), IsMapped, false,
              OpportunityCategory);

                financetoolentities.SaveChanges();
            }
            
            var myACTQuaterList = financetoolentities.quater_Actual.Where(i => i.IsActive == true).ToList();
            var myFCQuaterList = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();

            foreach (var quarters in myACTQuaterList)
            {
                var actresult = financetoolentities.ProjectActualByDUHs
                  .Where(i => i.OpportunityID == OpportunityId && i.QuaterID == quarters.QuaterID).Where(i => i.IsNew == true).SingleOrDefault();
               

                if (actresult != null)
                {

                    if (quarters.QuaterID == actresult.QuaterID && !string.IsNullOrEmpty(formcollection[quarters.QuaterName + "_AC"]))
                    {
                        //making old records as o
                        actresult.IsNew = false;
                        financetoolentities.SaveChanges();
                        //Added new records
                        ProjectActualByDUH _projactobj = new ProjectActualByDUH();
                        _projactobj.Createdby = Session["UserName"].ToString();
                        _projactobj.actualvaluebyduh = string.IsNullOrEmpty(formcollection[quarters.QuaterName.ToString() + "_AC"]) == true ? 0 : Convert.ToDecimal(formcollection[quarters.QuaterName.ToString() + "_AC"]);
                        _projactobj.OpportunityID = OpportunityId;
                        _projactobj.Createddate = System.DateTime.Now;
                        _projactobj.Modifiedby = Session["UserName"].ToString();
                        _projactobj.ModifiedDate = System.DateTime.Now;
                        _projactobj.QuaterID = quarters.QuaterID;
                        _projactobj.IsNew = true;
                        financetoolentities.ProjectActualByDUHs.Add(_projactobj);
                        financetoolentities.SaveChanges();
                    }
                }
                else
                {
                    ProjectActualByDUH _projactobj = new ProjectActualByDUH();
                    _projactobj.Createdby = Session["UserName"].ToString();
                    _projactobj.actualvaluebyduh = string.IsNullOrEmpty(formcollection[quarters.QuaterName.ToString() + "_AC"]) == true ? 0 : Convert.ToDecimal(formcollection[quarters.QuaterName.ToString() + "_AC"]);
                    _projactobj.OpportunityID = OpportunityId;
                    _projactobj.Createddate = System.DateTime.Now;
                    _projactobj.Modifiedby = Session["UserName"].ToString();
                    _projactobj.ModifiedDate = System.DateTime.Now;
                    _projactobj.QuaterID = quarters.QuaterID;
                    _projactobj.IsNew = true;
                    financetoolentities.ProjectActualByDUHs.Add(_projactobj);
                    financetoolentities.SaveChanges();
                }
            }
            foreach (var quarters in myFCQuaterList)
            {
                var fcresult = financetoolentities.ProjectForecastByDUHs
                 .Where(i => i.OpportunityID == OpportunityId && i.QuaterID == quarters.QuaterID).Where(i => i.IsNew == true).SingleOrDefault();
                if (fcresult != null)
                {
                    if (quarters.QuaterID == fcresult.QuaterID && !string.IsNullOrEmpty(formcollection[quarters.QuaterName + "_FC"].ToString()))
                    {
                        //Old forecast values are becoming null
                        fcresult.IsNew = false;
                        financetoolentities.SaveChanges();
                        //Adding new records
                        ProjectForecastByDUH _projfcobj = new ProjectForecastByDUH();
                        _projfcobj.Createdby = Session["UserName"].ToString();
                        _projfcobj.forecastvaluebyduh = string.IsNullOrEmpty(formcollection[quarters.QuaterName.ToString() + "_FC"]) == true ? 0 : Convert.ToDecimal(formcollection[quarters.QuaterName.ToString() + "_FC"]);
                        _projfcobj.OpportunityID = OpportunityId;
                        _projfcobj.Createddate = System.DateTime.Now;
                        _projfcobj.Modifiedby = Session["UserName"].ToString();
                        _projfcobj.ModifiedDate = System.DateTime.Now;
                        _projfcobj.QuaterID = quarters.QuaterID;
                        _projfcobj.IsNew = true;
                        financetoolentities.ProjectForecastByDUHs.Add(_projfcobj);
                        financetoolentities.SaveChanges();
                    }
                }
                else
                {
                    ProjectForecastByDUH _projfcobj = new ProjectForecastByDUH();
                    _projfcobj.Createdby = Session["UserName"].ToString();
                    _projfcobj.forecastvaluebyduh = string.IsNullOrEmpty(formcollection[quarters.QuaterName.ToString() + "_FC"]) == true ? 0 : Convert.ToDecimal(formcollection[quarters.QuaterName.ToString() + "_FC"]);
                    _projfcobj.OpportunityID = OpportunityId;
                    _projfcobj.Createddate = System.DateTime.Now;
                    _projfcobj.Modifiedby = Session["UserName"].ToString();
                    _projfcobj.ModifiedDate = System.DateTime.Now;
                    _projfcobj.QuaterID = quarters.QuaterID;
                    _projfcobj.IsNew = true;
                    financetoolentities.ProjectForecastByDUHs.Add(_projfcobj);
                    financetoolentities.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult BindOpportunity(string opportunityName)
        {
            ViewBag.opportunityName = opportunityName;
            return Json(opportunityName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        public List<FinaceTool.Models.CustomerDropdown> BindDropdowncustomerDetails()
        {
            List<FinaceTool.Models.CustomerDropdown> customerList = (from p in financetoolentities.customers.AsEnumerable()
                                                                     select new FinaceTool.Models.CustomerDropdown
                                                                     {
                                                                         CustomerName = p.CustomerName,
                                                                         CustomerID = p.CustomerID
                                                                     }).ToList();
            return customerList;
        }
        public List<SelectListItem> BindDropdownLobDetails()
        {
            List<SelectListItem> lobList = (from p in financetoolentities.LOBs.AsEnumerable()
                                            select new SelectListItem
                                            {
                                                Text = p.LOBName,
                                                Value = p.LOBID.ToString()
                                            }).ToList();
            return lobList;
        }

        public List<SelectListItem> BindDropdownOpportunityDetails()
        {
            List<SelectListItem> lobList = (from opportunity in financetoolentities.Opportunities.AsEnumerable()
                                            select new SelectListItem
                                            {
                                                Text = opportunity.OpportunityName,
                                                Value = opportunity.OpportunityID.ToString()
                                            }).ToList();
            return lobList;
        }
        public List<SelectListItem> BindPoAvailableDetails()
        {
            List<SelectListItem> poAvailable = new List<SelectListItem>();
            poAvailable.Add(new SelectListItem { Text = "Yes", Value = "1" });
            poAvailable.Add(new SelectListItem { Text = "No", Value = "0" });
            return poAvailable;
        }
        public List<SelectListItem> BindDropdownDUDetails()
        {
            List<SelectListItem> duList = (from p in financetoolentities.DUs.AsEnumerable()
                                           select new SelectListItem
                                           {
                                               Text = p.DUName,
                                               Value = p.DUID.ToString()
                                           }).ToList();
            return duList;
        }
        public List<SelectListItem> BindDropdownProjectDetails()
        {
            List<SelectListItem> projectList = (from p in financetoolentities.Projects.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ProjectCode + " : " + p.ProjectName,
                                                    Value = p.ProjectID.ToString()
                                                }).ToList();
            return projectList;
        }
        public List<SelectListItem> BindServiceLineDetails()
        {
            List<SelectListItem> projectList = (from p in financetoolentities.ServiceLines.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ServiceLineName,
                                                    Value = p.ServiceLineID.ToString()
                                                }).ToList();
            return projectList;
        }

        public List<SelectListItem> BindDealStageDetails()
        {
            List<SelectListItem> dealStageList = (from p in financetoolentities.DealStages.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = p.DealStage1,
                                                      Value = p.DealStageID.ToString()
                                                  }).ToList();
            return dealStageList;
        }

        public List<SelectListItem> BindSowStatusDetails()
        {
            List<SelectListItem> sowStatusList = (from p in financetoolentities.SOWStatus.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = p.SOWStatus,
                                                      Value = p.SOWStatusID.ToString()
                                                  }).ToList();
            return sowStatusList;
        }
        public List<SelectListItem> BindAmUserDetails()
        {
            List<SelectListItem> AMList = (from p in financetoolentities.Users.Where(i => i.RoleID == 10).AsEnumerable()
                                           select new SelectListItem
                                           {
                                               Text = p.UserName,
                                               Value = p.UserID.ToString()
                                           }).ToList();
            return AMList;
        }
        public List<SelectListItem> BindDdlProductGroupDetails()
        {
            List<SelectListItem> customerList = (from p in financetoolentities.ProductGroups.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.ProductGroupName,
                                                     Value = p.ProductGroupID.ToString()
                                                 }).ToList();
            return customerList;
        }
        [HttpPost]
        public JsonResult GetBindingdetails(string projectId)
        {
            var values = financetoolentities.Usp_GetAmDetailsbyProjectId(projectId);
            return Json(values, JsonRequestBehavior.AllowGet);
        }

        public List<DUHMainDetailsResult> Display()
        {
            var DUHGriddetails = financetoolentities.Usp_GetDUHMainDetailsByUserID_V2(Session["UserId"].ToString()).Where(i => i.IsUpdated == false).ToList();
            int Iteration = 0;

            List<DUHMainDetailsResult> DUHMainDetailsResultList = new List<DUHMainDetailsResult>();
            foreach (var DUHdbvalues in DUHGriddetails)
            {
                QuaterGenerator.UpdateActiveQuaters();
                DUHMainDetailsResult duhmaindetail = new DUHMainDetailsResult();
                duhmaindetail.AMID = DUHdbvalues.AMID;
                duhmaindetail.AMName = DUHdbvalues.AMName;
                duhmaindetail.CustomerID = DUHdbvalues.CustomerID;
                duhmaindetail.Customername = DUHdbvalues.Customername;
                duhmaindetail.DBBLDU = DUHdbvalues.DBBLDU;
                duhmaindetail.DBBLDUID = DUHdbvalues.DBBLDUID;
                duhmaindetail.DealStageID = DUHdbvalues.DealStageID;
                duhmaindetail.DUID = DUHdbvalues.DUID;
                duhmaindetail.DUName = DUHdbvalues.DUName;
                duhmaindetail.LOBID = DUHdbvalues.LOBID;
                duhmaindetail.LOBName = DUHdbvalues.LOBName;
                duhmaindetail.Note = DUHdbvalues.Note;
                duhmaindetail.OpportunityID = DUHdbvalues.OpportunityID;
                duhmaindetail.OpportunityKeyID = DUHdbvalues.OpportunityKeyID;
                duhmaindetail.OpportunityLobId = DUHdbvalues.OpportunityLobId;
                duhmaindetail.OpportunityLobName = DUHdbvalues.OpportunityLobName;
                duhmaindetail.OpportunityName = DUHdbvalues.OpportunityName;
               // duhmaindetail.poavilable = DUHdbvalues.poavilable;
                duhmaindetail.pobalance = DUHdbvalues.pobalance;
                duhmaindetail.ProductGroup = DUHdbvalues.ProductGroup;
                duhmaindetail.ProductGroupID = DUHdbvalues.ProductGroupID;
                duhmaindetail.ProgramName = DUHdbvalues.ProgramName;
                duhmaindetail.ProjectID = DUHdbvalues.ProjectID;
                duhmaindetail.ProjectName = DUHdbvalues.ProjectName;
                duhmaindetail.SDUID = DUHdbvalues.SDUID;
                duhmaindetail.ServiceLine = DUHdbvalues.ServiceLine;
                duhmaindetail.ServiceLineID = DUHdbvalues.ServiceLineID;
                duhmaindetail.SowStatus = DUHdbvalues.SOWStatus1;
                duhmaindetail.S_No = DUHdbvalues.S_No;
                if (DUHdbvalues.poavilable == "1")
                {
                    duhmaindetail.poavilable = "Yes";
                }
                else if (DUHdbvalues.poavilable == "0")
                {
                    duhmaindetail.poavilable = "No";
                }
                List<string> Objquater = new List<string>();
                List<string> QuaterName = new List<string>();
                List<string> Operations = new List<string>();
                Operations.Add("_FC");
                var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();
                var dbQuater_Actuallist = financetoolentities.quater_Actual.Where(i => i.IsActive == true).ToList();
                foreach (var quater in dbQuaterlist)
                {
                    foreach (var Obj in Operations)
                    {
                        var value = DUHdbvalues.GetType().GetProperty(quater.QuaterName + Obj);
                        var Quartervalue = value.GetValue(DUHdbvalues, null).ToString();
                        var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                        Objquater.Add(qvalue);
                    }
                    QuaterName.Add(quater.QuaterName + "_FC($K)");
                    // QuaterName.Add(quater.QuaterName + "_ACT($K)");
                }
                Operations.Clear();
                Operations.Add("_ACT");
                foreach (var quater in dbQuater_Actuallist)
                {
                    foreach (var Obj in Operations)
                    {
                        var value = DUHdbvalues.GetType().GetProperty(quater.QuaterName + Obj);
                        var Quartervalue = value.GetValue(DUHdbvalues, null).ToString();
                        var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                        Objquater.Add(qvalue);
                    }
                    //QuaterName.Add(quater.QuaterName + "_FC($K)");
                    QuaterName.Add(quater.QuaterName + "_ACT($K)");
                }

                if (Iteration == 0)
                {
                    duhmaindetail.QuaterName = QuaterName;
                }
                duhmaindetail.Quaterlist = Objquater;
                DUHMainDetailsResultList.Add(duhmaindetail);
                Iteration++;
            }
            return DUHMainDetailsResultList;
        }

       
      

        [HttpPost]
        public JsonResult CreateProject(string ProjectName, string ProjectCode, int OpportunityName,int SowStatus,string poavailable,string POBalance,string Note1)
        {

            Project project = new Project();
            var opportunityresult = financetoolentities.Opportunities.Where(i => i.OpportunityID == OpportunityName).FirstOrDefault();
            project.Createddate = System.DateTime.Now;
            project.IsActive = true;
            project.SowStatusID = SowStatus;
            project.OpportunityID = OpportunityName;
            project.Createdby = Session["UserName"].ToString();
            project.Modifiedby = Session["UserName"].ToString();
            project.ProjectName = ProjectName;
            project.poavilable = poavailable;
            project.pobalance = POBalance;
            project.Note = Note1;
            project.ProjectCode = ProjectCode;
            opportunityresult.IsMapped = true;
           // opportunityresult.IsActive = false;
            financetoolentities.SaveChanges();
            financetoolentities.Projects.Add(project);
            financetoolentities.SaveChanges();
            
            return Json(new { response = "Project Created Sucessfully." }, JsonRequestBehavior.AllowGet);
        }

    }
}