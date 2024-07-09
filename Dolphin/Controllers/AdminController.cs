using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using Dolphin.Filter;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Dolphin.Controllers
{
    public class AdminController : AdminBaseController
    {
        public ActionResult AdminDashBoard()
        {
            DashBoard newdata = new DashBoard();
            try
            {
                DataSet Ds = newdata.GetDetails();
                ViewBag.Associates = Ds.Tables[0].Rows[0]["Associates"].ToString();
                ViewBag.Customers = Ds.Tables[0].Rows[0]["TotalCustomers"].ToString();
                ViewBag.Plots = Ds.Tables[0].Rows[0]["Plots"].ToString();
                ViewBag.TotalBusiness = Ds.Tables[0].Rows[0]["TotalBusiness"].ToString();

                List<DashBoard> lst = new List<DashBoard>();
                DataSet dsblock = newdata.GetBookingDetailsList();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
                        obj.PK_BookingId = r["PK_BookingId"].ToString();
                        obj.BranchID = r["BranchID"].ToString();
                        obj.BranchName = r["BranchName"].ToString();
                        obj.CustomerID = r["CustomerID"].ToString();
                        obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingAmount = r["BookingAmt"].ToString();
                        obj.PaymentPlanID = r["PlanName"].ToString();
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingStatus = r["BookingStatus"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.Amount = r["NetPlotAmount"].ToString();

                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lst.Add(obj);
                    }

                    newdata.List = lst;
                }
                List<DashBoard> lstAssociate = new List<DashBoard>();
                DataSet dsAssociate = newdata.GetAssociateDetails();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
                        // obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.JoiningDate = r["JoiningDate"].ToString();
                        obj.FK_DesignationID = r["FK_DesignationID"].ToString();
                        obj.DesignationName = r["DesignationName"].ToString();
                        obj.ProfilePic = r["ProfilePic"].ToString();

                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstAssociate.Add(obj);
                    }

                    newdata.ListAssociate = lstAssociate;
                }

                List<DashBoard> lstInst = new List<DashBoard>();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();

                        obj.CustomerID = r["Customer"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();
                        obj.IntallmentDate = r["InstallmentDate"].ToString();

                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }
                List<DashBoard> lstSite = new List<DashBoard>();
                DataSet dsSite = newdata.GetBookingDetailsForDashboard();
                if (dsSite != null && dsSite.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsSite.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
                        obj.Site = r["SiteName"].ToString();
                        obj.BookingStatus = r["TotalBooked"].ToString();
                        lstSite.Add(obj);
                    }
                    newdata.ListSite = lstSite;
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }

        //public ActionResult BookingList()

        public ActionResult GetGraphDetails()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGraphDetailsAssociate()
        {
            List<DashBoard> dataList2 = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.BindGraphDetailsAssociate();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList2.Add(details);

                    count++;
                }
            }
            return Json(dataList2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAssociateJoiningDetails()
        {
            List<DashBoard> dataList3 = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetAssociateJoining();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.TotalUser = (dr["TotalUser"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VisitorForm()
        {
            Plot model = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion
            return View();
        }
        //[HttpPost]
        //[ActionName("VisitorForm")]
        //[OnAction(ButtonName = "Save")]
        //public ActionResult SaveVisitorForm(Plot obj)
        //{
        //    try
        //    {
        //        obj.AddedBy = Session["Pk_AdminId"].ToString();
        //        obj.VisiteDate = string.IsNullOrEmpty(obj.VisiteDate) ? null : Common.ConvertToSystemDate(obj.VisiteDate, "dd/MM/yyyy");
        //        DataSet ds = new DataSet();
        //        ds = obj.SaveVisitorDetails();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                TempData["Visitor"] = "Visitor Details saved successfully";
        //            }
        //            else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //            {
        //                TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Visitor"] = ex.Message;
        //    }
        //    return RedirectToAction("VisitorForm", "Admin");
        //}


        [HttpPost]
        public JsonResult save(Plot obj, string dataValue, string SiteID, string AssociateID, string AssociateName, string Amount, string VisiteDate)
        {

            obj.VisitDate = string.IsNullOrEmpty(VisiteDate) ? null : Common.ConvertToSystemDate(VisiteDate, "dd/MM/yyyy");
            bool status = false;
            var isValidModel = TryUpdateModel(obj);
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(dataValue);

            //var serializeData = JsonConvert.DeserializeObject<List<Customer>>(empdata);
            //System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            DataTable VisitorDetails = new DataTable();

            VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            obj.dtVisitorDetails = VisitorDetails;
            obj.AddedBy = Session["Pk_AdminId"].ToString();

            DataSet ds = new DataSet();
            ds = obj.SaveVisitorDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Visitor"] = "Visitor Details saved successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["DrExpenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
        }


        public ActionResult VisitorList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("VisitorList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult VisitorList(Plot model)
        {

            List<Plot> Visitorlist = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Downline = model.IsDownline == true ? "1" : "0";
            DataSet ds = model.VisitorList();
            ViewBag.Total = "0";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_VisitorID"].ToString());
                    obj.AssociateName = r["AssociateName"].ToString();
                    //obj.Mobile = r["Mobile"].ToString();
                    //obj.CustomerName = r["CustomerName"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    //obj.Address = r["Address"].ToString();
                    obj.VisitDate = r["VisitDate"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    Visitorlist.Add(obj);
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());
                }
                model.lstVistor = Visitorlist;
            }
            return View(model);
        }

        public ActionResult PrintVisitor(string Id)
        {
            Plot newdata = new Plot();
            List<Plot> lstvisitor = new List<Plot>();
            newdata.PK_VisitorId = Crypto.Decrypt(Id);
            DataSet ds = newdata.VisitorListById();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.PK_VisitorID = ds.Tables[0].Rows[0]["PK_VisitorID"].ToString();
                ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                ViewBag.VisitDate = ds.Tables[0].Rows[0]["VisitDate"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Plot obj = new Plot();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                        lstvisitor.Add(obj);
                    }
                }
                newdata.lstVistor = lstvisitor;
            }
            return View(newdata);
        }

        public ActionResult AccountHead()
        {
            return View();
        }

        [HttpPost]
        [ActionName("AccountHead")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult AccountHead(Plot model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveAccountHead();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Account head saved successfully !";

                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            FormName = "AccountHead";
            Controller = "Admin";
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult AccountHeadList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            DataSet ds = model.GetAccountHeadDetails();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AccountHeadId = dr["PK_AccountHeadId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Mobile = dr["Mobile"].ToString();
                    obj.Status = dr["Status"].ToString();
                    lst.Add(obj);
                }
                model.lstAccountHead = lst;
            }
            return View(model);
        }

        public ActionResult DeleteAccountHead(string Id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.AccountHeadId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteAccountHead();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Account head deleted successfully !";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            FormName = "AccountHeadList";
            Controller = "Admin";
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult InActivateAccountHead(string Id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.AccountHeadId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.InActivateAccountHead();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Account head inactivated successfully !";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            FormName = "AccountHeadList";
            Controller = "Admin";
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult ActivateAccountHead(string Id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.AccountHeadId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ActivateAccountHead();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Account head activated successfully !";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            FormName = "AccountHeadList";
            Controller = "Admin";
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult DeleteVisitor(string Id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.PK_VisitorId = Crypto.Decrypt(Id);
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteVisitor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["VisitMsg"] = "Visitor Deleted successfully !";
                    }
                    else
                    {
                        TempData["VisitMsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["VisitMsg"] = ex.Message;
            }
            FormName = "VisitorList";
            Controller = "Admin";
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult Testing()
        {
            return View();
        }

    }
}
