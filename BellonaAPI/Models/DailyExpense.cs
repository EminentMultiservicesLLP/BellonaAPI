using System;
using System.Collections.Generic;

namespace BellonaAPI.Models
{
    public class DailyExpense
    {
        public int DailyExpenseId { get; set; }
        public bool IsEdit { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public  string ExpenseDateStr { get; set; }
        public int ExpenseDay { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseYear { get; set; }
        public int Week { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal D_Supplies_BOH { get; set; }
        public decimal D_Supplies_Cleaning { get; set; }
        public decimal D_Supplies_Cutlery_Glassware { get; set; }
        public decimal D_Supplies_FOH { get; set; }
        public decimal D_Supplies_Office { get; set; }
        public decimal D_Supplies_Packaging { get; set; }
        public decimal D_Supplies_Stationary { get; set; }
        public decimal D_Supplies_Uniform { get; set; }
        public decimal D_Supplies_Total { get; set; }
        public decimal D_ProductionCost_Beer { get; set; }
        public decimal D_ProductionCost_Beverage { get; set; }
        public decimal D_ProductionCost_Food { get; set; }
        public decimal D_ProductionCost_Liquor { get; set; }
        public decimal D_ProductionCost_Other { get; set; }
        public decimal D_ProductionCost_Tobacco { get; set; }
        public decimal D_ProductionCost_Wine { get; set; }
        public decimal D_ProductionCost_Total { get; set; }
        public decimal D_LabourCost_CTC { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsStock { get; set; }
        public bool IsOpeningStock { get; set; }
        public List<DailyExpense> DailyExpenseEntries { get; set; }
    }

    public class financialYear
    {
        public int YearId { get; set; }
        public int Year { get; set; }
        public int IsCurrentYear { get; set; }
    }
    public class WeekModel
    {
        public int DateRangeId { get; set; }
        public string Period { get; set; }
        public string WeekRange { get; set; }
        public string Dates { get; set; }
        public string Days { get; set; }
        public string WeekNo { get; set; }
        public int IsExist { get; set; }
    }
}