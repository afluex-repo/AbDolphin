using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using Dolphin.Filter;
using System.IO;

namespace Dolphin.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult AssociateDashBoard()
        {
            return View();
        }

        //public ActionResult ViewProfile()
        //{
        //    Profile objprofile = new Profile();

        //    List<Profile> lstprofile = new List<Profile>();
        //    objprofile.LoginId = Session["LoginId"].ToString();
        //    Profile obj = new Profile();
        //    DataSet ds = objprofile.GetUserProfile();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {

        //        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //        obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
        //        obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
        //        obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
        //        obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
        //        obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
        //        obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
        //        obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
        //        obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
        //        obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
        //        obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
        //        obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
        //        obj.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
        //    }
        //    return View(obj);
        //}

        //[HttpPost]
        //[ActionName("ViewProfile")]
        //[OnAction(ButtonName = "btnUpdate")]
        //public ActionResult UpdateProfile(HttpPostedFileBase fileProfilePicture, Profile obj)
        //{
        //    string FormName = "";
        //    string Controller = "";
        //    try
        //    {
        //        if (fileProfilePicture != null)
        //        {
        //            obj.ProfilePicture = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
        //            fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePicture)));
        //        }

        //        //Profile objProfile = new Profile();
        //        obj.PK_UserID = Session["Pk_userId"].ToString();
        //        DataSet ds = obj.UpdateProfile();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                TempData["UpdateProfile"] = "Profile updated successfully..";
        //                FormName = "ViewProfile";
        //                Controller = "User";
        //                //return View();
        //            }
        //            else
        //            {
        //                TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                FormName = "ViewProfile";
        //                Controller = "User";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["UpdateProfile"] = ex.Message;
        //        FormName = "ViewProfile";
        //        Controller = "User";
        //    }
        //    return RedirectToAction(FormName, Controller);
        //}
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
            model.AssociateID = Session["LoginId"].ToString();
            model.Downline = model.IsDownline == true ? "1" : "0";
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

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
                    obj.PaymentDate = r["LastPaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingDate= r["BookingDate"].ToString();
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
        public ActionResult SaveMessages(string Message, string MessageBy)
        {
            DashBoard obj = new DashBoard();
            try
            {
                obj.Message = Message;
                obj.MessageBy = MessageBy;
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                obj.AddedBy = Session["Pk_UserId"].ToString();
                DataSet ds = obj.SaveMessage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Result = "1";
                    }
                    else
                    {
                        obj.Result = "Message Not Send";
                    }
                }
                else
                {
                    obj.Result = "Message Not Send";
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BusinessReport()
        {
            return View();
        }

        [HttpPost]
        [ActionName("BusinessReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult BusinessReport(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetBusiness();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginDetails"].ToString();
                    obj.TotalAllotmentAmount = r["TotalBusiness"].ToString();
                    lst.Add(obj);
                }
                model.lstP = lst;
            }

            return View(model);
        }

    }
}
