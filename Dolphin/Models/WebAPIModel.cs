using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Dolphin.Models
{
	public class WebAPIModel
	{

	}
	public class LoginAPI
	{
		public string Status {get; set;}
		public string ErrorMessage {get; set;}
		public string SuccessMessage {get; set;}
		public string LoginId {get; set;}
		public string Password {get; set;}
		public string Pk_userId {get; set;}
		public string UserType {get; set;}
		public string FullName {get; set;}
		public string ProfilePic {get; set;}
		public string StatusColor {get; set;}
		public DataSet LoginAction()
		{
			SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
			new SqlParameter("@Password",Password)
			};
            DataSet ds = Connection.ExecuteQuery("LoginAsAssociateForMobile", para);
            return ds;
		}
	}
	public class AssociateSignup
	{
		public string BranchID {get; set;}
		public string UserID {get; set;}
		public string DesignationID {get; set;}
		public string FirstName {get; set;}
		public string LastName {get; set;}
		public string Contact {get; set;}
		public string Email {get; set;}
		public string PinCode {get; set;}
		public string State {get; set;}
		public string City {get; set;}
		public string Address {get; set;}
		public string PanNo {get; set;}
		public string PanImage {get; set;}
        public string Password {get; set;}                          
		public string AdharNumber {get; set;}
		public string BankAccountNo{get;set;}
		public string AddedBy {get; set;}
		public string BankName {get; set;}
		public string BankBranch {get; set;}
		public string IFSCCode {get; set;}
		public string Signature {get; set;}
		public string ProfilePic {get; set;}
	}
	public class ResponseRegistration
	{
	public string Status{get;set;}
	public string SuccessMessage {get; set;}
	public string LoginId {get; set;}
	public string Name{get; set;}
	public string Password{get; set;}
	public string Pk_userId {get; set;}
	public string ErrorMessage {get; set;}
	public DataSet AssociateRegistration(AssociateSignup reg)
		{
		  SqlParameter[] para = { new SqlParameter("@BranchID", reg.BranchID) ,
                                  new SqlParameter("@SponsorID", reg.UserID) ,
                                  new SqlParameter("@DesignationID", reg.DesignationID) ,
                                  new SqlParameter("@RoleID", 2) ,
                                  new SqlParameter("@FirstName", reg.FirstName) ,
                                  new SqlParameter("@LastName", reg.LastName) ,
                                  new SqlParameter("@Contact", reg.Contact) ,
                                  new SqlParameter("@Email", reg.Email) ,
                                  new SqlParameter("@Pincode", reg.PinCode) ,
                                  new SqlParameter("@State", reg.State) ,
                                  new SqlParameter("@City", reg.City) ,
                                  new SqlParameter("@Address", reg.Address) ,
                                  new SqlParameter("@PanNo", reg.PanNo) ,
                                  new SqlParameter("@PanImage", reg.PanImage) ,
                                  new SqlParameter("@AddedBy", reg.AddedBy) ,
								  new SqlParameter("@Password", reg.Password) ,
                                  new SqlParameter("@AdharNumber", reg.AdharNumber) ,
                                  new SqlParameter("@BankAccountNo", reg.BankAccountNo) ,
                                  new SqlParameter("@BankName", reg.BankName) ,
                                  new SqlParameter("@BankBranch", reg.BankBranch) ,
                                  new SqlParameter("@IFSCCode", reg.IFSCCode),
                                  new SqlParameter("@ProfilePic", reg.ProfilePic) ,
                                  new SqlParameter("@Signature", reg.Signature)
                                  };
            DataSet ds = Connection.ExecuteQuery("AssociateRegistrationTraditional", para);
            return ds;
		}
	}
	public class ChangePassword
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set;}
        public string Pk_userId { get; set; }
		public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public DataSet ChangePasswordAssociate()
        {
            SqlParameter[] para ={new SqlParameter("@OldPassword",OldPassword),
                                new SqlParameter("@NewPassword",NewPassword),
                                new SqlParameter("@UpdatedBy",Pk_userId),
            };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordAssociate",para);
            return ds;
        }
    }
	public class ForgetPass
	{
       public string Status { get; set; }
	   public string ErrorMessage { get; set; }
	   public string SuccessMessage {get; set;}
	   public string LoginID {get;set;}
       public string Contact { get; set; }

       public DataSet ForgetPassword()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                  new SqlParameter("@Mobile", Contact)};
            DataSet ds =  Connection.ExecuteQuery("ForgotPassword", para);
            return ds;
        }
	}	
	public class AssociateDashboard
	{
	   public string Status{get;set;}
	    public string ErrorMessage {get; set;}
	    public string AssociateID { get; set; }
        public string Name { get; set; }


        public List<DueInstallment> lstdueinstallment {get; set;}
		public List<NewsDetails> lstnewsdetail {get; set;}
		public List<Associate> lstassociate {get; set;}
		
        
		public DataSet GetAssociateForDashboard()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDashboardForMobile", para);
            return ds;
        }
		
	}
	public class NewsDetails
	{
	  public string PK_NewsID { get; set; }
      public string NewsHeading { get; set; }
      public string NewsBody { get; set; }
      public string UploadFile { get; set; }
        
    }
	public class DueInstallment
	{
	  public string CustomerID { get; set; }
      public string CustomerLoginID { get; set; }
      public string CustomerName { get; set; }
      public string PlotNumber { get; set; }
      public string InstallmentNo { get; set; }
      public string InstallmentAmount { get; set; }
	}
	public class Associate
	{
	  public string TotalAssociate{get;set;}
	  public string TotalBusiness{get;set;}
	  public string TotalActiveId{get;set;}
	  public string SelfBusiness {get;set;}
	  public string TeamBusiness{get;set;}
	  public string TotalBooking {get;set;}
	  public string TeamBooking{get;set;}
	  public string SelfBooking{get;set;}
	  public string Totalregistry{get;set;}
	  public string SelfRegistry{get;set;}
	  public string TeamRegistry{get;set;}
     
    }
	public class Graph
	{
	public string Status {get; set;}
	public string ErrorMessage{get;set;}
	public string LoginId {get; set;}
	public List<GraphData> lstdata{get;set;}
	public DataSet BindGraphDetails()
     {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForAssociateDashboard", para);
            return ds;
      }
	}
	 public class GraphData
	 {
	 	 public string Title {get; set;}
		 public List<GraphDetails> lstgraphdetails {get; set;}
	 }
	 public class GraphDetails
	 {
	  public string Total {get; set;}
	 public string Status{get;set;}
	 }
	 public class GetViaPin
	 {
	 	public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Result { get; set; }
		 public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }
	 }
	 public class Site
	 {
	  public string Status{get;set;}
	  public string ErrorMessage{get;set;}
	  public List<SiteList> lstsite{get; set;}
	    public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }
	 }
	 public class SiteList
	 {
	 public string PK_SiteID {get; set;}
	 public string SiteName {get; set;}
	 }
	 public class SitePLCCharge
	 {
		 public string SiteID {get; set;}
	 	 public string Rate {get; set;}
		 public string Result {get; set;}
		 public string ErrorMessage {get; set;}
		 public List<PLCChargeList> lstPlcCharge {get; set;}
		  public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }
		 public DataSet GetPLCChargeList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetPlotPLCCharge", para);
            return ds;

        }
	 }
	 public class SiteType
	 {
	    public string Status{get;set;}
	    public string ErrorMessage{get; set;}
	    public List<SiteTypeList> lstsitetype{get;set;}
	 	public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeList");
            return ds;
        }
	 }
	 public class SiteTypeList
	 {
	 public string PK_SiteTypeID {get; set;}
	 public string SiteTypeName {get; set;}
	 }
	 public class PLCChargeList
	 {
	 	 public string SiteName{get; set;}
		 public string PLCName{get; set;}
		 public string PLCCharge {get; set;}
		 public string PLCID {get; set;}
	 }
	 public class BookingListDetails
	 {
	   
       public string BranchName {get; set;}
       public string CustomerID {get; set;}
       public string AssociateID {get; set;}
       public string AssociateLoginID {get; set;}
       public string AssociateName {get; set;}
	   public string BookingStatus {get; set;}
       public string PK_BookingId {get; set;}
      
       public string BookingDate{get; set;}
       public string BookingAmount{get; set;}
       public string PaymentPlanID{get; set;}
       public string BookingNumber{get; set;}
       public string PaidAmount {get; set;}
       public string PlotArea {get; set;}
       public string PlotAmount{get; set;}
       public string NetPlotAmount{get; set;}
       public string PK_PLCCharge{get; set;}
       public string PlotRate{get; set;}
	   public string PlotNumber {get; set;}
	   public string CustomerName {get; set;}
       public string CustomerLoginID {get; set;}
       public string BranchID {get; set;}
	 }
	public class BookingList
	{
	   public string Message{get; set;}
	   public string SiteID{get; set;}
       public string BookingNumber{get; set;}
	   public string SectorID {get; set;}
	   public string BlockID {get; set;}
	   public string FromDate {get; set;}
	   public string ToDate {get; set;}
       public string BranchID {get; set;}
	   public string PlotNumber {get; set;}
	   public string PK_BookingId {get; set;}
	   public string LoginId {get; set;}
       public string CustomerLoginID {get; set;}
       public string CustomerName {get; set;}
	    public List<BookingListDetails> lstbooking{get; set;}
		public DataSet List()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                     new SqlParameter("@AssociateID", LoginId)   ,

                                     new SqlParameter("@CustomerLoginID", CustomerLoginID)   ,
                                    new SqlParameter("@CustomerName", CustomerName)   ,
                                    new SqlParameter("@PK_SiteID", SiteID)   ,
                                    new SqlParameter("@PK_SectorID", SectorID)   ,
                                    new SqlParameter("@PK_BlockID", BlockID)   ,
                                    new SqlParameter("@PlotNumber", PlotNumber)   ,
                                    new SqlParameter("@BookingNumber", BookingNumber)   ,
                                    new SqlParameter("@FromDate", FromDate)   ,
                                    new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForAssociate", para);
            return ds;
        }
		}
		public class LedgerReport
		{
		public string Message {get; set;}
		public string BookingNumber {get; set;}
		public string LoginId{get; set;}
		public string SiteID{get; set;}
		public string SectorID{get; set;}
		public string BlockID{get; set;}
		public string PK_BookingId{get; set;}
		public string AssociateID {get;set;}
		public string CustomerID {get;set;}
		public string PlotNumber {get; set;}
		public string Result{get; set;}
		public string PlotAmount{get; set;}
		public string ActualPlotRate {get; set;}
		public string PlotRate{get; set;}
		public string PayAmount {get; set;}
		public string BookingDate {get; set;}
		public string BookingAmount {get; set;}
		public string PaymentDate {get; set;}
		public string PaidAmount {get; set;}
		public string Discount {get; set;}
		public string PaymentPlanID {get; set;}
		public string PlanName {get; set;}
		public string TotalAllotmentAmount {get; set;}
		public string PaidAllotmentAmount{get; set;}
		public string BalanceAllotmentAmount {get; set;}
		public string TotalInstallment{get; set;}
		public string InstallmentAmount {get; set;}
		public string PlotArea {get; set;}
		public string Balance{get; set;}
		public List<PaymentDetails> lstpayment{get; set;}
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
		 public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
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
		}
	
		public class PaymentDetails
		{
		public string PK_BookingDetailsId {get; set;}
		public string PK_BookingId{get; set;}
		public string InstallmentNo{get; set;}
		public string InstallmentDate{get; set;}
		public string PaymentDate{get; set;}
		public string PaidAmount{get; set;}
		public string InstallmentAmount{get; set;}
		public string PaymentMode{get; set;}
		public string DueAmount{get; set;}
		}
		public class Downline
		{
		public string Status{get; set;}
	    public string LoginId{get; set;}
		public string Message{get; set;}
		public List<DownlineDetails> lstdownline{get; set;}
        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }
		 public DataSet GetDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
		}
		public class DownlineDetails
		{
			public string AssociateID { get; set; }
			public string AssociateName { get; set; }
			public string DesignationName { get; set; }
			public string Percentage { get; set; }
			public string BranchName { get; set; }
			public string Contact { get; set; }
		}
		public class TreeList
		{
		public string LoginId { get; set; }
		public List<TreeDetails> lsttree{get;set;}

	    public DataSet GetTreeDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@RootAgentCode", LoginId),
                                      new SqlParameter("@AgentCode", LoginId),
                                  };
            DataSet ds = Connection.ExecuteQuery("BrokerTree", para);
            return ds;
        }
		}
		public class TreeDetails
		{
			 public string Fk_ParentId { get; set; }
			 public string Fk_UserId { get; set; }
			 public string DisplayName{get;set;}
		}
		public class DownlineTree
		{
		public string Fk_UserId { get; set; }
		public string LoginId { get; set; }	
		public List<DownlineTreeDetails> lstdownline {get;set;}
        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId),
                                          new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }
		}
		public class DownlineTreeDetails
		{
		public string Fk_UserId{get;set;}
		public string Fk_SponsorId{get;set;}
		public string LoginId{get;set;}
		public string FirstName{get;set;}
		public string Status{get;set;}
		public string ActiveStatus{get;set;}
		}
		public class Reward
		{
		public string Status{get;set;}
		public string RewardID{get;set;}
		public string UserID{get;set;}
		public string ErrorMessage{get;set;}
		public List<RewardData> lstreward{get;set;}
		public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardId", RewardID),
                                        new SqlParameter("@FK_UserId", UserID)};
            DataSet ds = Connection.ExecuteQuery("_GetRewardData", para);
            return ds;
        }
		}
		public class RewardData
		{
		public string Status{get;set;}
		public string QualifyDate{get;set;}
		public string RewardImage{get;set;}
		public string RewardName{get;set;}
		public string Contact{get;set;}
		public string PK_RewardItemId{get;set;}
		}
		public class RewardClaim
		{
		public string Fk_UserId{get;set;}
		public string Status{get;set;}
		public string PK_RewardItemId{get;set;}
		public string ErrorMessage{get;set;}
		public string SuccessMessage{get;set;}
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
		}
		public class UnpaidIncome
		{
		public string Message {get; set;}
		public string FromID { get; set; }
        public string ToID { get; set; }
		public string FromDate { get; set; }
		public string ToDate { get; set; }
		public string UserID{get;set;}
		public List<UnpaidIncomeDetails> lstunpaid{get;set;}
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
		}
		public class UnpaidIncomeDetails
		{
		public string FromID { get; set; }
        public string FromName { get; set; }
        public string ToID { get; set; }
        public string ToName { get; set; }
        public string Amount { get; set; }
		public string Income { get; set; }
		public string FromDate { get; set; }
		public string ToDate { get; set; }
		public string DifferencePercentage{get; set;}
		}
		public class PayoutBalance
		{
		public string Status{get;set;}
		public string UserID{get;set;}
		public string Balance{get;set;}
		public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
		}
		public class PayoutRequest
		{
		public string Status {get;set;}
		public string UserID {get;set;}
		public string Amount {get;set;}
		public string SuccessMessage {get;set;}
		public string ErrorMessage {get;set;}
		public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", UserID),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
		}
		public class Payout
		{
		public string UserID {get;set;}
		public string PayOutNo{get;set;}
		public string FromDate{get;set;}
		public string ToDate {get;set;}
		public string LoginId {get;set;}
		public List<PayoutDetailData> lstpayout {get; set;}
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
		}
		public class PayoutDetailData
		{
			public string PayOutNo {get;set;}
			public string ClosingDate {get; set;}
			public string AssociateLoginID {get; set;}
			public string FirstName {get; set;}
			public string GrossAmount {get; set;}
			public string TDS {get; set;}
			public string Processing {get; set;}
			public string NetAmount {get; set;}
		}
		public class PayoutLedgerData
		{
	    public string UserID {get;set;}
		public List<PayoutLedgerDetail> lstpayoutledger{get;set;}
		public DataSet PayoutLedger()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutLedgerAssociate", para);
            return ds;
        }
		}
		public class PayoutLedgerDetail
		{
		public string PayoutWalletID{get; set;}
		public string Narration{get;set;}
		public string Credit{get;set;}
		public string TransactionDate {get; set;}
		public string TType{get; set;}
		public string Debit{get; set;}
		}
		public class PayoutRequestReports
		{
		public string Message {get; set;}
		public string LoginId {get;set;}
		public string FromDate {get;set;}
		public string ToDate {get;set;}
		public string Status {get;set;}
		public List<PayoutRequestReportData> lstpayout {get;set;}
		public DataSet PayoutRequestReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@Status", Status),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
		}
		public class PayoutRequestReportData
		{
			public string RequestID {get;set;}
			public string ClosingDate {get;set;}
			public string AssociateLoginID {get;set;}
			public string FirstName {get;set;}
			public string GrossAmount {get;set;}
			public string Status {get;set;}
			public string DisplayName{get;set;}
		}
		//public class KYCDocument
		//{
		//public string ErrorMessage{get;set;}
		//public string SuccessMessage{get;set;}
		//public string UserID{get;set;}
		//public string AdharNumber {get;set;}
		//public string AdharImage {get;set;}
		//public string AdharStatus{get;set;}
		//public string ActionStatus { get; set; }
		//public string PanNumber{get;set;}
		//public string PanImage{get;set;}
		//public string PanStatus{get;set;}
		//public string DocumentNumber{get;set;}
		//public string DocumentImage{get;set;}
		//public string DocumentStatus{get;set;}
		//public DataSet UploadKYCDocuments()
  //      {
  //          SqlParameter[] para = { new SqlParameter("@FK_UserID",UserID ) ,
  //                                    new SqlParameter("@AdharNumber", AdharNumber) ,
  //                                    new SqlParameter("@AdharImage", AdharImage) ,
  //                                    new SqlParameter("@PanNumber", PanNumber),
  //                                    new SqlParameter("@PanImage", PanImage) ,
  //                                    new SqlParameter("@DocumentNumber", DocumentNumber) ,
  //                                    new SqlParameter("@DocumentImage", DocumentImage),
  //                                      new SqlParameter("@Action", ActionStatus),

  //                                };
  //          DataSet ds = Connection.ExecuteQuery("UploadKYC", para);
  //          return ds;

  //      }
		//}
		public class Enquiry
		{
		public List<EnquiryList> lstBlock1 { get; set; }
		public DataSet EnquiryList()
        {

            DataSet ds = Connection.ExecuteQuery("EnquiryList");
            return ds;
        }
		
		}
		public class EnquiryList
		{
		public string EnquiryID {get;set;}
		public string Name {get;set;}
		public string Details{get;set;}
		}
		public class EnquiryData
		{
			public string AddedBy{get;set;} 
			public string SuccessMessage{get;set;}
			public string ErrorMessage {get;set;}
			public string Details{get;set;}
			public string Name{get;set;}
			public DataSet SaveEnquiry()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Details", Details),
                                        new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("EnquiryMaster", para);
            return ds;
        }

		}
		public class Sector
		{
		public string Status {get; set;}
		public string ErrorMessage {get; set;}
		public string SiteID {get; set;}
		public List<SectorList> lstsite {get;set;}
		public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }
		}
		public class SectorList
		{
		  public string PK_SectorID {get; set;}
		  public string SectorName {get; set;}
		}
		public class Block
		{
			public string Status {get; set;}
			public string ErrorMessage {get; set;}
			public string SiteID {get; set;}
			public string SectorID {get; set;}
			public string PK_BlockID {get; set;}
			public List<BlockList> lstBlock{get;set;}
			 public DataSet GetBlockList()
            {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",PK_BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
            }
		}
		public class BlockList
		{
			public string PK_BlockID {get; set;}
			public string BlockName {get; set;}
		}
		public class PlotDetails
		{
		    public string Message {get; set;}
		    public string SiteID {get; set;}
			public string SectorID {get; set;}
			public string BlockID {get; set;}
			public string SiteTypeID {get; set;}
			public string PlotNumber {get; set;}
			public string LoginId {get; set;}
		    public List<PlotList> lstPlot {get;set;}
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
		}
		public class PlotList
		{
			public string PlotID {get; set;}
			public string PlotStatus {get; set;}
			public string ColorCSS {get; set;}
			public string PlotAmount {get; set;}
			public string PlotArea {get; set;}
			public string SiteName {get; set;}
			public string BlockName {get; set;}
			public string SectorName {get; set;}
            public string PlotNumber { get; set; }

        }
		public class Profile
		{
		public string UserID {get; set;}
		public string LoginId {get;set;}
		public string SponsorID{get;set;}
		public string SponsorName {get;set;}
		public string FirstName {get;set;}
		public string LastName {get;set;}
		public string DesignationID {get;set;}
		public string DesignationName {get;set;}
		public string BranchID {get;set;}
		public string Contact {get;set;}
		public string Email {get;set;}
		public string State {get;set;}
		public string City {get;set;}
		public string Address {get;set;}
		public string Pincode {get;set;}
		public string PanNo {get;set;}
		public string BranchName {get;set;}
		public string ProfilePic {get;set;}
		public string AdharNumber {get;set;}
		public string BankAccountNo {get;set;}
		public string BankName {get;set;}
		public string BankBranch {get;set;}
		public string IFSCCode {get;set;}
		public string ErrorMessage {get;set;}
		public string SuccessMessage {get;set;}
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
                                            new SqlParameter("@UpdatedBy", UserID) ,
                                            new SqlParameter("@ProfilePic", ProfilePic),
                                              new SqlParameter("@MemberAccNo", BankAccountNo),
                                                 new SqlParameter("@MemberBankName", BankName),
                                                  new SqlParameter("@MemberBranch", BankBranch),
                                                     new SqlParameter("@IFSCCode", IFSCCode)
                                  };
            DataSet ds = Connection.ExecuteQuery("EditAssociateDetailsForProfile", para);
            return ds;
        }
		}
		public class SummaryReport
		{
		    public string Message {get; set;}
			public string SiteID {get; set;}
			public string SectorID {get; set;}
			public string BlockID {get; set;}
			public string AssociateID {get; set;}
			public string Downline {get; set;}
			public string CustomerID {get; set;}
			public string PK_BookingId {get; set;}
			public string FromDate {get; set;}
			public string ToDate {get; set;}
			public string CustomerName {get; set;}
			public string AssociateName {get; set;}
			public string Mobile {get; set;}
			public string PlotNumber {get; set;}
			public string BookingNumber {get; set;}
			public List<SummaryList> lstSummary {get; set;}
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
		}
		public class SummaryList
		{
		    public string BranchName {get; set;}
			public string PK_BookingId {get; set;}
			public string AssociateName{get;set;}
			public string CustomerName {get; set;}
			public string PaidAmount {get; set;}
			public string PaymentDate {get; set;}
			public string PlotNumber {get; set;}
			public string PlotAmount {get; set;}
			public string Balance {get; set;}
			public string Amount {get; set;}
			public string BookingNumber {get; set;}
			public string Discount {get; set;}
		}
		public class Branch
		{
		public string Status {get; set;}
		public string PK_BranchID{get; set;}
		public string BranchName{get; set;}
		public string ErrorMessage{get; set;}
	     public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }
		}
		public class Designation
		{
		public string Status {get; set;}
		public string Percentage{get; set;}
		public string ErrorMessage{get; set;}
		public List<DesignationDetails> lstdesign{get;set;}
	     public DataSet GetDesignationList()
        {

            SqlParameter[] para = {
                                      
                                      new SqlParameter("@Percentage", Percentage)
                                  
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList",para);

            return ds;
        }

		}
		public class DesignationDetails
		{
		public string PK_DesignationID{get; set;} 
		public string DesignationName{get; set;}
		}
		public class TestLoginAPI 
	   {
		public string Status {get; set;}
		public string ErrorMessage {get; set;}
		public string SuccessMessage {get; set;}
		public string LoginId {get; set;}
		public string Password {get; set;}
		public string Pk_userId {get; set;}
		public string UserType {get; set;}
		public string FullName {get; set;}
		public string ProfilePic {get; set;}
		public string StatusColor {get; set;}
		public DataSet LoginActionTest()
		{
			SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId)
			};
            DataSet ds = Connection.ExecuteQuery("LoginAsAssociateForMobileTest", para);
            return ds;
		}
 
    }



    ///////////////////////////////////////////////////////////////

    public class Gettownshipbooking
    {
        //public string BookingId { get; set; }
        public DataSet GettownshipbookingDetails()
        {
            DataSet ds = Connection.ExecuteQuery("NewTownshipBooking");
            return ds;
        }
    }

    public class GettownshipbookingStatus
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalBookingId { get; set; }

    }
    ///////////////////////////////////////////////////////////////

    public class SaveKYC
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserID { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharBacksideImage { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string DocumentNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string IFSCCode { get; set; }
        public string DocumentImage { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }

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
    }

    public class KYCListAPI
    {
        public string UserID { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public List<lstKycDocument> lstKycdocuments { get; set; }

        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", UserID) };
            DataSet ds = Connection.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }
    }

    public class lstKycDocument
    {
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharBacksideImage { get; set; }
        public string AdharStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
    }

    public class VisitorListAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Downline { get; set; }
        public string TotalAmount { get; set; }
        public List<lstvisitor> lstvisitor { get; set; }

        public DataSet VisitorList()
        {
            SqlParameter[] para =
           {   
                new SqlParameter("@Associateid",LoginID),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@IsDownline",Downline),
            };
            DataSet ds = Connection.ExecuteQuery("VisitorListForAssociate", para);
            return ds;
        }
      
    }

    public class lstvisitor
    {
        public string AssociateLoginID { get; set; }
        public string PK_VisitorID { get; set; }
        public string AssociateName { get; set; }
        public string SiteName { get; set; }
        public string VisitDate { get; set; }
        public string Amount { get; set; }
    }

    public class PrintVisitor
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PK_VisitorId { get; set; }
        public string SiteName { get; set; }
        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string Amount { get; set; }
        public string VisitDate { get; set; }
        public List<visitorlst> visitorlst { get; set; }

        public DataSet VisitorListById()
        {
            SqlParameter[] para =
            {
                    new SqlParameter("@PK_VisitorId",PK_VisitorId),
                };
            DataSet ds = Connection.ExecuteQuery("VisitorListById", para);
            return ds;
        }
    }

    public class visitorlst
    {
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }

    public class AdvancePayment
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginID { get; set; }
        public string TotalAmount { get; set; }
        public List<lstAdvancePayment> lstAdvancePayment { get; set; }

        public DataSet GetAdvancePaymentList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID)
                                };
            DataSet ds = Connection.ExecuteQuery("GetAdvancePaymentList", para);
            return ds;
        }
    }

    public class lstAdvancePayment
    {
        public string LoginID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }
    }

    public class BusinessReport
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<lstBusinessReport> lstBusinessReport { get; set; }
        public DataSet GetBusiness()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetBusiness", para);
            return ds;
        }
    }

    public class lstBusinessReport
    {
        public string UserLoginId { get; set; }
        public string LoginId { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string TeamBusiness { get; set; }
        public string DirectMemberJoining { get; set; }
        public string TeamMemberJoining { get; set; }
    }

    public class DownBusiness
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<lstDownBusiness> lstDownBusiness { get; set; }

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
    }

    public class lstDownBusiness
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string TotalAllotmentAmount { get; set; }
    }

    public class ViewBussiness
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Fk_UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<lstViewBussiness> lstViewBussiness { get; set; }
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
    }

    public class lstViewBussiness
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Business { get; set; }
    }

    public class GetSelfDownlineBusines
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PK_BookingId { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string PlotNumber { get; set; }
        public string BookingNumber { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string Mobile { get; set; }
        public string Downline { get; set; }
        public string TotalPaidAmount { get; set; }
        public string TotalBalance { get; set; }
        public string TotalPlotAmount { get; set; }
        public string TotalDiscount { get; set; }
        public string TotalAmount { get; set; }
        public List<lstSelfDownlineBusiness> lstSelfDownlineBusiness { get; set; }
        public DataSet GetSelfDownlineBusiness()
        {
            SqlParameter[] para =
                             {
                                 new SqlParameter("@PK_BookingId",PK_BookingId),
                                 new SqlParameter("@CustomerID",CustomerLoginID ),
                                 new SqlParameter("@AssociateID",LoginID ),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate),
                                 new SqlParameter("@CustomerName",CustomerName),
                                 new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber),
                                 new SqlParameter("@PK_SiteID",SiteID),
                                 new SqlParameter("@PK_SectorID",SectorID),
                                 new SqlParameter("@PK_BlockID",BlockID),
                                 new SqlParameter("@Mobile",Mobile),
                                 new SqlParameter("@IsDownline",Downline)
                            };

            DataSet ds = Connection.ExecuteQuery("GetSelfDownlineBusiness", para);
            return ds;
        }
    }

    public class lstSelfDownlineBusiness
    {
        public string PK_BookingId { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string BranchName { get; set; }
        public string PaidAmount { get; set; }
        public string PaymentDate { get; set; }
        public string PlotAmount { get; set; }
        public string Amount { get; set; }
        public string PlotNumber { get; set; }
        public string Balance { get; set; }
        public string Discount { get; set; }
        public string BookingNumber { get; set; }
    }

}

