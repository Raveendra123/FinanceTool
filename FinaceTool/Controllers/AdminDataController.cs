using FinaceTool.Common;
using FinaceTool.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace FinaceTool.Controllers
{
    [CustomAuthorize(Roles = "14,8,9")]
    public class AdminDataController : Controller
    {
        string startDay = ConfigurationManager.AppSettings["StartDayKey"];
        string endDay = ConfigurationManager.AppSettings["EndDayKey"];
        int startTime = Convert.ToInt32(ConfigurationManager.AppSettings["StartTimeKey"]);
        int endTime = Convert.ToInt32(ConfigurationManager.AppSettings["EndTimeKey"]);
        // GET: AMGrid
        FinanceToolEntities financetoolentities = new FinanceToolEntities();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult DisplayAmDetails()
        {
            QuaterGenerator.UpdateActiveQuaters();
            var AmDetailsModel = financetoolentities.Usp_GetAdminMainDetails_V2().Where(i=>i.IsUpdated == false).ToList();
            List<AdminMainDetailsResult> _AdminMainResult = new List<AdminMainDetailsResult>();
            int Iteration = 0;
            
            foreach (var AmResult in AmDetailsModel)
            {
                AdminMainDetailsResult obj = new AdminMainDetailsResult();
                obj.acv = AmResult.acv;
                obj.AMID = Convert.ToInt16(AmResult.AMID);
                obj.AMName = AmResult.AMName;
                        
               obj.BillingStratDate = AmResult.BillingStratDate;                  
                obj.CustomerID = AmResult.CustomerID;
                obj.Customername = AmResult.Customername;
                obj.DBBLDU = AmResult.DBBLDU;
                obj.DBBLDUID = Convert.ToInt16(AmResult.DBBLDUID);
                obj.DealStagestatus = AmResult.DealStagestatus;
                obj.DUID = AmResult.DUID;
                obj.DUName = AmResult.DUName;
                obj.IsActive = AmResult.IsActive;
                obj.LOBID = Convert.ToInt16(AmResult.LOBID);
                obj.LOBName = AmResult.LOBName;
                obj.note = AmResult.note;
                obj.OpportunityID = Convert.ToInt16(AmResult.OpportunityID);
                obj.OpportunityKeyID = Convert.ToInt16(AmResult.OpportunityKeyID);
                obj.OpportunityLobId = AmResult.OpportunityLobId;
                obj.OpportunityLobName = AmResult.OpportunityLobName;
                obj.OpportunityName = AmResult.OpportunityName;
                obj.ProductGroup = AmResult.ProductGroup;
                obj.ProductGroupID = AmResult.ProductGroupID;
                obj.ProgramName = AmResult.ProgramName;
                obj.ProjectID = Convert.ToInt16(AmResult.ProjectID);
                obj.SDUID = AmResult.SDUID;
                obj.ServiceLine = AmResult.ServiceLine;
                obj.ServiceLineID = AmResult.ServiceLineID;
                obj.SowStatusValue = AmResult.SowStatusValue;
                obj.sowvalue = AmResult.SOWStatus;
                obj.S_No = AmResult.S_No;
                obj.TCV = AmResult.TCV;
                obj.IsUpdated = Convert.ToBoolean(AmResult.IsUpdated);
                List<string> Objquater = new List<string>();
                List<string> QuaterName = new List<string>();
               
                var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();

                foreach (var quater in dbQuaterlist)
                {
                    var value = AmResult.GetType().GetProperty(quater.QuaterName);
                    var Quartervalue = value.GetValue(AmResult, null).ToString();
                    var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                    Objquater.Add(qvalue);
                    QuaterName.Add(quater.QuaterName + "_FC($K)" );
                }
               
                if (Iteration == 0)
                {
                    obj.QuaterName = QuaterName;
                }
                obj.Quaterlist = Objquater;
                _AdminMainResult.Add(obj);
                Iteration++;
            }          
            return View(_AdminMainResult);
        }

        public static string MyCustomFormat(double myNumber)
        {
            var str = (string.Format("{0:0.00}", myNumber));
    return (str.EndsWith(".00") ? str.Substring(0, str.LastIndexOf(".00")) : str);
        }
        public JsonResult GetData(int OpportunityId, int ProjectId)
        {
            var result = financetoolentities.Projects.Where(i => i.OpportunityID == OpportunityId && i.ProjectID == ProjectId).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Edit(Usp_GetAdminMainDetails_Result _amSelctedObj)
        {
            FinaceTool.Models.AMModel _AmDetails = new FinaceTool.Models.AMModel();
            bool result = QuaterGenerator.EditRestrictionByRole(Session["RoleId"].ToString());
            if (result == false)
            {
                TempData["Message"] = "You do not have permissions to Edit in this Time Slab.";
                return View("DisplayAmDetails", Display());
            }
            else
            {

              List<Usp_GetAdminMainDetails_V2_Result> myresult = financetoolentities.Usp_GetAdminMainDetails_V2()
              .Where(i => i.OpportunityID == _amSelctedObj.OpportunityID &&
              i.CustomerID == _amSelctedObj.CustomerID &&
              i.AMID == _amSelctedObj.AMID &&
              i.OpportunityLobId == _amSelctedObj.LOBID && i.DUID == _amSelctedObj.DUID && i.ServiceLineID == _amSelctedObj.ServiceLineID && i.IsUpdated== false)
              .ToList();

                _AmDetails.CustomerList = BindDropdownAMDetails();
                _AmDetails.duList = BindDropdownDUDetails();
                _AmDetails.lobList = BindDropdownLobDetails();
                _AmDetails.ProjectList = BindDropdownProjectDetails();
                _AmDetails.serviceList = BindServiceLineDetails();
                _AmDetails.ProductGroupList = BindDdlProductGroupDetails();
                _AmDetails.DealStageList = BindDealStageDetails();
                _AmDetails.OpportunityCategoryList = BindOpportunityCategoryDetails();
                _AmDetails.SowStatusList = BindSowStatusDetails();
                _AmDetails.AmUserList = BindAmUserDetails();
                //drop down binding ended
                _AmDetails.OpportunityName = myresult[0].OpportunityName;
                _AmDetails.S_No = myresult[0].S_No;
                
                //_AmDetails.acv = myresult[0].acv;
                _AmDetails.AMId = myresult[0].AMID.ToString();
                _AmDetails.AMName = myresult[0].AMName;
                _AmDetails.BillingStratDate = myresult[0].BillingStratDate;
                _AmDetails.CustomerId = myresult[0].CustomerID.ToString();
                _AmDetails.Customername = myresult[0].Customername;
                _AmDetails.DBBLDU = myresult[0].DBBLDU;
                _AmDetails.DBBLDuId = myresult[0].DBBLDUID.ToString();
                _AmDetails.DuId = myresult[0].DUID.ToString();
                _AmDetails.DUName = myresult[0].DUName;
                _AmDetails.LOBName = myresult[0].OpportunityLobName;
                _AmDetails.LobId = myresult[0].OpportunityLobId.ToString();
                _AmDetails.OpportunityID = myresult[0].OpportunityID.ToString();
                _AmDetails.OpportunityKeyID = Convert.ToInt16(myresult[0].OpportunityKeyID);
                if (string.IsNullOrEmpty(myresult[0].ProjectID.ToString()))
                    _AmDetails.ProjectId = "0";
                else
                    _AmDetails.ProjectIdForEdit = myresult[0].ProjectID.ToString();
                _AmDetails.ServiceLine = myresult[0].ServiceLine;
                _AmDetails.ProgramName = myresult[0].ProgramName;
                _AmDetails.ServiceLineId = myresult[0].ServiceLineID.ToString();
                _AmDetails.ProductGroup = myresult[0].ProductGroup;
                _AmDetails.ProductGroupId = myresult[0].ProductGroupID.ToString();
                _AmDetails.sowvalue = myresult[0].sowvalue;
                _AmDetails.DealStageId = myresult[0].DealStage.ToString();
                _AmDetails.acv = Convert.ToDecimal(MyCustomFormat(Convert.ToDouble(myresult[0].acv)));
                _AmDetails.TCV = Convert.ToDecimal(MyCustomFormat(Convert.ToDouble(myresult[0].TCV)));
                
                _AmDetails.BillingStratDate = myresult[0].BillingStratDate;
                _AmDetails.Note_Comment = myresult[0].note;
                _AmDetails.IsUpdated = Convert.ToBoolean(myresult[0].IsUpdated);
                _AmDetails.OpportunityCategory = myresult[0].opportunitycategory;

                List<Quater> QuaterList = new List<Quater>();
                var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();
                foreach (var quater in dbQuaterlist)
                {
                    var value = myresult[0].GetType().GetProperty(quater.QuaterName);
                    var Quartervalue = value.GetValue(myresult[0], null).ToString();
                    var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                    quater.QuaterName = quater.QuaterName + "_FC($K):" + qvalue;
                    QuaterList.Add(quater);
                }
                _AmDetails.Quaterlist = QuaterList;
                _AmDetails.SowStatusValue = Convert.ToDecimal(MyCustomFormat(Convert.ToDouble(myresult[0].SowStatusValue)));
                return View(_AmDetails);
            }
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult UpdateOpportunity(FormCollection frmCollection)
        {
            var UiQuaterobj = frmCollection.AllKeys.Where(i => i.Contains("_FC"));
            int OpportunityId = Convert.ToInt16(frmCollection["OpportunityID"].ToString());
            if (!string.IsNullOrEmpty(frmCollection["OpportunityID"].ToString()))
            {
                var opportunityresult = financetoolentities.Opportunities
                    .SingleOrDefault(b => b.OpportunityID == OpportunityId && b.IsUpdated == false);

                var opportunitylatestresult = financetoolentities.Opportunity_Latest
                    .SingleOrDefault(b => b.OpportunityID == OpportunityId &&  b.IsUpdated == false);

                
                

                if (opportunityresult!= null)
                {
                    financetoolentities.Usp_UpdateOpportunity(opportunityresult.OpportunityID);
                }
                else
                {
                    financetoolentities.Usp_UpdateOpportunityLatest(opportunitylatestresult.OpportunityID);
                }

                AddOldAMDetails(frmCollection);


            }
            return RedirectToAction("DisplayAmDetails");
        }
        int OpportunityLatestInsert = 0;
        int opportunityForecastBySaleInsert = 0;

        //Adding Record into Replica table
        public void AddOldAMDetails(FormCollection _formcollectionobj)
        {
                var QuaterObj = _formcollectionobj.AllKeys.Where(i => i.Contains("_FC"));
                bool IsActive= true;
                int SalesForceCastID = 0;

                //if (Convert.ToInt16(_formcollectionobj["DealStageId"]) == 4 && !string.IsNullOrEmpty(_formcollectionobj["OpportunityName"])
                //   /* && Convert.ToInt16(_formcollectionobj["ProjectId"]) != 0*/)
                //{
                //    IsActive = false;
                //}
                //else
                //{
                //    IsActive = true;
                //}
            
            Random rnd = new Random();
            string rndmvalue = string.Empty;
            for (int x = 0; x <= 15; x++)
            {
                long salesForceCastIDRndm = rnd.Next(1, 999999999);
                var value = financetoolentities.OpportunityForecastBySales.Where(i => i.SalesForceGuid == salesForceCastIDRndm).ToList();
                rndmvalue = Convert.ToString(salesForceCastIDRndm);
                if (value.Count > 0) continue; else break;
            }
            OpportunityLatestInsert = financetoolentities.Usp_InsertOpportunityLatest(Convert.ToInt16(_formcollectionobj["OpportunityID"]), _formcollectionobj["OpportunityName"], Convert.ToInt32(_formcollectionobj["DealStageId"]), Convert.ToInt32(_formcollectionobj["sowvalue"]), Convert.ToInt16(_formcollectionobj["AMId"]), Convert.ToInt16(_formcollectionobj["CustomerId"]), _formcollectionobj["ProgramName"], Convert.ToInt16(_formcollectionobj["ServiceLineId"]), Convert.ToInt16(_formcollectionobj["ProductGroupId"]), Convert.ToInt16(_formcollectionobj["DBBLDuId"]), decimal.Parse(_formcollectionobj["TCV"]), decimal.Parse(_formcollectionobj["acv"]), Convert.ToInt16(_formcollectionobj["sowvalue"]), Convert.ToDateTime(_formcollectionobj["BillingStratDate"]), _formcollectionobj["Note_Comment"], Session["UserName"].ToString(), System.DateTime.Now, Session["UserName"].ToString(), System.DateTime.Now, 0, IsActive,
               long.Parse(rndmvalue), Convert.ToInt16(_formcollectionobj["DuId"]), Convert.ToInt16(_formcollectionobj["LobId"]),
             Convert.ToDecimal(_formcollectionobj["SowStatusValue"]), Convert.ToInt16(Session["UserId"]), Convert.ToInt16(Session["RoleId"]), false,false, _formcollectionobj["OpportunityCategory"]);

                List<Quater> QuaterList = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();

                foreach (var dbquater in QuaterList)
                {
                    foreach (var Quarter in QuaterObj)
                    {
                        if (Quarter == dbquater.QuaterName + "_FC")
                        {
                            OpportunityForecastBySale opportunityForecastBySale = new OpportunityForecastBySale();
                            opportunityForecastBySale.QuaterID = dbquater.QuaterID;
                            opportunityForecastBySale.forecastvaluebysales = string.IsNullOrEmpty(_formcollectionobj[Quarter].ToString()) == true ? 0 : decimal.Parse(_formcollectionobj[Quarter]);
                            opportunityForecastBySale.SalesForceGuid = long.Parse(rndmvalue);
                            opportunityForecastBySale.Createdby = Session["UserName"].ToString();
                            opportunityForecastBySale.Createddate = System.DateTime.Now;
                            opportunityForecastBySale.Modifiedby = Session["UserName"].ToString();
                            opportunityForecastBySale.ModifiedDate = System.DateTime.Now;
                            financetoolentities.OpportunityForecastBySales.Add(opportunityForecastBySale);
                            opportunityForecastBySaleInsert = financetoolentities.SaveChanges();
                            SalesForceCastID = opportunityForecastBySale.SalesForceCastID;
                        }
                    }
                }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreateAdmin()
        {
            FinaceTool.Models.AMModel _AmDetails = new FinaceTool.Models.AMModel();
            _AmDetails.CustomerList = BindDropdownAMDetails();
            _AmDetails.duList = BindDropdownDUDetails();
            _AmDetails.lobList = BindDropdownLobDetails();
            _AmDetails.ProjectList = BindDropdownProjectDetails();
            _AmDetails.serviceList = BindServiceLineDetails();
            _AmDetails.ProductGroupList = BindDdlProductGroupDetails();
            _AmDetails.DealStageList = BindDealStageDetails();
            _AmDetails.SowStatusList = BindSowStatusDetails();
            _AmDetails.AmUserList = BindAmUserDetails();
            _AmDetails.OpportunityCategoryList = BindOpportunityCategoryDetails();
            List<Quater> QuaterList = new List<Quater>();
            var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();
            foreach (var quater in dbQuaterlist)
            {
                quater.QuaterName = quater.QuaterName + "_FC" + " ($K)";
                QuaterList.Add(quater);
            }
            _AmDetails.Quaterlist = QuaterList;
            var AmDetailsModel = financetoolentities.Usp_GetAmMainDetails().OrderByDescending(item => item.S_No).ToList();
            if (AmDetailsModel.Count == 0) _AmDetails.S_No = 1;
            else
                _AmDetails.S_No = AmDetailsModel[0].S_No + 1;
            return View(_AmDetails);
        }

        [HttpPost]
        public ActionResult AddAMDetails(FormCollection _formcollectionobj)
        {
            var QuaterObj = _formcollectionobj.AllKeys.Where(i => i.Contains("txt"));
            int SalesForceCastID = 0;
            Opportunity _Opportunityobj = new Opportunity();
            _Opportunityobj.DealStageID = Convert.ToInt32(_formcollectionobj["DealStageId"]);
            _Opportunityobj.Createddate = System.DateTime.Now;
            _Opportunityobj.acv = decimal.Parse(_formcollectionobj["acv"]);
            _Opportunityobj.AMID = Convert.ToInt16(_formcollectionobj["AMId"] ) ;
            _Opportunityobj.DBBLDUID = string.IsNullOrEmpty(_formcollectionobj["DBBLDuId"]) == true ? 0 : Convert.ToInt16(_formcollectionobj["DBBLDuId"]);
            _Opportunityobj.BillingStratDate = Convert.ToDateTime(_formcollectionobj["BillingStratDate"]);
            _Opportunityobj.OpportunityID = Convert.ToInt16(_formcollectionobj["OpportunityID"]);
            _Opportunityobj.OpportunityName = _formcollectionobj["OpportunityName"];
            _Opportunityobj.ProductGroupID = Convert.ToInt16(_formcollectionobj["ProductGroupId"]);
            _Opportunityobj.ProgramName = _formcollectionobj["ProgramName"];
            _Opportunityobj.CustomerID = Convert.ToInt16(_formcollectionobj["CustomerId"]);
            _Opportunityobj.ServiceLineID = Convert.ToInt16(_formcollectionobj["ServiceLineId"]);
            _Opportunityobj.sowvalue = Convert.ToInt16(_formcollectionobj["SowStatusId"]);
            _Opportunityobj.TCV = decimal.Parse(_formcollectionobj["TCV"]);
            _Opportunityobj.Note = _formcollectionobj["Note_Comment"];
            _Opportunityobj.Createdby = Session["UserName"].ToString(); 
            _Opportunityobj.Createddate = System.DateTime.Now;
            _Opportunityobj.Modifiedby = Session["UserName"].ToString(); 
            _Opportunityobj.ModifiedDate = System.DateTime.Now;
            _Opportunityobj.SowStatusValue = decimal.Parse(_formcollectionobj["SowStatusValue"]);
            _Opportunityobj.OpportunityCategory = _formcollectionobj["OpportunityCategory"];
            //if (Convert.ToInt16(_formcollectionobj["DealStageId"]) == 4 && !string.IsNullOrEmpty(_formcollectionobj["OpportunityName"])
            //    /*&& Convert.ToInt16(_formcollectionobj["ProjectId"]) != 0*/)
            //{
                _Opportunityobj.IsActive = true;
           // }
            //else
            //{
            //    _Opportunityobj.IsActive = true;
            //}
            //if(Convert.ToInt16(_formcollectionobj["ProjectId"]) != 0)
            //{
            //    _Opportunityobj.IsMapped = true;
            //}
            //else
            //{
            //    _Opportunityobj.IsMapped = false;
            //}
            _Opportunityobj.IsMapped = false;
            _Opportunityobj.DUID = Convert.ToInt16(_formcollectionobj["DuId"]);
            _Opportunityobj.LobId = Convert.ToInt16(_formcollectionobj["LobId"]);

            Random rnd = new Random();
            string rndmvalue = string.Empty;
            for (int x = 0; x <= 15; x++)
            {
                long salesForceCastIDRndm = rnd.Next(1, 999999999);
                var value = financetoolentities.OpportunityForecastBySales.Where(i => i.SalesForceCastID == salesForceCastIDRndm).ToList();
                rndmvalue = Convert.ToString(salesForceCastIDRndm);
                if (value.Count > 0) continue; else break;
            }
            List<Quater> QuaterList = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();

            foreach (var dbquater in QuaterList)
            {
                foreach (var Quarter in QuaterObj)
                {
                    if (Quarter.Remove(0, 3).ToString() == dbquater.QuaterName + "_FC ($K)")
                    {
                        OpportunityForecastBySale opportunityForecastBySale = new OpportunityForecastBySale();
                        opportunityForecastBySale.QuaterID = dbquater.QuaterID;
                        opportunityForecastBySale.forecastvaluebysales = string.IsNullOrEmpty(_formcollectionobj[Quarter].ToString()) == true ? 0 : decimal.Parse(_formcollectionobj[Quarter]);
                        opportunityForecastBySale.SalesForceGuid = long.Parse(rndmvalue);
                        opportunityForecastBySale.Createdby = Session["UserName"].ToString();
                        opportunityForecastBySale.Createddate = System.DateTime.Now;
                        opportunityForecastBySale.Modifiedby = Session["UserName"].ToString();
                        opportunityForecastBySale.ModifiedDate = System.DateTime.Now;
                        financetoolentities.OpportunityForecastBySales.Add(opportunityForecastBySale);
                        financetoolentities.SaveChanges();
                        SalesForceCastID = opportunityForecastBySale.SalesForceCastID;
                    }
                }
            }
            _Opportunityobj.SalesForceGuid = long.Parse(rndmvalue);
            _Opportunityobj.SalesForceCastID = Convert.ToInt16(SalesForceCastID);
            financetoolentities.Opportunities.Add(_Opportunityobj);
            financetoolentities.SaveChanges();
            return View("DisplayAmDetails", Display());
        }
        public ActionResult DeleteAMGridById(FinaceTool.Models.AMModel amobj)
        {
            var result = financetoolentities.Opportunities.SingleOrDefault(b => b.OpportunityID.ToString() == amobj.OpportunityID);
            result.IsActive = false;
            financetoolentities.SaveChanges();
            ViewBag.ddlAM = BindDropdownAMDetails();
            return View("DisplayAmDetails", Display());
        }
        public List<SelectListItem> BindDropdownAMDetails()
        {


            List<SelectListItem> customerList = (from p in financetoolentities.customers.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CustomerName,
                                                     Value = p.CustomerID.ToString()
                                                 }).ToList();
            return customerList;
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

            List<SelectListItem> serviceLineList = (from p in financetoolentities.ServiceLines.AsEnumerable()
                                                    select new SelectListItem
                                                    {
                                                        Text = p.ServiceLineName,
                                                        Value = p.ServiceLineID.ToString()
                                                    }).ToList();

            return serviceLineList;
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

        public List<SelectListItem> BindOpportunityCategoryDetails()
        {
            List<SelectListItem> OpportunityCategoryList =
                                                   new List<SelectListItem>()
                                                  {
                                                      new SelectListItem{ Text="Deal Closure", Value = "1" },
                          new SelectListItem{ Text="Ramp Up Pending", Value = "2" } };
                                                  

            return OpportunityCategoryList;
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

            List<SelectListItem> sowStatusList = (from p in financetoolentities.Users.Where(i=>i.RoleID == 10).AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = p.UserName,
                                                      Value = p.UserID.ToString()
                                                  }).ToList();

            return sowStatusList;
        }
        public JsonResult GetAmDetailsbyProjectId(string projectId)
        {
            FinaceTool.Models.AMFilterPidModel AMfiltermodel = new Models.AMFilterPidModel();
            var mylist = financetoolentities.Usp_GetAmDetailsbyProjectId(projectId).SingleOrDefault();
            //var data = mylist[0];
            //if (data != null)
            //{

            //    AMfiltermodel.DealStage = data.DealStageID;
            //    AMfiltermodel.description = data.description;
            //    AMfiltermodel.ProductGroupName = data.ProductGroupName;
            //    AMfiltermodel.ProgramName = data.ProgramName;
            //    AMfiltermodel.ProjectName = data.ProjectName;
            //    AMfiltermodel.ServiceLineName = data.ServiceLineName;
            //    AMfiltermodel.SOWStatus = data.SOWStatus;
            //}
            return Json(mylist, JsonRequestBehavior.AllowGet);
        }


        public List<AdminMainDetailsResult> Display()
        {
            var AmDetailsModel = financetoolentities.Usp_GetAdminMainDetails_V2().Where(i => i.IsUpdated == false).ToList();
            List<AdminMainDetailsResult> _AdminMainResult = new List<AdminMainDetailsResult>();
            int Iteration = 0;
            foreach (var AmResult in AmDetailsModel)
            {
                AdminMainDetailsResult obj = new AdminMainDetailsResult();
                obj.acv = AmResult.acv;
                obj.AMID = AmResult.AMID;
                obj.AMName = AmResult.AMName;
                obj.BillingStratDate = AmResult.BillingStratDate;
                obj.CustomerID = AmResult.CustomerID;
                obj.Customername = AmResult.Customername;
                obj.DBBLDU = AmResult.DBBLDU;
                obj.DBBLDUID = AmResult.DBBLDUID;
                obj.DealStage = AmResult.DealStage;
                obj.DUID = AmResult.DUID;
                obj.DUName = AmResult.DUName;
                obj.IsActive = AmResult.IsActive;
                obj.LOBID = AmResult.LOBID;
                obj.LOBName = AmResult.LOBName;
                obj.note = AmResult.note;
                obj.OpportunityID = AmResult.OpportunityID;
                obj.OpportunityKeyID = AmResult.OpportunityKeyID;
                obj.OpportunityLobId = AmResult.OpportunityLobId;
                obj.OpportunityLobName = AmResult.OpportunityLobName;
                obj.OpportunityName = AmResult.OpportunityName;
                obj.ProductGroup = AmResult.ProductGroup;
                obj.ProductGroupID = AmResult.ProductGroupID;
                obj.ProgramName = AmResult.ProgramName;
                obj.ProjectID =Convert.ToInt16(AmResult.ProjectID);
                obj.SDUID = AmResult.SDUID;
                obj.ServiceLine = AmResult.ServiceLine;
                obj.ServiceLineID = AmResult.ServiceLineID;
                obj.SowStatusValue = AmResult.SowStatusValue;
                obj.sowvalue = AmResult.SOWStatus;
                obj.S_No = AmResult.S_No;
                obj.TCV = AmResult.TCV;
                obj.DealStagestatus = AmResult.DealStagestatus;
                List<string> Objquater = new List<string>();
                List<string> QuaterName = new List<string>();
                var dbQuaterlist = financetoolentities.Quaters.Where(i => i.IsActive == true).ToList();
                foreach (var quater in dbQuaterlist)
                {
                    var value = AmResult.GetType().GetProperty(quater.QuaterName);
                    var Quartervalue = value.GetValue(AmResult, null).ToString();
                    var qvalue = MyCustomFormat(Convert.ToDouble(Quartervalue));
                    Objquater.Add(qvalue);
                    QuaterName.Add(quater.QuaterName + "_FC");
                }
                if (Iteration == 0)
                {
                    obj.QuaterName = QuaterName;
                }
                obj.Quaterlist = Objquater;
                _AdminMainResult.Add(obj);
                Iteration++;
            }
            return _AdminMainResult;
        }

        public ActionResult DUHOpeartions()
        {
            QuaterGenerator.UpdateActiveQuaters();
            var DUHGriddetails = financetoolentities.Usp_GetDUHMainDetails_V2().Where(i => i.IsUpdated == false).ToList();
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
                //duhmaindetail.poavilable = DUHdbvalues.poavilable;
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
                if(DUHdbvalues.poavilable=="1")
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
    }
}