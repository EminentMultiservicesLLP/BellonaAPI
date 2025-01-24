using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class APCBudgetModel
    {
        public int APCBudgetId { get; set; }
        public string FinancialYear { get; set; }
        public decimal APCBudgetValue { get; set; }
        public string WeekNo { get; set; }
        public string OutletCode { get; set; }
        public string OutletName { get; set; }
        public string CityName { get; set; }
        public string ClusterName { get; set; }
        public string BrandName { get; set; }
        public Guid? InsertedBy { get; set; }
    }
    public class APCBudgetList
    {
        public List<APCBudgetModel> APCBudget { get; set; }
        public string  ReturnMessage  { get; set; }
        
    }
}