using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Filter;
using Dolphin.Models;

namespace Dolphin.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        //
        // GET: /Reports/

        public ActionResult Reports()
        {
            return View();
        }

        #region CustomerLedgerReport
        public ActionResult CustomerLedgerReport(string PK_BookingId)
        {

            Plot model = new Plot();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = PK_BookingId;
            DataSet dsBookingDetails = model.GetBookingDetailsList();
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

        public ActionResult Details(string BookingNumber, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            Plot model = new Plot();
            try
            {
                List<Plot> lst = new List<Plot>();
                model.SiteID = SiteID == "0" ? null : SiteID;
                model.SectorID = SectorID == "0" ? null : SectorID;
                model.BlockID = BlockID == "0" ? null : BlockID;
                model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
                model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
                // model.PlotNumber = PlotNumber;
                DataSet dsblock = model.FillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {

                        model.Result = "yes";
                        model.hdBookingNo = Crypto.Encrypt(dsblock.Tables[0].Rows[0]["BookingNo"].ToString());
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
                        model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();

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

                                lst.Add(obj);
                            }
                            model.lstPlot = lst;
                        }
                    }
                }
                else
                {
                    model.Result = "No record found !";
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
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PrintCustomerLedger(string bn)
        {
            Plot model = new Plot();
            try
            {
                List<Plot> lst = new List<Plot>();
                model.BookingNumber = Crypto.Decrypt(bn);
                DataSet dsblock = model.FillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "yes";
                        ViewBag.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString() + " (" + dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString() + ")";
                        ViewBag.CustomerMobile = dsblock.Tables[0].Rows[0]["Mobile"].ToString();
                        ViewBag.CustomerAddress = dsblock.Tables[0].Rows[0]["Address"].ToString();
                        ViewBag.Pincode = dsblock.Tables[0].Rows[0]["Pincode"].ToString();
                        ViewBag.State = dsblock.Tables[0].Rows[0]["statename"].ToString();
                        ViewBag.City = dsblock.Tables[0].Rows[0]["Districtname"].ToString();
                        ViewBag.SiteName = dsblock.Tables[0].Rows[0]["SiteName"].ToString();
                        ViewBag.SiteAddress = dsblock.Tables[0].Rows[0]["SiteAddress"].ToString();
                        ViewBag.SectorName = dsblock.Tables[0].Rows[0]["SectorName"].ToString();
                        ViewBag.BlockName = dsblock.Tables[0].Rows[0]["BlockName"].ToString();
                        ViewBag.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();

                        ViewBag.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                        ViewBag.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        ViewBag.NetAmtWords = dsblock.Tables[0].Rows[0]["NetAmountInWords"].ToString();
                        ViewBag.PaidAmtWords = dsblock.Tables[0].Rows[0]["PaidAmountInWords"].ToString();

                        ViewBag.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                        ViewBag.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        ViewBag.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        ViewBag.PLC = dsblock.Tables[0].Rows[0]["PLCCharge"].ToString();
                        ViewBag.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                        ViewBag.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();

                    }
                }
                else
                {
                    model.Result = "No record found !";
                }
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

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
            //return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Due Installment Report

        public ActionResult DueInstallmentReport(string PK_BookingId)
        {
            Plot model = new Plot();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = PK_BookingId;
            DataSet dsBookingDetails = model.GetBookingDetailsList();
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

        public ActionResult FillDueInstsDetails(string BookingNumber, string FromDate, string ToDate, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            // model.PlotNumber = PlotNumber;
            DataSet dsblock = model.FillDueInstDetails();
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


                }

            }
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

                    lst.Add(obj);
                }
                model.lstPlot = lst;

                //ViewBag.PaidAmount = double.Parse(dsblock.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                //ViewBag.InstallmentAmount = double.Parse(dsblock.Tables[0].Compute("sum(InstAmt)", "").ToString()).ToString("n2");
                //ViewBag.DueAmount = double.Parse(dsblock.Tables[0].Compute("sum(DueAmount)", "").ToString()).ToString("n2");

            }
            else
            {
                model.Result = "No record found !";
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

        #region PlotAvailability

        public ActionResult PlotAvailability(Master model)
        {
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

            return View();
        }

        public ActionResult GetSiteBySiteType(string SiteTypeID)
        {
            try
            {
                Master model = new Master();
                model.SiteTypeID = SiteTypeID;

                #region GetSite
                List<SelectListItem> ddlSite = new List<SelectListItem>();
                DataSet dsSector = model.GetSiteList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });

                    }
                }
                model.ddlSite = ddlSite;
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
                model.SiteID = SiteID;

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
                model.ddlSector = ddlSector;
                #endregion

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


        [HttpPost]
        [ActionName("PlotAvailability")]
        [OnAction(ButtonName = "Search")]

        public ActionResult Details(Master model)
        {
            //Master model = new Master();
            List<Master> lst = new List<Master>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.SiteID = SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;

            DataSet dsblock1 = model.GetDetails();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SectorID = r["FK_SectorID"].ToString();
                    obj.BlockID = r["FK_BlockID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    //model.PlotID = dsblock.Tables[0].Rows[0]["PK_PLotID"].ToString();
                    //model.SiteID = dsblock.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    //model.SectorID = dsblock.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    //model.BlockID = dsblock.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    //model.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();
                    //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                    lst.Add(obj);
                }

                model.lstPlot = lst;
            }

            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
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
            #endregion

            #region ddlSiteType
            Master objSiteType = new Master();
            int countType = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = model.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countType == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    countType = countType + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            return View(model);
        }
        #endregion 

        #region PlotAllotmentReport

        public ActionResult PlotAllotmentReport(Plot model)
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
            #endregion

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
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
        [ActionName("PlotAllotmentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetAllotRep(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    //obj.PlotRate = r["PlotRate"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");

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

        public ActionResult PrintAllotment(string id)
        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(id);
            newdata.PK_BookingDetailsId = Crypto.Decrypt(id);
            ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.List();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFatherName = ds.Tables[0].Rows[0]["FathersName"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    ViewBag.Contact = ds.Tables[0].Rows[0]["AssociateMobile"].ToString();
                    ViewBag.CustomerID = ds.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    ViewBag.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.PlotNo = ds.Tables[0].Rows[0]["PlotNumber"].ToString();

                    ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.ReasonOfPayment = ds.Tables[0].Rows[0]["ReasonOfPayment"].ToString();
                    ViewBag.PaymentDate = ds.Tables[0].Rows[0]["PaymentDate"].ToString();
                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.CorporateOffice = ds.Tables[0].Rows[0]["CorporateOffice"].ToString();
                    ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();

                    ViewBag.customerMobile = ds.Tables[0].Rows[0]["customerMobile"].ToString();
                    ViewBag.PLC = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PLC"].ToString()) ? "N/A" : ds.Tables[0].Rows[0]["PLC"].ToString();
                    ViewBag.PLCAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["PLCAmount"]);
                    ViewBag.AmountInWords = ds.Tables[0].Rows[0]["PaidAmountInWords"].ToString();
                    ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    ViewBag.NetPlotAmountInWords = ds.Tables[0].Rows[0]["NetPlotAmountInWords"].ToString();

                    ViewBag.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    ViewBag.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
                    ViewBag.BalanceAmount = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    ViewBag.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                    ViewBag.TotalPaid = ds.Tables[0].Rows[0]["TotalPaid"].ToString();
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

        #region Summary Report 


        public ActionResult SummaryReport(Plot model)
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
        [ActionName("SummaryReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetSummaryRep(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");

            model.Downline = model.IsDownline == true ? "1" : "0";
            DataSet ds = model.GetSummaryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateName = r["AssociateInfo"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.PaymentDate = r["LastPaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;

                ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                ViewBag.PlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");
                ViewBag.Balance = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(PlotAmount)", "").ToString()).ToString("n2");
                ViewBag.Discount = double.Parse(ds.Tables[0].Compute("sum(Discount)", "").ToString()).ToString("n2");
                ViewBag.PlotArea = double.Parse(ds.Tables[0].Compute("sum(TotalArea)", "").ToString()).ToString("n2");



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
            DataSet dsblock = objmaster.GetBlockList();


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
            #endregion
            return View(model);
        }

        #endregion 

        #region AssociateForSMS


        public ActionResult SMS(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();

                    obj.UserID = r["PK_UserId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    //   obj.LoginID = r["LoginId"].ToString();
                    //  obj.DesignationID = r["FK_DesignationID"].ToString();
                    // obj.FirstName = r["AName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    // obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SMS")]
        [OnAction(ButtonName = "Send")]
        public ActionResult SendSMS(TraditionalAssociate model)
        {

            try
            {
                string noofrows = Request["hdRows"].ToString();

                string id = "";
                string chk = "";
                string contact = "";
                DataTable dtst = new DataTable();

                dtst.Columns.Add("UserID", typeof(string));
                dtst.Columns.Add("Contact", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {

                    chk = Request["chkSelect_ " + i];
                    if (chk == "on")
                    {
                        id = Request["hdUserID_ " + i].ToString();
                        contact = Request["hdContact_ " + i].ToString();

                        dtst.Rows.Add(id, contact);
                        string TempId="";
                        string contact1 = dtst.Rows[i - 1]["Contact"].ToString();
                        BLSMS.SendSMS(contact1, model.Body, TempId);

                    }


                }


            }
            catch (Exception ex)
            {

            }

            return View();
        }

        #endregion

        #region WelcomeLetter Associate

        public ActionResult WelcomeLetter()
        {
            return View();
        }
        [HttpPost]
        [ActionName("WelcomeLetter")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();

                    obj.UserID = r["PK_UserId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    //   obj.LoginID = r["LoginId"].ToString();
                    //  obj.DesignationID = r["FK_DesignationID"].ToString();
                    // obj.FirstName = r["AName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    // obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }

        public ActionResult PrintWelcomeLetter(string id)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["DesignationName"].ToString();

                ViewBag.MemberAccNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                ViewBag.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                ViewBag.MemberBranch = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                ViewBag.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                ViewBag.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


                ViewBag.CompanyName = SoftwareDetails.CompanyName;
            }

            return View(obj);
        }
        public ActionResult PrintIDCard(string id)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["DesignationName"].ToString();
            }

            return View(obj);
        }
        #endregion

        #region ClaimRewardReport
        public ActionResult ClaimRewardReport(TraditionalAssociate model)
        {

            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            DataSet ds = model.ClaimRewardReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.RewardAchieverID = r["PK_RewardAchieverId"].ToString();
                    obj.UserID = r["FK_UserId"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.AssociateName = r["Name"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.PanImage = r["RewardImage"].ToString();
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }


        #endregion

        #region UnpaidIncome
        public ActionResult UnpaidIncome(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.Status = r["Status"].ToString();

                    lst.Add(obj);
                }
                model.lstTrad = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.DifferencePerc = double.Parse(ds.Tables[0].Compute("sum(DifferencePerc)", "").ToString()).ToString("n2");
                ViewBag.Income = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UnpaidIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnpaidIncomeBy(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstTrad = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.DifferencePerc = double.Parse(ds.Tables[0].Compute("sum(DifferencePerc)", "").ToString()).ToString("n2");
                ViewBag.Income = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        #endregion

        #region  PayoutDetails
        public ActionResult PayoutDetails(AssociateBooking model)
        {

            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutDetails")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutDetailsBy(AssociateBooking model)
        {

            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");

            }
            return View(model);
        }



        public ActionResult ClosingWisePayoutDetails(string PK_PaidPayoutId)
        {
            AssociateBooking model = new AssociateBooking();
            model.PK_PaidPayoutId = PK_PaidPayoutId;
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetPayoutWiseIncomeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    ViewBag.DisplayName = r["Name"].ToString();
                    ViewBag.ClosingDate = r["ClosingDate"].ToString();
                    ViewBag.PayoutNo = r["PayoutNo"].ToString();
                    ViewBag.UserId = r["LoginId"].ToString();
                    obj.CustomerID = r["CustomerId"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.CommPercentage = r["DifferencePerc"].ToString();
                    obj.SiteName = r["SiteName"].ToString();

                    



                    lst.Add(obj);
                }
                model.ClosingWisePayoutlist = lst;
            }
            return View(model);
        }


        public ActionResult PayoutRequestReport(AssociateBooking model)
        {
            #region ddlPaymentMode
            int count = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.Status = "Pending";
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.BankAccountNo = r["MemberAccNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();
                    obj.ApprovalDate = r["ApprovalDate"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.TotalGrossAmount = double.Parse(ds.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutRequestReportBy(AssociateBooking model)

        {
            #region ddlPaymentMode
            int count = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.AfromDate = string.IsNullOrEmpty(model.AfromDate) ? null : Common.ConvertToSystemDate(model.AfromDate, "dd/MM/yyyy");
            model.AtoDate = string.IsNullOrEmpty(model.AtoDate) ? null : Common.ConvertToSystemDate(model.AtoDate, "dd/MM/yyyy");


            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.BankAccountNo = r["MemberAccNo"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();
                    obj.ApprovalDate = r["ApprovalDate"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.TotalGrossAmount = double.Parse(ds.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult ApproveRequest(string requestID, string PaymentMode, string TransactionDate)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.RequestID = requestID;
                obj.PaymentMode = PaymentMode;
                obj.TransactionDate = TransactionDate;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ApproveRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        string Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        string TempId = "1707166114765806858";
                        BLSMS.SendSMS(Mobile, "Dear " + Name + ", Your request of Payout Request of Rs " + Amount + ", approved and process successfully. Please check your account.  AB DOLPHIN", TempId);
                        TempData["Request"] = "Request Approved";
                    }
                    else
                    {
                        TempData["Request"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Request"] = ex.Message;
            }
            return RedirectToAction("PayoutRequestReport");
        }

        public ActionResult DeclineRequest(string id,string Remarks)
        {
            AssociateBooking obj = new AssociateBooking();

           
            try
            {
                obj.RequestID = id;
                obj.DeclineRemarks = Remarks;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeclineRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Result = "1";
                        //TempData["Request"] = "Request Declined";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        //TempData["Request"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Request"] = ex.Message;
            }
           

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DistributePayment

        public ActionResult DistributePayment(AssociateBooking model)
        {

            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetDetailsForDistributePayment();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.Total = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("DistributePayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveDistributedPayment(AssociateBooking obj)
        {
            try
            {
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                DataSet ds = new DataSet();
                ds = obj.SaveDistributedPayment();


            }
            catch (Exception ex)
            {
                TempData["distributed"] = ex.Message;
            }
            return RedirectToAction("DistributePayment", "AdminReports");
        }
        #endregion

        #region TransactionLogReport

        public ActionResult TransactionLogReport(AssociateBooking model)
        {

            return View(model);
        }
        [HttpPost]
        [ActionName("TransactionLogReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TransactionLogReportBy(AssociateBooking model)
        {

            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.TransactionLogReportBy();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.DisplayName = r["ActionName"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.FromDate = r["CreatedDate"].ToString();
                    obj.TransactionNumber = r["TransactionBy"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        #endregion

        #region Approve KYC
        public ActionResult AssociateListForKYC(AssociateBooking model)
        {
            //List<AssociateBooking> lst = new List<AssociateBooking>();
            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
            //model.Status = "Pending";
            //DataSet ds = model.AssociateListForKYC();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        AssociateBooking obj = new AssociateBooking();
            //        obj.PK_DocumentID = r["PK_UserDocumentID"].ToString();
            //        obj.LoginId = r["LoginId"].ToString();
            //        obj.DisplayName = r["FirstName"].ToString();
            //        obj.DocumentNumber = r["DocumentNumber"].ToString();
            //        obj.DocumentType = r["DocumentType"].ToString();
            //        obj.DocumentImage = (r["DocumentImage"].ToString());
            //        obj.Status = (r["Status"].ToString());
            //        obj.Date = (r["UploadDate"].ToString());
            //        lst.Add(obj);
            //    }
            //    model.lstPlot = lst;
            //}
            return View(model);
        }
        [HttpPost]
        [ActionName("AssociateListForKYC")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetAssociateListForKYC(AssociateBooking model)
        {
           
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
            model.Status = model.Status;
            DataSet ds = model.AssociateListForKYC();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PK_DocumentID = r["PK_UserDocumentID"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["FirstName"].ToString();
                    obj.DocumentNumber = r["DocumentNumber"].ToString();
                    obj.DocumentType = r["DocumentType"].ToString();
                    obj.DocumentImage = (r["DocumentImage"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Date = (r["UploadDate"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
      
            return View(model);
        }

        public ActionResult ApproveKYC(string Id, string DocumentType, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
                ViewBag.ddlKYCStatus = ddlKYCStatus;
                List<AssociateBooking> lst = new List<AssociateBooking>();

                AssociateBooking model = new AssociateBooking();
                model.LoginId = LoginID;
                model.PK_DocumentID = Id;
                model.DocumentType = DocumentType;
                model.Status = "Approved";
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = new DataSet();
                ds = model.ApproveKYC();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["KYCVerification"] = "KYC Approved Successfully..";
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["KYCVerification"] = ex.Message;
                FormName = "AssociateListForKYC";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult RejectKYC(string Id, string DocumentType, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
                ViewBag.ddlKYCStatus = ddlKYCStatus;
                List<AssociateBooking> lst = new List<AssociateBooking>();

                AssociateBooking model = new AssociateBooking();
                model.LoginId = LoginID;
                model.PK_DocumentID = Id;
                model.DocumentType = DocumentType;
                model.Status = "Rejected";
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = new DataSet();
                ds = model.RejectKYC();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["KYCVerification"] = "KYC Rejected Successfully..";
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["KYCVerification"] = ex.Message;
                FormName = "AssociateListForKYC";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region WelcomeLetterCustomer

        public ActionResult CustomerWelcomeLetter()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CustomerWelcomeLetter")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult CustomerList(Customer model)
        {
            List<Customer> lst = new List<Customer>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.FK_SponsorId = r["FK_SponsorId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.LoginID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_UserId"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.ListCust = lst;


            }
            return View(model);


        }

        public ActionResult PrintWelcomeLetterForCustomer(string id)
        {
            Customer obj = new Customer();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.SponsorID = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                ViewBag.SponsorName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();

                ViewBag.CompanyName = SoftwareDetails.CompanyName;
            }

            return View(obj);
        }

        #endregion

        #region AgreementLetter
        public ActionResult CustomerAgreementLetter(Plot model)
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
        [ActionName("CustomerAgreementLetter")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AgreementList(Plot model)
        {
            if (model.SiteID == "0")
            {
                model.SiteID = null;
            }
            if (model.SectorID == "0")
            {
                model.SectorID = null;
            }
            if (model.BlockID == "0")
            {
                model.BlockID = null;
            }
            List<Plot> lst = new List<Plot>();
            DataSet ds = model.GetListAgreement();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.SiteID = r["Pk_SiteId"].ToString();
                    obj.SectorID = r["Pk_SectorId"].ToString();
                    obj.BlockID = r["Pk_BlockId"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();


                    obj.BranchName = r["BranchName"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotInfo = r["PlotInfo"].ToString();
                    obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    //obj.BookingNumber = r["BookingNumber"].ToString();

                    //obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.ListCust = lst;
                ViewBag.NetPlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");

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


        public ActionResult PrintAgreementLetterForCustomer(string PK_BookingId, string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)

        {


            Plot obj = new Plot();
            obj.PK_BookingId = PK_BookingId;
            List<Plot> lst = new List<Plot>();
            obj.SiteID = SiteID;
            obj.SectorID = SectorID;
            obj.BlockID = BlockID;
            obj.PlotNumber = PlotNumber;
            obj.BookingNumber = BookingNumber;
            DataSet ds = obj.PrintAgreementLatter();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.SponsorName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.AssociateLoginID = ds.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.PlotInfo = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                ViewBag.CurrentDate = ds.Tables[0].Rows[0]["CurrentDate"].ToString();
                ViewBag.AuthorizedSignatory = ds.Tables[0].Rows[0]["AuthorizedSignatory"].ToString();
                ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                ViewBag.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
                ViewBag.PlotAmountinrs = ds.Tables[0].Rows[0]["PlotAmountinrs"].ToString();
                ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                ViewBag.ActualPlotAmount = ds.Tables[0].Rows[0]["ActualPlotAmount"].ToString();
                ViewBag.Netamountinrs = ds.Tables[0].Rows[0]["Netamountinrs"].ToString();
                //ViewBag.BookingAmount = ds.Tables[0].Rows[0]["//"].ToString();
                //ViewBag.BookingAmountinrs= ds.Tables[0].Rows[0]["BookingAmountinrs"].ToString();
                ViewBag.SiteAddress = ds.Tables[0].Rows[0]["SiteAddress"].ToString();
                ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                ViewBag.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                ViewBag.Balance = ds.Tables[0].Rows[0]["Balance"].ToString();
                ViewBag.PaidAmt = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                ViewBag.PaidAmtWords = ds.Tables[0].Rows[0]["PaidAmtWords"].ToString();
                ViewBag.PlanName = ds.Tables[0].Rows[0]["PlanName"].ToString();
                ViewBag.CheckedBY = ds.Tables[0].Rows[0]["CheckedBY"].ToString();
                ViewBag.InstAmt = ds.Tables[1].Rows[0]["InstAmt"].ToString();
                ViewBag.PaidAmount = ds.Tables[2].Rows[0]["PaidAmount"].ToString();
                ViewBag.PaidAmtinrs = ds.Tables[2].Rows[0]["PaidAmtinrs"].ToString();
                ViewBag.PaymentDetail = ds.Tables[2].Rows[0]["PaymentDetail"].ToString();
                ViewBag.FirstName = ds.Tables[1].Rows[0]["FirstName"].ToString();
                ViewBag.CompanyName = SoftwareDetails.CompanyName;

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Plot obj1 = new Plot();
                        obj1.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                        obj1.PaymentDetail = r["PaymentDetail"].ToString();
                        obj1.InstallmentNo = r["InstallmentNo"].ToString();
                        obj1.InstallmentDate = r["InstallmentDate"].ToString();
                        obj1.InstAmt = r["InstAmt"].ToString();
                        obj1.PaymentDate = r["PaymentDate"].ToString();

                        lst.Add(obj1);
                    }
                    obj.lstPlot = lst;
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[2].Rows)
                    {
                        Plot obj1 = new Plot();
                        obj1.FK_BookingId = r["FK_BookingId"].ToString();
                        obj1.PaymentDetails = r["PaymentDetail"].ToString();
                        obj1.PaidAmount = r["PaidAmount"].ToString();
                        obj1.PaidAmtinrs = r["PaidAmtinrs"].ToString();
                        obj1.PayDate = r["PaymentDate"].ToString();
                        obj1.ReceiptNo = r["ReceiptNo"].ToString();
                        lst.Add(obj1);
                    }
                    obj.lstPlot = lst;
                }
            }

            return View(obj);
        }

        #endregion


        public ActionResult ContactList(Home model)
        {
            List<Home> lst = new List<Home>();
            DataSet ds = model.ContactList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    obj.Pk_MsgId = r["Pk_MsgId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Message = r["Message"].ToString();

                    lst.Add(obj);
                }
                model.lstMenu = lst;

            }
            return View(model);

        }

        public ActionResult UpdatePayment(string PK_BookingId)
        {
            Plot model = new Plot();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = PK_BookingId;
            DataSet dsBookingDetails = model.GetBookingDetailsList();
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
        [ActionName("UpdatePayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult Details1(Plot model)
        {

            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.BookingNumber = string.IsNullOrEmpty(model.BookingNumber) ? null : model.BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            // model.PlotNumber = PlotNumber;
            DataSet dsblock = model.FillDetails();

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
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.DueAmount = r["DueAmount"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
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

        #region  Associate tree
        public ActionResult AssociateTree()
        {
            if (Session["ds"] == null)
            {
                return View();
            }
            else
            {
                List<AssociateBooking> lst = new List<AssociateBooking>();

                DataTable ds = Session["ds"] as DataTable;
                AssociateBooking model = new AssociateBooking();
                if (ds != null && ds.Rows.Count > 0)
                {
                    ViewBag.Fk_SponsorId = ds.Rows[0]["Fk_SponsorId"].ToString();
                    foreach (DataRow r in ds.Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        obj.Fk_UserId = r["Pk_UserId"].ToString();
                        obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.FirstName = r["FirstName"].ToString();

                        obj.Status = r["Status"].ToString();
                        obj.ActiveStatus = r["ActiveStatus"].ToString();
                        lst.Add(obj);

                    }
                    model.lstPlot = lst;
                }
                return View(model);
            }

        }
        [HttpPost]
        [ActionName("AssociateTree")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AssociateTreeBy(AssociateBooking model)
        {

            //model.Fk_UserId = Session["Pk_UserId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();

                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);

                }
                model.lstPlot = lst;
            }


            return View(model);
        }
        public ActionResult AssociateTreeLevel(AssociateBooking model, string AssociateID)
        {
            if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
            }
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                Session["ds"] = ds.Tables[0];
                //foreach (DataRow r in ds.Tables[0].Rows)
                //{
                //    AssociateBooking obj = new AssociateBooking();
                //    obj.Fk_UserId = r["Pk_UserId"].ToString();
                //    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                //    obj.LoginId = r["LoginId"].ToString();
                //    obj.FirstName = r["FirstName"].ToString();

                //    obj.Status = r["Status"].ToString();
                //    obj.ActiveStatus = r["ActiveStatus"].ToString();
                //    lst.Add(obj);

                //}
                model.lstPlot = lst;
            }


            return Redirect("AssociateTree");
        }


        #endregion

        public ActionResult UpdateRegistryStatus(string PK_BookingId)
        {

            Reports model = new Reports();
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

            return View(model);
        }




        public ActionResult PlotBookingDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Reports model = new Reports();
            if (BookingNumber == "")
            {
                model.BookingNumber = null;
            }
            else
            {
                model.BookingNumber = BookingNumber;
            }
            model.SiteID = SiteID;
            if (model.SiteID == "0") { model.SiteID = null; }
            model.SectorID = SectorID;
            if (model.SectorID == "0") { model.SectorID = null; }
            model.BlockID = BlockID;
            if (model.BlockID == "0") { model.BlockID = null; }
            model.PlotNumber = PlotNumber;
            if (model.PlotNumber == "") { model.PlotNumber = null; }
            DataSet dsblock = model.GetFullpaymentList();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.BookingStatus = dsblock.Tables[0].Rows[0]["BookingStatus"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.BranchID = dsblock.Tables[0].Rows[0]["BranchID"].ToString();
                    model.BranchName = dsblock.Tables[0].Rows[0]["BranchName"].ToString();
                    model.CustomerID = dsblock.Tables[0].Rows[0]["CustomerID"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.Fk_UserId = dsblock.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsblock.Tables[0].Rows[0]["AssociateID"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.PlotNumber = dsblock.Tables[0].Rows[0]["PlotInfo"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();

                    model.RegistryName = dsblock.Tables[0].Rows[0]["RegistryName"].ToString();

                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.BookingNumber = dsblock.Tables[0].Rows[0]["BookingNo"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.PK_PLCCharge = dsblock.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();

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



        //[HttpPost]
        //[ActionName("UpdateRegistryStatus")]
        //[OnAction(ButtonName = "Search")]
        //public ActionResult GetBookingList(Reports model)
        //{
        //    List<Reports> lst = new List<Reports>();
        //    DataSet ds = model.GetFullpaymentList();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }
        //        else
        //        {
        //            foreach (DataRow r in ds.Tables[0].Rows)
        //            {
        //                Reports obj = new Reports();\
        //                obj.BookingStatus = r["BookingStatus"].ToString();
        //                obj.PK_BookingId = r["PK_BookingId"].ToString();
        //                obj.BranchID = r["BranchID"].ToString();
        //                obj.BranchName = r["BranchName"].ToString();
        //                obj.CustomerID = r["CustomerID"].ToString();
        //                obj.CustomerLoginID = r["CustomerLoginID"].ToString();
        //                obj.Fk_UserId = r["PK_UserId"].ToString();
        //                obj.CustomerName = r["CustomerName"].ToString();
        //                obj.AssociateID = r["AssociateID"].ToString();
        //                obj.AssociateLoginID = r["AssociateLoginID"].ToString();
        //                obj.AssociateName = r["AssociateName"].ToString();
        //                obj.PlotNumber = r["PlotInfo"].ToString();
        //                obj.BookingDate = r["BookingDate"].ToString();
        //                obj.BookingAmount = r["BookingAmt"].ToString();
        //                obj.PaymentPlanID = r["PlanName"].ToString();
        //                obj.BookingNumber = r["BookingNo"].ToString();
        //                obj.PaidAmount = r["PaidAmount"].ToString();
        //                obj.PlotArea = r["PlotArea"].ToString();
        //                obj.PlotAmount = r["PlotAmount"].ToString();
        //                obj.NetPlotAmount = r["NetPlotAmount"].ToString();
        //                obj.PK_PLCCharge = r["PLCCharge"].ToString();
        //                obj.PlotRate = r["PlotRate"].ToString();
        //                lst.Add(obj);
        //            }
        //            model.FullPaymentList = lst;
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ActionName("UpdateRegistryStatus")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveUpdateRegistryStatus(Reports model)
        {

            model.kharijdate = string.IsNullOrEmpty(model.kharijdate) ? null : Common.ConvertToSystemDate(model.kharijdate, "dd/MM/yyyy");
            model.RegistryDate = string.IsNullOrEmpty(model.RegistryDate) ? null : Common.ConvertToSystemDate(model.RegistryDate, "dd/MM/yyyy");
            model.gata = model.gata == "0" ? null : model.gata;
            model.Rakbano = model.Rakbano == "0" ? null : model.Rakbano;
            if (model.isRegistry == "1")
            {
                try
                {

                    model.AddedBy = Session["Pk_AdminId"].ToString();


                    if (model.BookingStatus != "Registered")
                    {
                        DataSet ds = model.UpdatingRegistryStatus();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                TempData["UpdateRegistryStatus"] = "Registry Updated";
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                            {
                                TempData["UpdateRegistryStatus"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                return RedirectToAction("UpdateRegistryStatus");
                            }
                        }
                    }
                    else
                    {
                        TempData["UpdateRegistryStatus"] = "Already Registered";
                    }

                }
                catch (Exception ex)
                {
                    TempData["UpdateRegistryStatus"] = ex.Message;
                    return RedirectToAction("UpdateRegistryStatus");
                }
            }
            else
            {
                TempData["UpdateRegistryStatus"] = "Please Check isRegistry";
                return RedirectToAction("UpdateRegistryStatus");
            }

            return RedirectToAction("UpdateRegistryStatus");
        }

        public ActionResult RegistryReport(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetRegistryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.RegistryName = r["RegistryName"].ToString();
                    obj.Status = r["PlotStatus"].ToString();
                    obj.MobileNo = r["Mobile"].ToString();
                    obj.BookingNo = r["BookingNo"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.FK_BookingId = r["Fk_BookId"].ToString();
                    lst.Add(obj);
                }
                model.lstRegistry = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("RegistryReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult FilterRegistryReport(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.BookingNo = string.IsNullOrEmpty(model.BookingNo) ? null : model.BookingNo;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet dsblock = model.GetRegistryList();

            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    obj.LoginId = r["LoginId"].ToString();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Status = r["PlotStatus"].ToString();
                    obj.MobileNo = r["Mobile"].ToString();
                    obj.RegistryName = r["RegistryName"].ToString();
                    obj.BookingNo = r["BookingNo"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.FK_BookingId = r["Fk_BookId"].ToString();
                    lst.Add(obj);
                }
                model.lstRegistry = lst;
            }
            return View(model);

        }
        public ActionResult PlotAvailabilityReport(Reports model)
        {

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

            List<SelectListItem> ddlAvailabilityStatus = Common.AvailabilityStatus();
            ViewBag.ddlAvailabilityStatus = ddlAvailabilityStatus;

            //model.SiteID = SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;


            model.BookingNo = string.IsNullOrEmpty(model.BookingNo) ? null : model.BookingNo;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");


            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetPlotAvailabilityReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.Status = (r["Status"].ToString());
                    lst.Add(obj);
                }
                model.lstPlotAvailabilityReport = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PlotAvailabilityReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult FilterPlotAvailabilityReport(Reports model)
        {

            List<SelectListItem> ddlAvailabilityStatus = Common.AvailabilityStatus();
            ViewBag.ddlAvailabilityStatus = ddlAvailabilityStatus;


            List<Reports> lst = new List<Reports>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;

            model.BookingNo = string.IsNullOrEmpty(model.BookingNo) ? null : model.BookingNo;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet dsblock = model.GetPlotAvailabilityReport();

            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.Status = (r["Status"].ToString());
                    lst.Add(obj);
                }
                model.lstPlotAvailabilityReport = lst;



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

            #region ddlSiteType
            Master objSiteType = new Master();
            int countType = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countType == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    countType = countType + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion
            return View(model);

        }
        public ActionResult SelfDownlineBusinessReport(Plot model)
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
        [ActionName("SelfDownlineBusinessReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetSelfDownlineBusinessReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.Downline = model.IsDownline == true ? "1" : "0";
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetSelfDownlineBusiness();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.CustomerID = r["LoginId"].ToString();
                    obj.AssociateID = r["AssoLoginId"].ToString();
                    obj.AssociateName = r["AssociateInfo"].ToString();
                    obj.PaidAmount = r["NewPaidAmount"].ToString();
                    obj.PaymentDate = r["LastPaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.Discount = r["Discount"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(NewPaidAmount)", "").ToString()).ToString("n2");
                ViewBag.PlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");
                ViewBag.Balance = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(PlotAmount)", "").ToString()).ToString("n2");
                ViewBag.Discount = double.Parse(ds.Tables[0].Compute("sum(Discount)", "").ToString()).ToString("n2");




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
            DataSet dsblock = objmaster.GetBlockList();


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
            #endregion
            return View(model);
        }
        public ActionResult editRegistryStatus(string Fk_BookingId)
        {
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            model.FK_BookingId = Fk_BookingId;
            DataSet ds = model.getPlotRegistryListById();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                model.BookingNo = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                model.BookingDate = ds.Tables[0].Rows[0]["BookingDate"].ToString();
                model.RegistryName = ds.Tables[0].Rows[0]["RegistryName"].ToString();
                model.PlotNo = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                model.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                model.BookingDate = ds.Tables[0].Rows[0]["BookingDate"].ToString();
                model.Rakbano = ds.Tables[0].Rows[0]["Rakba"].ToString();
                model.gata = ds.Tables[0].Rows[0]["gata"].ToString();
                model.BookingStatus = ds.Tables[0].Rows[0]["BookingStatus"].ToString();
                model.PlanName = ds.Tables[0].Rows[0]["PlanName"].ToString();
                model.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                model.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                model.PlotAmount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
                model.PK_PLCCharge = ds.Tables[0].Rows[0]["PLCCharge"].ToString();
                model.registerydate = ds.Tables[0].Rows[0]["registerydate"].ToString();
                model.kharijdate = ds.Tables[0].Rows[0]["kharijdate"].ToString();
                model.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                model.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                model.FK_BookingId = ds.Tables[0].Rows[0]["PK_Bookingid"].ToString();
                model.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                model.isRegistry = ds.Tables[0].Rows[0]["isRegister"].ToString();
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("editRegistryStatus")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult SaveeditRegistryStatus(Reports model)
        {

            model.kharijdate = string.IsNullOrEmpty(model.kharijdate) ? null : Common.ConvertToSystemDate(model.kharijdate, "dd/MM/yyyy");
            model.registerydate = string.IsNullOrEmpty(model.registerydate) ? null : Common.ConvertToSystemDate(model.registerydate, "dd/MM/yyyy");
            model.BookingNo = model.BookingNo;
            model.FK_BookingId = model.FK_BookingId;
            model.RegistryName = model.RegistryName;
            model.gata = model.gata == "0" ? null : model.gata;
            model.Rakbano = model.Rakbano == "0" ? null : model.Rakbano;


            try
            {

                model.AddedBy = Session["Pk_AdminId"].ToString();


                if (model.isRegistry == "1")
                {
                    DataSet ds = model.UpdateRegisterdPlot();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["RegistryReport"] = "Registry Updated";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["RegistryReport"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return RedirectToAction("RegistryReport");
                        }
                    }
                }
                else
                {
                    TempData["RegistryReport"] = "Already Registered";
                }

            }
            catch (Exception ex)
            {
                TempData["RegistryReport"] = ex.Message;
                return RedirectToAction("RegistryReport");
            }
            return RedirectToAction("RegistryReport");
        }
        public ActionResult BusinessDevelopmentExpenseMaster_Report(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.Month = model.Month == "0" ? null : model.Month;
            DataSet ds = model.BusinessDevelopmentExpenseMaster();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["FullName"].ToString();
                    obj.Target = r["Target"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.Business = r["Business"].ToString();
                    obj.Month = r["MonthName"].ToString();
                    lst.Add(obj);
                }
                model.lstExpenseMasterBusinessReport = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("BusinessDevelopmentExpenseMaster_Report")]
        [OnAction(ButtonName = "Search")]
        public ActionResult Search(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.Month = model.Month == "0" ? null : model.Month;
            DataSet ds = model.BusinessDevelopmentExpenseMaster();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["FullName"].ToString();
                    obj.Target = r["Target"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.Business = r["Business"].ToString();
                    obj.Month = r["MonthName"].ToString();
                    lst.Add(obj);
                }
                model.lstExpenseMasterBusinessReport = lst;
            }
            return View(model);
        }
        public ActionResult calculatebusinessdevelopmentexpenses()
        {
            return View();
        }
        [HttpPost]
        [ActionName("calculatebusinessdevelopmentexpenses")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult calculatebusinessdevelopmentexpenses(Reports model)
        {
            try
            {
                //model.MonthNo = MonthNo;
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                DataSet ds = model.CalculateBusinessDevelopmentExpenseMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["BusinessDevelopment"] = "Business Development Expense has been Updated";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["BusinessDevelopment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return RedirectToAction("calculatebusinessdevelopmentexpenses");
                    }
                }
                else
                {
                    TempData["BusinessDevelopment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["BusinessDevelopment"] = ex.Message;
                return RedirectToAction("calculatebusinessdevelopmentexpenses");
            }
            return RedirectToAction("calculatebusinessdevelopmentexpenses");
        }
        public ActionResult CommissionPaidReport(Plot model)
        {
            return View();
        }
        [HttpPost]
        [ActionName("CommissionPaidReport")]
        [OnAction(ButtonName ="btnSearch")]
        public ActionResult CommisionReport(Plot model)
        {
            List<Plot> lstpaid = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.getCommissionPaidReport();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    //obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FatherName"].ToString();
                    obj.Amount = double.Parse(r["CommissionPaid"].ToString()).ToString("n2");
                    obj.BookingPercent = double.Parse(r["TDS"].ToString()).ToString("n2");
                    obj.ApprovedDate = r["ApprovalDate"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    lstpaid.Add(obj);
                }
                model.lstpaidcommission = lstpaid;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(CommissionPaid)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult CommissionPaidReportTotal(Plot model)
        {
            return View();
        }
        [HttpPost]
        [ActionName("CommissionPaidReportTotal")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult TDSReportToal(Plot model)
        {
            List<Plot> lstpaid = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PaidCommissionReportTotal();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FatherName"].ToString();
                    obj.Amount = double.Parse(r["CommissionPaid"].ToString()).ToString("n2");
                    obj.BookingPercent = double.Parse(r["TDS"].ToString()).ToString("n2");
                    //obj.ApprovedDate = r["ApprovalDate"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    lstpaid.Add(obj);
                }
                model.lstpaidcommission = lstpaid;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(CommissionPaid)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult DirectUserReward()
        {
            Reports model = new Reports();
            return View(model);
        }
        [HttpPost]
        [ActionName("DirectUserReward")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetDirectUserReward(Reports model)
        {

          
            model.RewardID = "1";

            List<Reports> lst = new List<Reports>();

            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    //obj.Status = r["Status"].ToString();
                    obj.LoginId = r["Logindetails"].ToString();
                    //obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.BookingAmount = r["TotalBusiness"].ToString();
                    obj.Target = r["Target"].ToString();   
                    lst.Add(obj);
                }
                model.lstP = lst;
            }

            return View(model);
        }
        public ActionResult TeamUserReward()
        {
            Reports model = new Reports();
            return View(model);
        }
        [HttpPost]
        [ActionName("TeamUserReward")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetTeamUserReward(Reports model)
        {
            model.RewardID = "1";

            List<Reports> lst = new List<Reports>();

            DataSet ds = model.TeamRewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    //obj.Status = r["Status"].ToString();
                    obj.LoginId = r["Logindetails"].ToString();
                    //obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.BookingAmount = r["TotalBusiness"].ToString();
                    obj.Target = r["Target"].ToString();
                    lst.Add(obj);
                }
                model.lstP = lst;
            }

            return View(model);
        }
        public ActionResult BusinessReport()
        {
            Reports model = new Reports();
            return View(model);
        }
        [HttpPost]
        [ActionName("BusinessReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult BusinessReport(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetBusiness();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() != "0")
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.LoginId = r["LoginDetails"].ToString();
                        obj.TotalAllotmentAmount = r["TotalBusiness"].ToString();
                        obj.TeamBusiness = r["TeamBusiness"].ToString();
                        obj.TeamMemberJoining = r["TeamMemberJoining"].ToString();
                        obj.DirectMemberJoining = r["DirectMemberJoining"].ToString();
                        lst.Add(obj);
                    }
                }
                model.lstP = lst;
            }

            return View(model);
        }
        public ActionResult DownBusinessReport()
        {
            Reports model = new Reports();
            return View(model);
        }
        [HttpPost]
        [ActionName("DownBusinessReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DownBusinessReport(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetBusinessDown();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    obj.LoginId = r["LoginDetails"].ToString();
                    obj.TotalAllotmentAmount = r["TotalBusiness"].ToString();
                    lst.Add(obj);
                }
                model.lstP = lst;
            }

            return View(model);
        }
        public ActionResult GetDownLineBusinessById(string Fk_UserId, string FromDate,string ToDate)
        {
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            model.Fk_UserId = Fk_UserId;
            model.FromDate = string.IsNullOrEmpty(FromDate) ? null : Common.ConvertToSystemDate(FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(ToDate) ? null : Common.ConvertToSystemDate(ToDate, "dd/MM/yyyy");
            DataSet dspayout = model.GetDownLineBusinesById();
            if (dspayout != null && dspayout.Tables[0].Rows.Count > 0)
            {
         
                    model.Result = "yes";
                    if (dspayout != null && dspayout.Tables.Count > 0 && dspayout.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dspayout.Tables[0].Rows)
                        {
                            Reports obj = new Reports();
                            obj.LoginId = r["LoginId"].ToString();
                            obj.Name = r["Name"].ToString();
                            obj.Business = r["Business"].ToString();
                            lst.Add(obj);
                        }
                        model.lstDownLineBusiness = lst;
                  
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TranferPayment()
        {
            Plot model = new Plot();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
       
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
        [ActionName("TranferPayment")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchPlot(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.GetPaymentForTransfer();

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
                        obj.PK_BookingId = r["FK_BookingId"].ToString();
                        obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                        obj.CustomerName = r["CustomerDetails"].ToString();
                        obj.AssociateName = r["AssociateDetails"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.PaymentDate = r["PaymentDate"].ToString();
                        obj.PaymentMode = r["PaymentModeName"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;

                    ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");

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
        [HttpPost]
        [ActionName("TranferPayment")]
        [OnAction(ButtonName = "btntransfer")]
        public ActionResult TransferPayment(Plot model)
        {

            string ctrRowCount = Request["hdRows"].ToString();
            string chk = "";
            string Fk_BookingId = "";
            string PK_BookingDetailsId = "";
            int Id = 0;
            DataTable dtpayment = new DataTable();
            dtpayment.Columns.Add("Id");
            dtpayment.Columns.Add("Fk_BookingId");
            dtpayment.Columns.Add("PK_BookingDetailsId");
            for (int i = 1; i < int.Parse(ctrRowCount); i++)
            {

                chk = Request["chkpayment_" + i];
                if (chk == "on")
                {
                    Id = dtpayment.Rows.Count + 1;
                    Fk_BookingId = Request["Fk_BookingId_" + i].ToString();
                    PK_BookingDetailsId = Request["PK_BookingDetailsId_" + i].ToString();
                    dtpayment.Rows.Add(Id, Fk_BookingId, PK_BookingDetailsId);
                }

            }
            model.dtTable = dtpayment;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.TransferPayment();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Plot"] = "Payment Transfer successfully !";
                }
                else
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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
        public ActionResult PaymentReturn()
        {
            Plot model = new Plot();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

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
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            DataSet dsPayMode = model.getpaymentmodeForreturn();
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
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        [HttpPost]
        [ActionName("PaymentReturn")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetBalance(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.getBalanceForReturn();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    model.SiteID = model.SiteID;
                    model.BlockID = model.BlockID;
                    model.SectorID = model.SectorID;
                    model.PlotNumber = model.PlotNumber;
                    model.BookingNumber = model.BookingNumber;
                    ViewBag.Balance = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                }
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.getpaymentmodeForreturn();
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
            DataSet dsblock1 = model.GetBlockList();


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
            //List<SelectListItem> ddlSector = new List<SelectListItem>();
            //ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            //ViewBag.ddlSector = ddlSector;

            //List<SelectListItem> ddlBlock = new List<SelectListItem>();
            //ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            //ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        [HttpPost]
        [ActionName("PaymentReturn")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SavePaymentReturn(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.PaymentReturn();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    TempData["Plot"] = "Payment Return Successfully !!";
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            DataSet dsPayMode = model.getpaymentmodeForreturn();
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
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        //CustomerPaymentLedgerList
        public ActionResult CustomerPaymentLedgerList(Plot model)
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
        [ActionName("CustomerPaymentLedgerList")]
        [OnAction(ButtonName = "search")]
        public ActionResult SearchLedgerList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.GetCustomerPaymentLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"]= ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.Fk_UserId= r["Fk_userId"].ToString();
                    obj.UserDetails = r["UserDetails"].ToString();
                    //obj.PlotInfo = r["PlotInfo"].ToString();
                    obj.TotalAmount= r["TotalPaidAmount"].ToString();
                    obj.CrAmount = r["TotalCrAmount"].ToString();
                    obj.DrAmount = r["TotalDrAmount"].ToString();
                    //obj.PaymentDate = r["PaymentDate"].ToString();
                    //obj.BookingNumber = r["BookingNo"].ToString();
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

        public ActionResult Ledger(string userid, string Am ,string PAm)
        {
            Plot model = new Plot();

            {
                List<Plot> lst = new List<Plot>();
                model.Fk_UserId = userid;
                DataSet ds = model.getLedger();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        ViewBag.TotalPaidAmount = PAm;
                        ViewBag.TotalReturnAmount = Am;
                        ViewBag.balance = (Convert.ToDecimal(PAm) - Convert.ToDecimal(Am));
                        ViewBag.AssociateId = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                        ViewBag.AssoName = ds.Tables[0].Rows[0]["AssoName"].ToString();
                        ViewBag.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                        ViewBag.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Plot obj = new Plot();
                            //obj.UserDetails = r["UserDetails"].ToString();
                            obj.PlotInfo = r["PlotInfo"].ToString();
                            //obj.TotalAmount = r["TotalPaidAmount"].ToString();
                            obj.CrAmount = r["CrAmount"].ToString();
                            obj.DrAmount = r["DrAmount"].ToString();
                            obj.PaymentDate = r["PaymentDate"].ToString();
                            obj.BankBranch = r["BankBranch"].ToString();
                            obj.TransactionNumber = r["TransactionNo"].ToString();
                            obj.TransactionDate = r["TransactionDate"].ToString();
                            obj.BankBranch = r["BankBranch"].ToString();
                            obj.Remark = r["Remarks"].ToString();
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
            return View();
        }
        public ActionResult TransferPaymentList()
        {
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetTransferPaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.RecieverBookingNo = r["RecieverBookingNo"].ToString();
                    obj.TransferBookingNo = r["TransferBookingNo"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    obj.Remarks = r["PaymentRemarks"].ToString();
                    lst.Add(obj);
                }
                model.lstTransfer = lst;
            }
            return View(model);
        }

        public ActionResult TransferPlotList()
        {
            
            return View();
        }
        

        public ActionResult SiteAndAssociateWiseBooking(Reports model)
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

            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetSiteAndAssociateWiseBookingReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //foreach (DataRow r in ds.Tables[0].Rows)
                //{
                //    Reports obj = new Reports();
                //    obj.AssociateID = r["AssociateId"].ToString();
                //    obj.SiteName = r["SiteName"].ToString();
                //    obj.AssociateName = r["AssociateName"].ToString();
                //    obj.TotalBooking = r["TotalBooking"].ToString();
                //    lst.Add(obj);
                //}
                //model.lstSiteAndAssociateWiseBooking = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("SiteAndAssociateWiseBooking")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetSiteAndAssociateWiseBookingReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                    Reports obj = new Reports();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.TotalBooking = r["TotalBooking"].ToString();
                    lst.Add(obj);
                }
                    model.lstSiteAndAssociateWiseBooking = lst;
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


        #region PlotAvailability

        public ActionResult DetailsPlotAvailabilityReports(Master model)
        {
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

            return View();
        }

       
        [HttpPost]
        [ActionName("DetailsPlotAvailabilityReports")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DetailsPlotAvailabilityReportsAction(Master model)
        {
            //Master model = new Master();
            List<Master> lst = new List<Master>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.SiteID = SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;

            DataSet dsblock1 = model.DetailsPlotAvailabilityReports();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SectorID = r["FK_SectorID"].ToString();
                    obj.BlockID = r["FK_BlockID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();


                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.TotalPaidAmount = r["TotalPaidAmount"].ToString();
                    obj.HolderName = r["HolderName"].ToString();
                    obj.HoldFrom = r["HoldFrom"].ToString();
                    obj.HoldTo = r["HoldTo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
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
            #endregion

            #region ddlSiteType
            Master objSiteType = new Master();
            int countType = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = model.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countType == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    countType = countType + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            return View(model);
        }
        #endregion 


        public ActionResult DesignationUpdate()
        {
            
            #region ddlBranch
            Reports obj = new Reports();
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

            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet dsdesignation = obj.GetDesignationList();
            if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (desgnationCount == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    desgnationCount = desgnationCount + 1;
                }
            }

            ViewBag.ddlDesignation = ddlDesignation;

            #endregion
            return View();
        }

        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                Reports model = new Reports();
                model.LoginId = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetAssociateList();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.DesignationID = dsSponsorName.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.OldDesignationID = dsSponsorName.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dsSponsorName.Tables[0].Rows[0]["Percentage"].ToString();
                    model.BranchID = dsSponsorName.Tables[0].Rows[0]["Fk_BranchID"].ToString();

                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
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
        [ActionName("DesignationUpdate")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult UpdateDesignation(Reports model)
        {
            string FormName = "";
            string Controller = "";

            try
            {
                model.UpdatedBy = Session["Pk_AdminID"].ToString();
                DataSet ds = model.UpdateDesignation();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Designation"] = "Designation Updated Successfully.";
                        FormName = "DesignationUpdate";
                        Controller = "AdminReports";
                    }
                    else
                    {
                        TempData["Designation"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "DesignationUpdate";
                        Controller = "AdminReports";
                    }
                }
                else
                {
                    TempData["Designation"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    FormName = "DesignationUpdate";
                    Controller = "AdminReports";
                }
            }
            catch(Exception ex)
            {
                TempData["Designation"] = ex.Message;
                FormName = "DesignationUpdate";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName,Controller);
        }


        public ActionResult TDSReportDateWise()
        {
            return View();
        }


        [HttpPost]
        [ActionName("TDSReportDateWise")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchTDSReportDateWise(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetTDSReportDateWise();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PanNumber = r["PanNumber"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    lst.Add(obj);
                }
                model.lstTDSReportDateWise = lst;
            }
            return View(model);
        }


    }
}