using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Dolphin.Models
{
    public class Topup : Common
    {
        public string LoginID { get; set; }
        public string EPinNo { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int PK_UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PaymentDate { get; set; }

        public string PK_AdvancePaymentId { get; set; }
        

        public List<Topup> AdvancePaymentList { get; set; }

        public List<SelectListItem> ddlEPin { get; set; }

        public DataSet GetEPinList()
        {
            SqlParameter[] para = { new SqlParameter("@Status", Status),
                                  new SqlParameter("@Fk_UserId", Fk_UserId) };
            DataSet ds = Connection.ExecuteQuery("GetUnusedUsedPinsTraditional", para);
            return ds;
        }

        public DataSet TopupByEpin()
        {
            SqlParameter[] para = { new SqlParameter("@PK_EpinID", EPinNo),
                                  new SqlParameter("@TopupID", Fk_UserId),
                                  new SqlParameter("@CreatedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("TopupByEpinTraditional", para);
            return ds;
        }


        public DataSet GetNameByLoginId()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                };
            DataSet ds = Connection.ExecuteQuery("GetNameByLoginId", para);
            return ds;
        }

        public DataSet SaveAdvancePayment()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", PK_UserId),
                                      new SqlParameter("@Amount",Amount),
                                      new SqlParameter("@PaymentDate",PaymentDate),
                                     new SqlParameter("@AddedBy", AddedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("SaveAdvancePayment", para);
            return ds;
        }


        public DataSet GetAdvancePaymentList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                      new SqlParameter("@FromDate",FromDate),
                                     new SqlParameter("@ToDate", ToDate)
                                };

            DataSet ds = Connection.ExecuteQuery("GetAdvancePaymentList",para);
            return ds;
        }

    }
}