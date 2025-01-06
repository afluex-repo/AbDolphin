using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using System.Data;
using Dolphin.Filter;
using System.IO;

namespace Dolphin.Controllers
{
    public class MasterController : AdminBaseController
    {
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
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

        #region PLCMaster
        public ActionResult PLCMaster(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.PLCList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PLCID = r["PK_PLCID"].ToString();
                    obj.PLCName = r["PLCName"].ToString();

                    lst.Add(obj);
                }
                model.lstPLC = lst;


            }
            return View(model);
        }

        public ActionResult SavePLC(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SavePLC();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PLC"] = "PLC saved successfully";
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["PLC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PLC"] = ex.Message;
                FormName = "PLCMaster";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult DeletePLC(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PLCID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePLC();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PLC"] = "PLC deleted successfully";
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["PLC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PLC"] = ex.Message;
                FormName = "PLCMaster";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult UpdatePLC(string PLCID, string PLCName)
        {
            Master obj = new Master();
            try
            {
                obj.PLCID = PLCID;
                obj.PLCName = PLCName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdatePLC();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "PLC updated successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SiteMaster

        public ActionResult SiteMaster(string SiteID)
        {
            Master model = new Master();
            #region ddlUnits
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds1 = obj.GetUnitList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

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

            if (SiteID != null)
            {
                model.SiteID = Crypto.Decrypt(SiteID);
                List<Master> lst = new List<Master>();
                DataSet dsSite = model.GetSiteList();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.UnitID = dsSite.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.Location = dsSite.Tables[0].Rows[0]["Location"].ToString();
                    model.Rate = dsSite.Tables[0].Rows[0]["Rate"].ToString();
                    model.SiteTypeID = dsSite.Tables[0].Rows[0]["FK_SiteTypeID"].ToString();
                    model.DevelopmentCharge = dsSite.Tables[0].Rows[0]["DevelopmentCharge"].ToString();
                }


                DataSet ds = model.GetSitePlcChargeList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master objPLC = new Master();
                        objPLC.PLCID = r["PK_PLCID"].ToString();
                        objPLC.PLCName = r["PLCName"].ToString();
                        objPLC.PLCCharge = r["PLCCharge"].ToString();
                        lst.Add(objPLC);
                    }
                    model.lstPLC = lst;
                }
            }
            else
            {
                List<Master> lst = new List<Master>();
                DataSet ds = model.PLCList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master objPLC = new Master();
                        objPLC.PLCID = r["PK_PLCID"].ToString();
                        objPLC.PLCName = r["PLCName"].ToString();

                        lst.Add(objPLC);
                    }
                    model.lstPLC = lst;
                }

            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SaveSite")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSite(Master obj)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));
                string hdrows = "";
                if (Request["hdRows"] != null)
                {
                    hdrows = Request["hdRows"].ToString();
                }
                else
                {
                    hdrows = "0";
                }
                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    string plcid = Request["hdPLCID_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                obj.dtPLCCharge = dt;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveSite();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SiteMaster"] = "Site saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SiteMaster"] = ex.Message;
            }
            return RedirectToAction("SiteMaster", "Master");
        }

        [HttpPost]
        [ActionName("SaveSite")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateSite(Master obj)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));

                string hdrows = "";
                if (Request["hdRows"] != null)
                {
                    hdrows = Request["hdRows"].ToString();
                }
                else
                {
                    hdrows = "0";
                }
                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    string plcid = Request["hdPLCID_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                obj.dtPLCCharge = dt;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateSite();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SiteMaster"] = "Site Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SiteMaster"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SiteMaster"] = ex.Message;
            }
            return RedirectToAction("SiteMaster", "Master");
        }

        public ActionResult SiteList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetSiteList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteID = r["PK_SiteID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SiteID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.Location = r["Location"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.UnitName = (r["UnitName"].ToString());
                    obj.DevelopmentCharge = (r["DevelopmentCharge"].ToString());
                    obj.SiteTypeName = (r["SiteTypeName"].ToString());
                    lst.Add(obj);
                }
                model.lstSite = lst;
            }
            return View(model);
        }
        public ActionResult DeleteSite(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.SiteID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSite();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Site"] = "Site deleted successfully";
                        FormName = "SiteList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Site"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SiteList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Site"] = ex.Message;
                FormName = "SiteList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region PlotSizeMaster

        public ActionResult PlotSizeList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetPlotSizeList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotSizeID = r["PK_PlotSizeMaster"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotSizeMaster"].ToString());
                    obj.WidthFeet = r["WidthFeet"].ToString();
                    obj.WidthInch = r["WidthInch"].ToString();
                    obj.HeightFeet = r["HeightFeet"].ToString();
                    obj.HeightInch = (r["HeightInch"].ToString());
                    obj.PlotArea = (r["PlotArea"].ToString());

                    lst.Add(obj);
                }
                model.lstSite = lst;
            }

            return View(model);
        }

        public ActionResult PlotSizeMaster(string plotSizeID)
        {
            Master model = new Master();
            #region ddlUnits
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds1 = obj.GetUnitList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion

            if (plotSizeID != null)
            {
                model.PlotSizeID = Crypto.Decrypt(plotSizeID);
                DataSet ds = new DataSet();
                ds = model.GetPlotSizeList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PlotSizeID = ds.Tables[0].Rows[0]["PK_PlotSizeMaster"].ToString();
                    model.WidthFeet = ds.Tables[0].Rows[0]["WidthFeet"].ToString();
                    model.WidthInch = ds.Tables[0].Rows[0]["WidthInch"].ToString();
                    model.HeightFeet = ds.Tables[0].Rows[0]["HeightFeet"].ToString();
                    model.HeightInch = ds.Tables[0].Rows[0]["HeightInch"].ToString();
                    model.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SavePlotSize")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SavePlotSize(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SavePlotSize();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
            }
            return RedirectToAction("PlotSizeMaster", "Master");
        }

        [HttpPost]
        [ActionName("SavePlotSize")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePlotSize(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdatePlotSize();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
            }
            return RedirectToAction("PlotSizeMaster", "Master");
        }

        public ActionResult DeletePlotSize(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PlotSizeID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePlotSize();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size deleted successfully";
                        FormName = "PlotSizeList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotSizeList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
                FormName = "PlotSizeList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion
        #region PlotMaster
        public ActionResult GetSiteDetailsForPlotBooking(string SiteID)
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
                    foreach (DataRow r in dsSector.Tables[1].Rows)
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
        public ActionResult PlotMaster(Master model)
        {
            #region ddlSites
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion

            #region ddlPlotSize
            int countplot = 0;
            List<SelectListItem> ddlPlotSize = new List<SelectListItem>();
            DataSet dsPlotSize = obj.GetPlotSize();

            if (dsPlotSize != null && dsPlotSize.Tables.Count > 0 && dsPlotSize.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlotSize.Tables[0].Rows)
                {
                    if (countplot == 0)
                    {
                        ddlPlotSize.Add(new SelectListItem { Text = "Select Plot Size", Value = "0" });
                    }
                    ddlPlotSize.Add(new SelectListItem { Text = r["PlotSize"].ToString(), Value = r["PK_PlotSizeMaster"].ToString() });
                    countplot = countplot + 1;
                }
            }

            ViewBag.ddlPlotSize = ddlPlotSize;

            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View();
        }

        public ActionResult SavePlot(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));

                string hdrows = Request["hdRows"].ToString();
                for (int i = 0; i < int.Parse(hdrows) - 1; i++)
                {
                    string plcid = Request["plcid_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                obj.dtPLCCharge = dt;
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlot();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot saved successfully !";
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
            FormName = "PlotMaster";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }

        //Site Rate, Sectors & PLC Charge List of the selected Site
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
                model.SiteID = SiteID;

                #region GetSiteRate
                DataSet dsSiteRate = model.GetSiteList();
                if (dsSiteRate != null)
                {
                    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                    model.Result = "yes";
                }
                #endregion
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
                #region SitePLCCharge
                List<Master> lstPlcCharge = new List<Master>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = r["PLCCharge"].ToString();
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lstPlcCharge.Add(obj);
                    }
                    model.lstPLC = lstPlcCharge;
                }
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

        #endregion
        #region PlotMaster-List-Update-Delete

        public ActionResult PlotList(Master model)
        {
            DataSet ds = model.GetPlotList();


            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


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

        [HttpPost]
        [ActionName("PlotList")]
        [OnAction(ButtonName = "SearchPlot")]
        public ActionResult PlotListDetails(Master model)
        {
            List<Master> lst = new List<Master>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            DataSet ds = model.GetPlotList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PLCCharge = r["PLCCharge"].ToString();
                    obj.BookingPercent = r["BookingPercent"].ToString();
                    obj.AllottmentPercent = (r["AllottmentPercent"].ToString());
                    obj.PlotStatus = (r["PlotStatus"].ToString());
                    obj.ColorCSS = (r["StatusColor"].ToString());
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
            objmaster.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
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
            objmaster.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            objmaster.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
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

        public ActionResult GetPlotDetails(string Fk_PlotId,string status)
        {
            Master model = new Master();
            try
            {
                model.PlotID = Fk_PlotId;
                model.PlotStatus = status;
                if (model.PlotStatus == null)
                {
                    DataSet ds = model.BookingDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Result = "yes";
                        model.CustomerDetails = ds.Tables[0].Rows[0]["CustomerDetails"].ToString();
                        model.AssociateDetails = ds.Tables[0].Rows[0]["AssociateDetails"].ToString();
                        model.BookingNumber = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        model.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                        model.BookingDate = ds.Tables[0].Rows[0]["BookingDate"].ToString();
                    }
                    else
                    {
                        model.Result = "no";
                    }
                }
                else
                {
                    DataSet ds = model.BookingHoldDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Result = "yes";
                        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        model.HoldDate = ds.Tables[0].Rows[0]["HoldDate"].ToString();
                        model.HoldAmount = ds.Tables[0].Rows[0]["HoldAmount"].ToString();
                        model.RecieptNo = ds.Tables[0].Rows[0]["RecieptNo"].ToString();
                        model.BookingDate = ds.Tables[0].Rows[0]["Date"].ToString();
                    }
                    else
                    {
                        model.Result = "no";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult UpdatePlot(string PlotID)
        {
            Master model = new Master();
            try
            {
                if (PlotID != null)
                {
                    #region ddlSites
                    Master obj = new Master();
                    int count = 0;
                    List<SelectListItem> ddlSite = new List<SelectListItem>();
                    DataSet ds1 = obj.GetSiteList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                            }
                            ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlSite = ddlSite;

                    #endregion

                    #region ddlPlotSize
                    int countplot = 0;
                    List<SelectListItem> ddlPlotSize = new List<SelectListItem>();
                    DataSet dsPlotSize = obj.GetPlotSize();

                    if (dsPlotSize != null && dsPlotSize.Tables.Count > 0 && dsPlotSize.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsPlotSize.Tables[0].Rows)
                        {
                            if (countplot == 0)
                            {
                                ddlPlotSize.Add(new SelectListItem { Text = "Select Plot Size", Value = "0" });
                            }
                            ddlPlotSize.Add(new SelectListItem { Text = r["PlotSize"].ToString(), Value = r["PK_PlotSizeMaster"].ToString() });
                            countplot = countplot + 1;
                        }
                    }

                    ViewBag.ddlPlotSize = ddlPlotSize;

                    #endregion

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

                    #region ddlBlock
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
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

                    model.PlotID = Crypto.Decrypt(PlotID);

                    DataSet dsPlotDetails = model.GetPlotList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.PlotID = PlotID;
                        model.PlotSizeID = dsPlotDetails.Tables[0].Rows[0]["PK_PlotSizeMaster"].ToString();
                        model.SiteID = dsPlotDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                        model.SiteName = dsPlotDetails.Tables[0].Rows[0]["SiteName"].ToString();
                        model.SectorID = dsPlotDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                        model.SectorName = dsPlotDetails.Tables[0].Rows[0]["SectorName"].ToString();
                        model.BlockID = dsPlotDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                        model.BlockName = dsPlotDetails.Tables[0].Rows[0]["BlockName"].ToString();
                        model.PlotNumber = dsPlotDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                        model.PlotArea = dsPlotDetails.Tables[0].Rows[0]["TotalArea"].ToString();
                        model.PlotRate = dsPlotDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                        model.PlotAmount = dsPlotDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                        model.PLCCharge = dsPlotDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                        model.BookingPercent = dsPlotDetails.Tables[0].Rows[0]["BookingPercent"].ToString();
                        model.AllottmentPercent = dsPlotDetails.Tables[0].Rows[0]["AllottmentPercent"].ToString();
                        model.PlotStatus = dsPlotDetails.Tables[0].Rows[0]["PlotStatus"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UpdatePlot")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePlot(Master model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));

                string hdrows = Request["hdRows"].ToString();
                for (int i = 0; i < int.Parse(hdrows) - 1; i++)
                {
                    string plcid = Request["plcid_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                model.dtPLCCharge = dt;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.PlotID = Crypto.Decrypt(model.PlotID);
                DataSet ds = model.UpdatePlot();
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
            FormName = "PlotMaster";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult DeletePlot(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PlotID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePlot();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot deleted successfully";
                        FormName = "PlotList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
                FormName = "PlotList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion
        #region SectorMaster

        public ActionResult SectorMaster(string SectorID)
        {
            Master model = new Master();
            #region ddlSite
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion

            if (SectorID != null)
            {
                model.SectorID = Crypto.Decrypt(SectorID);
                List<Master> lst = new List<Master>();
                DataSet dsSite = model.GetSector();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteID = dsSite.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.SectorName = dsSite.Tables[0].Rows[0]["SectorName"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SaveSector")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSector(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveSector();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
            }
            return RedirectToAction("SectorMaster", "Master");
        }

        [HttpPost]
        [ActionName("SaveSector")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateSector(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateSector();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
            }
            return RedirectToAction("SectorMaster", "Master");
        }

        public ActionResult SectorList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetSector();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorID = r["PK_SectorID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SectorID"].ToString());
                    obj.SectorName = r["SectorName"].ToString();

                    lst.Add(obj);
                }
                model.lstSector = lst;
            }
            return View(model);
        }

        public ActionResult DeleteSector(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.SectorID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSector();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector deleted successfully";
                        FormName = "SectorList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SectorList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
                FormName = "SectorList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion 
        #region BlockMaster
        public ActionResult BlockMaster(string BlockID)
        {
            Master model = new Master();
            if (BlockID != null)
            {
                model.BlockID = Crypto.Decrypt(BlockID);

                DataSet dsSite = model.GetBlockList();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteID = dsSite.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.SectorName = dsSite.Tables[0].Rows[0]["SectorName"].ToString();
                    model.BlockName = dsSite.Tables[0].Rows[0]["BlockName"].ToString();
                    model.BlockID = dsSite.Tables[0].Rows[0]["PK_BlockID"].ToString();



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
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsSite.Tables[0].Rows[0]["PK_SiteID"].ToString();


                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;


            }

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet Site = model.GetSiteList();
            if (Site != null && Site.Tables.Count > 0 && Site.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in Site.Tables[0].Rows)
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
        [ActionName("SaveBlock")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveBlock(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveBlock();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
            }
            return RedirectToAction("BlockMaster", "Master");
        }
        [HttpPost]
        [ActionName("SaveBlock")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBlock(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateBlock();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
            }
            return RedirectToAction("BlockMaster", "Master");
        }
        public ActionResult BlockList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetBlockList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorID = r["PK_SectorID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SectorID"].ToString());
                    obj.SiteID = r["PK_SiteID"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockID = r["PK_BlockID"].ToString();
                    obj.BlockName = r["BlockName"].ToString();

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }
        public ActionResult DeleteBlock(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.BlockID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBlock();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block deleted successfully";
                        FormName = "BlockList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "BlockList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
                FormName = "BlockList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion 
        #region Branch Master

        public ActionResult BranchMaster(string BranchID)
        {
            Master model = new Master();
            if (BranchID != null)
            {
                model.BranchID = Crypto.Decrypt(BranchID);

                DataSet Branch = model.GetBranchList();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.BranchID = Branch.Tables[0].Rows[0]["PK_BranchID"].ToString();
                    model.BranchName = Branch.Tables[0].Rows[0]["BranchName"].ToString();

                }
            }

            return View(model);

        }
        public ActionResult BranchList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetBranchList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BranchID = r["PK_BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BranchID"].ToString());

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("SaveBranch")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveBranch(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveBranch();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
            }
            return RedirectToAction("BranchMaster", "Master");
        }

        [HttpPost]
        [ActionName("SaveBranch")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBranch(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateBranch();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
            }
            return RedirectToAction("BranchMaster", "Master");
        }
        public ActionResult DeleteBranch(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.BranchID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBranch();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch deleted successfully";
                        FormName = "BranchList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "BranchList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
                FormName = "BranchList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion 
        #region RankMaster

        public ActionResult RankMaster(Master model)
        {
            //Master model = new Master();
            //if (DesignationID != null)
            //{
            //    model.DesignationID = Crypto.Decrypt(DesignationID);

            //    DataSet Branch = model.GetDesignation();
            //    if (Branch != null && Branch.Tables.Count > 0)
            //    {
            //        model.DesignationID = Branch.Tables[0].Rows[0]["PK_DesignationID"].ToString();
            //        model.DesignationName = Branch.Tables[0].Rows[0]["DesignationName"].ToString();
            //        model.Percentage = Branch.Tables[0].Rows[0]["Percentage"].ToString();
            //    }
            //}
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetDesignation();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DesignationID = r["PK_DesignationID"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_DesignationID"].ToString());

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);

        }

        public ActionResult RankList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetDesignation();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DesignationID = r["PK_DesignationID"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_DesignationID"].ToString());

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SaveRank")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveRank(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Rank"] = "Rank saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Rank"] = ex.Message;
            }
            return RedirectToAction("RankMaster", "Master");
        }

        [HttpPost]
        [ActionName("SaveRank")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateRank(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateRank();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Rank"] = "Rank updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Rank"] = ex.Message;
            }
            return RedirectToAction("RankMaster", "Master");
        }

        public ActionResult DeleteRank(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.DesignationID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteRank();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Rank"] = "Rank deleted successfully";
                        FormName = "RankList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "RankList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rank"] = ex.Message;
                FormName = "RankList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion
        #region News Master 

        public ActionResult NewsMaster(string NewsID)
        {
            Master model = new Master();
            if (NewsID != null)
            {
                model.NewsID = NewsID;

                DataSet Branch = model.GetNews();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.NewsID = Branch.Tables[0].Rows[0]["PK_NewsID"].ToString();
                    model.NewsHeading = Branch.Tables[0].Rows[0]["NewsHeading"].ToString();
                    model.NewsBody = Branch.Tables[0].Rows[0]["NewsBody"].ToString();
                    model.NewsFor = Branch.Tables[0].Rows[0]["NewsFor"].ToString();
                }
            }

            List<SelectListItem> NewsFor = Common.NewsForList();
            ViewBag.NewsFor = NewsFor;
            List<Master> lst = new List<Master>();
            return View(model);

        }

        public ActionResult NewsList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetNews();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.NewsID = r["PK_NewsID"].ToString();
                    obj.NewsHeading = r["NewsHeading"].ToString();
                    obj.NewsBody = r["NewsBody"].ToString();
                    obj.NewsFor = r["NewsFor"].ToString();
                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SaveNews")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveNews(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveNews();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["News"] = "News saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
            }
            return RedirectToAction("NewsMaster", "Master");
        }

        [HttpPost]
        [ActionName("SaveNews")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateNews(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateNews();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["News"] = "News updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["News"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
            }
            return RedirectToAction("NewsMaster", "Master");
        }
        public ActionResult DeleteNews(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.NewsID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteNews();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["News"] = "News deleted successfully";
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
                FormName = "NewsList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion 
        #region GalleryMaster 

        public ActionResult GalleryMaster(string PK_GalleryID)
        {
            Master model = new Master();
            #region ddlSite
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion
            if (PK_GalleryID != null)
            {
                model.PK_GalleryID = PK_GalleryID;

                DataSet Branch = model.GetGallery();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.PK_GalleryID = Branch.Tables[0].Rows[0]["PK_GalleryID"].ToString();
                    model.SiteID = Branch.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    model.SiteName = Branch.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteImage = Branch.Tables[0].Rows[0]["SiteImage"].ToString();
                }
            }

            return View(model);

        }
        public ActionResult GalleryList(Master model)
        {
            #region ddlSite
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Project", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("GalleryList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(Master model)
        {
            List<Master> lst = new List<Master>();

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.GetGallery();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_GalleryID = r["PK_GalleryID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SiteImage = r["SiteImage"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlSite
            int count1 = 0;
            Master model1 = new Master();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet Site = model1.GetSiteList();
            if (Site != null && Site.Tables.Count > 0 && Site.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in Site.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Project", Value = "0" });
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
        [ActionName("SaveGallery")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveGallery(HttpPostedFileBase postedFile, Master obj)
        {
            try
            {
                if (postedFile != null)
                {
                    obj.SiteImage = "../files/assets/images/siteImages/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.SiteImage)));

                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveGallery();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Images"] = "Image saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Images"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Images"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Images"] = ex.Message;
            }
            return RedirectToAction("GalleryMaster", "Master");
        }

        //[HttpPost]
        //[ActionName("SaveNews")]
        //[OnAction(ButtonName = "btnUpdate")]
        //public ActionResult UpdateNews(Master obj)
        //{
        //    try
        //    {

        //        obj.AddedBy = Session["Pk_AdminId"].ToString();
        //        DataSet ds = new DataSet();
        //        ds = obj.UpdateNews();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                TempData["News"] = "News updated successfully";
        //            }
        //            else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //            {
        //                TempData["News"] = ds.Tables[0].Rows[0][0].ToString();
        //            }
        //        }
        //        else
        //        {
        //            TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["News"] = ex.Message;
        //    }
        //    return RedirectToAction("NewsMaster", "Master");
        //}
        public ActionResult DeleteSiteImage(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PK_GalleryID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteImage();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Images"] = "Image deleted successfully";
                        FormName = "GalleryList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Images"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "GalleryList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Images"] = ex.Message;
                FormName = "GalleryList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion
        #region EnquiryMaster


        public ActionResult EnquiryMaster(string EnquiryID)
        {
            Master model = new Master();
            if (EnquiryID != null)
            {
                model.EnquiryID = EnquiryID;

                DataSet Branch = model.EnquiryList();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.EnquiryID = Branch.Tables[0].Rows[0]["PK_EnquiryMasterId"].ToString();
                    model.Name = Branch.Tables[0].Rows[0]["Name"].ToString();
                    model.Details = Branch.Tables[0].Rows[0]["Details"].ToString();

                }
            }
            List<Master> lst = new List<Master>();

            DataSet ds = model.EnquiryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EnquiryID = r["PK_EnquiryMasterId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Details = r["Details"].ToString();

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        public ActionResult EnquiryList(Master model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("EnquiryMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveEnquiry(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Enquiry"] = "Enquiry saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Enquiry"] = ex.Message;
            }
            return RedirectToAction("EnquiryMaster", "Master");
        }
        #endregion
        #region  RowHouseSizeMaster

        public ActionResult RowHouseSizeList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetRowHouseSizeList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.RowHouseSizeID = r["PK_RowHouseSizeID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_RowHouseSizeID"].ToString());
                    obj.WidthFeet = r["WidthFeet"].ToString();
                    obj.WidthInch = r["WidthInch"].ToString();
                    obj.HeightFeet = r["HeightFeet"].ToString();
                    obj.HeightInch = (r["HeightInch"].ToString());
                    obj.PlotArea = (r["PlotArea"].ToString());
                    obj.Price = (r["Price"].ToString());
                    obj.SiteID = (r["SiteID"].ToString());
                    obj.SiteName = (r["SiteName"].ToString());
                    obj.GroundFloorArea = (r["GroundFloorArea"].ToString());
                    obj.FirstFloorArea = (r["FirstFloorArea"].ToString());
                    obj.BookingPercentage = (r["BookingPercentage"].ToString());
                    obj.AllotmentPercentage = (r["AllotmentPercentage"].ToString());
                    obj.UnitID = (r["UnitID"].ToString());
                    obj.UnitName = (r["UnitName"].ToString());
                    lst.Add(obj);
                }
                model.lstSite = lst;
            }

            return View(model);
        }

        public ActionResult RowHouseSizeMaster(string RowHouseSizeID)
        {
            Master model = new Master();
            #region ddlUnits
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds1 = obj.GetUnitList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion
            #region ddlSite
            Master obj1 = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds11 = obj1.GetSiteList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
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
            if (RowHouseSizeID != null)
            {
                model.RowHouseSizeID = Crypto.Decrypt(RowHouseSizeID);
                DataSet ds = new DataSet();
                ds = model.GetRowHouseSizeList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.RowHouseSizeID = ds.Tables[0].Rows[0]["PK_RowHouseSizeID"].ToString();
                    model.WidthFeet = ds.Tables[0].Rows[0]["WidthFeet"].ToString();
                    model.WidthInch = ds.Tables[0].Rows[0]["WidthInch"].ToString();
                    model.HeightFeet = ds.Tables[0].Rows[0]["HeightFeet"].ToString();
                    model.HeightInch = ds.Tables[0].Rows[0]["HeightInch"].ToString();
                    model.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Price = ds.Tables[0].Rows[0]["Price"].ToString();
                    model.SiteID = ds.Tables[0].Rows[0]["SiteID"].ToString();
                    model.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    model.GroundFloorArea = ds.Tables[0].Rows[0]["GroundFloorArea"].ToString();
                    model.FirstFloorArea = ds.Tables[0].Rows[0]["FirstFloorArea"].ToString();
                    model.BookingPercentage = ds.Tables[0].Rows[0]["BookingPercentage"].ToString();
                    model.AllotmentPercentage = ds.Tables[0].Rows[0]["AllotmentPercentage"].ToString();
                    model.UnitID = ds.Tables[0].Rows[0]["UnitID"].ToString();
                    model.UnitName = ds.Tables[0].Rows[0]["UnitName"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SaveRowHouseSize")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveRowHouseSize(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveRowHouseSize();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["RowHouseSize"] = "Row House Size saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["RowHouseSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["RowHouseSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["RowHouseSize"] = ex.Message;
            }
            return RedirectToAction("RowHouseSizeMaster", "Master");
        }
        [HttpPost]
        [ActionName("SaveRowHouseSize")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateRowHouseSize(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateRowHouseSize();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["RowHouseSize"] = "Row House Size updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["RowHouseSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["RowHouseSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["RowHouseSize"] = ex.Message;
            }
            return RedirectToAction("RowHouseSizeMaster", "Master");
        }

        public ActionResult DeleteRowHouseSize(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.NewsID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteRowHouseSize();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["House"] = "Row House Size deleted successfully";
                        FormName = "RowHouseSizeList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["House"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "RowHouseSizeList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["House"] = ex.Message;
                FormName = "RowHouseSizeList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion
        public ActionResult SetDesignation()
        {

            Master model = new Master();
            List<Master> lst1 = new List<Master>();
            DataSet ds = model.RetriveAllDataRankUpdateLog();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EntryDate = r["EntryDate"].ToString();
                    obj.DoneBy = r["DoneBy"].ToString();
                    obj.OldPercentage = r["OldPercentage"].ToString();
                    obj.NewPercentage = r["NewPercentage"].ToString();
                    obj.OldDesignation = r["OldDesignation"].ToString();
                    obj.NewDesignation = r["NewDesignation"].ToString();
                    lst1.Add(obj);
                }
                model.lstRanklog = lst1;
            }
            List<SelectListItem> ddldesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationListForUpdateRank();
            int designationcount = 0;
            if (dsdesignation != null && dsdesignation.Tables.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (designationcount == 0)
                    {
                        ddldesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddldesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    designationcount = 1;
                }
            }
            ViewBag.ddldesignation = ddldesignation;

            return View(model);
        }
        public ActionResult getUserDetails(string LoginID)
        {
            Master model = new Master();
            try
            {
                model.LoginId = LoginID;
                DataSet dsuserdetails = model.GetUserDetails();
                if (dsuserdetails != null && dsuserdetails.Tables.Count > 0 && dsuserdetails.Tables[0].Rows.Count > 0)
                {
                    model.Result = "yes";
                    model.Name = dsuserdetails.Tables[0].Rows[0]["Name"].ToString();
                    model.DesignationName = dsuserdetails.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.Percentage = dsuserdetails.Tables[0].Rows[0]["Percentage"].ToString();
                    model.DesignationID = dsuserdetails.Tables[0].Rows[0]["DesignationID"].ToString();
                }
                else
                {
                    model.Result = "no";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("SetDesignation")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDesignation(Master obj)
        {
            try
            {
                DataSet ds = new DataSet();
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                ds = obj.UpdateDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["success"] = "Rank updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("SetDesignation", "Master");
        }
        public ActionResult UpdateRankLog()
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();
            DataSet ds = model.RetriveAllDataRankUpdateLog();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.EntryDate = r["EntryDate"].ToString();
                    obj.DoneBy = r["DoneBy"].ToString();
                    obj.OldPercentage = r["OldPercentage"].ToString();
                    obj.NewPercentage = r["NewPercentage"].ToString();
                    obj.OldDesignation = r["OldDesignation"].ToString();
                    obj.NewDesignation = r["NewDesignation"].ToString();
                    lst1.Add(obj);
                }
                model.lstRanklog = lst1;
            }
            return View(model);
        }

        public ActionResult ChangeDesignation()
        {
            Master model = new Master();
            List<SelectListItem> ddldesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            int designationcount = 0;
            if (dsdesignation != null && dsdesignation.Tables.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (designationcount == 0)
                    {
                        ddldesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddldesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    designationcount = 1;
                }
            }
            ViewBag.ddldesignation = ddldesignation;

            return View(model);
        }


        [HttpPost]
        [ActionName("ChangeDesignation")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ChangeDesignation(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetDesignationDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.FK_DesignationId = ds.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                ViewBag.Remark = ds.Tables[0].Rows[0]["Remarks"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.DesignationName = ds.Tables[0].Rows[0]["DesignationName"].ToString();
            }

            List<SelectListItem> ddldesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            int designationcount = 0;
            if (dsdesignation != null && dsdesignation.Tables.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (designationcount == 0)
                    {
                        ddldesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddldesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    designationcount = 1;
                }
            }
            ViewBag.ddldesignation = ddldesignation;


            return View(model);
        }
        
        public ActionResult UpdateDesignation(string LoginId,string FK_DesignationId,string Remark)
        {
            Master model = new Master();
            try
            {
               
                model.LoginId = LoginId;
                model.FK_DesignationId = FK_DesignationId;
                model.Remark = Remark;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ChangeDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    model.Result = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result= ex.Message;
            }
            return Json(model,JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateSponsor()
        {
            return View();
        }


        [HttpPost]
        [ActionName("UpdateSponsor")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UpdateSponsor(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetSponsorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                ViewBag.FK_SponsorId = ds.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                ViewBag.FK_ParentId = ds.Tables[0].Rows[0]["FK_ParentId"].ToString();
            }
            return View(model);
        }
        
        public ActionResult UpdateSponsorName(string LoginId, string SponsorId, string Remark)
        {
            Master model = new Master();
            try
            {
                model.LoginId = LoginId;
                model.FK_SponsorId = SponsorId;
                model.Remark = Remark;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateSponsor();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    model.Result = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Promoter()
        {
            return View();
        }

        public ActionResult SavePromoter(List<string> loginIds, string LoginId)
        {
            Master model = new Master();
            var response = new { Result = "0", Message = "An error occurred" };

            try
            {
                var dtst = new DataTable();
                string LoginIdss = "";

                dtst.Columns.Add("LoginIdss", typeof(string));

                for (int i = 0; i <= (loginIds.Count) - 1; i++)
                {
                    LoginIdss = loginIds[i].ToString();
                    dtst.Rows.Add(LoginIdss);
                }

                model.LoginId = LoginId;
                model.AssociatedownLoginId = dtst;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.promotersave();

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    response = new { Result = "1", Message = "Data saved successfully." };
                }
                else
                {
                    response = new { Result = "0", Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString() };
                }
            }
            catch (Exception ex)
            {
                response = new { Result = "0", Message = ex.Message };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
    }
}
