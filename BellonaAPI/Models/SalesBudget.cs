using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class SalesBudget
    {
        public int SalesBudgetID { get; set; }
        public int OutletID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalBudgetAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<SalesCategoryBudget> SalesCategoryBudget { get; set; }
        public List<SalesDayBudget> SalesDayBudget { get; set; }
    }
    public class SalesCategoryBudget
    {
        public int SalesCategoryBudgetID { get; set; }
        public int SalesBudgetID { get; set; }
        public int SalesCategoryID { get; set; }
        public string SalesCategory { get; set; }
        public decimal? CategorySalesPercentage { get; set; }
        public decimal? CategorySalesAmount { get; set; }
    }
    public class SalesDayBudget
    {
        public int SalesDayBudgetID { get; set; }
        public int SalesBudgetID { get; set; }
        public string Day { get; set; }
        public int NumberOfDays { get; set; }
        public decimal? DaySalesPercentage { get; set; }
        public decimal? DaySalesAmount { get; set; }
    }
    public class SalesBudgetDetail
    {
        public long SalesBudgetDetailsID { get; set; }
        public int SalesBudgetID { get; set; }
        public int SalesCategoryID { get; set; }
        public string SalesCategory { get; set; }
        public decimal CategoryAmount { get; set; }
        public DateTime Date { get; set; }
        public string strDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int WeekNo { get; set; }
        public int DayID { get; set; }
        public string DayName { get; set; }
        public int DayNo { get; set; }
    }
    public class SalesCategoryModel
    {
        public int SalesCategoryID { get; set; }
        public string SalesCategory { get; set; }
    }
}