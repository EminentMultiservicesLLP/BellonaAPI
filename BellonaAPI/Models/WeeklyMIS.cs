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
    public class SalesVsBudget
    {
        public string Date { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }
    public class WeeklyCoversTrend
    {
        public string SessionName { get; set; }
        public Dictionary<string, int> SessionDetails { get; set; } = new Dictionary<string, int>();
    }
    public class BeverageVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }
    public class TobaccoVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }
}