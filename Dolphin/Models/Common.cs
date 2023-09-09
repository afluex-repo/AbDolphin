using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class Common
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string FK_DesignationId { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string Url { get; set; }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length >= 2)
                    DateString = Year + "-" + Month + "-" + Day;
                //DateString = InputDate;

                else
                    DateString = Month + "/" + Day + "/" + Year;

            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }
            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }

        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetTradMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetTradMemberName", para);

            return ds;
        }
        public DataSet BindProduct()
        {

            DataSet ds = Connection.ExecuteQuery("GetProductList");
            return ds;
        }
        public DataSet BindDesignation(string FK_DesignationId)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_DesignationId", FK_DesignationId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignation");
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }


        public static List<SelectListItem> BindPaymentMode()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Cheque", Value = "Cheque" });
            PaymentMode.Add(new SelectListItem { Text = "NEFT", Value = "NEFT" });
            PaymentMode.Add(new SelectListItem { Text = "RTGS", Value = "RTGS" });
            PaymentMode.Add(new SelectListItem { Text = "Demand Draft", Value = "DD" });
            return PaymentMode;
        }
        public static List<SelectListItem> BindPasswordType()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Profile Password", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Transaction Password", Value = "T" });

            return PasswordType;
        }
        public static List<SelectListItem> TransactionType()
        {
            List<SelectListItem> TransactionType = new List<SelectListItem>();
            TransactionType.Add(new SelectListItem { Text = "Select", Value = "0" });
            TransactionType.Add(new SelectListItem { Text = "Credit", Value = "Credit" });
            TransactionType.Add(new SelectListItem { Text = "Debit", Value = "Debit" });

            return TransactionType;
        }
        public static List<SelectListItem> BindKYCStatus()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "All", Value = null });
            PasswordType.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            PasswordType.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            PasswordType.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });
            return PasswordType;
        }

        public static List<SelectListItem> AttendanceStatus()
        {
            List<SelectListItem> AttendType = new List<SelectListItem>();
            AttendType.Add(new SelectListItem { Text = "Select", Value = "0" });
            AttendType.Add(new SelectListItem { Text = "Present", Value = "P" });
            AttendType.Add(new SelectListItem { Text = "Absent", Value = "A" });

            return AttendType;
        }

        public string Fk_UserId { get; set; }


        public static List<SelectListItem> BindPaymentStatus()
        {
            List<SelectListItem> PaymentStatus = new List<SelectListItem>();
            PaymentStatus.Add(new SelectListItem { Text = "All", Value = null });
            PaymentStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            PaymentStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            PaymentStatus.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });

            return PaymentStatus;
        }

        public static List<SelectListItem> RequestStatus()
        {
            List<SelectListItem> RequestStatus = new List<SelectListItem>();
            RequestStatus.Add(new SelectListItem { Text = "All", Value = null });
            RequestStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            RequestStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            RequestStatus.Add(new SelectListItem { Text = "Declined", Value = "Declined" });

            return RequestStatus;
        }

        public static List<SelectListItem> AvailabilityStatus()
        {
            List<SelectListItem> ddlAvailabilityStatus = new List<SelectListItem>();
            ddlAvailabilityStatus.Add(new SelectListItem { Text = "All", Value = null });
            ddlAvailabilityStatus.Add(new SelectListItem { Text = "Available", Value = "A" });
            ddlAvailabilityStatus.Add(new SelectListItem { Text = "Booked", Value = "B" });
            ddlAvailabilityStatus.Add(new SelectListItem { Text = "Hold", Value = "H" });
            ddlAvailabilityStatus.Add(new SelectListItem { Text = "Allotted", Value = "AL" });
            ddlAvailabilityStatus.Add(new SelectListItem { Text = "Registered", Value = "R" });
            return ddlAvailabilityStatus;
        }

        public static List<SelectListItem> NewsForList()
        {
            List<SelectListItem> NewsFor = new List<SelectListItem>();
            NewsFor.Add(new SelectListItem { Text = "Select", Value = null });
            NewsFor.Add(new SelectListItem { Text = "Associate", Value = "Associate" });
            NewsFor.Add(new SelectListItem { Text = "Customer", Value = "Customer" });


            return NewsFor;
        }
        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = Connection.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }


        public class SMSCredential
        {
            public static string UserName = "";
            public static string Password = "";
            public static string SenderId = "";
        }



    }

    public class SoftwareDetails
    {
        public static string CompanyName = "Dolphin InfraTech";
        public static string CompanyAddress = "Gomti Nagar";
        public static string Pin1 = "226010";
        public static string State1 = "UP";
        public static string City1 = "Lucknow";
        public static string ContactNo = "6389002500";
        public static string LandLine = "";
        public static string Website = "abdolphininfratech.com";
        public static string EmailID = "dolphininfratech125@gmail.com";
    }
}