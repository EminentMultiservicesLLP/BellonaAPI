using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.ManPower
{
    public class ManPowerBudgetDashboardModel
    {
        public int? OutletID { get; set; }
        public string OutletName { get; set; } = string.Empty;
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string ActualEntryDate { get; set; }
        public decimal? ActualValue { get; set; }
        public string BudgetEntryDate { get; set; }
        public decimal? BudgetValue { get; set; }
    }
}