using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class Website:Common
    {
        public List<Website> lstPlot { get; set; }
        public string SectorName { get; set; }
        public string BlockName { get; set; }
        public string PlotArea { get; set; }
        public string PlotAmount { get; set; }
        public string ColorCSS { get; set; }
        public string PlotStatus { get; set; }
        public string PlotID { get; set; }
        public string LoginId { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<Website> lstPLC { get; set; }
        public List<Website> lstTopBusiness { get; set; }
        public string PLCID { get; set; }
        public string PLCCharge { get; set; }
        public string PLCName { get; set; }
        public string SiteName { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string Rate { get; set; }
        public string SiteTypeID { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public List<SelectListItem> ddlSite { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public string BookingId { get; set; }

        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }

        public DataSet SiteListForWebsite()
        {
            DataSet ds = Connection.ExecuteQuery("SiteListForWebsite");
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

        public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeList");
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
        public DataSet GetTopBusiness()
        {
            DataSet ds = Connection.ExecuteQuery("GetTopBusinessAssociateList");
            return ds;
        }

        public DataSet GettownshipbookingDetails()
        {
            DataSet ds = Connection.ExecuteQuery("NewTownshipBooking");
            return ds;
        }
    }
}