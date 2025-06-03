using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.ManPower
{
    public class ManPowerBudgetModel
    {
        public int? OutletID { get; set; }
        public string EntryDate { get; set; }
        public bool IsBudget { get; set; }
        public List<ManPowerBudgetDetailsModel> ManPowerBudgetDetails { get; set; }
        public string CreatedBy { get; set; }
    }
    public class ManPowerBudgetDetailsModel
    {
        public int? BudgetID { get; set; }
        public string EntryDate { get; set; }
        public int? DepartmentDesignationID { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? DesignationID { get; set; }
        public string DesignationName { get; set; }
        public decimal? BudgetCount { get; set; }
        public decimal? ActualCount { get; set; }
    }
}