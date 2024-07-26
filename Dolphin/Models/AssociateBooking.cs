using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class AssociateBooking : Common
    {
        #region Properties
        public string AdharBacksideImage { get; set; }
        public List<AssociateBooking> ClosingWisePayoutlist { get; set; }
        public string SMS { get; set; }
        public string SenderID { get; set; }

        public string Details { get; set; }
        public string SenderAccountNo { get; set; }
        public string SenderName { get; set; }
        public string CommPercentage { get; set; }
        public string PK_PaidPayoutId { get; set; }
        public string Fk_SponsorId { get; set; }
        public string ActiveStatus { get; set; }
        public string ProfilePic { get; set; }
        public string PK_BookingId { get; set; }
        public string UserID { get; set; }
        public bool IsDownline { get; set; }
        public string Downline { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotAmount { get; set; }
        public string NetPlotAmount { get; set; }
        public string PK_PLCCharge { get; set; }
        public string PlotRate { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingAmount { get; set; }
        public string PayAmount { get; set; }
        public string Discount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Remark { get; set; }
        public string TotalPLC { get; set; }
        public string LoginId { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> ddlSite { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public string BookingDate { get; set; }
        public string ActualPlotRate { get; set; }
        public string DevelopmentCharge { get; set; }
        public List<AssociateBooking> lstPlot { get; set; }
        public string BookingStatus { get; set; }
        public string SiteTypeID { get; set; }
        public string BlockName { get; set; }
        public string SectorName { get; set; }
        public string ColorCSS { get; set; }
        public string Rate { get; set; }
        public string SiteName { get; set; }
        public string PLCCharge { get; set; }
        public string PLCName { get; set; }
        public string PLCID { get; set; }
        public List<AssociateBooking> lstPLC { get; set; }
        public List<AssociateBooking> lstVistor { get; set; }
        public string VisitDate { get; set; }
        public string EncryptKey { get; set; }
        public string VisitorId { get; set; }
        public string Mobile { get; set; }
        public string PK_VisitorId { get; set; }
        
        #endregion


        public DataSet List()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                     new SqlParameter("@AssociateID", AssociateID),
                                     new SqlParameter("@CustomerLoginID",CustomerID),
                                     new SqlParameter("@Loginid",LoginId),
                                    new SqlParameter("@CustomerName", CustomerName)   ,
                                    new SqlParameter("@PK_BranchID", BranchID),
                                    new SqlParameter("@PK_SiteID", SiteID)   ,
                                    new SqlParameter("@PK_SectorID", SectorID)   ,
                                    new SqlParameter("@PK_BlockID", BlockID)   ,
                                    new SqlParameter("@PlotNumber", PlotNumber)   ,
                                    new SqlParameter("@BookingNumber", BookingNumber)   ,
                                    new SqlParameter("@FromDate", FromDate)   ,
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@IsDownline",Downline)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForAssociate", para);
            return ds;
        }

        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }



        public string DesignationName { get; set; }

        public string Percentage { get; set; }
        public string PK_RewardItemId { get; set; }
        public string Contact { get; set; }

        public string BookingNumber { get; set; }

        public string PaidAmount { get; set; }

        public string PlanName { get; set; }

        public string TotalAllotmentAmount { get; set; }

        public string PaidAllotmentAmount { get; set; }

        public string BalanceAllotmentAmount { get; set; }

        public string TotalInstallment { get; set; }

        public string InstallmentAmount { get; set; }

        public string PlotArea { get; set; }

        public string Balance { get; set; }

        public string PK_BookingDetailsId { get; set; }

        public string InstallmentNo { get; set; }

        public string InstallmentDate { get; set; }

        public string DueAmount { get; set; }
        public DataSet FillDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@LoginId",LoginId),

                                   new SqlParameter("@FK_SiteID",SiteID),
                                    new SqlParameter("@FK_SectorID",SectorID),
                                     new SqlParameter("@FK_BlockID",BlockID),
                                      new SqlParameter("@PlotNumber",PlotNumber)


                            };
            DataSet ds = Connection.ExecuteQuery("GetLedgerDetailsForAssociate", para);
            return ds;
        }
        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                        new SqlParameter("@CustomerID", CustomerID),
                                          new SqlParameter("@AssociateID", AssociateID)

                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }

        public DataSet GetDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordAssociate", para);
            return ds;
        }

        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public string Total { get; set; }
        public string Date { get; set; }

        public string Status { get; set; }

        public string TotalBooking { get; set; }

        public string Month { get; set; }
        public DataSet GetDetailsForBarGraph()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_AssociateId", AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForGraphOnAssociateDashboard", para);
            return ds;
        }
        public List<AssociateBooking> dataList3 { get; set; }
        public List<AssociateBooking> ListInstallment { get; set; }

        public DataSet GetDueInstallmentList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_AssociateId", AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDueInstallmentForAssociateDashboard", para);
            return ds;
        }
        public DataSet BindGraphDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForAssociateDashboard", para);
            return ds;
        }


        public string SponsorID { get; set; }

        public string SponsorName { get; set; }

        public string DesignationID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PanNo { get; set; }

        public string Address { get; set; }
        public string ActionStatus { get; set; }
        public string NewsFor { get; set; }

        #region EditProfile


        public DataSet GetList()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@AssociateId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDetailsForEditProfile", para);
            return ds;
        }



        public DataSet UpdatePersonalDetails()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@Email", Email) ,
                                       new SqlParameter("@PinCode", Pincode) ,
                                        new SqlParameter("@State", State) ,
                                         new SqlParameter("@City", City) ,
                                          new SqlParameter("@Address", Address) ,
                                           new SqlParameter("@PanNumber", PanNo) ,
                                            new SqlParameter("@UpdatedBy", UpdatedBy) ,
                                            new SqlParameter("@ProfilePic", ProfilePic),
                                              new SqlParameter("@MemberAccNo", BankAccountNo),
                                                 new SqlParameter("@MemberBankName", BankName),
                                                  new SqlParameter("@MemberBranch", BankBranch),
                                                     new SqlParameter("@IFSCCode", IFSCCode)
                                  };
            DataSet ds = Connection.ExecuteQuery("EditAssociateDetailsForProfile", para);
            return ds;
        }



        #endregion


        public List<AssociateBooking> ListNEWS { get; set; }
        public DataSet GetNews()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_NewsID", PK_NewsID) ,
                                      new SqlParameter("@NewsFor", NewsFor)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetNewsList", para);
            return ds;
        }


        public string PK_NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }


        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId),
                                          new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }

        public string Fk_RewardID { get; set; }
        public string RewardID { get; set; }

        public string QualifyDate { get; set; }
        public string Target { get; set; }
        public string RewardName { get; set; }
        public string RewardImage { get; set; }
        public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardId", RewardID),
                                        new SqlParameter("@FK_UserId", UserID)};
            DataSet ds = Connection.ExecuteQuery("_GetRewardData", para);
            return ds;
        }

        public DataSet ClaimReward()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardItemId", PK_RewardItemId),
                                        new SqlParameter("@FK_UserId", Fk_UserId),
                                        new SqlParameter("@Status", Status),
            };
            DataSet ds = Connection.ExecuteQuery("ClaimReward", para);
            return ds;
        }



        public string FromID { get; set; }
        public string FromName { get; set; }
        public string ToID { get; set; }
        public string ToName { get; set; }
        public string Amount { get; set; }
        public string DifferencePercentage { get; set; }
        public string Income { get; set; }
        public string PayoutWalletID { get; set; }
        public string Narration { get; set; }
        public string Credit { get; set; }
        public string TType { get; set; }
        public string Debit { get; set; }
        public string PayOutNo { get; set; }
        public string ClosingDate { get; set; }
        public string GrossAmount { get; set; }
        public string TDS { get; set; }
        public string Processing { get; set; }
        public string NetAmount { get; set; }
        public string RequestID { get; set; }
        public string Action { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentStatus { get; set; }
        public string PK_DocumentID { get; set; }
        public string AccountStatus { get; set; }
        public string DocumentType { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string AccountHolderName { get; set; }

        public string PlotStatus { get; set; }

        public DataSet UnpaidIncomes()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", UserID),
                 new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@FromLoginId", FromID),
                new SqlParameter("@ToLoginId", ToID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetUnpaidIncomes", para);
            return ds;
        }

        public DataSet PayoutLedger()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutLedgerAssociate", para);
            return ds;
        }
        public DataSet PayoutDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_Userid", UserID),
                  new SqlParameter("@PayoutNo", PayOutNo),
                    new SqlParameter("@FromDate", FromDate),
                     new SqlParameter("@ToDate", ToDate),
                      new SqlParameter("@LoginId", LoginId),

                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutDetails", para);
            return ds;
        }

        public DataSet GetPayoutWiseIncomeDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_PaidPayoutId", PK_PaidPayoutId)

                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutWiseIncomeDetails", para);
            return ds;
        }
        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
        public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", UserID),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
        public DataSet PayoutRequestReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", UserID),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@Status", Status),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }

        public DataSet GetPaymentModeList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_paymentID",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList", para);
            return ds;
        }

        public DataSet ApproveRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RequestID", RequestID),
                                    new SqlParameter("@ApprovedBy", AddedBy),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@TransactionDate", TransactionDate)
                                      };
            DataSet ds = Connection.ExecuteQuery("ApprovePayoutRequest", para);
            return ds;
        }
        public DataSet DeclineRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RequestID", RequestID),
                                    new SqlParameter("@ApprovedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("DeclinePayoutRequest", para);
            return ds;
        }
        public DataSet SaveDistributedPayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ToDate),


                                      };
            DataSet ds = Connection.ExecuteQuery("_AutoDistributePayment", para);
            return ds;
        }
        public DataSet TransactionLogReportBy()
        {
            SqlParameter[] para = { new SqlParameter("@ActionName", DisplayName),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate),


                                      };
            DataSet ds = Connection.ExecuteQuery("TransactionReport", para);
            return ds;
        }
        #region kyc
        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", UserID) };
            DataSet ds = Connection.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }

        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = {   new SqlParameter("@FK_UserID",UserID ) ,
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@AdharBacksideImage",AdharBacksideImage),
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage),
                                      new SqlParameter("@BankHolderName", AccountHolderName),
                                      new SqlParameter("@MemberBankName", BankName) ,
                                      new SqlParameter("@MemberBranch", BankBranch) ,
                                      new SqlParameter("@IFSCCode", IFSCCode)
                                  };
            DataSet ds = Connection.ExecuteQuery("UploadKYC", para);
            return ds;

        }


        public DataSet AssociateListForKYC()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                  new SqlParameter("@Status", Status),
                                new SqlParameter("@FromDate",FromDate),
                                  new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAgentListForKYC", para);
            return ds;
        }

        public DataSet ApproveKYC()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@PK_DocumentID", PK_DocumentID),
                                      new SqlParameter("@DocumentType", DocumentType),
                                      new SqlParameter("@Status", Status),
                                      new SqlParameter("@UpdatedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ApproveKYC", para);
            return ds;
        }
        public DataSet RejectKYC()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@PK_DocumentID", PK_DocumentID),
                                      new SqlParameter("@DocumentType", DocumentType),
                                      new SqlParameter("@Status", Status),
                                      new SqlParameter("@UpdatedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("RejectKYC", para);
            return ds;
        }
        #endregion

        public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeList");
            return ds;
        }

        public DataSet GetPLCChargeList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetPlotPLCCharge", para);
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

        public DataSet GetPlotDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@FK_SiteTypeID",SiteTypeID),
                                new SqlParameter("@PlotNumber",PlotNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAvailabilityStatus", para);
            return ds;
        }

        public DataSet GetDetailsForDistributePayment()
        {

            DataSet ds = Connection.ExecuteQuery("GetDetailsForDistributePayment");
            return ds;
        }

        public DataSet GetAssociateForDashboard()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",AssociateID)
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateForDashboard", para);
            return ds;
        }

        public DataSet VisitorList()
        {
            SqlParameter[] para =
            {   new SqlParameter("@PK_UserId",UserID),
                new SqlParameter("@PK_VisitorId",VisitorId),
                //new SqlParameter("AssociateId",AssociateLoginID),
                  new SqlParameter("@Associateid",AssociateLoginID),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                 new SqlParameter("@IsDownline",Downline),
            };
            DataSet ds = Connection.ExecuteQuery("VisitorListForAssociate", para);
            return ds;
        }
        public DataSet VisitorListById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_VisitorId",PK_VisitorId),
            };
            DataSet ds = Connection.ExecuteQuery("VisitorListById", para);
            return ds;
        }

        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }

        public DataSet FillDetailsbyAccociates()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@LoginId",LoginId),

                                   new SqlParameter("@FK_SiteID",SiteID),
                                    new SqlParameter("@FK_SectorID",SectorID),
                                     new SqlParameter("@FK_BlockID",BlockID),
                                      new SqlParameter("@PlotNumber",PlotNumber)


                            };
            DataSet ds = Connection.ExecuteQuery("GetLedgerDetailsByAssociate", para);
            return ds;
        }
    }

}



