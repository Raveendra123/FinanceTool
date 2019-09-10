using FinaceTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FinaceTool.Controllers
{
    public class ComparisonController : Controller
    {
        FinanceToolEntities financetoolentities = new FinanceToolEntities();
        // GET: Comparison
        public ActionResult Index()
        {
            CompareDUHModel objCompareDUHModel = new CompareDUHModel();            
            DUHModel objDUHModel = new DUHModel();
            objDUHModel.SDUList = BindSDUData();
            objDUHModel.DUList = BindDUData(0);
            objDUHModel.ProjectList = BindProjectData();

            objCompareDUHModel.DUHModel = objDUHModel;           
            objCompareDUHModel.DUHMainDetailsResult = Get_SDU_DU_ALL_NewData();
            objCompareDUHModel.OldDUHMainDetailsResult = Get_SDU_DU_ALL_OldData();

            return View(objCompareDUHModel);
        }
      

        public List<SelectListItem> BindSDUData()
        {
            List<SelectListItem> SDUList = new List<SelectListItem>();
            

            SDUList = (from p in financetoolentities.SDUs.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = p.SDUName,
                                                      Value = p.SDUID.ToString()
                                                  }).ToList();
            SDUList.Insert(0, new SelectListItem {
                Text = "ALL",
                Value = "0",
                
            });
            

            return SDUList;
        }

        public List<SelectListItem> BindProjectData()
        {

            List<SelectListItem> projectList = (from p in financetoolentities.Projects.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ProjectCode + " : " + p.ProjectName,
                                                    Value = p.ProjectID.ToString()
                                                }).ToList();
            projectList.Insert(0, new SelectListItem
            {
                Text = "ALL",
                Value = "0",
                Selected=true
            });
            return projectList;
        }
        public List<SelectListItem> BindDUData(int sduid)
        {
            List<SelectListItem> DUList = new List<SelectListItem>();

            if (sduid==0)
            {
                
                DUList = (from p in financetoolentities.DUs.AsEnumerable()
                           select new SelectListItem
                           {
                               Text = p.DUName,
                               Value = p.DUID.ToString()
                           }).ToList();
                DUList.Insert(0, new SelectListItem
                {
                    Text = "ALL",
                    Value = "0",
                    Selected = true
                });
                
            }
            else
            {
               
                DUList = (from p in financetoolentities.DUs.Where(i => i.SDUID == sduid).AsEnumerable()
                           select new SelectListItem
                           {
                               Text = p.DUName,
                               Value = p.DUID.ToString()
                           }).ToList();
                DUList.Insert(0, new SelectListItem
                {
                    Text = "ALL",
                    Value = "0",
                    Selected = true
                });
            }
            return DUList;
         
        }

        public ActionResult GetDUList(string SDUID)
        {
            List<SelectListItem> lstDU = new List<SelectListItem>();
            int SDUId = Convert.ToInt32(SDUID);
            lstDU = BindDUData(SDUId);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstDU);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(FormCollection _formcollectionobj)
        {
            CompareDUHModel objcompareDUHModel = new CompareDUHModel();
            DUHModel objDUHModel = new DUHModel();

            objcompareDUHModel.DUHMainDetailsResult = NewDetails(_formcollectionobj);
            objcompareDUHModel.OldDUHMainDetailsResult = OldDetails(_formcollectionobj);

            objDUHModel.SDUID =Convert.ToInt32(_formcollectionobj["DUHModel.SDUID"]);
            objDUHModel.DUID = Convert.ToInt32(_formcollectionobj["DUHModel.DUID"]);
            objDUHModel.CompareDate =Convert.ToDateTime (_formcollectionobj["DUHModel.CompareDate"]);
            objDUHModel.SDUList = BindSDUData();
            objDUHModel.DUList = BindDUData(Convert.ToInt32(_formcollectionobj["DUHModel.SDUID"]));
            objDUHModel.ProjectList = BindProjectData();

            objcompareDUHModel.DUHModel = objDUHModel;
            
            return View(objcompareDUHModel);

        }
        public static string MyCustomFormat(double myNumber)
        {
            var str = (string.Format("{0:0.00}", myNumber));
            return (str.EndsWith(".00") ? str.Substring(0, str.LastIndexOf(".00")) : str);
        }

        public List<DUHMainDetailsResult> OldDetails(FormCollection _formcollectionobj)
        {
           
            string SDUID = string.Empty;
            string DUID = string.Empty;
            if (_formcollectionobj["DUHModel.SDUID"] == "0")
            {
                SDUID = "ALL";
            }
            else
            {
                SDUID = _formcollectionobj["DUHModel.SDUID"];
            }
            if (_formcollectionobj["DUHModel.DUID"] == "0")
            {
                DUID = "ALL";
            }
            else
            {

                DUID = _formcollectionobj["DUHModel.DUID"];
            }
            QuaterGenerator.UpdateActiveQuaters();
            List<Usp_GetDUHDetails_Comparision_OldDetails_Result> DUHGriddetails = new List<Usp_GetDUHDetails_Comparision_OldDetails_Result>();
            if (_formcollectionobj["DUHModel.ProjectID"]=="0")
            {
                 DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_OldDetails(SDUID, DUID, Convert.ToDateTime(_formcollectionobj["DUHModel.CompareDate"])).ToList();

            }
            else
            {
                 DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_OldDetails(SDUID, DUID, Convert.ToDateTime(_formcollectionobj["DUHModel.CompareDate"])).Where(i => i.ProjectID == Convert.ToInt32(_formcollectionobj["DUHModel.ProjectID"])).ToList();

            }
            //List<Usp_GetDUHDetails_Comparision_OldDetails_Result> DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_OldDetails(SDUID, DUID,Convert.ToDateTime(_formcollectionobj["DUHModel.CompareDate"])).Where(i=>i.ProjectID==Convert.ToInt32(_formcollectionobj["DUHModel.ProjectID"])).ToList();
            // var DUHGriddetails = financetoolentities.Usp_GetDUHMainDetails().ToList();
            int Iteration = 0;
            CompareDUHModel objcompareDUHModel = new CompareDUHModel();
           
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
                duhmaindetail.poavilable = DUHdbvalues.poavilable;
                duhmaindetail.pobalance = DUHdbvalues.pobalance;
                duhmaindetail.ProductGroup = DUHdbvalues.ProductGroup;
                duhmaindetail.ProductGroupID = DUHdbvalues.ProductGroupID;
                duhmaindetail.ProgramName = DUHdbvalues.ProgramName;
                duhmaindetail.ProjectID = DUHdbvalues.ProjectID;
                duhmaindetail.ProjectName = DUHdbvalues.ProjectName;
                duhmaindetail.SDUID = DUHdbvalues.SDUID;
                duhmaindetail.ServiceLine = DUHdbvalues.ServiceLine;
                duhmaindetail.ServiceLineID = DUHdbvalues.ServiceLineID;
                //duhmaindetail.SowStatus = DUHdbvalues.SOWStatus1;
                //duhmaindetail.S_No = DUHdbvalues.S_No;
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

        public List<DUHMainDetailsResult> NewDetails(FormCollection _formcollectionobj)
        {
            string SDUID="ALL" ;
            string DUID="ALL" ;
            if (_formcollectionobj["DUHModel.SDUID"] == "0")
            {
                SDUID = "ALL";
            }
            else
            {
                SDUID = _formcollectionobj["DUHModel.SDUID"];
            }
            if (_formcollectionobj["DUHModel.DUID"] == "0")
            {
                DUID = "ALL";
            }
            else
            {

                DUID = _formcollectionobj["DUHModel.DUID"];
            }
            QuaterGenerator.UpdateActiveQuaters();
            List<Usp_GetDUHDetails_Comparision_V2_Result> DUHGriddetails = new List<Usp_GetDUHDetails_Comparision_V2_Result>();
            if (_formcollectionobj["DUHModel.ProjectID"] == "0")
            {
                DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_V2(SDUID, DUID).Where(i => i.IsUpdated == false).ToList();

            }
            else
            {
                DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_V2(SDUID, DUID).Where(i => i.IsUpdated == false && i.ProjectID == Convert.ToInt32(_formcollectionobj["DUHModel.ProjectID"])).ToList();

            }
           // var DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_V2(SDUID, DUID).Where(i => i.IsUpdated == false).ToList();
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
                duhmaindetail.poavilable = DUHdbvalues.poavilable;
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

        public List<DUHMainDetailsResult> Get_SDU_DU_ALL_NewData()
        {
            
            QuaterGenerator.UpdateActiveQuaters();
            var DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_V2("ALL", "ALL").Where(i => i.IsUpdated == false).ToList();
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
                duhmaindetail.poavilable = DUHdbvalues.poavilable;
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

        public List<DUHMainDetailsResult> Get_SDU_DU_ALL_OldData()
        {
            QuaterGenerator.UpdateActiveQuaters();
            var DUHGriddetails = financetoolentities.Usp_GetDUHDetails_Comparision_OldDetails("ALL", "ALL", DateTime.Now).ToList();
            // var DUHGriddetails = financetoolentities.Usp_GetDUHMainDetails().ToList();
            int Iteration = 0;
            CompareDUHModel objcompareDUHModel = new CompareDUHModel();

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
                duhmaindetail.poavilable = DUHdbvalues.poavilable;
                duhmaindetail.pobalance = DUHdbvalues.pobalance;
                duhmaindetail.ProductGroup = DUHdbvalues.ProductGroup;
                duhmaindetail.ProductGroupID = DUHdbvalues.ProductGroupID;
                duhmaindetail.ProgramName = DUHdbvalues.ProgramName;
                duhmaindetail.ProjectID = DUHdbvalues.ProjectID;
                duhmaindetail.ProjectName = DUHdbvalues.ProjectName;
                duhmaindetail.SDUID = DUHdbvalues.SDUID;
                duhmaindetail.ServiceLine = DUHdbvalues.ServiceLine;
                duhmaindetail.ServiceLineID = DUHdbvalues.ServiceLineID;
                //duhmaindetail.SowStatus = DUHdbvalues.SOWStatus1;
                //duhmaindetail.S_No = DUHdbvalues.S_No;
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
    }
}