using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolphin.Models;
using System.Data;
using Dolphin.Filter;
using System.Net;
using System.Net.Mail;

namespace Dolphin.Controllers
{
    public class CustomerController : AdminBaseController
    {
        public ActionResult CustomerRegistration(string UserID)
        {
            Customer model = new Customer();
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
                        model.FK_SponsorId = dsPlotDetails.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                        model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                        model.AssociateID = dsPlotDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                        model.AssociateName = dsPlotDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                        model.JoiningDate = dsPlotDetails.Tables[0].Rows[0]["JoiningDate"].ToString();
                    }
                }

                else
                {


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
                Customer model = new Customer();
                model.LoginID = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetSponsorName();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
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
        [ActionName("CustomerRegistration")]
        [OnAction(ButtonName = "btnRegistration")]
        public ActionResult CustomerRegistration(Customer model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                string ctrpass = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(ctrpass);
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet dsRegistration = model.CustomerRegistration();
                if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                {
                    if (model.Email != null)
                    {
                        string mailbody = "";
                        try
                        {
                            mailbody = "Dear  " + dsRegistration.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as Dolphin Customer.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsRegistration.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());

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

                            using (var message = new MailMessage("abc@gmail.com", model.Email)
                            {
                                IsBodyHtml = true,
                                Subject = "Customer Registration",
                                Body = mailbody
                            })
                                smtp.Send(message);
                            TempData["Message"] = "Registration Successfull !";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());

                        string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        string mob = model.Contact;
                        string TempId = "1707166013477489896";
                        string str = BLSMS.CustomerRegistration(name, id, pass);
                        try
                        {
                            BLSMS.SendSMS(mob, str,TempId);
                        }
                        catch { }

                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistration";
            Controller = "Customer";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult CustomerList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CustomerList")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult CustomerList(Customer model)
        {
            List<Customer> lst = new List<Customer>();
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
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    lst.Add(obj);
                }
                model.ListCust = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("CustomerRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult Update(Customer model)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.UserID = Crypto.Decrypt(model.UserID);
                DataSet ds = model.UpdateCustomer();
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
            FormName = "CustomerList";
            Controller = "Customer";

            return RedirectToAction(FormName, Controller);
        }
        
        public ActionResult Delete(string UserID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Customer obj = new Customer();
                obj.UserID = UserID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCustomer();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = "Customer deleted successfully";
                        FormName = "CustomerList";
                        Controller = "Customer";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "CustomerList";
                        Controller = "Customer";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
                FormName = "CustomerList";
                Controller = "Customer";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult BlockUser(string FK_UserID, string LoginID)
        {
            Customer model = new Customer();
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
            return RedirectToAction("CustomerList", "Customer");
        }

        public ActionResult UnblockUser(string FK_UserID, string LoginID)
        {
            Customer model = new Customer();
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
            return RedirectToAction("CustomerList", "Customer");
        }


        public ActionResult EditCustomer()
        {
            return View();
        }





    }
}
