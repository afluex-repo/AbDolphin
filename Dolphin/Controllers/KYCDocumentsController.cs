using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Dolphin.Models;
using Dolphin.Filter;
using System.IO;
using System.Collections;

namespace Dolphin.Controllers
{
    public class KYCDocumentsController : BaseController
    {
        //
        // GET: /KYCDocuments/

        public ActionResult KYCDocuments()
        {
            KYCDocuments objKYC = new KYCDocuments();

            //List<Profile> lstprofile = new List<Profile>();
            objKYC.PKUserID = Session["Pk_userId"].ToString();
            KYCDocuments obj = new KYCDocuments();
            DataSet ds = objKYC.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult KYCDocuments(IEnumerable<HttpPostedFileBase> postedFile,KYCDocuments obj)
        {
            string FormName = "";
            string Controller = "";
            int count = 0;
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        if (count == 0)
                        {
                            obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                        }
                        if (count == 1)
                        {
                            obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                        }
                        if (count == 2)
                        {
                            obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                        }
                        //if ((((System.Web.HttpPostedFileBase[])(postedFile))[0]) != null || ((System.Web.HttpPostedFileWrapper)(((System.Web.HttpPostedFileBase[])(postedFile))[0])).ContentLength > 0)
                        //{
                        //    //obj.AdharImage = (((System.Web.HttpPostedFileBase[])(postedFile))[0]).ToString();    
                        //    obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        //    file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                        //}
                        //if ((((System.Web.HttpPostedFileBase[])(postedFile))[1]) != null || ((System.Web.HttpPostedFileWrapper)(((System.Web.HttpPostedFileBase[])(postedFile))[1])).ContentLength > 0)
                        //{
                        //    //obj.AdharImage = (((System.Web.HttpPostedFileBase[])(postedFile))[0]).ToString();    
                        //    obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        //    file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                        //}
                        //if ((((System.Web.HttpPostedFileBase[])(postedFile))[2]).ToString() != null || ((System.Web.HttpPostedFileWrapper)(((System.Web.HttpPostedFileBase[])(postedFile))[2])).ContentLength > 0)
                        //{
                        //    //obj.AdharImage = (((System.Web.HttpPostedFileBase[])(postedFile))[0]).ToString();    
                        //    obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        //    file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                        //}

                    }
                    count++;
                }
                
                obj.PKUserID = Session["Pk_userId"].ToString();
                
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "KYCDocuments";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "KYCDocuments";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "KYCDocuments";
            }
            return RedirectToAction(FormName, Controller);
        }

    }
}
