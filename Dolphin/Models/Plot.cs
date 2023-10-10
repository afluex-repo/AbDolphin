using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class Plot : Common
    {
        public string PlanID { get; set; }
        public string PK_BranchID { get; set; }
        public string SiteName { get; set; }
        public string MLMLoginId { get; set; }
        public string TotalAmount { get; set; }
        public List<Plot> lstAccountHead { get; set; }
        public List<Plot> lstpaidcommission { get; set; }
        public string AccountHeadId { get; set; }
        public string TPlotNumber { get; set; }
        public string TSiteID { get; set; }
        public string TPlotID { get; set; }
        public string TSectorID { get; set; }
        public string TBlockID { get; set; }
        public string TBookingNumber { get; set; }

        public string UserDetails { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string LedgerId { get; set; }

        public string PK_PLanID { get; set; }

        

        #region Properties
        public DataTable dtVisitorDetails { get; set; }
        public string PK_VisitorId { get; set; }
        public DateTime VisiteDate { get; set; }
        public string ApprovedFromDate { get; set; }

        public string ApprovedToDate { get; set; }

        public List<Plot> TermsListDetails { get; set; }
        public List<Plot> Paid { get; set; }
        public string PK_PlotID { get; set; }
        public string ReceiptNo { get; set; }
        public string Amount { get; set; }
        public string PK_BookingId { get; set; }
        public string NetPlotAmount { get; set; }
        public string CssClass { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectedDate { get; set; }
        public string PlotSize { get; set; }
        public string BookingPercent { get; set; }
        public string UserID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public bool IsDownline { get; set; }
        public string Downline { get; set; }
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
        public string PLCName { get; set; }
        public string LoginId { get; set; }
        public string hdBookingNo { get; set; }
        public string PaymentRemarks { get; set; }
        public string ReceiverBank { get; set; }
        public List<SelectListItem> lstBlock { get; set; }

        public List<SelectListItem> ddlSector { get; set; }
        public List<SelectListItem> ddlPlotArea { get; set; }

        public string BookingDate { get; set; }
        public string ActualPlotRate { get; set; }
        public string DevelopmentCharge { get; set; }
        public List<Plot> lstPlot { get; set; }
        public List<Plot> lstVistor { get; set; }
        public string BookingStatus { get; set; }
        public string CancelRemark { get; set; }
        public string CancelDate { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string JoiningFromDate { get; set; }
        public string JoiningToDate { get; set; }
        public string FK_SponsorId { get; set; }
        public string isBlocked { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
        public string JoiningDate { get; set; }
        public string EncryptKey { get; set; }
        public string PlotInfo { get; set; }
        public List<Plot> ListCust { get; set; }
        public string PaymentDetails { get; set; }
        public string InstAmt { get; set; }
        public string FK_BookingId { get; set; }
        public string PaymentDetail { get; set; }
        public string PaidAmtinrs { get; set; }
        public string PayDate { get; set; }
        public string NoOfEMI { get; set; }
        public string VisitDate { get; set; }
        public List<SelectListItem> ddlAccountHead { get; set; }
        public string FK_AccountHeadId { get; set; }
        public string AccountHeadName { get; set; }
        public string PlotDetails { get; set; }

        public string SectorName { get; set; }
        public string BlockName { get; set; }



        #endregion

        #region PlotBooking

        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }

        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }

        public DataSet GetCustomerName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetCustomerDetailsForBooking", para);
            return ds;
        }

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        public DataSet TransferPlot()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_BookingId", PK_BookingId),
                new SqlParameter("@PK_BookingDetailsId", PK_BookingDetailsId),
                  new SqlParameter("@TransferPlotID", PlotID),
                   new SqlParameter("@PK_PlotID", PK_PlotID),
                    new SqlParameter("@AddedBy", AddedBy),
                     new SqlParameter("@TotalPLC", TotalPLC),
                      new SqlParameter("@PlotRate", PlotRate),
            };
            DataSet ds = Connection.ExecuteQuery("TransferPlot", para);
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

        public DataSet GetPaymentPlanList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PLanID", PaymentPlanID) };
            DataSet ds = Connection.ExecuteQuery("GetPaymentPlan", para);
            return ds;
        }

        public DataSet CheckPlotAvailibility()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotStatus", para);
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
        public DataSet getpaymentmodeForreturn()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_paymentID",null)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList", para);
            return ds;
        }
        public DataSet SavePlotBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@CustomerId ",CustomerID),
                                        new SqlParameter("@AssociateId" , AssociateID),
                                        new SqlParameter("@Fk_BranchId" , BranchID),
                                        new SqlParameter("@Fk_PlotId"  , PlotID),
                                        new SqlParameter("@Fk_PlanId" ,PaymentPlanID),
                                        new SqlParameter("@BookingDate"  ,BookingDate),
                                        new SqlParameter("@PlotAmount" ,PlotAmount),
                                        new SqlParameter("@Discount", Discount),
                                        new SqlParameter("@ActualPlotRate"  , ActualPlotRate),
                                        new SqlParameter("@PlotRate"  , PlotRate),
                                        new SqlParameter("@BookingAmt"  , BookingAmount),
                                        new SqlParameter("@PaidAmount"  , PayAmount),
                                        new SqlParameter("@PaymentDate"  , PaymentDate),
                                        new SqlParameter("@PLCCharge"  , TotalPLC),
                                        new SqlParameter("@PaymentMode"  , PaymentMode),
                                        new SqlParameter("@TransactionNo"  , TransactionNumber),
                                        new SqlParameter("@TransactionDate"  , TransactionDate),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@BankBranch"   , BankBranch),
                                        new SqlParameter("@PaymentRemarks", PaymentRemarks),
                                        new SqlParameter("@ReceiverBank", ReceiverBank),
                                        new SqlParameter("@AddedBy",AddedBy),
                                        new SqlParameter("@NoofEMI",NoOfEMI),
                                         new SqlParameter("@AccountHeadName",AccountHeadName),


                            };
            DataSet ds = Connection.ExecuteQuery("PlotBooking", para);
            return ds;
        }

        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@PK_BranchID", BranchID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                       new SqlParameter("@PaymentPlanID", PaymentPlanID),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }


        public DataSet GetDuePaymentList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetDuePaymentList", para);
            return ds;
        }

        public DataSet GetBookingDetailsList1()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForCancelList", para);
            return ds;
        }

        public DataSet UpdatePlotBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_BookingId ",PK_BookingId),
                                        new SqlParameter("@CustomerId ",CustomerID),
                                        new SqlParameter("@AssociateId" , AssociateID),
                                        new SqlParameter("@Fk_BranchId" , BranchID),
                                        new SqlParameter("@Fk_PlotId"  , PlotID),
                                        new SqlParameter("@Fk_PlanId" ,PaymentPlanID),
                                        new SqlParameter("@PlotAmount" ,PlotAmount),
                                        new SqlParameter("@Discount", Discount),
                                        new SqlParameter("@ActualPlotRate"  , ActualPlotRate),
                                        new SqlParameter("@PlotRate"  , PlotRate),
                                        new SqlParameter("@BookingAmt"  , BookingAmount),
                                        new SqlParameter("@BookingDate"  ,  BookingDate),
                                        new SqlParameter("@PaidAmount"  , PayAmount),
                                        new SqlParameter("@PaymentDate"  , PaymentDate),
                                        new SqlParameter("@PLCCharge"  , TotalPLC),
                                        new SqlParameter("@PaymentMode"  , PaymentMode),
                                        new SqlParameter("@TransactionNo"  , TransactionNumber),
                                        new SqlParameter("@TransactionDate"  ,TransactionDate),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@BankBranch" , BankBranch),
                                        new SqlParameter("@UpdatedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePlotBooking", para);
            return ds;
        }

        public DataSet CancelPlotBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_BookingId ",PK_BookingId),
                                        new SqlParameter("@CancelledBy ",AddedBy),
                                        new SqlParameter("@CancelRemark ", CancelRemark)

                            };
            DataSet ds = Connection.ExecuteQuery("CancelPlotBooking", para);
            return ds;
        }

        public DataSet GetCancelledBookingDetailsList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo",BookingNumber),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetCancelledBooking", para);
            return ds;
        }
        #endregion

        #region HoldPlot
        public string HoldFrom { get; set; }
        public string HoldTo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string PK_PlotHoldID { get; set; }

        public DataSet SavePlotHold()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_PlotId ",PlotID),
                                        new SqlParameter("@FK_SiteID ",SiteID),
                                        new SqlParameter("@FK_SectorID" , SectorID),
                                        new SqlParameter("@FK_BlockID" , BlockID),
                                        new SqlParameter("@PlotNumber"  , PlotNumber),
                                        new SqlParameter("@HoldFrom" ,HoldFrom),
                                        new SqlParameter("@HoldTo" ,HoldTo),
                                        new SqlParameter("@Name" ,Name),
                                        new SqlParameter("@Mobile" ,Mobile),
                                        new SqlParameter("@AddedBy",AddedBy)  ,
                                         new SqlParameter("@Remark1",Remark),
                                           new SqlParameter("@HoldAmount",Amount),
                            };
            DataSet ds = Connection.ExecuteQuery("PlotHold", para);
            return ds;
        }
        public DataSet GetPlotHoldList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotHoldID", PK_PlotHoldID),

                                   new SqlParameter("@FK_SiteID" ,SiteID),
                                        new SqlParameter("@FK_SectorID" ,SectorID),
                                        new SqlParameter("@FK_BlockID" ,BlockID),
                                        new SqlParameter("@PlotNumber" ,PlotNumber)


                                  };


            DataSet ds = Connection.ExecuteQuery("getPlotHoldList", para);
            return ds;
        }
        public DataSet DeletePlotHold()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_PlotHoldID ",PK_PlotHoldID),
                                        new SqlParameter("@DeletedBy ",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("DeleteHoldPlot", para);
            return ds;
        }
        #endregion

        #region Plot Allotment
        public string PaidAmount { get; set; }
        public string PlanName { get; set; }
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


        public DataSet PddFillBookedPlotDetails()
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
        public DataSet SavePlotAllotment()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_BookingId ",PK_BookingId),
                                        new SqlParameter("@PaymentDate" , PaymentDate),
                                        new SqlParameter("@PaidAmount"  , PaidAmount),
                                        new SqlParameter("@PaymentMode" ,PaymentMode),
                                        new SqlParameter("@TransactionNo"  ,TransactionNumber),
                                        new SqlParameter("@TransactionDate" ,TransactionDate),
                                        new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@PaymentRemarks",PaymentRemarks),
                                          new SqlParameter("@ReceiverBank",ReceiverBank),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@AddedBy",AddedBy),
                                         new SqlParameter("@AccountHeadName",AccountHeadName)

                            };
            DataSet ds = Connection.ExecuteQuery("PlotAllotment", para);
            return ds;
        }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }
        #endregion

        #region EMI Payment

        public string TotalInstallment { get; set; }
        public string InstallmentAmount { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string BookingNumber { get; set; }  // -------AAAA
        public string PlotArea { get; set; }
        public string Balance { get; set; }
        public string DueAmount { get; set; }

        public DataSet FillBookedPlotDetailsForEmi()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForEMIPayment", para);
            return ds;
        }

        public DataSet SaveEMIPayment()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_BookingId ",PK_BookingId),
                                        new SqlParameter("@PaymentDate" , PaymentDate),
                                        new SqlParameter("@PaidAmount"  , PaidAmount),
                                        new SqlParameter("@PaymentMode" ,PaymentMode),
                                        new SqlParameter("@TransactionNo"  ,TransactionNumber),
                                        new SqlParameter("@TransactionDate" ,TransactionDate),
                                        new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@UpdatedBy",AddedBy),
                                            new SqlParameter("@PaymentRemarks", PaymentRemarks),
                                          new SqlParameter("@ReceiverBank", ReceiverBank),
                                        new SqlParameter("@ReceiptNoManual",ReceiptNo),
                                        new SqlParameter("@AccountHeadName",AccountHeadName)

                            };
            DataSet ds = Connection.ExecuteQuery("EMIPayment", para);
            return ds;
        }

        #endregion

        #region Customer Ledger Report

        public DataSet FillDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),

                                  new SqlParameter("@FK_SiteID",SiteID),
                                   new SqlParameter("@FK_SectorID",SectorID),
                                    new SqlParameter("@FK_BlockID",BlockID),
                                     new SqlParameter("@PlotNumber",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForCustomerLedger", para);
            return ds;
        }

        #endregion

        #region  DueInstallmentReport

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public DataSet FillDueInstDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@FK_SiteID",SiteID),
                                   new SqlParameter("@FK_SectorID",SectorID),
                                   new SqlParameter("@FK_BlockID",BlockID),
                                   new SqlParameter("@PlotNumber",PlotNumber),

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForDueInstallment", para);
            return ds;
        }
        #endregion

        #region Cheque/neft/cashpayment

        public DataSet GetPaymentList()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PaymentMode",PaymentMode),
                                 new SqlParameter("@PK_BranchID",BranchID),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDeatilsForChequeCashPayment", para);
            return ds;
        }

        public string PaymentStatus { get; set; }

        public string Description { get; set; }

        public DataSet ApprovePayment()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingDetailsId",UserID),
                                  new SqlParameter("@Description",Description),
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                    new SqlParameter("@ApprovedDate",ApprovedDate)
                            };
            DataSet ds = Connection.ExecuteQuery("ApprovePayment", para);
            return ds;
        }
        public DataSet ApprovePaymentNew()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                    new SqlParameter("@ApprovedDate",ApprovedDate),
                                    new SqlParameter("@dtPayment",dtTable)
                            };
            DataSet ds = Connection.ExecuteQuery("MultipleApprovePayment", para);
            return ds;
        }
        public DataSet MultipleReject()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                    new SqlParameter("@ApprovedDate",ApprovedDate),
                                    new SqlParameter("@dtPayment",dtTable)
                            };
            DataSet ds = Connection.ExecuteQuery("MultipleRejectPayment", para);
            return ds;
        }
        public DataSet RejectPayment()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingDetailsId",UserID),
                                  new SqlParameter("@Description",Description),
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                     new SqlParameter("@ApprovedDate",ApprovedDate)
                            };
            DataSet ds = Connection.ExecuteQuery("RejectPayment", para);
            return ds;
        }

        #endregion

        #region PaymentReport

        public DataSet GetPaymentReportList()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@CustomerLoginID",CustomerID),
                                 new SqlParameter("@PaymentStatus",PaymentStatus),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@ApprovedToDate",ApprovedToDate),
                                   new SqlParameter("@ApprovedFromDate",ApprovedFromDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDeatilsForPaymentReport", para);
            return ds;
        }

        public string ApproveDescription { get; set; }
        public string RejectDescription { get; set; }

        #endregion

        #region ApproveRejectedPayment

        public DataSet GetList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PaymentMode",PaymentMode),
                                new SqlParameter("@PK_BranchID",BranchID),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsOfRejectedPayment", para);
            return ds;
        }

        public DataSet ApproveRejectPayment()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingDetailsId",UserID),
                                      new SqlParameter("@Description",Description),
                                      new SqlParameter("@UpdatedBy",AddedBy),
                                        new SqlParameter("@ApprovedDate",ApprovedDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("ApproveRejectedPayment", para);
            return ds;
        }

        #endregion

        #region RejectPaymentApproveReport

        public DataSet GetPaymentRejAppReport()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@CustomerLoginID",CustomerID),
                                 new SqlParameter("@PaymentMode ",PaymentMode ),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDeatilsForApprovedRejectPaymentReport", para);
            return ds;
        }

        #endregion

        #region AllotmentReport
        public DataSet List()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingId",PK_BookingId),
                                 new SqlParameter("@CustomerID",CustomerID ),
                                 new SqlParameter("@AssociateID",AssociateID ),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate),
                                 new SqlParameter("@PK_BranchID",BranchID),
                                  new SqlParameter("@PK_SiteID",SiteID),
                                   new SqlParameter("@PK_SectorID",SectorID),
                                    new SqlParameter("@PK_BlockID",BlockID),
                                     new SqlParameter("@PlotNumber",PlotNumber),
                                       new SqlParameter("@BookingNo",BookingNumber),
                                         new SqlParameter("@PK_BookingDetailsId",PK_BookingDetailsId),


                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAllotmentReport", para);
            return ds;
        }

        #endregion

        #region SummaryReport

        public DataSet GetSummaryList()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingId",PK_BookingId),
                                 new SqlParameter("@CustomerID",CustomerID ),
                                 new SqlParameter("@AssociateID",AssociateID ),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate),
                                 new SqlParameter("@CustomerName",CustomerName),
                                 new SqlParameter("@Mobile",Mobile),
                                 new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber),
                                new SqlParameter("@PK_SiteID",SiteID),
                                new SqlParameter("@PK_SectorID",SectorID),
                                new SqlParameter("@PK_BlockID",BlockID),
                                new SqlParameter("@AssociateName",AssociateName),
                                 new SqlParameter("@IsDownline",Downline)
                            };

            DataSet ds = Connection.ExecuteQuery("GetDetailsForSummaryReport", para);
            return ds;
        }

        public DataSet GetSelfDownlineBusiness()
        {
            SqlParameter[] para =
                             {
                                 new SqlParameter("@PK_BookingId",PK_BookingId),
                                 new SqlParameter("@CustomerID",CustomerID ),
                                 new SqlParameter("@AssociateID",AssociateID ),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate),
                                 new SqlParameter("@CustomerName",CustomerName),
                                 new SqlParameter("@Mobile",Mobile),
                                 new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber),
                                new SqlParameter("@PK_SiteID",SiteID),
                                new SqlParameter("@PK_SectorID",SectorID),
                                new SqlParameter("@PK_BlockID",BlockID),
                                new SqlParameter("@AssociateName",AssociateName),
                                 new SqlParameter("@IsDownline",Downline)
                            };

            DataSet ds = Connection.ExecuteQuery("GetSelfDownlineBusiness", para);
            return ds;
        }

        #endregion

        #region PlotTransfer

        public string SiteID1 { get; set; }
        public string SectorID1 { get; set; }
        public string BlockID1 { get; set; }
        public string PlotNumber1 { get; set; }



        #endregion

        public DataSet GetListAgreement()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@PK_SiteID", SiteID),
                                   new SqlParameter("@PK_SectorID", SectorID),
                                  new SqlParameter("@PK_BlockID", BlockID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                  new SqlParameter("@BookingNo", BookingNumber)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotForAgreementLatter", para);
            return ds;
        }

        public DataSet PrintAgreementLatter()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId) };
            DataSet ds = Connection.ExecuteQuery("PrintAgreementLetter", para);
            return ds;
        }

        #region RowHouseBooking
        public string RowHouseBookingID { get; set; }
        public string PK_PLCCharge { get; set; }
        public string Status { get; set; }
        public DataSet SaveRowHouseBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_PlotId ",PlotID),

                            };
            DataSet ds = Connection.ExecuteQuery("", para);
            return ds;
        }
        public DataSet GetRateandPLC()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@SiteID ",SiteID),

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAreaAndPLCforRowHouse", para);
            return ds;
        }
        public DataSet GetRate()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_RowHouseSizeID ",PlotArea),
                            };
            DataSet ds = Connection.ExecuteQuery("GetPriceForRowHouse", para);
            return ds;
        }
        public DataTable dtTable { get; set; }

        public DataSet SaveRowHouse()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_BranchId ",BranchID),
                                         new SqlParameter("@CustomerCode ",CustomerLoginID),
                                          new SqlParameter("@AssociateCode ",AssociateLoginID),
                                           new SqlParameter("@Fk_SiteId ",SiteID),
                                               new SqlParameter("@Amount",Amount),
                                                 new SqlParameter("@Fk_PlanId",PaymentPlanID),
                                                  new SqlParameter("@BookingDate",BookingDate),
                                                     new SqlParameter("@Discount",Discount ),
                                                      new SqlParameter("@PaidAMount",PayAmount),
                                                        new SqlParameter("@PaymentDate",PaymentDate),
                                                    new SqlParameter("@PaymentMode",PaymentMode),
                                                       new SqlParameter("@RowHousePLC",dtTable),
                                                    new SqlParameter("@TransactionNo",TransactionNumber),
                                                    new SqlParameter("@TransactionDate",TransactionDate),
                                                    new SqlParameter("@BankName",BankName),
                                                    new SqlParameter("@BankBranch",BankBranch),
                                                    new SqlParameter("@AddedBy",AddedBy),
                                                      new SqlParameter("@PlotArea",PlotArea),
                                             new SqlParameter("@GroundFloor",GroundFloorArea),
                                               new SqlParameter("@FirstFloor",FirstFloorArea),
                                             new SqlParameter("@BuildUpArea",BuildupArea),
                                               new SqlParameter("@Rate",Rate),
                                                   new SqlParameter("@NoofEMI",NoOfEMI),
                                                new SqlParameter("@PlotNo",PlotNumber),
                            };
            DataSet ds = Connection.ExecuteQuery("BookingForRowHouse", para);
            return ds;
        }
        public DataSet PlotBookingDetailsForDocument()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                new SqlParameter("@BookingNo",BookingNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForDocumentUpload", para);
            return ds;

        }
        public DataSet SaveUploadPlotDocument()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@BookingID",PK_BookingId),
                                new SqlParameter("@File",DocumentFile),
                                new SqlParameter("@AddedBy",AddedBy),

                            };
            DataSet ds = Connection.ExecuteQuery("UploadPlotDocument", para);
            return ds;

        }


        public string DocumentFile { get; set; }

        public string GroundFloorArea { get; set; }
        public string FirstFloorArea { get; set; }
        public string BuildupArea { get; set; }
        public string Rate { get; set; }
        public string Months { get; internal set; }
        #endregion
        public DataSet SaveVisitorDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_SiteID", SiteID),
                                       new SqlParameter("@AssociateID", AssociateID),
                                       new SqlParameter("@Amount", Amount),
                                        new SqlParameter("@AddedBy", AddedBy),
                                       new SqlParameter("@VisiteDate", VisitDate),                                        
                                      new SqlParameter("@DtVisitorDetail",dtVisitorDetails)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveVisitor", para);
            return ds;
        }
        public DataSet VisitorList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("AssociateId",AssociateLoginID),
                new SqlParameter("@IsDownline",Downline),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("VisitorList", para);
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

        public DataSet SaveAccountHead()
        {
            SqlParameter[] para = {
                new SqlParameter("@Name", Name),
                new SqlParameter("@Mobile", Mobile),
                 new SqlParameter("@AddedBy", AddedBy)

            };
            DataSet ds = Connection.ExecuteQuery("SaveAccountHead", para);
            return ds;
        }

        public DataSet GetAccountHeadDetails()
        {
            DataSet ds = Connection.ExecuteQuery("GetAccountHeadDetails");
            return ds;
        }

        public DataSet DeleteAccountHead()
        {
            SqlParameter[] para = {
                new SqlParameter("@AccountHeadId", AccountHeadId),
                 new SqlParameter("@DeletedBy", AddedBy)

            };
            DataSet ds = Connection.ExecuteQuery("DeleteAccountHead", para);
            return ds;
        }

        public DataSet InActivateAccountHead()
        {
            SqlParameter[] para = {
                new SqlParameter("@AccountHeadId", AccountHeadId),
                 new SqlParameter("@DeletedBy", AddedBy)

            };
            DataSet ds = Connection.ExecuteQuery("InActivateAccountHead", para);
            return ds;
        }

        public DataSet ActivateAccountHead()
        {
            SqlParameter[] para = {
                new SqlParameter("@AccountHeadId", AccountHeadId),
                 new SqlParameter("@DeletedBy", AddedBy)

            };
            DataSet ds = Connection.ExecuteQuery("ActivateAccountHead", para);
            return ds;
        }


        public DataSet DeleteVisitor()
        {
            SqlParameter[] para = {
                new SqlParameter("@VisitorId", PK_VisitorId),
                 new SqlParameter("@DeletedBy", AddedBy)

            };
            DataSet ds = Connection.ExecuteQuery("DeleteVisitor", para);
            return ds;
        }

        public DataSet GetAccountHeadDetailsNew()
        {

            DataSet ds = Connection.ExecuteQuery("GetAccountHeadList");
            return ds;
        }
        public DataSet getCommissionPaidReport()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@LoginId",LoginId),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("PaidCommissionReport", para);
            return ds;
        }
        public DataSet PaidCommissionReportTotal()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@LoginId",LoginId),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("PaidCommissionReportTotal", para);
            return ds;
        }

        public DataSet GetCancleBookingDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", FK_BookingId),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetCancelledBookingDetails", para);
            return ds;
        }
        public DataSet GetPaymentForTransfer()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                   new SqlParameter("@BookingNo",BookingNumber)
            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForTransferPayment", para);
            return ds;
        }
        public DataSet TransferPayment()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                    new SqlParameter("@SectorID",TSectorID),
                                      new SqlParameter("@SiteID",TSiteID),
                                        new SqlParameter("@BlockID",TBlockID),
                                          new SqlParameter("@PlotNumber",TPlotNumber),
                                         new SqlParameter("@BookingNo",TBookingNumber),
                                    new SqlParameter("@dtPayment",dtTable),
                                    new SqlParameter("@Remarks",PaymentRemarks)
                            };
            DataSet ds = Connection.ExecuteQuery("Transferpayment", para);
            return ds;
        }
        public DataSet getBalanceForReturn()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                  new SqlParameter("@BookingNo",BookingNumber)
            };
            DataSet ds = Connection.ExecuteQuery("GetCustomerBalanceForReturn", para);
            return ds;
        }
        public DataSet PaymentReturn()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                    new SqlParameter("@BankBranch",BankBranch),
                                      new SqlParameter("@BankName",BankName),
                                       new SqlParameter("@TransactionNo",TransactionNumber),
                                        new SqlParameter("@TransactionDate",TransactionDate),
                                          new SqlParameter("@PaymentRemarks",PaymentRemarks),
                                           new SqlParameter("@PaymentMode",PaymentMode),
                                           new SqlParameter("@SectorID",SectorID),
                                             new SqlParameter("@SiteID",SiteID),
                                               new SqlParameter("@BlockID",BlockID),
                                                new SqlParameter("@PlotNumber",PlotNumber),
                                                  new SqlParameter("@BookingNo",BookingNumber),
                                                   new SqlParameter("@Amount",Amount)
                            };
            DataSet ds = Connection.ExecuteQuery("PaymentReturn", para);
            return ds;
        }

        public DataSet GetCustomerPaymentLedger()
        {
            SqlParameter[] para =
                            {
                                    new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@FK_SiteID",SiteID),
                                      new SqlParameter("@FK_SectorID",SectorID),
                                    new SqlParameter("@FK_BlockID",BlockID),
                                      new SqlParameter("@PlotNumber",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("getReturnPayment", para);
            return ds;
        }
        public DataSet getLedger()
        {
            SqlParameter[] para =
                            {
                                    new SqlParameter("@Fk_UserId",Fk_UserId)
                            };
            DataSet ds = Connection.ExecuteQuery("getCustomerPaymentLedger", para);
            return ds;
        }
       
        public DataSet GetUpdateNewPaymentPlan()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_PlanId", PlotNumber)
                //new SqlParameter("@SiteID", SiteID),
                //new SqlParameter("@SectorID", SectorID),
                //new SqlParameter("@BlockID", BlockID),
                //new SqlParameter("@PlotNumber", PlotNumber),

            };
            DataSet ds = Connection.ExecuteQuery("GetUpdatePaymentPlan", para);
            return ds;
        }


        public DataSet UpdatePaymentPlan()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_BookingId",PK_BookingId),
                                        new SqlParameter("@PaymentPlanID",PaymentPlanID),
                                        new SqlParameter("@Discount"  ,Discount),
                                        new SqlParameter("@UpdatedBY",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePaymentPlan", para);
            return ds;
        }


        public DataSet UpdatePlotBookingDetails()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_BookingId ",PK_BookingId),
                                        new SqlParameter("@CustomerId ",CustomerID),
                                        new SqlParameter("@AssociateId" , AssociateID),
                                        new SqlParameter("@PlotAmount" ,PlotAmount),
                                        new SqlParameter("@NetPlotAmount" ,NetPlotAmount),
                                        new SqlParameter("@Discount", Discount),
                                        new SqlParameter("@PlotRate"  , PlotRate),
                                        new SqlParameter("@PLCCharge"  , TotalPLC),
                                        new SqlParameter("@UpdatedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePlotBookingDetails", para);
            return ds;
        }

    }
}


