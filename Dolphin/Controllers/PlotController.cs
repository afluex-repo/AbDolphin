using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using System.Data;
using Dolphin.Filter;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Dolphin.Controllers
{
    public class PlotController : AdminBaseController
    {
        public ActionResult PlotBooking(string PK_BookingId)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();

                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();
                }
            }
            else
            {
                model.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            model.BranchID = "1";
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion



            #region AccountHead
            int countAccHead = 0;
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            DataSet ds = model.GetAccountHeadDetailsNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countAccHead == 0)
                    {
                        ddlAccountHead.Add(new SelectListItem { Text = "Select Account Head", Value = "0" });
                    }
                    ddlAccountHead.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AccountHeadId"].ToString() });
                    countAccHead = countAccHead + 1;
                }
                //ddlAccountHead.Add(new SelectListItem { Text = "N/A", Value = "0" });
            }
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion

            model.BranchID = "1";
            return View(model);
        }

        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;

                #region GetSiteRate
                //DataSet dsSiteRate = model.GetSiteList();
                //if (dsSiteRate != null)
                //{
                //    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                //    model.Result = "yes";
                //}
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                model.Result = "yes";
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                //#region SitePLCCharge
                //List<Master> lstPlcCharge = new List<Master>();
                //DataSet dsPlcCharge = model.GetPLCChargeList();

                //if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                //{
                //    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.SiteName = r["SiteName"].ToString();
                //        obj.PLCName = r["PLCName"].ToString();
                //        obj.PLCCharge = r["PLCCharge"].ToString();
                //        obj.PLCID = r["PK_PLCID"].ToString();

                //        lstPlcCharge.Add(obj);
                //    }
                //    model.lstPLC = lstPlcCharge;
                //}
                //#endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master model = new Master();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPlot(string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            DataSet dsblock = model.CheckPlotAvailibility();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "no";
                }
                else
                {
                    model.Result = "yes";
                    model.BookingPercent = dsblock.Tables[0].Rows[0]["BookingPercent"].ToString();
                    model.PlotSize = dsblock.Tables[0].Rows[0]["TotalArea"].ToString();
                    model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmount"].ToString();
                    model.TotalPLC = dsblock.Tables[0].Rows[0]["TotalPLC"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult GetCustomerNameFromCustomerID(string CustomerID)
        {
            try
            {
                Plot model = new Plot();

                model.LoginId = CustomerID;

                #region GetCustomerName
                DataSet dsCustomerName = model.GetCustomerName();
                if (dsCustomerName != null && dsCustomerName.Tables[0].Rows.Count > 0)
                {
                    model.CustomerName = dsCustomerName.Tables[0].Rows[0]["Name"].ToString();
                    model.LoginId = dsCustomerName.Tables[0].Rows[0]["LoginId"].ToString();
                    model.AssociateID = dsCustomerName.Tables[0].Rows[0]["AssociateLoginId"].ToString();
                    model.AssociateName = dsCustomerName.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.CustomerID = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetAssociateName(string AssociateID)
        {
            try
            {
                Plot model = new Plot();
                model.LoginId = AssociateID;

                #region GetSiteRate
                DataSet dsAssociateName = model.GetSponsorName();
                if (dsAssociateName != null && dsAssociateName.Tables[0].Rows.Count > 0)
                {
                    model.AssociateName = dsAssociateName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsAssociateName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.AssociateName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("SavePlotBooking")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotBooking(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;
                obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();


                obj.NoOfEMI = obj.NoOfEMI == null ? "0" : obj.NoOfEMI;

                DataSet ds = obj.SavePlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Plot"] = " Plot Booked successfully !";
                        TempData["Booking"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();

                        string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                        string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                        string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        //string Plotamout = obj.PlotAmount;
                        string Plotamout = obj.NetPlotAmount;
                        string TempId = "1707166013299856267";
                        string str = BLSMS.Booking(Bookno, Bookamt, AsstName, plot, Plotamout);
                        try
                        {
                            BLSMS.SendSMS(mob, str, TempId);
                        }
                        catch { }

                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotBookingList(Plot model)
        {

            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            //model.BranchID = "1";
            #endregion

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
            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = model.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PaymentPlanID = model.PaymentPlanID == "0" ? null : model.PaymentPlanID;
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromPlotNumber = model.FromPlotNumber == " " ? null : model.FromPlotNumber;
            model.ToPlotNumber = model.ToPlotNumber == " " ? null : model.ToPlotNumber;

            DataSet ds = model.GetBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.BookingStatus = r["BookingStatus"].ToString();
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
                        obj.BookingNumber = r["BookingNo"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.PlotArea = r["PlotArea"].ToString();
                        obj.PlotAmount = r["PlotAmount"].ToString();
                        obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                        obj.PK_PLCCharge = r["PLCCharge"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.Mobile = r["CustomerMobileNo"].ToString();
                        obj.AllTotalPaidAmount = r["AllTotalPaidAmount"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                    ViewBag.TotalPaidAmount = double.Parse(ds.Tables[0].Compute("sum(AllTotalPaidAmount)", "").ToString()).ToString("n2");
                }
            }
            #region ddlBranch
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = model.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            //model.BranchID = "1";
            #endregion
            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = model.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        //---------------------------------------------------


        public ActionResult DuePayment(Plot model)
        {

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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }


        [HttpPost]
        [ActionName("DuePayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetDuePayment(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetDuePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.BookingStatus = r["BookingStatus"].ToString();
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
                        obj.BookingNumber = r["BookingNo"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.PlotArea = r["PlotArea"].ToString();
                        obj.PlotAmount = r["PlotAmount"].ToString();
                        obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                        obj.PK_PLCCharge = r["PLCCharge"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.DueAmount = r["DueAmount"].ToString();
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;

                    ViewBag.PlotAmount = double.Parse(ds.Tables[0].Compute("sum(PlotAmount)", "").ToString()).ToString("n2");
                    ViewBag.PLCCharge = double.Parse(ds.Tables[0].Compute("sum(PLCCharge)", "").ToString()).ToString("n2");
                    ViewBag.NetPlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");
                    ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                    ViewBag.DueAmount = double.Parse(ds.Tables[0].Compute("sum(DueAmount)", "").ToString()).ToString("n2");

                }
            }
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }


        //----------------------------------------------------------------------------------------------

        public ActionResult PlotBookingListForCancel(Plot model)
        {

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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotBookingListForCancel")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList1(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsList1();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
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
                        obj.BookingNumber = r["BookingNo"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }


        //-----------------------------------------------------------------------------------------------

        [HttpPost]
        [ActionName("SavePlotBooking")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdatePlotBooking(Plot model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (model.TransactionDate == "")
                {
                    model.TransactionDate = null;
                }
                else
                {
                    model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
                }
                model.BookingDate = string.IsNullOrEmpty(model.BookingDate) ? null : Common.ConvertToSystemDate(model.BookingDate, "dd/mm/yyyy");
                model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/mm/yyyy");

                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdatePlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot updated successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public JsonResult CancelPlotBooking(string BookingID, string Remark)
        {
            var response = new { Success = false, Message = "An error occurred while cancelling the booking." };
            //string FormName = "";
            //string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_BookingId = BookingID;
                model.CancelRemark = Remark;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.CancelPlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        response = new { Success = true, Message = "Plot Booking Cancelled successfully!" };
                    }
                    else
                    {
                        response = new { Success = false, Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString() };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new { Success = false, Message = ex.Message };
            }

            //FormName = "PlotBookingListForCancel";
            //Controller = "Plot";
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelledPlotBookingList(Plot model)
        {

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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        [HttpPost]
        [ActionName("CancelledPlotBookingList")]
        [OnAction(ButtonName = "SearchCancelled")]
        public ActionResult GetCancelledBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.GetCancelledBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchName"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateName = r["AssociateInfo"].ToString();
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.CancelRemark = r["CancelRemark"].ToString();
                    obj.CancelDate = r["CancelledDate"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BookingId"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        #region  HoldPlot
        public ActionResult HoldPlot(string PK_PlotHoldID)
        {
            Plot model = new Plot();
            if (PK_PlotHoldID != null)
            {


                model.PK_PlotHoldID = PK_PlotHoldID;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_PlotHoldID;
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();

                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    model.HoldFrom = dsBookingDetails.Tables[0].Rows[0]["HoldFrom"].ToString();
                    model.HoldTo = dsBookingDetails.Tables[0].Rows[0]["HoldTo"].ToString();
                    model.Name = dsBookingDetails.Tables[0].Rows[0]["Name"].ToString();
                    model.Mobile = dsBookingDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Remark = dsBookingDetails.Tables[0].Rows[0]["Remark1"].ToString();
                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            return View(model);
        }

        [HttpPost]
        [ActionName("SavePlotHold")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotHold(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " Plot is on Hold !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "HoldPlot";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotHoldList(Plot model)
        {
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        [HttpPost]
        [ActionName("PlotHoldList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPlotHoldList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.GetPlotHoldList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_PlotHoldID = r["PK_PlotHoldID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotHoldID"].ToString());
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.HoldFrom = r["HoldFrom"].ToString();
                    obj.HoldTo = r["HoldTo"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Mobile = r["Mobile"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
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

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = model.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            return View(model);
            #endregion
        }



        public ActionResult DeletePlotHold(string PK_PlotHoldID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_PlotHoldID = PK_PlotHoldID;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeletePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Deleted successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotHoldList";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult PrintPlotHold(string id)
        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(id);
            newdata.PK_PlotHoldID = Crypto.Decrypt(id);
            //  ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.GetPlotHoldList();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.PK_PlotHoldID = ds.Tables[0].Rows[0]["PK_PlotHoldID"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["Name"].ToString();

                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.PlotNo = ds.Tables[0].Rows[0]["PlotNumber"].ToString();

                    //  ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.HoldAmount = ds.Tables[0].Rows[0]["HoldAmount"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.Amountwords = ds.Tables[0].Rows[0]["AmountInWords"].ToString();



                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = SoftwareDetails.Pin1;
                    ViewBag.State1 = SoftwareDetails.State1;
                    ViewBag.City1 = SoftwareDetails.City1;
                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
                    ViewBag.LandLine = SoftwareDetails.LandLine;
                    ViewBag.Website = SoftwareDetails.Website;
                    ViewBag.EmailID = SoftwareDetails.EmailID;
                }
            }

            return View(newdata);
        }
        #endregion

        #region  Plot Allotment
        public ActionResult PlotAllotment(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion




            #region AccountHead
            int countAccHead = 0;
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            DataSet ds = model.GetAccountHeadDetailsNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countAccHead == 0)
                    {
                        ddlAccountHead.Add(new SelectListItem { Text = "Select Account Head", Value = "0" });
                    }
                    ddlAccountHead.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AccountHeadId"].ToString() });
                    countAccHead = countAccHead + 1;
                }
                //ddlAccountHead.Add(new SelectListItem { Text = "N/A", Value = "0" });
            }
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion





            return View(model);
        }


        public ActionResult PlotBookingDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("SavePlotAllotment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotAllotment(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.PaymentDate) ? null : obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SavePlotAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " Plot Allotted successfully !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string amt = obj.PaidAmount;
                        string TempId = "1707166036748099409";
                        string str = BLSMS.PlotAllotment(name, Plot, amt);
                        try
                        {
                            BLSMS.SendSMS(mob, str, TempId);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotAllotment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region EMIPayment

        public ActionResult EMIPayment(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region AccountHead
            int countAccHead = 0;
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            DataSet ds = model.GetAccountHeadDetailsNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countAccHead == 0)
                    {
                        ddlAccountHead.Add(new SelectListItem { Text = "Select Account Head", Value = "0" });
                    }
                    ddlAccountHead.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AccountHeadId"].ToString() });
                    countAccHead = countAccHead + 1;
                }
                //ddlAccountHead.Add(new SelectListItem { Text = "N/A", Value = "0" });
            }
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion


            return View(model);
        }

        public ActionResult Details(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetailsForEmi();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    model.Result = "yes";
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsblock.Tables[1].Rows)
                        {
                            Plot obj = new Plot();
                            obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                            obj.PK_BookingId = r["Fk_BookingId"].ToString();
                            obj.InstallmentNo = r["InstallmentNo"].ToString();
                            obj.InstallmentDate = r["InstallmentDate"].ToString();
                            obj.PaymentDate = r["PaymentDate"].ToString();
                            obj.PaidAmount = r["PaidAmount"].ToString();
                            obj.InstallmentAmount = r["InstAmt"].ToString();
                            obj.PaymentMode = r["PaymentModeName"].ToString();
                            obj.DueAmount = r["DueAmount"].ToString();
                            obj.CssClass = r["CssClass"].ToString();

                            lst.Add(obj);
                        }
                        model.lstPlot = lst;
                    }

                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SaveEMI")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEMI(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.PaymentDate) ? null : obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEMIPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " EMI Paid !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string instno = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                        string amt = obj.PaidAmount;
                        string TempId = "1707166036762581861";

                        string str = BLSMS.EMIPayment(name, Plot, bookno, instno, amt);
                        try
                        {
                            BLSMS.SendSMS(mob, str, TempId);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "EMIPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region  ChequNeftCash

        public ActionResult Payment(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            List<SelectListItem> ddlSite = new List<SelectListItem>();
            ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            ViewBag.ddlSite = ddlSite;

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        [HttpPost]
        [ActionName("Payment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(Plot model)
        {

            List<Plot> lst = new List<Plot>();
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
            model.PlotNumber = model.PlotNumber == " " ? null : model.PlotNumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");


            DataSet ds = model.GetPaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentRemarks = r["PaymentRemarks"].ToString();
                    obj.ReceiverBank = r["ReceiverBank"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.AccountHeadName = r["AccountHeadName"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region ddlBranch
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = model.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            List<SelectListItem> ddlSite = new List<SelectListItem>();
            ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            ViewBag.ddlSite = ddlSite;

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }


        [HttpPost]
        [ActionName("Payment")]
        [OnAction(ButtonName = "btnapprove")]
        public ActionResult SaveApprove(Plot model)
        {

            string ctrRowCount = Request["hdRows"].ToString();
            string chk = "";
            string Remarks = "";
            string PK_BookingDetailsId = "";
            int Id = 0;
            DataTable dtpayment = new DataTable();
            dtpayment.Columns.Add("Id");
            dtpayment.Columns.Add("PK_BookingDetailsId");
            dtpayment.Columns.Add("Remarks");
            for (int i = 1; i < int.Parse(ctrRowCount); i++)
            {
                chk = Request["chkpayment_" + i];
                if (chk == "on")
                {
                    Id = dtpayment.Rows.Count + 1;
                    PK_BookingDetailsId = Request["PK_BookingDetailsId_" + i].ToString();
                    Remarks = Request["txtDesc_" + i].ToString();
                    dtpayment.Rows.Add(Id, PK_BookingDetailsId, Remarks);
                }
            }
            model.dtTable = dtpayment;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            model.ApprovedDate = string.IsNullOrEmpty(model.ApprovedDate) ? null : Common.ConvertToSystemDate(model.ApprovedDate, "dd/MM/yyyy");
            DataSet ds = model.ApprovePaymentNew();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Plot"] = "Payment Approved successfully !";
                }
                else
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            List<SelectListItem> ddlSite = new List<SelectListItem>();
            ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            ViewBag.ddlSite = ddlSite;

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion
            return RedirectToAction("Payment", "Plot");
        }
        [HttpPost]
        [ActionName("Payment")]
        [OnAction(ButtonName = "btnreject")]
        public ActionResult MultipleReject(Plot model)
        {

            string ctrRowCount = Request["hdRows"].ToString();
            string chk = "";
            string Remarks = "";
            string PK_BookingDetailsId = "";
            int Id = 0;
            DataTable dtpayment = new DataTable();
            dtpayment.Columns.Add("Id");
            dtpayment.Columns.Add("PK_BookingDetailsId");
            dtpayment.Columns.Add("Remarks");
            for (int i = 1; i < int.Parse(ctrRowCount); i++)
            {
                chk = Request["chkpayment_" + i];
                if (chk == "on")
                {
                    Id = dtpayment.Rows.Count + 1;
                    PK_BookingDetailsId = Request["PK_BookingDetailsId_" + i].ToString();
                    Remarks = Request["txtDesc_" + i].ToString();
                    dtpayment.Rows.Add(Id, PK_BookingDetailsId, Remarks);
                }

            }
            model.dtTable = dtpayment;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            model.ApprovedDate = string.IsNullOrEmpty(model.ApprovedDate) ? null : Common.ConvertToSystemDate(model.ApprovedDate, "dd/MM/yyyy");
            DataSet ds = model.MultipleReject();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Plot"] = "Payment Rejected successfully !";
                }
                else
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }


            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            List<SelectListItem> ddlSite = new List<SelectListItem>();
            ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            ViewBag.ddlSite = ddlSite;

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion
            return View(model);
        }
        public ActionResult ApprovePayment(string UserID, string Description, string ApprovedDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                DataSet ds = model.ApprovePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Approved successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "Payment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);

        }
        public ActionResult RejectPayment(string UserID, string Description, string ApprovedDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.RejectPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Rejected  !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "Payment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region PaymentApproval/RejectionReport

        public ActionResult PaymentReport(Plot model)
        {
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("PaymentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.ApprovedFromDate = string.IsNullOrEmpty(model.ApprovedFromDate) ? null : Common.ConvertToSystemDate(model.ApprovedFromDate, "dd/MM/yyyy");
            model.ApprovedToDate = string.IsNullOrEmpty(model.ApprovedToDate) ? null : Common.ConvertToSystemDate(model.ApprovedToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPaymentReportList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    ViewBag.TotalAmount = ds.Tables[0].Rows[0]["Total2"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.ApprovedBy = r["UpdatedByName"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    //  obj.RejectedDate = r["RejectedDate"].ToString();
                    obj.ApproveDescription = r["Description"].ToString();
                    //  obj.RejectDescription = r["RejectDescription"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }


        #endregion 

        #region ApproveRejectedPayment

        public ActionResult ApproveRejectedPayment(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("ApproveRejectedPayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetListOfRejectedPayment(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.AccountHeadName = r["AccountHeadName"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region ddlBranch
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = model.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            //model.BranchID = "1";
            #endregion
            return View(model);
        }

        public ActionResult ApproveRejPayment(string UserID, string Description, string ApprovedDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ApproveRejectPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Approved successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "ApproveRejectedPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion 

        #region RejectPaymentApproveReport

        public ActionResult RejectPaymentApproveReport(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("RejectPaymentApproveReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetRejectPaymentApproveReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();

            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPaymentRejAppReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    // obj.ApproveDescription = r["ApproveDescription"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    //   obj.RejectedDate = r["RejectedDate"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }


        #endregion 

        #region TransferBooking

        public ActionResult TransferPlotBooking(string PK_BookingId)
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);



        }

        [HttpPost]
        [OnAction(ButtonName = "Transfer")]
        [ActionName("TransferPlotBooking")]
        public ActionResult TransferPlot(Plot model)
        {

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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


            model.PK_BookingId = model.PK_BookingId;
            model.PK_BookingDetailsId = model.PK_BookingDetailsId;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.TransferPlot();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["TransferPlotBooking"] = "Plot Transfered";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["TransferPlotBooking"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return View(model);



        }
        public ActionResult BookingDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            //  DataSet dsBookingDetails = model.GetBookingDetailsList();


            //model.SiteID = SiteID;
            //model.SectorID = SectorID;
            //model.BlockID = BlockID;
            //model.PlotNumber = PlotNumber;
            //model.BookingNumber = BookingNumber;
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            DataSet dd = model.FillBookedPlotDetails();
            if (dd != null && dd.Tables[0].Rows.Count > 0)
            {

                if (dd.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    model.PK_PlotID = dd.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dd.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dd.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dd.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dd.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dd.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dd.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PK_BookingDetailsId = dd.Tables[0].Rows[0]["PK_BookingDetailsId"].ToString();
                    model.PaymentDate = dd.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dd.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dd.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dd.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dd.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dd.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dd.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dd.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dd.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dd.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dd.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dd.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dd.Tables[0].Rows[0]["BalanceAmount"].ToString();
                }
                else
                {
                    model.Result = dd.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {

            }
            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = objmaster.GetSiteList();
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

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = objmaster.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion
            return Json(model, JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region SaveRowHouseBooking
        public ActionResult RowHouseBooking(string RowHouseBookingID)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (RowHouseBookingID != null)
            {
                model.RowHouseBookingID = RowHouseBookingID;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.RowHouseBookingID = RowHouseBookingID;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();

                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    //  model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();

                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    //  model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    //  model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    //   model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    //    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    //     model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();
                }
            }
            else
            {
                model.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");


            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            List<SelectListItem> ddlPlotArea = new List<SelectListItem>();
            ddlPlotArea.Add(new SelectListItem { Text = "Select Plot Area", Value = "0" });
            ViewBag.ddlPlotArea = ddlPlotArea;
            model.BranchID = "1";
            return View(model);
        }

        public ActionResult GetRateandPLC(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;


                #region GetPlotArea
                List<SelectListItem> ddlPlotArea = new List<SelectListItem>();
                model.Result = "1";
                DataSet dsSector = model.GetRateandPLC();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlPlotArea.Add(new SelectListItem { Text = r["Area"].ToString(), Value = r["PK_RowHouseSizeID"].ToString() });

                    }
                }
                model.ddlPlotArea = ddlPlotArea;
                #endregion

                #region

                List<Plot> lst = new List<Plot>();


                if (dsSector != null && dsSector.Tables.Count > 0 && dsSector.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[1].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_PLCCharge = r["PK_PLCCharge"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.TotalPLC = r["PLCCharge"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                    #endregion
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }


        public ActionResult GetRate(string PlotArea)
        {

            Plot model = new Plot();
            model.PlotArea = PlotArea;


            DataSet dsSiteRate = model.GetRate();
            if (dsSiteRate != null)
            {
                model.PlotRate = dsSiteRate.Tables[0].Rows[0]["Price"].ToString();
                model.BookingAmount = dsSiteRate.Tables[0].Rows[0]["BookingAmount"].ToString();
                model.BookingPercent = dsSiteRate.Tables[0].Rows[0]["BookingPercentage"].ToString();
                model.Result = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("RowHouseBooking")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveRowHouse(Plot obj)
        {
            string FormName = "";
            string Controller = "";



            obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;

            obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");
            obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
            obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
            obj.PlotArea = string.IsNullOrEmpty(obj.PlotArea) ? "0" : obj.PlotArea;
            obj.GroundFloorArea = string.IsNullOrEmpty(obj.GroundFloorArea) ? "0" : obj.GroundFloorArea;
            obj.FirstFloorArea = string.IsNullOrEmpty(obj.FirstFloorArea) ? "0" : obj.FirstFloorArea;

            try
            {

                string noofrows = Request["hdrows"].ToString();
                string plc = "";
                string chk = "";
                string plccharge = "";
                DataTable dtst = new DataTable();

                dtst.Columns.Add("FK_PLCID", typeof(string));
                dtst.Columns.Add("PLCCharge", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    plccharge = Request["plccharge_" + i].ToString();
                    if (plccharge != "0" && plccharge != "")
                    {
                        plc = Request["Pk_Plcid_" + i].ToString();
                        plccharge = Request["plccharge_" + i].ToString();
                        dtst.Rows.Add(plc, plccharge);
                    }


                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtTable = dtst;
                obj.NoOfEMI = obj.NoOfEMI == null ? "0" : obj.NoOfEMI;
                DataSet ds = obj.SaveRowHouse();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["RowHouse"] = " Row House Saved successfully !";
                        TempData["Booking"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();


                        string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                        string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                        string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string TempId = "1707166036770779741";
                        string str = BLSMS.HouseBooking(Bookno, Bookamt, AsstName, plot);
                        try
                        {
                            BLSMS.SendSMS(mob, str, TempId);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["RowHouse"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["RowHouse"] = ex.Message;
            }
            FormName = "RowHouseBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region UploadPlot/HouseDocument

        public ActionResult UploadPlotDocument(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }

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

            return View(model);
        }


        public ActionResult PlotBookingDetailsForDocument(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.PlotBookingDetailsForDocument();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("UploadPlotDocument")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveUploadPlotDocument(HttpPostedFileBase postedFile, Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    obj.DocumentFile = "/PlotHouseDocument/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.DocumentFile)));

                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveUploadPlotDocument();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Doc"] = " Document Uploaded successfully !";

                    }
                    else
                    {
                        TempData["Doc"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Doc"] = ex.Message;
            }
            FormName = "UploadPlotDocument";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region EMICalculator 
        public ActionResult EMICalculator(Plot obj)
        {
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;



            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                //    obj.Months = dsPlan.Tables[1].Rows[0]["Months"].ToString();

                foreach (DataRow r in dsPlan.Tables[1].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion
            return View();
        }
        public ActionResult GetMonths(string PaymentPlanID)
        {
            try
            {
                Plot model = new Plot();
                model.PaymentPlanID = PaymentPlanID;

                DataSet dsSector = model.GetPaymentPlanList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    model.Months = dsSector.Tables[1].Rows[0]["Months"].ToString();
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion


        #region CancleBookingDetails
        //[HttpPost]
        //[ActionName("CancleBookingDetails")]
        //[OnAction(ButtonName = "SearchCancel")]
        public ActionResult CancleBookingDetails(string BookingID)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.FK_BookingId = Crypto.Decrypt(BookingID);
            DataSet ds = model.GetCancleBookingDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();

                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.CancelDate = r["CancelledDate"].ToString();
                    obj.CancelRemark = r["CancelRemark"].ToString();
                    obj.PaymentRemarks = r["PaymentRemarks"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        #endregion



        #region Update PaymentPlan
        public ActionResult UpdatePaymentPlan(Plot model, string PK_BookingId, string PaymentPlanID)
        {

            //Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {


                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            #region ddlPlan
            int count2 = 0;
            Plot obj1 = new Plot();
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = model.GetUpdateNewPaymentPlan();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Payment Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion
            return View(model);
        }


        public ActionResult UpdatePaymentPlanDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }

            #region ddlPlan
            int count2 = 0;
            Plot obj1 = new Plot();
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = model.GetUpdateNewPaymentPlan();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Payment Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("UpdatePaymentPlan")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdatePaymentPlan(Plot obj, string PK_BookingId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.PK_BookingId = PK_BookingId;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdatePaymentPlan();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Plan Updated successfully !";
                        //string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        //string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        //string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        //string amt = obj.PaidAmount;
                        //string TempId = "1707166036748099409";
                        //string str = BLSMS.PlotAllotment(name, Plot, amt);
                        //try
                        //{
                        //    BLSMS.SendSMS(mob, str, TempId);
                        //}
                        //catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "UpdatePaymentPlan";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        public ActionResult ReturnCancelledPlotPayment(string PK_BookingId)
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region AccountHead
            int countAccHead = 0;
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            DataSet ds = model.GetAccountHeadDetailsNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countAccHead == 0)
                    {
                        ddlAccountHead.Add(new SelectListItem { Text = "Select Account Head", Value = "0" });
                    }
                    ddlAccountHead.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AccountHeadId"].ToString() });
                    countAccHead = countAccHead + 1;
                }
                //ddlAccountHead.Add(new SelectListItem { Text = "N/A", Value = "0" });
            }
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }


        public ActionResult ReturnCancelledPlotPaymentDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.GetReturnCancelledPlotPaymentDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    model.Result = "yes";
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();

                    model.ReturnPaidAmount = dsblock.Tables[0].Rows[0]["ReturnPaidAmount"].ToString();

                    model.BalanceReturnPaidAmount = dsblock.Tables[0].Rows[0]["BalanceReturnPaidAmount"].ToString();


                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                    model.CancelDate = dsblock.Tables[0].Rows[0]["CancelledDate"].ToString();


                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("ReturnCancelledPlotPayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveReturnCancelledPlotPayment(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveReturnCancelledPlotPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ReturnCancelledPlot"] = " Return Cancelled Plot Payment Request sent successfully !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string amt = obj.PaidAmount;
                        if (ds.Tables[0].Rows[0]["PK_ReturnPaymentId"].ToString() != "")
                        {
                            TempData["CancelledPartPaymentSucesssMessage"] = "Return Cancelled Plot Payment Request sent successfully !";
                            Session["PartPaymentId"] = Crypto.Encrypt(ds.Tables[0].Rows[0]["PK_ReturnPaymentId"].ToString());
                        }
                        try
                        {
                            // BLSMS.SendSMSNew(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["ReturnCancelledPlot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ReturnCancelledPlot"] = ex.Message;
            }
            FormName = "ReturnCancelledPlotPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }


        #region ReturnPaymentCancelledPlot

        public ActionResult ApprovalReturnPaymentCancelledPlot(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            return View(model);
        }


        [HttpPost]
        [ActionName("ApprovalReturnPaymentCancelledPlot")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetReturnCancelledPlotPaymentList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetReturnCancelledPlotPaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_ReturnPaymentId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();

                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();

                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentRemarks = r["PaymentRemarks"].ToString();
                    obj.ReceiverBank = r["ReceiverBank"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.AccountHeadName = r["AccountHeadName"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            return View(model);
        }





        [HttpPost]
        [ActionName("ApprovalReturnPaymentCancelledPlot")]
        [OnAction(ButtonName = "btnapprovereturn")]
        public ActionResult SaveApprovePaymentForReturnCancelled(Plot model)
        {

            string ctrRowCount = Request["hdRows"].ToString();
            string chk = "";
            string Remarks = "";
            string PK_ReturnPaymentId = "";
            int Id = 0;
            DataTable dtAppreturnpayment = new DataTable();
            dtAppreturnpayment.Columns.Add("Id");
            dtAppreturnpayment.Columns.Add("PK_ReturnPaymentId");
            dtAppreturnpayment.Columns.Add("Remarks");
            for (int i = 1; i < int.Parse(ctrRowCount); i++)
            {
                chk = Request["chkpayment_" + i];
                if (chk == "on")
                {
                    Id = dtAppreturnpayment.Rows.Count + 1;
                    PK_ReturnPaymentId = Request["PK_ReturnPaymentId_" + i].ToString();
                    Remarks = Request["txtDesc_" + i].ToString();
                    dtAppreturnpayment.Rows.Add(Id, PK_ReturnPaymentId, Remarks);
                }
            }
            model.dtTable = dtAppreturnpayment;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            model.ApprovedDate = string.IsNullOrEmpty(model.ApprovedDate) ? null : Common.ConvertToSystemDate(model.ApprovedDate, "dd/MM/yyyy");
            DataSet ds = model.ApprovePaymentForReturnCancelledplot();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Plot"] = "Return Payment Approved successfully !";
                }
                else
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }


        [HttpPost]
        [ActionName("ApprovalReturnPaymentCancelledPlot")]
        [OnAction(ButtonName = "btnrejectreturn")]
        public ActionResult RejectPaymentForReturnCancelled(Plot model)
        {

            string ctrRowCount = Request["hdRows"].ToString();
            string chk = "";
            string Remarks = "";
            string PK_ReturnPaymentId = "";
            int Id = 0;
            DataTable dtAppreturnpayment = new DataTable();
            dtAppreturnpayment.Columns.Add("Id");
            dtAppreturnpayment.Columns.Add("PK_ReturnPaymentId");
            dtAppreturnpayment.Columns.Add("Remarks");
            for (int i = 1; i < int.Parse(ctrRowCount); i++)
            {
                chk = Request["chkpayment_" + i];
                if (chk == "on")
                {
                    Id = dtAppreturnpayment.Rows.Count + 1;
                    PK_ReturnPaymentId = Request["PK_ReturnPaymentId_" + i].ToString();
                    Remarks = Request["txtDesc_" + i].ToString();
                    dtAppreturnpayment.Rows.Add(Id, PK_ReturnPaymentId, Remarks);
                }

            }
            model.dtTable = dtAppreturnpayment;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            model.ApprovedDate = string.IsNullOrEmpty(model.ApprovedDate) ? null : Common.ConvertToSystemDate(model.ApprovedDate, "dd/MM/yyyy");
            DataSet ds = model.RejectPaymentForReturnCancelled();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Plot"] = "Return Payment Rejected successfully !";
                }
                else
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "All Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion
            return View(model);
        }




        public ActionResult ReturnPlotPaymentLedger(Plot model)
        {
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("ReturnPlotPaymentLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ReturnPlotPaymentLedgerDetails(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.ApprovedFromDate = string.IsNullOrEmpty(model.ApprovedFromDate) ? null : Common.ConvertToSystemDate(model.ApprovedFromDate, "dd/MM/yyyy");
            //model.ApprovedToDate = string.IsNullOrEmpty(model.ApprovedToDate) ? null : Common.ConvertToSystemDate(model.ApprovedToDate, "dd/MM/yyyy");

            DataSet ds = model.GetReturnPlotPaymentLedger();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_ReturnPaymentId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    //ViewBag.TotalAmount = ds.Tables[0].Rows[0]["Total2"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    //  obj.RejectedDate = r["RejectedDate"].ToString();
                    obj.ApproveDescription = r["Description"].ToString();
                    //  obj.RejectDescription = r["RejectDescription"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;


                ViewBag.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
            }
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }
        #endregion
        public ActionResult PendingPaymentReport(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("PendingPaymentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPendingPaymentReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.ApprovedFromDate = string.IsNullOrEmpty(model.ApprovedFromDate) ? null : Common.ConvertToSystemDate(model.ApprovedFromDate, "dd/MM/yyyy");
            model.ApprovedToDate = string.IsNullOrEmpty(model.ApprovedToDate) ? null : Common.ConvertToSystemDate(model.ApprovedToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPendingPaymentReportList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.PK_PaymentID = r["PaymentModeID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    ViewBag.TotalAmount = ds.Tables[0].Rows[0]["Total2"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    //  obj.RejectedDate = r["RejectedDate"].ToString();
                    obj.ApproveDescription = r["Description"].ToString();
                    //  obj.RejectDescription = r["RejectDescription"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            return View(model);
        }

        public ActionResult UpdatePaymentMode(string PK_BookingDetailsId, string PaymentMode, string TransactionNumber, string TransactionDate, string BankName, string BankBranch, string Remark)
        {
            Plot model = new Plot();
            try
            {
                model.UpdatedBy = Session["PK_AdminID"].ToString();
                model.PK_BookingDetailsId = PK_BookingDetailsId;
                model.PaymentMode = PaymentMode;
                model.TransactionNumber = TransactionNumber;
                model.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? "" : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");
                model.BankName = BankName;
                model.BankBranch = BankBranch;
                model.Remark = Remark;
                DataSet dd = model.UpdatePaymentMode();
                if (dd != null && dd.Tables[0].Rows.Count > 0)
                {

                    if (dd.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "yes";
                    }
                    else if (dd.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = dd.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    model.Result = dd.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        public ActionResult UpdateBookingDetails(string PK_BookingId)
        {
            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    ViewBag.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    ViewBag.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    ViewBag.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    ViewBag.BookingNo = dsBookingDetails.Tables[0].Rows[0]["BookingNo"].ToString();
                    ViewBag.PlanName = dsBookingDetails.Tables[0].Rows[0]["PlanName"].ToString();
                    ViewBag.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    ViewBag.BranchName = dsBookingDetails.Tables[0].Rows[0]["BranchName"].ToString();
                    ViewBag.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    ViewBag.Contact = dsBookingDetails.Tables[0].Rows[0]["CustomerMobileNo"].ToString();
                    ViewBag.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    ViewBag.PlotInfo = dsBookingDetails.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    ViewBag.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    ViewBag.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    ViewBag.PlotArea = dsBookingDetails.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.PlotSize = dsBookingDetails.Tables[0].Rows[0]["TotalArea"].ToString();
                    ViewBag.NetPlotAmount = dsBookingDetails.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    ViewBag.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    ViewBag.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    ViewBag.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();

                }
            }
            return View(model);
        }

        public ActionResult EditBookingDetails(Plot model, string PK_BookingId, string CustomerID, string AssociateID, string PlotAmount, string Discount, string NetPlotAmount, string TotalPLC, string PlotRate)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdatePlotBookingDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "yes";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch
            {
                model.Result = "Plot Details Not Updated!!";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReverseApprovePayment(Plot model)
        {

            return View();
        }

        [HttpPost]
        [ActionName("ReverseApprovePayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetReverseCancelledPlotPaymentList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.ApprovedFromDate = string.IsNullOrEmpty(model.ApprovedFromDate) ? null : Common.ConvertToSystemDate(model.ApprovedFromDate, "dd/MM/yyyy");
            model.ApprovedToDate = string.IsNullOrEmpty(model.ApprovedToDate) ? null : Common.ConvertToSystemDate(model.ApprovedToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPaymentApprovedReportList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    ViewBag.TotalAmount = ds.Tables[0].Rows[0]["Total2"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    //  obj.RejectedDate = r["RejectedDate"].ToString();
                    obj.ApproveDescription = r["Description"].ToString();
                    //  obj.RejectDescription = r["RejectDescription"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            //#region ddlpaymentStatus
            //List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            //ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            //#endregion ddlpaymentStatus

            return View(model);
        }



        public ActionResult ReverseApprovePaymentDetails(Plot model, string UserID, string Description, string ApprovedDate)
        {
            try
            {
                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"]?.ToString();

                DataSet ds = model.ReverseApprovePayment();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var resultCode = ds.Tables[0].Rows[0][0].ToString();
                    if (resultCode == "1")
                    {
                        model.Result = "Yes";
                    }
                    else if (resultCode == "2")
                    {
                        model.Result = "Closing done for this payment";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"]?.ToString();
                    }
                }
                else
                {
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result = "An error occurred: " + ex.Message;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ApproveReturnPaymentRequest(string PK_BookingId)
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


            List<Plot> lst = new List<Plot>();
            DataSet ds1 = model.GetReturnRequestPaymentList();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Plot obj1 = new Plot();
                    obj1.UserID = r["PK_ReturnPaymentId"].ToString();
                    obj1.CustomerID = r["CustomerLoginID"].ToString();
                    obj1.CustomerName = r["CustomerName"].ToString();
                    obj1.AssociateID = r["AssociateLoginID"].ToString();
                    obj1.AssociateName = r["AssociateName"].ToString();
                    obj1.BookingNumber = r["BookingNo"].ToString();
                    obj1.PaymentMode = r["PaymentMode"].ToString();
                    obj1.TransactionDate = r["TransactionDate"].ToString();
                    obj1.CancelDate = r["CancelledDate"].ToString();
                    obj1.TransactionNumber = r["TransactionNo"].ToString();
                    obj1.Remark = r["Remark"].ToString();
                    obj1.PlotNumber = r["Plotdetails"].ToString();
                    obj1.PaidAmount = r["RequestedReturnAmount"].ToString();
                    obj1.PaymentStatus = r["PaymentStatus"].ToString();
                    obj1.PaymentDate = r["PaymentRequestedDate"].ToString();
                    obj1.BankDetails = r["Details"].ToString();
                    lst.Add(obj1);

                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ApproveReturnPaymentRequest")]
        [OnAction(ButtonName = "SearchPlot")]
        public ActionResult GetApproveReturnPaymentRequest(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = (!string.IsNullOrEmpty(SiteID) && SiteID != "0") ? SiteID : null;
            model.SectorID = (!string.IsNullOrEmpty(SectorID) && SectorID != "0") ? SectorID : null;
            model.BlockID = (!string.IsNullOrEmpty(BlockID) && BlockID != "0") ? BlockID : null;
            model.PlotNumber = (!string.IsNullOrEmpty(PlotNumber) && PlotNumber != "0") ? PlotNumber : null;
            model.BookingNumber = (!string.IsNullOrWhiteSpace(BookingNumber)) ? BookingNumber.Trim() : null;

            //model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            //model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            //model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            //model.PlotNumber = model.PlotNumber == "0" ? null : model.PlotNumber;
            //model.BookingNumber = model.BookingNumber == "0" ? null : model.BookingNumber;
            //model.BookingNumber = BookingNumber;
           
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
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

           DataSet ds = model.GetReturnRequestPaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_ReturnPaymentId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.CancelDate = r["CancelledDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PlotNumber = r["Plotdetails"].ToString();
                    obj.PaidAmount = r["RequestedReturnAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentRequestedDate"].ToString();
                    obj.BankDetails = r["Details"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View("ApproveReturnPaymentRequest", model);
        }


        [HttpPost]
        public JsonResult ApproveReturnRequest(string UserID, string Description, string ApprovedDate, string PayAmount)
        {
            try
            {
                Plot model = new Plot
                {
                    UserID = UserID,
                    Description = Description,
                    ApprovedDate = ApprovedDate,
                    PayAmount= PayAmount,
                    AddedBy = Session["Pk_AdminId"]?.ToString(),
                    Result = "yes"
                };

                DataSet ds = model.ApproveReturnRequestPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Json(new { success = true, message = "Payment Approved successfully!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString() });
                    }
                }

                return Json(new { success = false, message = "Unexpected response from database." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult RejectReturnRequest(string UserID, string Description, string ApprovedDate)
        {
            try
            {
                Plot model = new Plot
                {
                    UserID = UserID,
                    Description = Description,
                    ApprovedDate = ApprovedDate,
                    AddedBy = Session["Pk_AdminId"].ToString()
                };

                DataSet ds = model.RejectReturnRequestPayment();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    return Json(new { success = true, message = "Payment Rejected successfully!" });
                }
                else
                {
                    string error = ds?.Tables[0].Rows[0]["ErrorMessage"].ToString() ?? "Unknown error";
                    return Json(new { success = false, message = error });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
         
    
