using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class MonthlyMIS
    {
    }
    public class Months
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public bool Deactive { get; set; }
    }
    public class MonthlyMISDataModel
    {
        public decimal? ActualSale { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Variance { get; set; }
        public int? Covers { get; set; }
        public decimal? DineInSale { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? NetProfit { get; set; }
        public decimal? SalePerSQft { get; set; }
        public decimal? APC { get; set; }
        public decimal? DeliverySale { get; set; }
        public decimal? NetDiscountAmount { get; set; }
        public decimal? NetChargeAmount { get; set; }
        public decimal? DirectCharge { get; set; }
        public decimal? TakeAway { get; set; }
        public decimal? OtherSale { get; set; }
        public decimal? ADC { get; set; }
        public decimal? SALEPERC { get; set; }
        public decimal? SALEVARPERC { get; set; }
        public decimal? GROSSPERC { get; set; }
        public decimal? NETPERC { get; set; }
        public decimal? DININPERC { get; set; }
        public decimal? DELIVERYPERC { get; set; }
        public decimal? ADSWeekdays { get; set; }
        public decimal? ADSWeekend { get; set; }
        public decimal? NetSale { get; set; }
        public decimal? DISCOUNTAMTPERC { get; set; }
    }

    public class Last12MonthBudgetSaleComparison
    {
        public int MonthId { get; set; }
        public string Date { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class MonthlyMTDSalesVsBudget
    {
        public string Category { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? Percentage { get; set; }
    }
}