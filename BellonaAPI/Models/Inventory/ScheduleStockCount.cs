using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Inventory
{
    public class FinancialYear
    {
        public int FinancialYearID { get; set; }
        public string FinancialYearName { get; set; }
    }
    public class StockSchedule
    {
        public int ScheduleID { get; set; }
        public int FinancialYearID { get; set; }
        public string FinancialYearName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public bool Deactive { get; set; }
        public List<StockScheduleDetails> StockScheduleDetails { get; set; }
        public List<OutletList> OutletList { get; set; }
        public string InsertedBy { get; set; }
        public DateTime? InsertedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class StockScheduleDetails
    {
        public int DetailID { get; set; }
        public int ScheduleID { get; set; }
        public string MonthName { get; set; }
        public int ScheduleNumber { get; set; }
        public string ScheduleDate { get; set; }
        public int? ScheduleStatus { get; set; }
        public string ScheduleStatusName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime? AuthorizedOn { get; set; }
        public string OutletName { get; set; }
    }
    public class OutletList
    {
        public int OutletID { get; set; }
        public string OutletName { get; set; }
    }
    public class StockCount
    {
        public int ScheduleDetailID { get; set; }
        public List<StockCountDetails> StockCountDetails { get; set; }
        public string InsertedBy { get; set; }
        public DateTime? InsertedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class StockCountDetails
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal? ClosingQty { get; set; }
        public decimal? OpeningQty { get; set; }
        public int ItemOutletID { get; set; }
        public string strBatchDate { get; set; }
        public decimal? UnitRate { get; set; }
    }
}