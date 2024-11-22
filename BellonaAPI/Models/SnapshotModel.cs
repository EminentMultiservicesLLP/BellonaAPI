using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class SnapshotModel
    {
        public int WeekNo { get; set; }
        public int OutletId { get; set; }
        public string FinancialYear { get; set; }
        public string WeekDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public List<WeeklySnapshot> WeeklySnapshotlist { get; set; }
    }
    public class WeeklySnapshot
    {
        public string ClusterName { get; set; }
        public string OutletName { get; set; }
        public int OutletID { get; set; }
        public string SnapshotType { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }

    public class WeeklySalesSnapshot
    {
        public string Category { get; set; }
        public decimal Monday { get; set; }
        public decimal Tuesday { get; set; }
        public decimal Wednesday { get; set; }
        public decimal Thursday { get; set; }
        public decimal Friday { get; set; }
        public decimal Saturday { get; set; }
        public decimal Sunday { get; set; }
    }

    public class Weekdays
    {
        public int DayID { get; set; }
        public string Days { get; set; }
    }
    public class DsrComparisonModel
    {
        public int ID { get; set; }
        public string Attribute { get; set; }
        public decimal CurrentDailySale { get; set; }
        public decimal CurrentDailyBudget { get; set; }
        public decimal LastWeekSale { get; set; }
        public decimal LastYearSale { get; set; }
        public decimal CurrentWeek_WeeklySale { get; set; }
        public decimal WeeklyBudget { get; set; }
        public decimal LastWeek_WeeklySale { get; set; }
        public decimal LastYear_WeeklySale { get; set; }
    }

    public class WeeklySnapshotsViewModel
    {
        public List<WeeklySnapshot> CurrentWeekSnapshot { get; set; }
        public List<WeeklySnapshot> LastWeekSnapshot { get; set; }
        public List<WeeklySnapshot> LastYearWeekSnapshot { get; set; }
    }

}