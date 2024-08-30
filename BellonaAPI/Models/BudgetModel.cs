using System;
using System.Collections.Generic;

namespace BellonaAPI.Models
{
    public class BudgetList_ForGrid
    {
        public int BudgetId { get; set; }
        public int OutletId { get; set; }
        public string OutletName { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal CashFlow { get; set; }
        public string StartFinancialMonthYear { get; set; }
        public string EndFinancialMonthYear { get; set; }
    }


    public class BudgetModel: BudgetList_ForGrid
    {
        public List<BudgetModel_Monthly> MonthlyBudget { get; set; }

        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class BudgetModel_Monthly
    {
        public int BudgetId { get; set; }
        public int BudgetMonth { get; set; }
        public int BudgetYear { get; set; }
        public string MonthYear { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal LUNCH_UTILISATION_WEEK_DAYS { get; set; }
        public decimal EVENING_UTILISATION_WEEK_DAYS { get; set; }
        public decimal DINNER_UTILIZATION_WEEK_DAYS { get; set; }
        public decimal LUNCH_UTILISATION_WEEKEND { get; set; }
        public decimal EVENING_UTILISATION_WEEKEND { get; set; }
        public decimal DINNER_UTILIZATION_WEEKEND { get; set; }
        public decimal UTILIZATION_Total { get; set; }
        public decimal LUNCH_ASPG_WEEKDAY { get; set; }
        public decimal EVENING_ASPG_WEEKDAY { get; set; }
        public decimal DINNER_ASPG_WEEK_DAY { get; set; }
        public decimal LUNCH_ASPG_WEEKEND { get; set; }
        public decimal EVENING_ASPG_WEEKEND { get; set; }
        public decimal DINNER_ASPG_WEEKEND { get; set; }
        public decimal ASAP_Total { get; set; }
        public decimal LUNCH_SALE_WEEKDAY { get; set; }
        public decimal EVENING_SALE_WEEKDAY { get; set; }
        public decimal DINNER_SALE_WEEKDAY { get; set; }
        public decimal LUNCH_SALE_WEEKEND { get; set; }
        public decimal EVENING_SALE_WEEKEND { get; set; }
        public decimal DINNER_SALE_WEEKEND { get; set; }
        public decimal TOTAL_SALE { get; set; }
        public decimal FOOD_SALE { get; set; }
        public decimal BEVERAGE_SALE { get; set; }
        public decimal WINE_SALE { get; set; }
        public decimal LIQUOR_SALE { get; set; }
        public decimal BEER_SALE { get; set; }
        public decimal TOBACCO_SALE { get; set; }
        public decimal OTHER_SALE { get; set; }
        public decimal DELIVERY_SALE { get; set; }
        public decimal TAKEAWAY_SALE { get; set; }
        public decimal SaleBreakup_Total { get; set; }
        public decimal FOOD_COST { get; set; }
        public decimal LIQUOR_COST { get; set; }
        public decimal WINE_COST { get; set; }
        public decimal BEER_COST { get; set; }
        public decimal BEVERAGE_COST { get; set; }
        public decimal OTHER_COST { get; set; }
        public decimal TOBACCO_COST { get; set; }
        public decimal CTC_SALARY { get; set; }
        public decimal SERVICE_CHARGE { get; set; }
        public decimal ProdCost_Total { get; set; }

        /** labour expense change 25-Sep-2020***/
        //public decimal INCENTIVES { get; set; }
        //public decimal STAFF_MEAL_COST { get; set; }
        //public decimal RECRUITMENT_TRAINING { get; set; }
        //public decimal COMPLIMENTARY_STAFF { get; set; }
        //public decimal COMPLIMENTARY_MANAGEMENT { get; set; }
        //public decimal OTHER_LABOUR_COST { get; set; }
        public decimal LabourCost_Other_Complimentary_Manag { get; set; }
        public decimal LabourCost_Other_Complimentary_Staff { get; set; }
        public decimal LabourCost_Other_Icentive { get; set; }
        public decimal LabourCost_Other_Other { get; set; }
        public decimal LabourCost_Other_Recuit_Training { get; set; }
        public decimal LabourCost_Other_Staff_AccommodationCost { get; set; }
        public decimal LabourCost_Other_StaffMeal { get; set; }
        public decimal LabourCost_Other_PayRollTax { get; set; }
        public decimal LabourCost_CTC_Service { get; set; }
        public decimal LabourCost_CTC_Kitchen { get; set; }
        public decimal LabourCost_CTC_Management { get; set; }
        public decimal LabourCost_CTC_MIT { get; set; }
        public decimal LabourCost_CTC_VACA_BONUS_13Month { get; set; }
        public decimal LabourCost_Other_HealthInsurance_Medical { get; set; }
        public decimal LabourCost_Other_WorkerCompensation { get; set; }
        public decimal LabourCost_Other_EmployeeBenefit { get; set; }
        public decimal LabourCost_Total { get; set; }

        /** Financial Charges  changed on 25-Sep***/
        public decimal BANK_FEES { get; set; }
        public decimal FinanceCharge_Accounting_Admin { get; set; }
        public decimal FinanceCharge_LegalFees { get; set; }
        public decimal FinanceCharge_ProfessionalFees { get; set; }
        public decimal DEPRICIATION { get; set; }
        public decimal PERMITS_LICENSE { get; set; }
        public decimal CREDIT_CARD_CHARGES { get; set; }
        public decimal BANK_INTEREST_LOAN { get; set; }
        public decimal INSURANCE_EXPENSE { get; set; }
        public decimal CASH_PICK_UP_CHARGES { get; set; }
        public decimal FinancialCharges_Total { get; set; }

        /** Other Expense change on 25-ep-2020 **/
        public decimal LINEN_LAUNDRY { get; set; }
        public decimal MISCELLANEOUS_EXPENSE { get; set; }
        public decimal POSTAGE_COURIER { get; set; }
        public decimal FREIGHT { get; set; }
        public decimal BUSINESS_TRAVEL { get; set; }
        public decimal OtherExpense_OtherDirect { get; set; }//
        public decimal OtherExpense_SuperVisionFees { get; set; }//
        public decimal OtherExpense_Total { get; set; }

        /*** Marketing & Business Cost change on 25-Sep-2020**/
        public decimal ADVERTISING_EXPENSE { get; set; }
        public decimal COMMISSION_DELIVERY_PARTNERS { get; set; }
        public decimal COMMISSION_ONLINE_PARTNERS { get; set; }
        public decimal PUBLIC_RELATIONS { get; set; }
        public decimal COMPRIMENTARY_GUEST { get; set; }
        public decimal BUSINESS_TIE_UPS_ { get; set; }
        public decimal DUES_MEMBERSHIPS { get; set; }
        public decimal MarketingCost_CreditCardDiscount { get; set; }
        public decimal Marketing_Total { get; set; }

        /** Property & Royalty Charges**/
        public decimal RENT { get; set; }
        public decimal RENT_PERC { get; set; }
        public decimal CAM { get; set; }
        public decimal PROPERTY_TAXES { get; set; }
        public decimal RENT_LATE_FEES { get; set; }
        public decimal Property_Total { get; set; }
        public decimal ROYALTY_CHARGE { get; set; }
        public decimal ROYALTY_PENALTY { get; set; }
        public decimal ROYALTY_OTHERS { get; set; }
        public decimal Royalty_Total { get; set; }

        /** Equipment cost ***/
        public decimal RENTAL_EQUIPMENT_OTHERS { get; set; }
        public decimal SMALL_WARE_SMALL_EQUIPMENTS { get; set; }
        public decimal EquipSmallWare_Total { get; set; }

        /** ITSoftware ***/
        public decimal SOFTWARE_RENTAL { get; set; }
        public decimal SOFTWARE_AMC { get; set; }
        public decimal WEB_SITE_COST { get; set; }
        public decimal CLOUD_HOSTING_COST { get; set; }
        public decimal SOFTWARE_LICENSE_COST { get; set; }
        public decimal IT_HARDWARE { get; set; }
        public decimal IT_Total { get; set; }

        /** Utilities & Maintenance ***/
        public decimal TELEPHONE_INTERNET { get; set; }
        public decimal WATER_SEWER { get; set; }
        public decimal ELECRTICITY { get; set; }
        public decimal GAS_COAL { get; set; }
        public decimal UTILITIES_Total { get; set; }
        public decimal REPAIR_MAINTENANCE { get; set; }
        public decimal ANNUAL_MAINTENANCE_CONTRACT { get; set; }
        public decimal CLEANING_SERVICE { get; set; }
        public decimal WASTE_REMOVAL { get; set; }
        public decimal MAINTENANCE_Total { get; set; }

        /** Supplies Purchase change on 25-Sep-2020***/
        public decimal CUTLERY_CROCKERY_GLASSWARE { get; set; }
        public decimal OFFICE_SUPPLIES { get; set; }
        public decimal CLEANING_MATERIAL { get; set; }
        public decimal PACKGING_MATERIAL { get; set; }
        public decimal PRINTING_AND_STATIONARY { get; set; }
        public decimal FOH_SUPPLIES { get; set; }
        public decimal BOH_SUPPLIES { get; set; }
        public decimal UNIFORM { get; set; }
        public decimal PurchaseSupplies_OTHER { get; set; } //
        public decimal SUPPLIES_Total { get; set; }

    }
}