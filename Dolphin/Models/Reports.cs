using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class Reports : Common
    {
        public string Pk_PromoterId { get; set; }
        public string Email { get; set; }
        public string DesignationID { get; set; }
        public string NewDesignationID { get; set; }
        public string UserID { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public List<Reports> lstTransfer { get; set; }
        public string RegistryName { get; set; }
        public string BlockName { get; set; }
        public string SectorName { get; set; }
        public string SiteName { get; set; }
        public string SiteTypeID { get; set; }
        public List<Reports> lstPlotAvailabilityReport { get; set; }
        public List<Reports> lstExpenseMasterBusinessReport { get; set; }
        public string BookingNo { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string UserLoginId { get; set; }
        public string Mobile { get; set; }


        public List<Reports> lstRegistry { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string PK_BookingId { get; set; }
        public string BookingNumber { get; set; }
        public List<Reports> lstP { get; set; }
        public List<Reports> lstsummry { get; set; }
        public List<Reports> FullPaymentList { get; set; }
        public string PlotAmount { get; set; }
        public string CustomerID { get; set; }
        public string AssociateID { get; set; }
        public string ActualPlotRate { get; set; }
        public string PlotRate { get; set; }
        public string PayAmount { get; set; }
        public string BranchName { get; set; }
        public string BookingStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLoginID { get; set; }
        public string AssociateName { get; set; }
        public string PK_PLCCharge { get; set; }
        public string NetPlotAmount { get; set; }
        public string AssociateLoginID { get; set; }
        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }
        public string BranchID { get; set; }
        public string SelectedValue { get; set; }

        public string Discount { get; set; }
        public string PaymentPlanID { get; set; }
        public string PlanName { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public string TotalInstallment { get; set; }

        public string Balance { get; set; }
        public string PlotArea { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string PaymentMode { get; set; }
        public string DueAmount { get; set; }
        public string Remarks { get; set; }
        public string isRegistry { get; set; }
        public string PlotNumber { get; set; }
        public string SiteID { get; set; }
        public string PlotID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string gata { get; set; }
        public string Rakbano { get; set; }
        public string RegistryDate { get; set; }
        public string kharijdate { get; set; }
        public string PlotNo { get; set; }
        public string FK_BookingId { get; set; }
        public string registerydate { get; set; }
        public string RecieverBookingNo { get; set; }
        public string TransferBookingNo { get; set; }
        public string TransferDate { get; set; }
        public bool IsDownline { get; set; }
        public string TeamBusiness { get; set; }
        public string DirectMemberJoining { get; set; }
        public string TeamMemberJoining { get; set; }

        public List<Reports> lstSiteAndAssociateWiseBooking { get; set; }
        public string TotalBooking { get; set; }
        public string Pk_DownPromId { get; set; }
        public string AssociateDeatils { get; set; }
        public string DownAssociateDeatils { get; set; }
        public string Date { get; set; }


        #region 31/01/2022
        public string DesignationName { get; set; }
        public string Percentage { get; set; }
        public string Target { get; set; }
        public string Income { get; set; }
        public string Month { get; set; }
        public string MonthNo { get; set; }
        public string Business { get; set; }

        public string RewardID { get; set; }
        public string QualifyDate { get; set;}
        public string RewardImage { get; set; }
        public string Contact { get; set; }
        public string PK_RewardItemId { get; set; }
        public string RewardName { get; set; }
        public List<Reports> lstDownLineBusiness { get; set; }
        #endregion
        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId) };
            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }
        public DataSet getPlotRegistryListById()
        {
            SqlParameter[] para = {
                                   new SqlParameter ("@FK_BookingId", FK_BookingId)
            };
            DataSet ds = Connection.ExecuteQuery("[GetEditRegistryby]", para);

            return ds;
        }
        public DataSet GetFullpaymentList()
        {
            SqlParameter[] para = {
                     new SqlParameter("@SiteID",SiteID),
                     new SqlParameter("@SectorID",SectorID),
                     new SqlParameter("@BlockID",BlockID),
                     new SqlParameter("@PlotNumber",PlotNumber),
                     new SqlParameter("@BookingNo",BookingNumber)
                  };
            DataSet ds = Connection.ExecuteQuery("GetFullpaymentList", para);
            return ds;
        }
        public DataSet UpdatingRegistryStatus()
        {
            SqlParameter[] para = { new SqlParameter("@BookingNo",BookingNo),
            new SqlParameter("@Fk_UserId", Fk_UserId),
            new SqlParameter("@Remarks", Remarks),
             new SqlParameter("@isRegister", isRegistry),
            new SqlParameter("@AddedBy", AddedBy),
           new SqlParameter("@RegistryDate", RegistryDate),
           new SqlParameter("@KharijDate", kharijdate),
           new SqlParameter("@Rakbano", Rakbano),
             new SqlParameter("@gata", gata),
             };
            DataSet ds = Connection.ExecuteQuery("UpdateRegistryStatus", para);
            return ds;
        }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }
        public DataSet UpdateRegisterdPlot()
        {
            SqlParameter[] para = {
                 new SqlParameter("@Remarks", Remarks),
                new SqlParameter("@AddedBy", AddedBy),
                new SqlParameter("@BookingNo",BookingNo),
                 new SqlParameter("@isRegister", isRegistry),
                 new SqlParameter ("@RegistryName",RegistryName),
                    new SqlParameter("@registerydate", registerydate),
                      new SqlParameter("@KharijDate", kharijdate),
                        new SqlParameter("@Rakbano", Rakbano),
                         new SqlParameter("@gata", gata),
                        new SqlParameter("@Fk_BookingId", FK_BookingId),
             };
            DataSet ds = Connection.ExecuteQuery("UpdateRegisterdPlot", para);
            return ds;
        }
        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }
        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
        }
        public DataSet FillBookedPlotDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForAllotment", para);
            return ds;
        }
        public DataSet GetRegistryList()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@LoginId",LoginId),
                                new SqlParameter("@FromDate",FromDate),
                                new SqlParameter("@ToDate",ToDate),
                                new SqlParameter("@BookingNo",BookingNo),
                                 new SqlParameter("@PlotNumber",PlotNumber)//gg

                            };
            DataSet ds = Connection.ExecuteQuery("GetRegisteredPlot", para);
            return ds;
        }
        public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeList");
            return ds;
        }
        public DataSet GetPlotAvailabilityReport()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotID", PlotID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                   new SqlParameter("@Status", Status),
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotAvailabilityReport", para);
            return ds;
        }
        public DataSet BusinessDevelopmentExpenseMaster()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Month", Month)
            };
            DataSet ds = Connection.ExecuteQuery("BusinessDevelopmentExpenseReport", para);
            return ds;
        }
        public DataSet CalculateBusinessDevelopmentExpenseMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Date", FromDate) };
            DataSet ds = Connection.ExecuteQuery("CalculateBusinessDevelopmentExpense", para);
            return ds;
        }
        public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetDirectRewardList", para);
            return ds;
        }
        public DataSet TeamRewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetTeamRewardList", para);
            return ds;
        }
        public DataSet GetBusiness()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate)
                                        //new SqlParameter("@Fk_UserId", AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("GetBusiness", para);
            return ds;
        }

        
        public DataSet GetBusinessDown()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetBusinessDownline", para);
            return ds;
        }
        public DataSet GetDownLineBusinesById()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_UserId", Fk_UserId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetDownBusiness", para);
            return ds;
        }
        public DataSet GetTransferPaymentList()
        {
            SqlParameter[] para =
           {
               new SqlParameter("@SiteID",SiteID),
               new SqlParameter("@SectorID",SectorID),
               new SqlParameter("@BlockID",BlockID),
               new SqlParameter("@PlotNumber",PlotNumber)
            };
            DataSet ds = Connection.ExecuteQuery("getTransferPaymentList",para);
            return ds;
        }

        
        public List<Reports> lstTransferPlot { get; set; }
        public string OldTransferPlot { get; set; }
        public string NewTransferPlot { get; set; }
        public string PlotTransferBy { get; set; }
        public string PlotTransferDate { get; set; }

        public DataSet GetTransferPlotList()
        {
            SqlParameter[] para =
            {
               new SqlParameter("@SiteID",SiteID),
               new SqlParameter("@SectorID",SectorID),
               new SqlParameter("@BlockID",BlockID),
               //new SqlParameter("@FK_SiteTypeID",SiteTypeID),
               new SqlParameter("@PlotNumber",PlotNumber)
            };
            DataSet ds = Connection.ExecuteQuery("TransferPlotList",para);
            return ds;
        }

        public DataSet GetSummryReports()
        {
            SqlParameter[] para = {
                
                           new SqlParameter("@LoginId", UserLoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetSummryReports",para);
            return ds;
        }

        public DataSet GetSiteAndAssociateWiseBookingReport()
        {
            SqlParameter[] para = { new SqlParameter("@AssociateId", AssociateID),
                                  new SqlParameter("@Fk_SiteId", SiteID),
                                  new SqlParameter("@FromDate", FromDate),
                                  new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("AssociateAndSiteWisePlot", para);
            return ds;
        }
        
        public List<SelectListItem> ddlDesignation { get; set; }

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }


        public DataSet GetDesignationList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Percentage", Percentage)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);
            return ds;
        }

        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }

        public string OldDesignationID { get; set; }

        public DataSet UpdateDesignation()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", UserID),
                                      new SqlParameter("@Fk_DesignationIdOLD", OldDesignationID),
                                      new SqlParameter("@Fk_DesignationIdNEW", NewDesignationID),
                                      new SqlParameter("@Remark", Remarks),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateDesignationNew", para);
            return ds;
        }

        public string TDS { get; set; }
        public string PanNumber { get; set; }
        public List<Reports> lstTDSReportDateWise { get; set; }

        public DataSet GetTDSReportDateWise()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetTDSReportDateWise", para);
            return ds;
        }

        public List<Reports> lstpromoter { get; set; }

        public DataSet GetPromoter()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      //new SqlParameter("@DownLoginId", AssociateLoginID),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPromoter", para);
            return ds;
        }

        public DataSet promoterDelete()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@Pk_PromoterId", Pk_DownPromId),
                                       new SqlParameter("@DeletedBy", UpdatedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("DeletePromoter", para);
            return ds;
        }




        public List<Reports> lstDownline { get; set; }
        public string TeamBusinessAmount { get; set; }
        public string TotalBusiness { get; set; }
        public string LoginDetails { get; set; }
        
        public DataSet GetDownLineReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_UserId",Fk_UserId),
                                      new SqlParameter("@LoginID", AssociateID),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetBusinessUserDown", para);
            return ds;
        }

        public DataSet Getpromoters()
        {
            DataSet ds = Connection.ExecuteQuery("Getpromoters");
            return ds;
        }

        public List<Reports> lstpromoterbusiness { get; set; }

        public DataSet GetPromoterbusiness()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      //new SqlParameter("@DownLoginId", AssociateLoginID),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPromoterBusiness", para);
            return ds;
        }

        public DataSet GetDownLineBusinespromoter()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_UserId", Fk_UserId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetDownBusinessPrmoter", para);
            return ds;
        }

    }
}


