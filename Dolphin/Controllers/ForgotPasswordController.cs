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
    public class ForgotPasswordController : Controller 
    {
        //
        // GET: /ForgotPassword/

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        [OnAction(ButtonName = "btnGetPassword")]
        public ActionResult ValidateData(ForgotPassword obj)
        {

            DataSet ds = obj.ValidateData();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                     
                    string passwordRecoveryMessage = BLSMS.ForgetPassword(ds.Tables[0].Rows[0]["FirstName"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                    string TempId = "1707166036749633527";
                    BLSMS.SendSMS(ds.Tables[0].Rows[0]["Mobile"].ToString(), passwordRecoveryMessage,TempId);
                    TempData["recoverPassword"] = "Password is sent on your registered mobile no.";
                }
            }
            else
            {
                TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return RedirectToAction("ForgotPassword", "ForgotPassword");
        }
    }
}
