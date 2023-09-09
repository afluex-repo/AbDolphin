using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Dolphin.Filter;
using Dolphin.Models;

namespace Dolphin.Controllers
{
    public class TopupController : AdminBaseController
    {

        public ActionResult Topup()
        {
            Topup model = new Models.Topup();

            List<SelectListItem> ddlEpin = new List<SelectListItem>();
            ddlEpin.Add(new SelectListItem { Text = "Select EPin", Value = "0" });
            ViewBag.ddlEpin = ddlEpin;
            return View();
        }

        [HttpPost]
        [ActionName("Topup")]
        [OnAction(ButtonName = "btnTopup")]
        public ActionResult MemberTopup(Topup model)
        {
            List<SelectListItem> ddlEpin = new List<SelectListItem>();
            ddlEpin.Add(new SelectListItem { Text = "Select EPin", Value = "0" });
            ViewBag.ddlEpin = ddlEpin;
            
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.TopupByEpin();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Topup"] = "ID successfully Topup";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Topup"] = ex.Message;
            }
            return View();
        }

        public ActionResult GetEPinList(string UserID)
        {
            try
            {
                Topup model = new Topup();
                model.Fk_UserId = UserID;
                model.Status = "Unused";

                List<SelectListItem> ddlEPin = new List<SelectListItem>();
                DataSet ds = model.GetEPinList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Result = "yes";
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlEPin.Add(new SelectListItem { Text = r["ePinNo"].ToString(), Value = r["PK_ePinId"].ToString() });
                    }
                }
                model.ddlEPin = ddlEPin;
                
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }



        public ActionResult GetNameByLoginId(string LoginID)
        {
            Topup model = new Topup();
            model.LoginID = LoginID;
            DataSet ds = model.GetNameByLoginId();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                if(ds.Tables[0].Rows[0][0].ToString()=="1")
                {
                    model.Result = "yes";
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.PK_UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_UserId"]);
                }
                else if(ds.Tables[0].Rows[0][0].ToString()=="0")
                {
                    model.Result = "no";
                }
            }
            else
            {
                model.Result = "no";
            }
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        


        public ActionResult AdvancePayment()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AdvancePayment")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult AdvancePayment(Topup model)
        {
            try
            {
                model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/MM/yyyy");
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveAdvancePayment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Topup"] = "Paymenyt save successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Topup"] = ex.Message;
            }
            return RedirectToAction("AdvancePayment", "Topup");
        }

        public ActionResult AdvancePaymentList()
        {
            return View();
          
        }
        
        [HttpPost]
        [ActionName("AdvancePaymentList")]
        [OnAction(ButtonName = "getData")]
        public ActionResult AdvancePaymentList(Topup model)
       {
            List<Topup> lst = new List<Topup>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetAdvancePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Topup obj = new Topup();
                    //obj.PK_AdvancePaymentId = r["PK_AdvancePaymentId"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"]);
                    obj.LoginID = r["LoginID"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.Name = r["Name"].ToString();
                    lst.Add(obj);
                }
                model.AdvancePaymentList = lst;
            }
            return View(model);
        }

    }
}
