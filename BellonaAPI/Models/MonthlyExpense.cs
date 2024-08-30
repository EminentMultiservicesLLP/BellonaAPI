using BellonaAPI.Models.Masters;
using System;

namespace BellonaAPI.Models
{
    public class MonthlyExpense
    {
        public int MonthlyExpenseId { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseYear { get; set; }

        public decimal Equipement_Other { get; set; }
        public decimal Equipement_Rental { get; set; }
        public decimal Equipement_SmallWare { get; set; }
        public decimal FinanceCharge_BankFees { get; set; }
        public decimal FinanceCharge_BankInterest_Loan { get; set; }
        public decimal FinanceCharge_CashPickUp { get; set; }
        public decimal FinanceCharge_CreditCard { get; set; }
        public decimal FinanceCharge_Depreciation { get; set; }
        public decimal FinanceCharge_Insurance { get; set; }
        public decimal FinanceCharge_Permit { get; set; }
        public decimal FinanceCharge_ProfessionalFees { get; set; }
        public decimal FinanceCharge_LegalFees { get; set; }
        public decimal ITSoftware_AMC { get; set; }
        public decimal ITSoftware_Cloud { get; set; }
        public decimal ITSoftware_ITHardware { get; set; }
        public decimal ITSoftware_License { get; set; }
        public decimal ITSoftware_Rental { get; set; }
        public decimal ITSoftware_WebSite { get; set; }

        public decimal LabourCost_Other_Complimentary_Manag { get; set; }
        public decimal LabourCost_Other_Complimentary_Staff { get; set; }
        public decimal LabourCost_Other_Icentive { get; set; }
        public decimal LabourCost_Other_Other { get; set; }
        public decimal LabourCost_Other_Recuit_Training { get; set; }
        public decimal LabourCost_Other_Staff_AccommodationCost { get; set; }
        public decimal LabourCost_Other_StaffMeal { get; set; }
        public decimal LabourCost_Other_PayRollTax { get; set; }
        public decimal LabourCost_CTC { get; set; }
        public decimal LabourCost_CTC_Service { get; set; }
        public decimal LabourCost_CTC_Kitchen { get; set; }
        public decimal LabourCost_CTC_Management { get; set; }
        public decimal LabourCost_CTC_MIT { get; set; }
        public decimal LabourCost_CTC_VACA_BONUS_13Month { get; set; }

        
public decimal LabourCost_Other_HealthInsurance_Medical { get; set; } 
public decimal LabourCost_Other_WorkerCompensation { get; set; } 
public decimal LabourCost_Other_EmployeeBenefit { get; set; } 
public decimal FinanceCharge_Accounting_Admin  { get; set; } 


        public decimal Maintenance_Annual { get; set; }
        public decimal Maintenance_Cleaning { get; set; }
        public decimal Maintenance_Repair { get; set; }
        public decimal Maintenance_Waste { get; set; }
        public decimal MarketingCost_Advertising { get; set; }
        public decimal MarketingCost_BusinessTieUp { get; set; }
        public decimal MarketingCost_Commission { get; set; }
        public decimal MarketingCost_CommissonOnline { get; set; }
        public decimal MarketingCost_ComplimentaryGuest { get; set; }
        public decimal MarketingCost_Dues { get; set; }
        public decimal MarketingCost_PubRelation { get; set; }
        public decimal MarketingCost_CreditCardDiscount { get; set; }

        public DeliveryPartnersMonthlyExpense[] MarketingCost_DeliveryPartners { get; set; }
        public GuestPartnersMonthlyExpense[] MarketingCost_GuestPartners { get; set; }
        public decimal OtherExpense_Courier { get; set; }
        public decimal OtherExpense_Freight { get; set; }
        public decimal OtherExpense_Laundry { get; set; }
        public decimal OtherExpense_Misc { get; set; }
        public decimal OtherExpense_Travel { get; set; }
        //public decimal OtherExpense_OfficeSupplies { get; set; }
        public decimal OtherExpense_OtherDirect { get; set; }
        public decimal OtherExpense_SuperVisionFees { get; set; }

        public decimal Property_Cam { get; set; }
        public decimal Property_LateFees { get; set; }
        public decimal Property_Rent { get; set; }
        public decimal Property_RentPerc { get; set; }
        public decimal Property_Tax { get; set; }
        public decimal Royalty_Charge { get; set; }
        public decimal Royalty_Others { get; set; }
        public decimal Royalty_Penalty { get; set; }

        public decimal Utilities_Electricity { get; set; }
        public decimal Utilities_GasCoal { get; set; }
        public decimal Utilities_Telephone { get; set; }
        public decimal Utilities_Water { get; set; }

        //public decimal ProductionCost_Beer { get; set; }
        //public decimal ProductionCost_Beverage { get; set; }
        //public decimal ProductionCost_Food { get; set; }
        //public decimal ProductionCost_Liquor { get; set; }
        //public decimal ProductionCost_Other { get; set; }
        //public decimal ProductionCost_Tobacco { get; set; }
        //public decimal ProductionCost_Wine { get; set; }
        //public decimal Supplies_BOH { get; set; }
        //public decimal Supplies_Cleaning { get; set; }
        //public decimal Supplies_Cutlery_Glassware { get; set; }
        //public decimal Supplies_FOH { get; set; }
        //public decimal Supplies_Office { get; set; }
        //public decimal Supplies_Packaging { get; set; }
        //public decimal Supplies_Stationary { get; set; }
        //public decimal Supplies_Uniform { get; set; }

        //public decimal ASPG_DinnerWeekDays { get; set; }
        //public decimal ASPG_DinnerWeekend { get; set; }
        //public decimal ASPG_EveningWeekDays { get; set; }
        //public decimal ASPG_EveningWeekend { get; set; }
        //public decimal ASPG_LunchWeekDays { get; set; }
        //public decimal ASPG_LunchWeekend { get; set; }

        //public decimal Sale_DinnerWeekDays { get; set; }
        //public decimal Sale_DinnerWeekend { get; set; }
        //public decimal Sale_EveningWeekDays { get; set; }
        //public decimal Sale_EveningWeekend { get; set; }
        //public decimal Sale_LunchWeekDays { get; set; }
        //public decimal Sale_LunchWeekend { get; set; }

        public decimal SaleDinein_Food { get; set; }
        public decimal SaleDinein_Beverage { get; set; }
        public decimal SaleDinein_Beer { get; set; }
        public decimal SaleDinein_Wine { get; set; }
        public decimal SaleDinein_Liquor { get; set; }
        public decimal SaleDinein_TOBACCO { get; set; }
        public decimal SaleDinein_Other { get; set; }
        public decimal SaleDinein_Delivery { get; set; }
        public decimal SaleDinein_TakeAway { get; set; }
        public decimal SaleDinein_CashierShortOver { get; set; }

        public decimal CustomerCount_Lunch { get; set; }
        public decimal CustomerCount_Evening { get; set; }
        public decimal CustomerCount_Dinner { get; set; }

        public decimal PurchaseSupplies_CUTLERY { get; set; }
        public decimal PurchaseSupplies_Office { get; set; }
        public decimal PurchaseSupplies_CLEANINGMATERIAL { get; set; }
        public decimal PurchaseSupplies_PACKAGINGMATERIAL { get; set; }
        public decimal PurchaseSupplies_PRINTINGSTATIONARY { get; set; }
        public decimal PurchaseSupplies_FOH { get; set; }
        public decimal PurchaseSupplies_BOH { get; set; }
        public decimal PurchaseSupplies_UNIFORM { get; set; }
        public decimal PurchaseSupplies_OTHER { get; set; }
        public decimal PurchaseDinein_Food { get; set; }
        public decimal PurchaseDinein_Beverage { get; set; }
        public decimal PurchaseDinein_Beer { get; set; }
        public decimal PurchaseDinein_Wine { get; set; }
        public decimal PurchaseDinein_Liquor { get; set; }
        public decimal PurchaseDinein_TOBACCO { get; set; }
        public decimal PurchaseDinein_Other { get; set; }
        public decimal PurchaseDinein_Delivery { get; set; }
        public decimal PurchaseDinein_TakeAway { get; set; }

        //public decimal Utilization_DinnerWeekDays { get; set; }
        //public decimal Utilization_DinnerWeekend { get; set; }
        //public decimal Utilization_EveningWeekDays { get; set; }
        //public decimal Utilization_EveningWeekend { get; set; }
        //public decimal Utilization_LunchWeekDays { get; set; }
        //public decimal Utilization_LunchWeekend { get; set; }

        public MonthlyExpenseList Totals { get; set; }

        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class DeliveryPartnersMonthlyExpense
    {
        public int MonthlyExpenseID { get; set; }
        public int DeliveryPartnerID { get; set; }
        public string DeliveryPartnerName { get; set; }
        public decimal Amount { get; set; }
    }

    public class GuestPartnersMonthlyExpense
    {
        public int MonthlyExpenseID { get; set; }
        public int GuestPartnerID { get; set; }
        public string GuestPartnerName { get; set; }
        public int GuestCount { get; set; }
    }

    public class MonthlyExpenseList
    {
        public int MonthlyExpenseId { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public string ExpensePeriod { get; set; }

        public int ExpenseMonth { get; set; }
        public int ExpenseYear { get; set; }


        public decimal Equipement_Total { get; set; }
        public decimal Sale_Total { get; set; }
        public decimal Purchase_Total { get; set; }
        public decimal PurchaseSupplies_Total { get; set; }
        public decimal FinanceCharge_Total { get; set; }
        public decimal ITSoftware_Total { get; set; }
        public decimal LabourCost_Total { get; set; }
        public decimal Maintenance_Total { get; set; }
        public decimal MarketingCost_Total { get; set; }

        public decimal OtherExpense_Total { get; set; }
        //public decimal ProductionCost_Total { get; set; }
        public decimal Property_Total { get; set; }
        public decimal Royalty_Total { get; set; }
        public decimal Utilities_Total { get; set; }

        //public decimal Supplies_Total { get; set; }
        //public decimal ASPG_Total { get; set; }
        //public decimal Sale_Total { get; set; }
        //public decimal SaleBreakUp_Total { get; set; }
        //public decimal Utilization_Total { get; set; }
    }

}