using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using System.Data;
using System.Net;
using System.Net.Mail;

using System.IO;
using static Dolphin.Models.TestLoginAPI;

namespace Dolphin.Controllers
{
    public class WebAPIController : Controller
    {
        #region AssociateLogin
        public ActionResult Login(LoginAPI objParameters)
        {
            LoginAPI obj = new LoginAPI();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Password == "" || objParameters.Password == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.Password = Crypto.Encrypt(objParameters.Password);
                DataSet dsResult = objParameters.LoginAction();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1" && dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Trad Associate")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Successfully logged in!";
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                        obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                        obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                        obj.ProfilePic = dsResult.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.StatusColor = dsResult.Tables[0].Rows[0]["StatusColor"].ToString();
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion AssociateLogin

        #region SignUp
        public ActionResult AssociateSignup(AssociateSignup objParameters)
        {
            ResponseRegistration obj = new ResponseRegistration();
            if (objParameters.BranchID == "" || objParameters.BranchID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Select Branch";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.UserID == "" || objParameters.UserID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Select User";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.DesignationID == "" || objParameters.DesignationID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Select Designation";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.FirstName == "" || objParameters.FirstName == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.LastName == "" || objParameters.LastName == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Last Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Contact == "" || objParameters.Contact == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Contact";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                Random rnd = new Random();
                string pass = rnd.Next(111111, 999999).ToString();
                objParameters.Password = Crypto.Encrypt(pass);
                DataSet dsResult = obj.AssociateRegistration(objParameters);
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if (objParameters.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                /* mailbody = "Dear  " + dsResult.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as Dolphin Associate.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsResult.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());

                                 System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                 {
                                     Host = "smtp.gmail.com",
                                     Port = 587,
                                     EnableSsl = true,
                                     DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                     UseDefaultCredentials = false,
                                     Credentials = new NetworkCredential("abc@gmail.com", "awneesh1")

                                 };

                                 using (var message = new MailMessage("abc@gmail.com", objParameters.Email)
                                 {
                                     IsBodyHtml = true,
                                     Subject = "Associate Registration",
                                     Body = mailbody
                                 })
                                     smtp.Send(message);*/
                                obj.Status = "0";
                                obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                obj.Password = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                                obj.Name = dsResult.Tables[0].Rows[0]["Name"].ToString();
                                obj.Pk_userId = dsResult.Tables[0].Rows[0]["PKUserID"].ToString();
                                obj.SuccessMessage = "Registration Successful !";
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Problem in connection.Please try after some times.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion SignUp

        #region ChangePassword
        public ActionResult ChangePassword(ChangePassword objParameters)
        {
            ChangePassword obj = new ChangePassword();
            if (objParameters.Pk_userId == "" || objParameters.Pk_userId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter User Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.OldPassword == "" || objParameters.OldPassword == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter OldPassword";
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            if (objParameters.NewPassword == "" || objParameters.NewPassword == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter NewPassword";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.OldPassword = Crypto.Encrypt(objParameters.OldPassword);
                objParameters.NewPassword = Crypto.Encrypt(objParameters.NewPassword);
                DataSet dsResult = objParameters.ChangePasswordAssociate();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Password Changed Successfuly.";

                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Problem In connection";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ChangePassword

        #region ForgetPassword
        public ActionResult ForgetPass(ForgetPass objParameters)
        {
            ForgetPass obj = new ForgetPass();
            if (objParameters.Contact == "" || objParameters.Contact == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Mobile No.";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DataSet dsResult = objParameters.ForgetPassword();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        obj.Status = "0";
                        //string TempId = "1707166036749633527";
                        //string passwordRecoveryMessage = BLSMS.ForgetPassword(dsResult.Tables[0].Rows[0]["FirstName"].ToString(), Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString()));
                        //BLSMS.SendSMS(dsResult.Tables[0].Rows[0]["Mobile"].ToString(), passwordRecoveryMessage, TempId);
                        obj.SuccessMessage = "Password is sent on your registered mobile no.";
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ForgetPassword

        #region AssociateDashBoard
        public ActionResult GetDueInstallmentForAssociateDashBoard(AssociateDashboard newdata)
        {
            AssociateDashboard obj = new AssociateDashboard();
            try
            {
                List<DueInstallment> objdueinstallment = new List<DueInstallment>();
                DataSet ds = newdata.GetAssociateForDashboard();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Status = "0";
                        #region DueInstallment
                        foreach (DataRow r in (ds.Tables[0].Rows))
                        {
                            DueInstallment obj1 = new DueInstallment();
                            obj1.CustomerID = r["PK_UserId"].ToString();
                            obj1.CustomerLoginID = r["LoginId"].ToString();
                            obj1.CustomerName = r["FirstName"].ToString();
                            obj1.PlotNumber = r["PlotInfo"].ToString();
                            obj1.InstallmentNo = r["InstallmentNo"].ToString();
                            obj1.InstallmentAmount = r["InstAmt"].ToString();
                            objdueinstallment.Add(obj1);
                        }
                        obj.lstdueinstallment = objdueinstallment;
                        #endregion DueInstallment
                    }
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    obj.Status = "1";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(newdata, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetNewsDetailsForAssociateDashBoard(AssociateDashboard newdata)
        {
            AssociateDashboard obj = new AssociateDashboard();
            try
            {
                List<NewsDetails> lst = new List<NewsDetails>();
                DataSet ds = newdata.GetAssociateForDashboard();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        obj.Status = "0";
                        #region NewsDetails
                        foreach (DataRow r in (ds.Tables[1].Rows))
                        {
                            NewsDetails obj1 = new NewsDetails();
                            obj1.PK_NewsID = r["PK_NewsID"].ToString();
                            obj1.NewsHeading = r["NewsHeading"].ToString();
                            obj1.NewsBody = r["NewsBody"].ToString();
                            obj1.UploadFile = r["UploadFile"].ToString();
                            lst.Add(obj1);
                        }
                        obj.lstnewsdetail = lst;
                        #endregion NewsDetails
                    }
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    obj.Status = "1";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTotalForAssociateDashBoard(AssociateDashboard newdata)
        {
            AssociateDashboard obj = new AssociateDashboard();
            try
            {
                List<Associate> lst = new List<Associate>();
                DataSet ds = newdata.GetAssociateForDashboard();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        obj.Status = "0";
                        #region Associate
                        foreach (DataRow r in (ds.Tables[2].Rows))
                        {
                            Associate obj1 = new Associate();
                            obj1.TotalAssociate = r["TotalAssociate"].ToString();
                            obj1.TotalBusiness = r["TotalBusiness"].ToString();
                            obj1.TotalActiveId = r["TotalActiveId"].ToString();
                            obj1.SelfBusiness = r["SelfBusiness"].ToString();
                            obj1.TeamBusiness = r["TeamBusiness"].ToString();
                            obj1.TotalBooking = r["TotalBooking"].ToString();
                            obj1.TeamBooking = r["TeamBooking"].ToString();
                            obj1.SelfBooking = r["SelfBooking"].ToString();
                            obj1.Totalregistry = r["Totalregistry"].ToString();
                            obj1.SelfRegistry = r["SelfRegistry"].ToString();
                            obj1.TeamRegistry = r["TeamRegistry"].ToString();
                            obj1.TotalTDS = r["TotalTDS"].ToString();
                            obj1.UserRewards = r["Rewards"].ToString();

                            lst.Add(obj1);
                        }
                        obj.lstassociate = lst;
                        #endregion NewsDetails
                    }
                    obj.Name = ds.Tables[3].Rows[0]["Name"].ToString();
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    obj.Status = "1";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion AssociateDashBoard

        #region GraphDetails
        public ActionResult GetGraphDetailsOfPlot(Graph objParameters)
        {
            Graph newdata = new Graph();
            try
            {
                List<GraphData> datalist = new List<GraphData>();
                DataSet ds = new DataSet();
                ds = objParameters.BindGraphDetails();
                if (ds.Tables.Count > 0)
                {
                    newdata.Status = "0";
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        newdata.lstdata = datalist;
                    }
                    List<GraphDetails> objgraphdetails = new List<GraphDetails>();
                    foreach (DataRow r in (ds.Tables[0].Rows))
                    {
                        objgraphdetails.Add(new GraphDetails
                        {
                            Total = r["Total"].ToString(),
                            Status = r["Status"].ToString()
                        });
                    }
                    datalist.Add(new GraphData
                    {
                        Title = "Graph Details",
                        lstgraphdetails = objgraphdetails
                    });

                }
                return Json(newdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                newdata.ErrorMessage = ex.Message;
                return Json(newdata, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region GetStateCity

        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                GetViaPin model = new GetViaPin();
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
                throw;
            }
        }

        #endregion

        #region GetSite

        public ActionResult GetSite(Site model)
        {
            List<SiteList> lst = new List<SiteList>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    SiteList obj = new SiteList();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.PK_SiteID = r["PK_SiteID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstsite = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetSiteType

        public ActionResult GetSiteType(SiteType model)
        {
            List<SiteTypeList> lst = new List<SiteTypeList>();
            DataSet ds = model.GetSiteTypeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SiteTypeList obj = new SiteTypeList();
                    obj.SiteTypeName = r["SiteTypeName"].ToString();
                    obj.PK_SiteTypeID = r["PK_SiteTypeID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstsitetype = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ddlSector
        public ActionResult GetSector(Sector model)
        {
            List<SectorList> lst = new List<SectorList>();
            DataSet ds = model.GetSectorList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SectorList obj = new SectorList();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.PK_SectorID = r["PK_SectorID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstsite = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ddlBlock
        public ActionResult GetBlock(Block model)
        {
            List<BlockList> lst = new List<BlockList>();
            DataSet ds = model.GetBlockList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BlockList obj = new BlockList();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PK_BlockID = r["PK_BlockID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstBlock = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BookingList

        public ActionResult BookingList(BookingList model)
        {
            List<BookingListDetails> lst = new List<BookingListDetails>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BookingListDetails obj = new BookingListDetails();
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
                    lst.Add(obj);
                }
                model.lstbooking = lst;
                model.Message = "Record Found.";
            }
            else
            {
                model.Message = "No Record Found.";
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();

            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CustomerLedgerReport

        public ActionResult CustomerLedgerReport(string PK_BookingId)
        {
            LedgerReport model = new LedgerReport();
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
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        public ActionResult Details(string BookingNumber, string LoginId, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            LedgerReport model = new LedgerReport();
            List<PaymentDetails> lst = new List<PaymentDetails>();
            model.LoginId = LoginId;
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            DataSet dsblock = model.FillDetails();
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
                    PaymentDetails obj = new PaymentDetails();

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
                model.lstpayment = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No record found !";

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

        #region AssociateDownline

        public ActionResult AssociateDownlineDetail(Downline model)
        {

            List<DownlineDetails> lst = new List<DownlineDetails>();
            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DownlineDetails obj = new DownlineDetails();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstdownline = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No Record Found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #endregion 

        #region AssociateUpline

        public ActionResult AssociateUplineDetail(Downline model)
        {

            List<DownlineDetails> lst = new List<DownlineDetails>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DownlineDetails obj = new DownlineDetails();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstdownline = lst;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tree
        public ActionResult Tree1(TreeList model)
        {

            List<TreeDetails> lst = new List<TreeDetails>();

            DataSet ds = model.GetTreeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TreeDetails obj = new TreeDetails();
                    obj.Fk_ParentId = r["Parentid"].ToString();
                    obj.DisplayName = r["MemberName"].ToString();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    lst.Add(obj);
                }
                model.lsttree = lst;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AssociateTree
        public ActionResult AssociateTree(DownlineTree model, string AssociateID)
        {
            if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            List<DownlineTreeDetails> lst = new List<DownlineTreeDetails>();
            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DownlineTreeDetails obj = new DownlineTreeDetails();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstdownline = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UserReward
        public ActionResult UserReward(Reward model)
        {
            model.RewardID = "1";
            List<RewardData> lst = new List<RewardData>();
            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    RewardData obj = new RewardData();

                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Contact = r["BackColor"].ToString();
                    obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    lst.Add(obj);
                }
                model.lstreward = lst;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimReward(RewardClaim obj)
        {
            try
            {
                obj.Status = "Claim";
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.SuccessMessage = "Reward Claimed";
                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SkipReward(RewardClaim obj)
        {
            try
            {
                obj.Status = "Skip";
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.SuccessMessage = "Reward Skipped";
                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UnpaidIncomeReport
        public ActionResult UnpaidIncome(UnpaidIncome model)
        {
            List<UnpaidIncomeDetails> lst = new List<UnpaidIncomeDetails>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UnpaidIncomeDetails obj = new UnpaidIncomeDetails();
                    obj.FromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    lst.Add(obj);
                }
                model.lstunpaid = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No Record Found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PayoutLedger
        public ActionResult PayoutRequest(PayoutBalance model)
        {
            model.Status = "0";
            DataSet Ds = model.GetPayoutBalance();
            model.Balance = Ds.Tables[0].Rows[0]["Balance"].ToString();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePayoutRequest(PayoutRequest obj)
        {
            try
            {
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = " Payout requested successfully !";
                        try
                        {
                            string mob = ds.Tables[0].Rows[0]["Column1"].ToString();
                            string name = ds.Tables[0].Rows[0]["Column3"].ToString();
                            string TempId = "";
                            BLSMS.SendSMS(mob, "Dear " + name + ", Your request of Payout Request of Rs " + ds.Tables[0].Rows[0]["Column2"].ToString() + " has been processed Successfully. Your amount will credited in your account within 1 to 5 working days.", TempId);
                        }
                        catch (Exception ex)
                        {
                            obj.Status = "1";
                            obj.ErrorMessage = ex.Message;
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PayoutDetails(Payout model)
        {
            List<PayoutDetailData> lst = new List<PayoutDetailData>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PayoutDetailData obj = new PayoutDetailData();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstpayout = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayoutLedger(PayoutLedgerData model)
        {
            List<PayoutLedgerDetail> lst = new List<PayoutLedgerDetail>();
            DataSet ds = model.PayoutLedger();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PayoutLedgerDetail obj = new PayoutLedgerDetail();
                    obj.PayoutWalletID = r["Pk_PayoutWalletId"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.Credit = r["Credit"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TType = r["TransactionType"].ToString();
                    obj.Debit = r["Debit"].ToString();


                    lst.Add(obj);
                }
                model.lstpayoutledger = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PayoutRequestReport
        public ActionResult PayoutRequestReport(PayoutRequestReports model)
        {
            model.Status = model.Status == "0" ? null : model.Status;
            List<PayoutRequestReportData> lst = new List<PayoutRequestReportData>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PayoutRequestReportData obj = new PayoutRequestReportData();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstpayout = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No Record Found !";
            }
            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#region KYC

        //public ActionResult KYCDocuments(KYCDocuments model)
        //{
        //    KYCDocuments obj = new KYCDocuments();
        //    DataSet ds = model.GetKYCDocuments();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
        //        obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
        //        obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
        //        obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
        //        obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
        //        obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
        //        obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
        //        obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
        //        obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult KYCDocuments(KYCDocument obj, IEnumerable<HttpPostedFileBase> postedFile)
        //{
        //    try
        //    {
        //        foreach (var file in postedFile)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        //                file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));

        //            }

        //        }
        //        obj.ActionStatus = "Adhar";
        //        DataSet ds = obj.UploadKYCDocuments();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                obj.SuccessMessage = "Document uploaded successfully..";

        //            }
        //            else
        //            {
        //                obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.ErrorMessage = ex.Message;

        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult KYCDocuments2(KYCDocument obj, IEnumerable<HttpPostedFileBase> postedFile)
        //{
        //    try
        //    {
        //        foreach (var file in postedFile)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                //E:\BitBucket\DolphinZone\Dolphin\files\assets\images\

        //                obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        //                file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));


        //            }

        //        }
        //        obj.ActionStatus = "Pan";
        //        DataSet ds = obj.UploadKYCDocuments();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                obj.SuccessMessage = "Document uploaded successfully..";

        //            }
        //            else
        //            {
        //                obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.ErrorMessage = ex.Message;

        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult KYCDocuments3(KYCDocument obj, IEnumerable<HttpPostedFileBase> postedFile)
        //{
        //    try
        //    {
        //        foreach (var file in postedFile)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                //E:\BitBucket\DolphinZone\Dolphin\files\assets\images\

        //                obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        //                file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));


        //            }

        //        }
        //        obj.ActionStatus = "Doc";
        //        DataSet ds = obj.UploadKYCDocuments();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                obj.SuccessMessage = "Document uploaded successfully..";

        //            }
        //            else
        //            {
        //                obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.ErrorMessage = ex.Message;

        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region Enquiry

        public ActionResult EnquiryList(Enquiry model)
        {

            List<EnquiryList> lst = new List<EnquiryList>();

            DataSet ds = model.EnquiryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EnquiryList obj = new EnquiryList();
                    obj.EnquiryID = r["PK_EnquiryMasterId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Details = r["Details"].ToString();
                    lst.Add(obj);
                }

                model.lstBlock1 = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveEnquiry(EnquiryData obj)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = obj.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.SuccessMessage = "Enquiry saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PlotAvailability

        public ActionResult PlotAvailability(PlotDetails model)
        {
            List<PlotList> lst = new List<PlotList>();
            DataSet ds = model.GetPlotDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.SiteID = ds.Tables[0].Rows[0]["FK_SiteID"].ToString();
                model.SectorID = ds.Tables[0].Rows[0]["FK_SectorID"].ToString();
                model.BlockID = ds.Tables[0].Rows[0]["FK_BlockID"].ToString();
                model.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PlotList obj = new PlotList();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No Record Found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetSitesDetails

        public ActionResult GetSitesDetails(SitePLCCharge model)
        {
            try
            {
                #region GetSiteRate
                DataSet dsSiteRate = model.GetSiteList();
                if (dsSiteRate != null)
                {
                    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                    model.Result = "yes";
                }
                #endregion

                #region SitePLCCharge
                List<PLCChargeList> lst = new List<PLCChargeList>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        PLCChargeList obj = new PLCChargeList();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = r["PLCCharge"].ToString();
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlcCharge = lst;
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ViewProfile

        public ActionResult ViewProfile(Profile model)
        {
            try
            {
                DataSet dsPlotDetails = model.GetList();
                if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                {
                    model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                    model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                    model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                    model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                    model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                    model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                    model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                    model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();

                    model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region UpdateProfile

        public ActionResult UpdateProfile(HttpPostedFileBase postedFile, Profile model)
        {
            try
            {
                if (postedFile != null)
                {
                    model.ProfilePic = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                }
                DataSet ds = model.UpdatePersonalDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.SuccessMessage = " Updated successfully !";
                    }
                    else
                    {
                        model.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetSummaryReport

        public ActionResult GetSummaryReport(SummaryReport model)
        {
            List<SummaryList> lst = new List<SummaryList>();
            DataSet ds = model.GetSummaryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SummaryList obj = new SummaryList();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateName = r["AssociateInfo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["LastPaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    lst.Add(obj);
                }
                model.lstSummary = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No Record Found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetBranch

        public ActionResult GetBranch(Branch model)
        {
            List<Branch> lst = new List<Branch>();
            int count1 = 0;
            DataSet ds = model.GetBranchList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    if (count1 == 0)
                    {
                        model.BranchName = "Select Branch";
                        model.PK_BranchID = "0";
                    }
                    model.Status = "0";
                    model.BranchName = r["BranchName"].ToString();
                    model.PK_BranchID = r["PK_BranchID"].ToString();
                    //ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetDesignation

        public ActionResult GetDesignation(Designation model)
        {
            List<DesignationDetails> lst = new List<DesignationDetails>();
            DataSet ds = model.GetDesignationList();
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        DesignationDetails obj = new DesignationDetails();
                        obj.DesignationName = r["DesignationName"].ToString();
                        obj.PK_DesignationID = r["PK_DesignationID"].ToString();
                        lst.Add(obj);
                    } 
                    model.Status = "0";
                    model.lstdesign = lst;
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.ErrorMessage = ex.Message;   
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region TestLogin

        public ActionResult TestLogin(TestLoginAPI objParameters)
        {
            LoginAPI obj = new LoginAPI();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.Password = Crypto.Encrypt(objParameters.Password);
                DataSet dsResult = objParameters.LoginActionTest();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1" && dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Trad Associate")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Successfully logged in!";
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                        obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                        obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                        obj.ProfilePic = dsResult.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.StatusColor = dsResult.Tables[0].Rows[0]["StatusColor"].ToString();
                        obj.Password = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region GettownshipbookingDetails

        [HttpPost]
        public ActionResult GettownshipbookingDetails(Gettownshipbooking model)
        {
             
            GettownshipbookingStatus obj = new GettownshipbookingStatus();
            DataSet ds = model.GettownshipbookingDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.TotalBookingId = ds.Tables[0].Rows[0]["TotalPk_BookingId"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        #region Associate KYC
        public ActionResult KYCDocuments(HttpPostedFileBase AdharImage, HttpPostedFileBase AdharBacksideImage, HttpPostedFileBase PanImage, HttpPostedFileBase DocumentImage, SaveKYC obj)
        {
            try
            {
                if (AdharImage != null)
                {
                    obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(AdharImage.FileName);
                    AdharImage.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                }
                if (AdharBacksideImage != null)
                {
                    obj.AdharBacksideImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(AdharBacksideImage.FileName);
                    AdharBacksideImage.SaveAs(Path.Combine(Server.MapPath(obj.AdharBacksideImage)));
                }
                if (PanImage != null)
                {
                    obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(PanImage.FileName);
                    PanImage.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                }
                if (DocumentImage != null)
                {
                    obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(DocumentImage.FileName);
                    DocumentImage.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                }
                obj.AccountHolderName = obj.AccountHolderName == " " ? null : obj.AccountHolderName;
                obj.BankName = obj.BankName == " " ? null : obj.BankName;
                obj.BankBranch = obj.BankBranch == " " ? null : obj.BankBranch;
                obj.IFSCCode = obj.IFSCCode == " " ? null : obj.IFSCCode;
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Document uploaded successfully..";
                        try
                        {
                            string KYCUploadedyMessage = "Dear " + ds.Tables[0].Rows[0]["FirstName"].ToString() + ", your kyc uploaded and kyc will be approved in 4-5 working days. AB DOLPHIN";
                            string TempId = "1707166097826950598";
                            BLSMS.SendSMS(ds.Tables[0].Rows[0]["Mobile"].ToString(), KYCUploadedyMessage, TempId);
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetKYCList(KYCListAPI model)
        {
            KYCListAPI obj = new KYCListAPI();
            try
            {
                List<lstKycDocument> lstKycdocuments = new List<lstKycDocument>();
                DataSet ds = model.GetKYCDocuments();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lstKycDocument KYClist = new lstKycDocument();
                        KYClist.AdharNumber = dr["AdharNumber"].ToString();
                        KYClist.AdharImage = dr["AdharImage"].ToString();
                        KYClist.AdharBacksideImage = dr["AdharBacksideImage"].ToString();
                        KYClist.AdharStatus = dr["AdharStatus"].ToString();
                        KYClist.PanNumber = dr["PanNumber"].ToString();
                        KYClist.PanImage = dr["PanImage"].ToString();
                        KYClist.PanStatus = dr["PanStatus"].ToString();
                        KYClist.DocumentNumber = dr["DocumentNumber"].ToString();
                        KYClist.DocumentImage = dr["DocumentImage"].ToString();
                        KYClist.DocumentStatus = dr["DocumentStatus"].ToString();
                        KYClist.AccountHolderName = dr["BankHolderName"].ToString();
                        KYClist.BankName = dr["BankName"].ToString();
                        KYClist.IFSCCode = dr["IFSCCode"].ToString();
                        KYClist.BankBranch = dr["BankBranch"].ToString();
                        lstKycdocuments.Add(KYClist);
                    }
                    obj.lstKycdocuments = lstKycdocuments;

                    obj.Status = "0";
                    obj.Message = "KYC Documents Fetched";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    obj.Status = "1";
                    obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region VisitorList

        public ActionResult VisitorList(VisitorListAPI model)
        {
            try
            {
                List<lstvisitor> lstvisitor = new List<lstvisitor>();
                DataSet ds = model.VisitorList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        lstvisitor obj = new lstvisitor();
                        obj.AssociateLoginID = r["LoginId"].ToString();
                        obj.PK_VisitorID = r["PK_VisitorID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.VisitDate = r["VisitDate"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        lstvisitor.Add(obj);
                    }
                    model.lstvisitor = lstvisitor;
                    model.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                    model.Status = "0";
                    model.Message = "Visitor List Fetched";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PrintVisitor(PrintVisitor model)
        {
            List<visitorlst> visitorlst = new List<visitorlst>();
            try
            {
                DataSet ds = model.VisitorListById();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_VisitorId = ds.Tables[0].Rows[0]["PK_VisitorID"].ToString();
                    model.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                    model.VisitDate = ds.Tables[0].Rows[0]["VisitDate"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            visitorlst obj = new visitorlst();
                            obj.CustomerName = r["CustomerName"].ToString();
                            obj.Mobile = r["Mobile"].ToString();
                            obj.Address = r["Address"].ToString();
                            visitorlst.Add(obj);
                        }
                    }
                    model.visitorlst = visitorlst;
                    model.Status = "0";
                    model.Message = "Data Fetched..";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region AdvancePaymentList

        public ActionResult AdvancePaymentList(AdvancePayment model)
        {
            try
            {
                List<lstAdvancePayment> lstAdvancePayment = new List<lstAdvancePayment>();
                DataSet ds = model.GetAdvancePaymentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        lstAdvancePayment obj = new lstAdvancePayment();
                        obj.Amount = r["Amount"].ToString(); ;
                        obj.LoginID = r["LoginID"].ToString();
                        obj.PaymentDate = r["PaymentDate"].ToString();
                        obj.Name = r["Name"].ToString();
                        lstAdvancePayment.Add(obj);
                    }
                    model.lstAdvancePayment = lstAdvancePayment;
                    model.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                    model.Status = "0";
                    model.Message = "Data Fetched..";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region BussinessReport

        public ActionResult BusinessReport(BusinessReport model)
        {
           try
            {
                List<lstBusinessReport> lstBusinessReport = new List<lstBusinessReport>();
                DataSet ds = model.GetBusiness();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Status = "1";
                        model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            lstBusinessReport obj = new lstBusinessReport();
                            obj.UserLoginId = r["LoginId"].ToString();
                            obj.LoginId = r["LoginDetails"].ToString();
                            obj.TotalAllotmentAmount = r["TotalBusiness"].ToString();
                            obj.TeamBusiness = r["TeamBusiness"].ToString();
                            obj.DirectMemberJoining = r["DirectMemberJoining"].ToString();
                            obj.TeamMemberJoining = r["TeamMemberJoining"].ToString();
                            lstBusinessReport.Add(obj);
                        }
                        model.lstBusinessReport = lstBusinessReport;
                        model.Status = "0";
                        model.Message = "Data Fetched..";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DownBusinessReport

        public ActionResult DownBusinessReport(DownBusiness model)
        {
            try
            {
                List<lstDownBusiness> lstDownBusiness = new List<lstDownBusiness>();
                DataSet ds = model.GetBusinessDown();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        lstDownBusiness obj = new lstDownBusiness();
                        obj.Fk_UserId = r["PK_UserId"].ToString();
                        obj.LoginId = r["LoginDetails"].ToString();
                        obj.TotalAllotmentAmount = r["TotalBusiness"].ToString();
                        lstDownBusiness.Add(obj);
                    }
                    model.lstDownBusiness = lstDownBusiness;
                    model.Status = "0";
                    model.Message = "Data Fetched..";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region GetDownLineBusinessById

        public ActionResult GetDownLineBusinessById(ViewBussiness model)
        {
            try
            {
                List<lstViewBussiness> lstViewBussiness = new List<lstViewBussiness>();
                DataSet dspayout = model.GetDownLineBusinesById();
                if (dspayout != null && dspayout.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dspayout.Tables[0].Rows)
                    {
                        lstViewBussiness obj = new lstViewBussiness();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Business = r["Business"].ToString();
                        lstViewBussiness.Add(obj);
                    }
                    model.lstViewBussiness = lstViewBussiness;
                    model.Status = "0";
                    model.Message = "Data Fetched..";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = dspayout.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region GetSelfDownlineBusinessReport

        public ActionResult GetSelfDownlineBusinessReport(GetSelfDownlineBusines model)
        {
           try
            {
                List<lstSelfDownlineBusiness> lstSelfDownlineBusiness = new List<lstSelfDownlineBusiness>();
                model.SiteID = model.SiteID == "0" ? null : model.SiteID;
                model.SectorID = model.SectorID == "0" ? null : model.SectorID;
                model.BlockID = model.BlockID == "0" ? null : model.BlockID;
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.GetSelfDownlineBusiness();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        lstSelfDownlineBusiness obj = new lstSelfDownlineBusiness();
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
                        lstSelfDownlineBusiness.Add(obj);   
                    }
                    model.lstSelfDownlineBusiness = lstSelfDownlineBusiness;
                    model.TotalPaidAmount = double.Parse(ds.Tables[0].Compute("sum(NewPaidAmount)", "").ToString()).ToString("n2");
                    model.TotalPlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");
                    model.TotalBalance = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");
                    model.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(PlotAmount)", "").ToString()).ToString("n2");
                    model.TotalDiscount = double.Parse(ds.Tables[0].Compute("sum(Discount)", "").ToString()).ToString("n2");
                    model.Status = "0";
                    model.Message = "Data Fetched..";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Associate Downline Reports

        public ActionResult GetDownLineReports(AssociateDownLineReports model)
        {
            List<lstAssociateDownLine> lstAssociateDownLine = new List<lstAssociateDownLine>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");

                //model.Fk_UserId = Session["Pk_UserId"].ToString();

                DataSet ds = model.GetDownLineReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        lstAssociateDownLine obj = new lstAssociateDownLine();

                        obj.LoginDetails = r["LoginDetails"].ToString();
                        obj.TotalBusiness = r["TotalBusiness"].ToString();
                        obj.TeamBusinessAmount = r["TeamBusiness"].ToString();
                        obj.DirectMemberJoining = r["DirectMemberJoining"].ToString();
                        obj.TeamMemberJoining = r["TeamMemberJoining"].ToString();
                        obj.Income = r["Income"].ToString();

                        lstAssociateDownLine.Add(obj);
                    }
                    model.lstAssociateDownLine = lstAssociateDownLine;
                    model.Status = "0";
                    model.Message = "Associate Downline Data Fetched..";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            
        }



        #endregion

    }
}