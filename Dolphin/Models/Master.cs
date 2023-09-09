using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class Master : Common
    {
        #region Properties
        public string Subject { get; set; }
        public string LoginId { get; set; }
        public string Body { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string FileUpload { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string EnquiryID { get; set; }
        public string EncryptKey { get; set; }

        public string ColorCSS { get; set; }
        public string SiteID { get; set; }
        public string PLCID { get; set; }
        public string PLCName { get; set; }
        public string SiteName { get; set; }
        public string Location { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string Rate { get; set; }
        public string PLCCharge { get; set; }
        public string DevelopmentCharge { get; set; }

        public string PlotSizeID { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string PlotStatus { get; set; }
        public string PlotRate { get; set; }
        public string PlotAmount { get; set; }
        public string PlotPrefix { get; set; }
        public string FromPlot { get; set; }
        public string ToPlot { get; set; }
        public string BookingPercent { get; set; }
        public string AllottmentPercent { get; set; }
        public string WidthFeet { get; set; }
        public string WidthInch { get; set; }
        public string HeightFeet { get; set; }
        public string HeightInch { get; set; }
        public string PlotArea { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string SectorID { get; set; }
        public string SectorName { get; set; }
        public string BlockID { get; set; }
        public string BlockName { get; set; }

        public DataTable dtPLCCharge { get; set; }
        public List<Master> lstPLC { get; set; }
        public List<Master> lstSite { get; set; }
        public List<Master> lstPlot { get; set; }
        public List<Master> lstRanklog { get; set; }
        public List<Master> lstRankDetails { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public List<SelectListItem> ddlSite { get; set; }


        public string SiteTypeID { get; set; }
        public string SiteTypeName { get; set; }
        public string NewsFor { get; set; }
        public string PK_DesignationID { get; set; }

        public int RankLogId { get; set; }
        public string EntryDate { get; set; }
        public string DoneBy { get; set; }
        public string OldPercentage { get; set; }
        public string NewPercentage { get; set; }
        public string OldDesignation { get; set; }
        public string NewDesignation { get; set; }
        public string Remark { get; set; }
        public string AssociateName { get; set; }
        public string FK_SponsorId { get; set; }
        
        #endregion

        #region PLCMaster
        public DataSet SavePLC()
        {
            SqlParameter[] para = { new SqlParameter("@PLCName", PLCName),
                                  new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("SavePLC", para);
            return ds;
        }

        public DataSet PLCList()
        {
            DataSet ds = Connection.ExecuteQuery("PLCList");
            return ds;
        }

        public DataSet DeletePLC()
        {
            SqlParameter[] para = { new SqlParameter("@PLCID", PLCID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeletePLC", para);
            return ds;
        }

        public DataSet UpdatePLC()
        {
            SqlParameter[] para = { new SqlParameter("@PLCID", PLCID),
                                      new SqlParameter("@PLCName", PLCName),
                                  new SqlParameter("@UpdatedBy", UpdatedBy) };
            DataSet ds = Connection.ExecuteQuery("UpdatePLC", para);
            return ds;
        }

        #endregion

        #region SiteMaster

        public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeList");
            return ds;
        }
        public DataSet SaveSite()
        {
            SqlParameter[] para = { new SqlParameter("@SitePlcCharge", dtPLCCharge),
                                      new SqlParameter("@SiteName", SiteName),
                                      new SqlParameter("@Location", Location),
                                      new SqlParameter("@Rate", Rate),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@DevelopmentCharge", DevelopmentCharge),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@FK_SiteTypeId", SiteTypeID)

            };
            DataSet ds = Connection.ExecuteQuery("SiteMaster", para);
            return ds;
        }

        public DataSet UpdateSite()
        {
            SqlParameter[] para = {   new SqlParameter("@SitePlcCharge", dtPLCCharge),
                                      new SqlParameter("@SiteID", SiteID),
                                      new SqlParameter("@SiteName", SiteName),
                                      new SqlParameter("@Location", Location),
                                      new SqlParameter("@Rate", Rate),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@DevelopmentCharge", DevelopmentCharge),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@FK_SiteTypeID", SiteTypeID)

            };
            DataSet ds = Connection.ExecuteQuery("UpdateSite", para);
            return ds;
        }

        public DataSet GetSiteList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                    new SqlParameter("@FK_SiteTypeID", SiteTypeID),
            };
            DataSet ds = Connection.ExecuteQuery("SiteList", para);
            return ds;
        }

        public DataSet GetUnitList()
        {
            DataSet ds = Connection.ExecuteQuery("GetUnitList");
            return ds;
        }

        public DataSet GetSitePlcChargeList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("SitePlcChargeList", para);
            return ds;
        }

        public DataSet DeleteSite()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteSite", para);
            return ds;
        }

        #endregion

        #region PlotSizeMaster

        public DataSet SavePlotSize()
        {
            SqlParameter[] para = { new SqlParameter("@WidthFeet", WidthFeet),
                                      new SqlParameter("@WidthInch", WidthInch),
                                      new SqlParameter("@HeightFeet", HeightFeet),
                                      new SqlParameter("@HeightInch", HeightInch),
                                      new SqlParameter("@TotalArea", PlotArea),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("SavePlotSize", para);
            return ds;
        }

        public DataSet UpdatePlotSize()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotSizeMasterID", PlotSizeID),
                                      new SqlParameter("@WidthFeet", WidthFeet),
                                      new SqlParameter("@WidthInch", WidthInch),
                                      new SqlParameter("@HeightFeet", HeightFeet),
                                      new SqlParameter("@HeightInch", HeightInch),
                                      new SqlParameter("@TotalArea", PlotArea),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@UpdatedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("UpdatePlotSize", para);
            return ds;
        }

        public DataSet GetPlotSizeList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotSizeMasterID", PlotSizeID) };
            DataSet ds = Connection.ExecuteQuery("PlotSizeMasterList", para);
            return ds;
        }

        public DataSet DeletePlotSize()
        {
            SqlParameter[] para = { new SqlParameter("@PlotSizeID", PlotSizeID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeletePlotSize", para);
            return ds;
        }

        #endregion

        #region PlotMaster-Insert

        public DataSet SavePlot()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SitePlcCharge",dtPLCCharge),
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotPrefix",PlotPrefix),
                                new SqlParameter("@FromPlot",FromPlot),
                                new SqlParameter("@ToPlot",ToPlot),
                                new SqlParameter("@PlotSizeID",PlotSizeID),
                                new SqlParameter("@PlotRate",PlotRate),
                                new SqlParameter("@PlotAmount",PlotAmount),
                                new SqlParameter("@BookingPercent",BookingPercent),
                                new SqlParameter("@AllottmentPercent",AllottmentPercent),
                                new SqlParameter("@AddedBy",AddedBy)


                            };
            DataSet ds = Connection.ExecuteQuery("PlotMaster", para);
            return ds;
        }

        public DataSet GetPlotSize()
        {
            DataSet ds = Connection.ExecuteQuery("GetPlotSize");
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

        #endregion

        #region PlotMaster-List-Update-Delete

        public DataSet GetPlotList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotID", PlotID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotList", para);
            return ds;
        }

        public DataSet UpdatePlot()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SitePlcCharge",dtPLCCharge),
                                new SqlParameter("@PK_PlotID",PlotID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotSizeID",PlotSizeID),
                                new SqlParameter("@PlotRate",Rate),
                                new SqlParameter("@PlotAmount",PlotAmount),
                                new SqlParameter("@BookingPercent",BookingPercent),
                                new SqlParameter("@AllottmentPercent",AllottmentPercent),
                                new SqlParameter("@UpdatedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePlot", para);
            return ds;
        }

        public DataSet DeletePlot()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_PlotID",PlotID),
                                new SqlParameter("@DeletedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("DeletePlotEntry", para);
            return ds;
        }


        #endregion

        #region plot availability
        public DataSet GetDetails()
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


        #endregion

        #region  Sector

        public DataSet SaveSector()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@FK_SiteID",SiteID),
                                new SqlParameter("@SectorName",SectorName),
                                new SqlParameter("@AddedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("SectorMaster", para);
            return ds;
        }


        public DataSet UpdateSector()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_SectorID",SectorID),
                                new SqlParameter("@FK_SiteID",SiteID),
                                new SqlParameter("@SectorName",SectorName),
                                new SqlParameter("@UpdatedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdateSector", para);
            return ds;
        }

        public List<Master> lstSector { get; set; }
        public List<Master> lstBlock1 { get; set; }
        public DataSet GetSector()
        {
            SqlParameter[] para =
             {
                                new SqlParameter("@PK_SectorID",SectorID)

                            };
            DataSet ds = Connection.ExecuteQuery("SelectSector", para);
            return ds;
        }


        public DataSet DeleteSector()
        {
            SqlParameter[] para = { new SqlParameter("@PK_SectorID", SectorID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteSector", para);
            return ds;
        }

        #endregion

        #region BlockMaster

        public DataSet DeleteBlock()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BlockID", BlockID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteBlock", para);
            return ds;
        }


        public DataSet SaveBlock()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@BlockName", BlockName),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@AddedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("BlockMaster", para);
            return ds;
        }

        public DataSet UpdateBlock()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BlockID", BlockID),
                                      new SqlParameter("@FK_SiteID ", SiteID ),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                        new SqlParameter("@BlockName", BlockName),
                                      new SqlParameter("@UpdatedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateBlockMaster", para);
            return ds;
        }

        #endregion

        #region Branch Master

        public string BranchID { get; set; }
        public string BranchName { get; set; }

        public DataSet GetBranchList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BranchID", BranchID),


                                  };
            DataSet ds = Connection.ExecuteQuery("GetBranchList", para);
            return ds;
        }

        public DataSet SaveBranch()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@BranchName", BranchName),
                                      new SqlParameter("@AddedBy ", AddedBy )


                                  };
            DataSet ds = Connection.ExecuteQuery("BranchMaster", para);
            return ds;
        }
        public DataSet UpdateBranch()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BranchID", BranchID),
                                      new SqlParameter("@BranchName", BranchName),
                                      new SqlParameter("@UpdatedBy ", AddedBy )

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateBranch", para);
            return ds;
        }
        public DataSet DeleteBranch()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_BranchId", BranchID),
                                      new SqlParameter("@DeletedBy ", AddedBy )

                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteBranch", para);
            return ds;
        }
        #endregion

        #region Designation Master

        public string DesignationName { get; set; }
        public string Percentage { get; set; }
        public string DesignationID { get; set; }

        public DataSet GetDesignation()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_DesignationID", DesignationID),


                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);
            return ds;
        }

        public DataSet SaveDesignation()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@DesignationName", DesignationName),
                                       new SqlParameter("@Percentage", Percentage),
                                      new SqlParameter("@AddedBy ", AddedBy )

                                  };
            DataSet ds = Connection.ExecuteQuery("DesignationMaster", para);
            return ds;
        }
        public DataSet UpdateRank()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_DesignationID", DesignationID),
                                       new SqlParameter("@DesignationName", DesignationName),
                                        new SqlParameter("@Percentage", Percentage),
                                      new SqlParameter("@UpdatedBy ", AddedBy )

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateDesignation", para);
            return ds;
        }
        public DataSet DeleteRank()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_DesignationID", DesignationID),
                                       new SqlParameter("@DeletedBy", AddedBy)


                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteDesignation", para);
            return ds;
        }

        #endregion

        #region News Master

        public string NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }


        public DataSet GetNews()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_NewsID", NewsID)

                                  };
            DataSet ds = Connection.ExecuteQuery("GetNewsList", para);
            return ds;
        }


        public DataSet SaveNews()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@NewsHeading", NewsHeading),
                                      new SqlParameter("@NewsBody", NewsBody),
                                      new SqlParameter("@AddedBy", AddedBy),
                                         new SqlParameter("@NewsFor", NewsFor),
                                  };
            DataSet ds = Connection.ExecuteQuery("NewsMaster", para);
            return ds;
        }

        public DataSet UpdateNews()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@NewsID", NewsID),
                                      new SqlParameter("@NewsHeading", NewsHeading),
                                      new SqlParameter("@NewsBody", NewsBody),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                       new SqlParameter("@NewsFor", NewsFor),
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateNews", para);
            return ds;
        }
        public DataSet DeleteNews()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@NewsID", NewsID),
                                      new SqlParameter("@DeletedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteNews", para);
            return ds;
        }
        #endregion

        #region GalleryMaster

        public string PK_GalleryID { get; set; }
        public string SiteImage { get; set; }



        public DataSet GetGallery()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_GalleryID", PK_GalleryID),
                                      new SqlParameter("@FK_SiteID", SiteID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetGalleryImages", para);
            return ds;
        }




        public DataSet SaveGallery()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@SiteImage ", SiteImage),
                                      new SqlParameter("@AddedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("GalleryMaster", para);
            return ds;
        }

        public DataSet DeleteImage()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_GalleryID ", PK_GalleryID),
                                      new SqlParameter("@DeletedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteSiteImage", para);
            return ds;
        }

        #endregion



        public string SiteID1 { get; set; }
        public string SectorID1 { get; set; }
        public string BlockID1 { get; set; }
        public string PlotNumber1 { get; set; }
        public DataSet AutoUnholdPlot()
        {

            DataSet ds = Connection.ExecuteQuery("AutoUnholdPlot");
            return ds;
        }

        public DataSet EnquiryList()
        {

            DataSet ds = Connection.ExecuteQuery("EnquiryList");
            return ds;
        }

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

        public DataSet SaveCareer()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Mobile", Mobile),
                                       new SqlParameter("@Email", Email),
                                      new SqlParameter("@Description", Description),
                                       new SqlParameter("@FileUpload", FileUpload),


                                  };
            DataSet ds = Connection.ExecuteQuery("InsertCareerDetails", para);
            return ds;
        }


        #region RowHouseSize

        public string RowHouseSizeID { get; set; }
        public string GroundFloorArea { get; set; }
        public string FirstFloorArea { get; set; }
        public string Price { get; set; }
        public string BookingPercentage { get; set; }
        public string AllotmentPercentage { get; set; }


        public DataSet GetRowHouseSizeList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_RowHouseSizeID", RowHouseSizeID),
                                  };
            DataSet ds = Connection.ExecuteQuery("RowHouseSizeList", para);
            return ds;
        }
        public DataSet SaveRowHouseSize()
        {
            SqlParameter[] para = {


                                     new SqlParameter("@WidthFeet", WidthFeet),
                                      new SqlParameter("@WidthInch", WidthInch),
                                      new SqlParameter("@HeightFeet", HeightFeet),
                                      new SqlParameter("@HeightInch", HeightInch),
                                      new SqlParameter("@TotalArea",PlotArea),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@GroundFloorArea", GroundFloorArea),
                                      new SqlParameter("@FirstFloorArea", FirstFloorArea),
                                      new SqlParameter("@SiteID", SiteID),
                                      new SqlParameter("@Price", Price),
                                      new SqlParameter("@BookingPercentage", BookingPercentage),
                                      new SqlParameter("@AllotmentPercentage", AllotmentPercentage),

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveRowHouseSize", para);
            return ds;
        }
        public DataSet UpdateRowHouseSize()
        {
            SqlParameter[] para = {


                                     new SqlParameter("@WidthFeet", WidthFeet),
                                      new SqlParameter("@WidthInch", WidthInch),
                                      new SqlParameter("@HeightFeet", HeightFeet),
                                      new SqlParameter("@HeightInch", HeightInch),
                                      new SqlParameter("@TotalArea",PlotArea),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@GroundFloorArea", GroundFloorArea),
                                      new SqlParameter("@FirstFloorArea", FirstFloorArea),
                                      new SqlParameter("@SiteID", SiteID),
                                      new SqlParameter("@Price", Price),
                                      new SqlParameter("@BookingPercentage", BookingPercentage),
                                      new SqlParameter("@AllotmentPercentage", AllotmentPercentage),
                                       new SqlParameter("@PK_RowHouseSizeID", RowHouseSizeID),

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateRowHouseSize", para);
            return ds;
        }
        public DataSet DeleteRowHouseSize()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_RowHouseSizeID", RowHouseSizeID),
                                       new SqlParameter("@DeletedBy", AddedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteRowHouseSize", para);
            return ds;
        }

        #endregion

        #region getuserdetails
        public DataSet GetUserDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("UserDetailsforRank", para);
            return ds;
        }
        public DataSet GetDesignationListForUpdateRank()
        {
            DataSet ds = Connection.ExecuteQuery("SelectDesignation");
            return ds;
        }
        public DataSet UpdateDesignation()
        {
            SqlParameter[] para = {
               new SqlParameter("@Fk_DesignationId",FK_DesignationId),
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@UpdatedBy",UpdatedBy)                      
            };
            DataSet ds = Connection.ExecuteQuery("UpdateRank", para);
            return ds;
        }


        public DataSet RetriveAllDataRankUpdateLog()
        {
            DataSet ds = Connection.ExecuteQuery("SelectRankupdatelog");
            return ds;
        }
        
             public DataSet GetDesignationList()
        {
            SqlParameter[] para = {
               new SqlParameter("@PK_DesignationID",PK_DesignationID),
                new SqlParameter("@Percentage",Percentage)
            };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);
            return ds;
        }

        public DataSet GetDesignationDetails()
        {
            SqlParameter[] para = {
               new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetDesignationDetails", para);
            return ds;
        }
        
        public DataSet ChangeDesignation()
        {
            SqlParameter[] para = {
               new SqlParameter("@Fk_DesignationId",FK_DesignationId),
                new SqlParameter("@Remarks",Remark),
                  new SqlParameter("@LoginId",LoginId),
                     new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("ChangedPercenatage", para);
            return ds;
        }


        public DataSet GetSponsorDetails()
        {
            SqlParameter[] para = {
               new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetSponsorDetails", para);
            return ds;
        }


        public DataSet UpdateSponsor()
        {
            SqlParameter[] para = {
               new SqlParameter("@FK_SponsorLoginId",FK_SponsorId),
                new SqlParameter("@Remarks",Remark),
                  new SqlParameter("@LoginId",LoginId),
                     new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateSponsor", para);
            return ds;
        }
        
        #endregion

    }
}

