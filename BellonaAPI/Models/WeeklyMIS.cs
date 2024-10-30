using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class WeeklyMIS
    {
        public int? Id { get; set; }
        //public DateTime InvoiceDay { get; set; }
        public string InvoiceDay { get; set; }
        public Decimal? FoodSale { get; set; }
        public Decimal? BeverageSale { get; set; }
        public Decimal? LiquorSale { get; set; }
        public Decimal? TobaccoSale { get; set; }
        public Decimal? OtherSale { get; set; }
    }
   

    public class TimeWiseSalesBreakup
    {
        public int? Id { get; set; }
        public string SessionName { get; set; }
        public Decimal? Session_NetAmount { get; set; }
        public Decimal? Total_NetAmount { get; set; }
        public Decimal? Percentage { get; set; }
    }
    public class SalesVsBudget
    {
        public string Date { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
    }

    public class AverageCoverTrend    
    {
        public string InvoiceDay { get; set; }
        public decimal? ApcDineIn { get; set; }
        public decimal? TotalCovers { get; set; }
        public decimal? TotalSale { get; set; }
    }
    public class WeeklyCoversTrend
    {
        public string SessionName { get; set; }
        public Dictionary<string, int> SessionDetails { get; set; } = new Dictionary<string, int>();
    }
    public class BeverageVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
    }
    public class TobaccoVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
    }

    public class LiquorVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
    }
    public class FoodVsBudgetTrend
    {
        public string Date { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
    }

    public class MISWeeklyDataModel
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
    }

    public class SaleTrendModel
    {
        public string Date { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? Value { get; set; }
    }
    public class CogsBreakUp
    {
        public string Category { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class UtilityCostModel
    {
        public string UtilityCost { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
    }


    public class MarketingPromotion
    {
        public string BusinessPromotion { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
    }
    public class OtherOperationalCostModel
    {
        public string OtherOperationalCost { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class OccupationalCostModel
    {
        public string OccupationalCost { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class CostBreakUpModel
    {
        public string CostBreakUp { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
    }
}