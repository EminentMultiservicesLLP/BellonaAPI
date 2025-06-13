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
        public decimal? NETCHARGEPERC { get; set; }
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
    public class DayWiseSaleTrend
    {
        public string Day { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class Monthly_SalesBreakup
    {
        public string InvoiceDay { get; set; }
        public Decimal? FoodSale { get; set; }
        public Decimal? BeverageSale { get; set; }
        public Decimal? LiquorSale { get; set; }
        public Decimal? TobaccoSale { get; set; }
        public Decimal? OtherSale { get; set; }
    }
    public class Monthly_TimeWiseSalesBreakup
    {
        public int? Id { get; set; }
        public string SessionName { get; set; }
        public Decimal? Session_NetAmount { get; set; }
        public Decimal? Total_NetAmount { get; set; }
        public Decimal? Percentage { get; set; }
    }

    public class Monthly_DeliverySaleBreakup
    {
        public string Source { get; set; }
        public decimal? SaleAmount { get; set; }
        public decimal? Percentage { get; set; }
    }
    public class Monthly_AverageCoverTrend
    {
        public string InvoiceDay { get; set; }
        public decimal? ApcDineIn { get; set; }
        public decimal? ApcBudget { get; set; }
        public decimal? TotalCovers { get; set; }
        public decimal? TotalSale { get; set; }
    }
    public class Monthly_DeliverySaleTrend
    {
        public int MonthId { get; set; }
        public string Month { get; set; }
        public decimal? DeliverySale { get; set; }
    }
    public class Monthly_CoversTrend
    {
        public string SessionName { get; set; }
        public Dictionary<string, int> SessionDetails { get; set; } = new Dictionary<string, int>();
    }
    public class Monthly_YTDChartModel
    {
        public decimal? SaleAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? Percentage { get; set; }
    }
}