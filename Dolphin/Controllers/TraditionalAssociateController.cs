using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using System.Data;
using Dolphin.Filter;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Dolphin.Controllers
{
    public class TraditionalAssociateController : AdminBaseController
    {

        public ActionResult AssociateRegistration(string UserID)
        {


            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                if (UserID != null)
                {

                    model.UserID = Crypto.Decrypt(UserID);
                    //model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.UserID = UserID;
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
                        //   ViewBag.Disabled = "disabled";
                        model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                        model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                        model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                        model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                        model.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                    }
                }

                else
                {
                    // ViewBag.Disabled = "";

                }

                #region ddlBranch
                TraditionalAssociate obj = new TraditionalAssociate();
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
                model.BranchID = "1";
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.LoginID = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetAssociateList();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.SponsorDesignationID = dsSponsorName.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dsSponsorName.Tables[0].Rows[0]["Percentage"].ToString();
                    int desgnationCount = 0;

                    DataSet dsdesignation = model.GetDesignationList();
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
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

                    // ViewBag.ddlDesignation = ddlDesignation;

                    model.ddlDesignation = ddlDesignation;
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

        public ActionResult GetPanDetails(string PanNo)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.PanNo = PanNo;
                #region GetPanDetails
                DataSet dspandetails = model.GetPanDetails();
                if (dspandetails != null && dspandetails.Tables[0].Rows.Count > 0)
                {
                    if (dspandetails.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "yes";
                    }
                    else if (dspandetails.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = "no";
                    }
                }
                else
                {
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
        public ActionResult GetAdharDetails(string AdharNumber)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.AdharNumber = AdharNumber;
                #region GetAdharDetails
                DataSet dsadhardetails = model.GetAdharDetails();
                if (dsadhardetails != null && dsadhardetails.Tables[0].Rows.Count > 0)
                {
                    if (dsadhardetails.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "yes";
                    }
                    else if (dsadhardetails.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = "no";
                    }
                }
                else
                {
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
        public ActionResult GetAccountDetails(string BankAccountNo)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.BankAccountNo = BankAccountNo;
                #region GetAdharDetails
                DataSet dsaccountdetails = model.GetAccountDetails();
                if (dsaccountdetails != null && dsaccountdetails.Tables[0].Rows.Count > 0)
                {
                    if (dsaccountdetails.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "yes";
                    }
                    else if (dsaccountdetails.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = "no";
                    }
                }
                else
                {
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
        [ActionName("AssociateRegistration")]
        [OnAction(ButtonName = "btnRegistration")]
        public ActionResult AssociateRegistration(HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1, TraditionalAssociate model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());
                model.AddedBy = Session["Pk_AdminId"].ToString();
                if (postedFile != null)
                {
                    model.ProfilePic = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    Session["ProfilePic"] = model.ProfilePic;
                }
                if (postedFile1 != null)
                {
                    model.Signature = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile1.FileName);
                    postedFile1.SaveAs(Path.Combine(Server.MapPath(model.Signature)));

                }

                DataSet dsRegistration = model.AssociateRegistration();
                if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                {
                    if (model.Email != null)
                    {
                        string mailbody = "";
                        try
                        {
                            mailbody = "Dear  " + dsRegistration.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as Dolphin Associate.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsRegistration.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());

                            //var fromAddress = new MailAddress("prakher.afluex@gmail.com");
                            //var toAddress = new MailAddress(model.Email);

                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential("abc@gmail.com", "awneesh1")

                            };

                            //using (var message = new MailMessage("abc@gmail.com", model.Email)
                            //{
                            //    IsBodyHtml = true,
                            //    Subject = "Associate Registration",
                            //    Body = mailbody
                            //})
                            //    smtp.Send(message);
                            TempData["Message"] = "Registration Successfull !";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    FormName = "ConfirmRegistration";
                    Controller = "TraditionalAssociate";
                }
                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        //TempData["Registration"] = "Registration successfull !";
                        //TempData["AssociateDetails"] = "Login ID : " + dsRegistration.Tables[0].Rows[0]["LoginId"].ToString() + "\nPassword : " + dsRegistration.Tables[0].Rows[0]["Password"].ToString();


                        Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());

                        string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        string mob = model.Contact;
                        string TempId = "1707166036743941195";
                        try
                        {
                            string str = BLSMS.AssociateRegistration(name, id, pass);

                            BLSMS.SendSMS(mob, str,TempId);
                        }
                        catch { }
                        FormName = "ConfirmRegistration";
                        Controller = "TraditionalAssociate";
                    }
                    else if (dsRegistration.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateRegistration";
                        Controller = "TraditionalAssociate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult AssociateList()
        {
            TraditionalAssociate model = new TraditionalAssociate();
            //List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            //DataSet ds = model.GetList();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        TraditionalAssociate obj = new TraditionalAssociate();

            //        obj.UserID = r["PK_UserId"].ToString();
            //        obj.AssociateID = r["AssociateId"].ToString();
            //        obj.AssociateName = r["AssociateName"].ToString();
            //        obj.SponsorID = r["SponsorId"].ToString();
            //        obj.SponsorName = r["SponsorName"].ToString();
            //        //   obj.LoginID = r["LoginId"].ToString();
            //        //  obj.DesignationID = r["FK_DesignationID"].ToString();
            //        // obj.FirstName = r["AName"].ToString();
            //        obj.isBlocked = r["isBlocked"].ToString();
            //        obj.Contact = r["Mobile"].ToString();
            //        obj.Email = r["Email"].ToString();
            //        obj.PanNo = r["PanNumber"].ToString();
            //        obj.City = r["City"].ToString();
            //        obj.State = r["State"].ToString();
            //        obj.Address = r["Address"].ToString();
            //        // obj.PanNo = r["PanNumber"].ToString();
            //        obj.BranchName = r["BranchName"].ToString();
            //        obj.DesignationName = r["DesignationName"].ToString();
            //        obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
            //        obj.Password = Crypto.Decrypt(r["Password"].ToString());
            //        lst.Add(obj);
            //    }
            //    model.lstTrad = lst;
            //}
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
        [ActionName("AssociateList")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            model.BranchID = model.BranchID == "0" ? null : model.BranchID;
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
                    obj.AdharNumber = r["AdharNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
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

            return View(model);
        }

        public ActionResult BlockUser(string FK_UserID, string LoginID)
        {
            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.LoginID = LoginID;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.BlockUser();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User blocked";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "TraditionalAssociate");
        }

        public ActionResult UnblockUser(string FK_UserID, string LoginID)
        {
            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.LoginID = LoginID;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UnblockUser();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User unblocked";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "TraditionalAssociate");
        }


        [HttpPost]
        [ActionName("AssociateRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult Update(HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1, TraditionalAssociate model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.UserID = Crypto.Decrypt(model.UserID);
                model.AddedBy = Session["Pk_AdminId"].ToString();
                if (postedFile != null)
                {
                    model.ProfilePic = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    Session["ProfilePic"] = model.ProfilePic;
                }
                if (postedFile1 != null)
                {
                    model.Signature = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile1.FileName);
                    postedFile1.SaveAs(Path.Combine(Server.MapPath(model.Signature)));

                }
                DataSet ds = model.UpdateAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = " Updated successfully !";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "AssociateRegistration";
            Controller = "TraditionalAssociate";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult Delete(string UserID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                TraditionalAssociate obj = new TraditionalAssociate();
                obj.UserID = UserID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteAssociate();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate deleted successfully";
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "TraditionalAssociate";
            }
            return RedirectToAction(FormName, Controller);
        }

        #region AssociateUplineDetail

        public ActionResult AdvisorUplineDetail(TraditionalAssociate model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("AdvisorUplineDetail")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();

                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }

            return View(model);
        }

        #endregion

        #region AssociateDownlineDetail


        public ActionResult AdvisorDownlineDetail(TraditionalAssociate model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("AdvisorDownlineDetail")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetDownlineList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();

            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.Status = r["Status"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }

            return View(model);
        }

        #endregion

        #region AssociateTeamTree

        public ActionResult AssociateTeamTree(TraditionalAssociate model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("AssociateTeamTree")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetTreeList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();

            //DataSet ds = model.GetDownlineDetails();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        TraditionalAssociate obj = new TraditionalAssociate();
            //        obj.AssociateID = r["AssociateId"].ToString();
            //        obj.AssociateName = r["AssociateName"].ToString();
            //        obj.DesignationName = r["DesignationName"].ToString();
            //        obj.Percentage = r["Percentage"].ToString();
            //        obj.BranchName = r["BranchName"].ToString();
            //        obj.Contact = r["Mobile"].ToString();
            //        lst.Add(obj);
            //    }
            //    model.lstTrad = lst;
            //}

            return View(model);
        }


        #endregion

        public ActionResult LevelTree()
        {
            return View();
        }


    }
}
