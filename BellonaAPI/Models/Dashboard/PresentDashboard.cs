using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Dashboard
{
    public class PresentDashboard
    {

    }
    public class PresentMonthAllOutetSale
    {
        public string Company { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Outlet { get; set; }
        public int OutletId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal AsOfBudgetSale { get; set; }
        public decimal AsOfDeliverySale { get; set; }
        public decimal AsOfTakeAwaySale { get; set; }
        public decimal TotalBudgetSale { get; set; }
        public decimal AsOfBudgetExpense { get; set; }
        public decimal TotalBudgetExpense { get; set; }

        public decimal AsOfSale { get; set; }
        public decimal MonthlyDailyExpense { get; set; }
        public decimal AsOfDayMonthlyExpense { get; set; }
        public decimal TotalMonthlyExpense { get; set; }

    }

    public class PresentMonthCashFlow
    {
        public string Company { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Outlet { get; set; }
        public int OutletId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal BudgetSale { get; set; }
        public decimal BudgetExpense { get; set; }

        public decimal ProjectedSale { get; set; }
        public decimal ProjectedDailyExpense { get; set; }
        public decimal ProjectedOtherExpense { get; set; }

    }

    public class PurchaseAndProductionCost
    {
        public string ConsumptionName { get; set; }
        public decimal ConsumptionValue { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal BudgetCost { get; set; }
    }

    public class SaleVsBudget
    {
        public decimal Sales { get; set; }
        public decimal DeliveryPartnerSales { get; set; }
        public decimal TotalSalesBudgeted { get; set; }
        public decimal TotalMonthlyPurchase_Supplies { get; set; }
        public decimal TotalMonthlyExpese { get; set; }
        public decimal MonthlyExpenseOnSalePerc { get; set; }
        public int TotalMonthWiseDays { get; set; }
        public int NoOfDays { get; set; }
        public int RemainingDays { get; set; }
        public ActualSalesDetail ActualSaleCatgoriesWise { get; set; }
        public BudgetModel_Monthly BudgetSaleCatgoriesWise { get; set; }
        public SaleSessionTrend SaleSession { get; set; }
        public GuestTrend GuestData { get; set; }
        public AverageSalePerGuest SaleASPG { get; set; }
        public AverageSalePerGuest BudgetASPG { get; set; }
        public List<DeliveryTrend> DeliveryData { get; set; }
    }

    public class AverageSalePerGuest
    {
        public decimal LunchWeekDay { get; set; }
        public decimal LunchWeekEnd { get; set; }
        public decimal EveningWeekDay { get; set; }
        public decimal EveningWeekEnd { get; set; }
        public decimal DinnerWeekDay { get; set; }
        public decimal DinnerWeekEnd { get; set; }
    }

    public class ActualSalesData
    {
        public List<ActualSalesDetail> SalesData {get;set;}
        public List<DeliveryPartnersDailySales> DeliveryPartnerSales { get; set; }
        public List<GuestPartnersDailySales> GuestPartnerCount { get; set; }
    }
    public class ActualSalesDetail
    {
        public int DSREntryID { get; set; }
        public int OutletID { get; set; }
        public DateTime DSREntryDate { get; set; }
        public decimal SaleLunchDinein { get; set; }
        public decimal SaleEveningDinein { get; set; }
        public decimal SaleDinnerDinein { get; set; }
        public decimal SaleTakeAway { get; set; }

        public decimal SaleFood { get; set; }
        public decimal SaleBeverage { get; set; }
        public decimal SaleWine { get; set; }
        public decimal SaleBeer { get; set; }
        public decimal SaleLiquor { get; set; }
        public decimal SaleTobacco { get; set; }
        public decimal SaleOther { get; set; }

        public int ItemsPerBill { get; set; }
        public int TotalNoOfBills { get; set; }
        public decimal CTCSalary { get; set; }

        public int GuestCountLunch { get; set; }
        public int GuestCountEvening { get; set; }
        public int GuestCountDinner { get; set; }
        public decimal TotalSaleDinein { get; set; }
        public decimal DeliveryPartnerAmount { get; set; }
        public int Week { get; set; }

        public int ActualWeek { get; set; }
        public int SalesMonth { get; set; }
        public int SalesYear { get; set; }
    }

    public  class BudgetVsProjectedExpense
    {
        public string ExpenseType { get; set; }
        public decimal BudgetedExpense { get; set; }
        public decimal ProjectedExpense { get; set; }
        public decimal Variance { get; set; }
        public decimal VariancePercentage { get; set; }
        public bool IsVariancePositive { get; set; }

    }


    public class SaleTrendAnalysis
    {
        public List<SaleTrend> saleTrends { get; set; }
        public List<SaleSessionTrend> saleSessionTrend { get; set; }
        public List<SaleCategoryTrend> saleCategoryTrend { get; set; }
        public List<GuestTrend> guestTrend { get; set; }
        public List<DeliveryTrend> deliveryTrend { get; set; }
        public List<ASPGTrend> saleASPGTrend { get; set; }
        public List<ASPGTrend> budgetASPGTrend { get; set; }
    }

    public class SaleTrend
    {
        public int OutletId { get; set; }
        public int SaleMonth { get; set; }
        public int SaleYear { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal BudgetAmount { get; set; }
    }

    public class SaleSessionTrend
    {
        public int OutletId { get; set; }
        public int SaleMonth { get; set; }
        public int SaleYear { get; set; }
        public decimal LunchDinein { get; set; }
        public decimal EveningDinein { get; set; }
        public decimal DinnerDinein { get; set; }
    }

    public class ASPGTrend
    {
        public int OutletId { get; set; }
        public decimal Lunch { get; set; }
        public decimal Evening { get; set; }
        public decimal Dinner { get; set; }
        public int SaleMonth { get; set; }
        public int SaleYear { get; set; }
    }


    public class SaleCategoryTrend
    {
        public int OutletId { get; set; }
        public decimal Delivery { get; set; }
        public decimal TakeAway { get; set; }
        public decimal Food { get; set; }
        public decimal Beverage { get; set; }
        public decimal Wine { get; set; }
        public decimal Beer { get; set; }
        public decimal Liquor { get; set; }
        public decimal Tobacco { get; set; }
        public decimal Other { get; set; }

        public int SaleMonth { get; set; }
        public int SaleYear { get; set; }
    }

    public class GuestTrend
    {
        public int OutletId { get; set; }
        public int Lunch { get; set; }
        public int Evening { get; set; }
        public int Dinner { get; set; }

        public decimal BudgetLunch { get; set; }
        public decimal BudgetEvening { get; set; }
        public decimal BudgetDinner { get; set; }

        public int SaleMonth { get; set; }
        public int SaleYear { get; set; }
    }

    public class DeliveryTrend
    {
        public int OutletId { get; set; }
        public string PartnerName { get; set; }
        public int PartnerId { get; set; }
        public decimal SaleAmount { get; set; }

        public int SaleMonth { get; set; }
        public int SaleYear { get; set; }
    }

    public class CashFlowBreakup
    {
        public List<CashFlow_Expense_Sale_Breakup> SaleBreakUp { get; set; }
        public List<CashFlow_Expense_Sale_Breakup> ExpenseBreakUp { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalCashFlow { get; set; }
        public decimal TotalSalesBudgeted { get; set; }
        public decimal AsOfDaySalesBudgeted { get; set; }
        public decimal SaleVariance { get; set; }
        public decimal AvgSalePerDay { get; set; }
    }
    public class CashFlow_Expense_Sale_Breakup
    {
        public string Expense_Sale_Type { get; set; }
        public string Expense_Sale_Sequence { get; set; }
        public decimal Expense_Sale_Amount { get; set; }
    }
    public class CashFlow_PnL_Trend
    {
        public List<ExpenseCashFlow_PnL_Trend> expenseCashFlow_PnL_Trend;
        public List<BudgetCashFlow_PnL_Trend> budgetCashFlow_PnL_Trend;
    }

    public class Cost_Of_Goods
    {
        public string Value_Type { get; set; }
        public decimal Food { get; set; }
        public decimal Liquor { get; set; }
        public decimal Beer { get; set; }
        public decimal Beverage { get; set; }
        public decimal Tobbaco { get; set; }
        public decimal Other { get; set; }
        public decimal NonFoodSupplies { get; set; }
    }

    public class ExpenseCashFlow_PnL_Trend
    {
        public decimal CashFlowPnL { get; set; }

        public int CashFlowMonth { get; set; }
        public int CashFlowYear { get; set; }
    }
    public class BudgetCashFlow_PnL_Trend
    {
        public decimal CashFlowPnL { get; set; }

        public int CashFlowMonth { get; set; }
        public int CashFlowYear { get; set; }
    }

    public class CashFlowDetail_Breakup_Trend
    {
        public List<ExpenseDetail_Breakup> ExpenseDetailBreakup_Trend { get; set; }
        public List<SaleDetailBreakup> SaleDetailBreakup_Trend { get; set; }
    }
    public class ExpenseDetail_Breakup
    {
        public decimal P_Supplies_Total { get; set; }
        public decimal P_ProductionCost_Total { get; set; }
        public decimal P_Utilities_Total { get; set; }
        public decimal P_OtherExpense_Total { get; set; }
        public decimal P_MarketingCost_Total { get; set; }
        public decimal P_DeliveyPerc_Total { get; set; }
        public decimal P_FinanceCharge_Total { get; set; }
        public decimal P_Financial_PERC { get; set; }
        public decimal P_Property_Total { get; set; }
        public decimal P_RENTPERC { get; set; }
        public decimal P_Royalty_total { get; set; }
        public decimal P_Royalty_PERC { get; set; }
        public decimal P_LabourCost_Total { get; set; }
        public decimal P_CTC_Total { get; set; }
        public decimal P_SERVICECHARGES_Total { get; set; }
        public decimal P_Equipment_Total { get; set; }
        public decimal P_Maintenance_Total { get; set; }
        public decimal P_ITSoftware_Total { get; set; }
        public decimal P_Total_Expense { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseYear { get; set; }
    }
    public class SaleDetailBreakup
    {
        public decimal P_DINEINE_Total { get; set; }
        public decimal P_TAKEWAY { get; set; }
        public decimal P_DELIVERYSALE { get; set; }
        public decimal P_TOTALSALE { get; set; }
        public int SaleYear { get; set; }
        public int SaleMonth { get; set; }
    }

    public class OthetCost_Breakup
    {
        public string SrNo { get; set; }
        public string CostType { get; set; }
        public decimal Utilities_Total { get; set; }
        public decimal OtherExpense_Total { get; set; }
        public decimal MarketingCost_Total { get; set; }
        public decimal DeliveyPerc_Total { get; set; }
        public decimal FinanceCharge_Total { get; set; }
        public decimal Financial_PERC { get; set; }
        public decimal Property_Total { get; set; }
        public decimal RENTPERC { get; set; }
        public decimal Royalty_total { get; set; }
        public decimal Royalty_PERC { get; set; }
        public decimal LabourCost_Total { get; set; }
        public decimal Equipment_Total { get; set; }
        public decimal Maintenance_Total { get; set; }
        public decimal ITSoftware_Total { get; set; }
    }
}