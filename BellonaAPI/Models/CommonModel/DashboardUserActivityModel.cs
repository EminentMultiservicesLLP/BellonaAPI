using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.CommonModel
{
    public class DashboardUserActivityModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int MenuId { get; set; }
        public int Brand { get; set; }
        public int City { get; set; }
        public int Cluster { get; set; }
        public string BranchCode { get; set; }
        public int MonthNo { get; set; }
        public string WeekNos { get; set; }
        public int Year { get; set; }
        public string FinancialYear { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string InsertedIpAddress { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedMacName { get; set; }
    }
}