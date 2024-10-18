using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class WeeklyMIS
    {
        public int Id { get; set; }
        //public DateTime InvoiceDay { get; set; }
        public string InvoiceDay { get; set; }
        public Decimal FoodSale { get; set; }
        public Decimal BeverageSale { get; set; }
        public Decimal LiquorSale { get; set; }
        public Decimal TobaccoSale { get; set; }
        public Decimal OtherSale { get; set; }
    }
   

    public class TimeWiseSalesBreakup
    {
        public int Id { get; set; }
        public string SessionName { get; set; }
        public Decimal Session_NetAmount { get; set; }
        public Decimal Total_NetAmount { get; set; }
        public Decimal Percentage { get; set; }
    }
    public class SalesVsBudget
    {
        public string Date { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }

    public class AverageCoverTrend    
    {
        public string InvoiceDay { get; set; }
        public decimal ApcDineIn { get; set; }
        public decimal TotalCovers { get; set; }
        public decimal TotalSale { get; set; }
    }
    public class WeeklyCoversTrend
    {
        public string SessionName { get; set; }
        public Dictionary<string, int> DailyCovers { get; set; }
    }

    public class LiquorVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }
    public class FoodVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }

}