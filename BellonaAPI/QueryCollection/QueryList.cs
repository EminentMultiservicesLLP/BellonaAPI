namespace BellonaAPI.QueryCollection
{
    public class QueryList
    {
        public const string GetRegions = "dbsp_GetRegions";
        public const string GetCountryList = "dbsp_GetCountries";
        public const string UpdateCountry = "dbsp_SaveCountries";

        public const string GetOutletTypeList = "dbsp_GetOutletTypeList";
        public const string GetOutletList = "dbsp_GetDistinctOutlets";
        public const string GetAccessOutlets = "dbsp_GetAccessOutlets";
        public const string UpdateOutlet = "dbsp_SaveOutlets";

        public const string GetCurrencyList = "dbsp_GetCurrencies";
        public const string GetBrandList = "dbsp_GetBrands";
        public const string GetCityList = "dbsp_GetCities";
        public const string GetClusterList = "dbsp_GetClusters";
        public const string GetRentTypes = "dbsp_GetRentTypes";

        public const string GetDeliveryPartners = "dbsp_GetDeliveryPartners";
        public const string GetGuestPartners = "dbsp_GetGuestPartners";

        public const string VerifyUserLogin = "dbsp_VerifyUserLogin";
        public const string GetUserMenuRights = "dbsp_GetUserMenuRights";
        public const string UpdateUserMenuSettings = "dbsp_UpdateUserMenuSettings";

        public const string GetDSREntryList = "dbsp_GET_DSREntries";
        public const string UpdateDSREntry = "dbsp_SAVE_DSREntry";

        public const string GetMonthlyExpenseDetailByID = "dbsp_GET_MonthlyExpenseDetailByID";
        public const string GetActualMonthlyExpenseDetailByID = "dbsp_GET_ActualMonthlyExpenseDetailByID";
        public const string GetMonthlyExpenseDetailByOutlet_Month = "dbsp_GET_MonthlyExpenseDetailByOutlet_Month";
        public const string GetActualMonthlyExpenseDetailByOutlet_Month = "dbsp_GET_ActualMonthlyExpenseDetailByOutlet_Month";
        public const string GetMonthlyExpenseEntryList = "dbsp_GET_MonthlyExpenseList";
        public const string GetActualMonthlyExpenseEntryList = "dbsp_GET_ActualMonthlyExpenseList";
        public const string UpdateMonthlyExpenseEntry = "dbsp_SAVE_MonthlyExpense";
        public const string UpdateActualMonthlyExpenseEntry = "dbsp_SAVE_ActualMonthlyExpense";

        public const string GetBudgetList = "dbsp_GetBudgetList";
        public const string GetBudgetDetailByID = "dbsp_GetBudgetDetailByID";
        public const string GetBudget_SaleCatgoryWiseForMonth = "dbsp_GetBudget_SaleCatgoryWiseForMonth";
        public const string DeleteYearlyBudget = "dbsp_DELETE_Budget";
        public const string UpdateYearlyBudget = "dbsp_SAVE_Budget";

        public const string GetTimeZones = "dbsp_GetTimeZones";
        public const string GetRole = "dbsp_GetRole";
        public const string SaveRole = "dbsp_SaveRole";
        public const string SaveUser = "dbsp_SaveUser";
        public const string GetUser = "dbsp_GetUser";
        public const string SaveOutlet = "dbsp_SaveOutletAccess";
        public const string GetSaveOutletAccess = "dbsp_GetSaveOutletAccess";
        public const string DeleteSaveOutletAccess = "dbsp_DeleteSaveOutletAccess";
        public const string DeleteSavedMenuAccess = "dbsp_DeleteSavedMenuAccess";
        public const string SaveMenuAccess = "dbsp_SaveMenuAccess";

        public const string GetParentMenu = "dbsp_GetParentMenu";
        public const string GetChildMenu = "dbsp_GetChildMenu";
        public const string GetMenuRoleAccess = "dbsp_GetRoleMenuAccess";
        public const string GetUserAccess = "dbsp_GetUserAccess";
        public const string GetOutletAccess = "dbsp_GetOutletAccess";
        public const string DeleteMenuRoleAccess = "dbsp_DeleteRoleMenuAccess";
        public const string SaveRoleMenuAccess = "dbsp_SaveRoleMenuAccess";
        public const string InsertUserAccessRoleWise = "dbsp_InsertUserAccessRoleWise";
        public const string DailyExpenseEntries = "dbsp_SAVE_DailyExpense";
        public const string GetDailyExpenseEntries = "dbsp_GetDailyExpenseEntries";
        public const string GetMenuforRWAccess = "dbsp_GetMenuforRWAccess";
        public const string SaveReadWriteAccess = "dbsp_SaveReadWriteAccess";
        public const string GetFormMenuAccess = "dbsp_GetFormMenuAccess";


        public const string GetSaleDineIn = "dbsp_GetSaleDineInForMonth";
        public const string GetSaleBreakUp = "dbsp_GetSaleBreakUpForMonth";
        public const string GetDeliverySale = "dbsp_GetSaleDeliveryForMonth";
        public const string GetForgotPassword = "dbsp_GetForgotPassword";

        public const string Dashboard_GetCashflowProjection_Present = "dbsp_GetBudgetVsProjectedCashflow";
        public const string GetPurchaseAndProductionCost = "dbsp_GetPurchaseAndProductionCost";
        public const string GetSalesVsBudget = "dbsp_GetSalesVsBudget";
        public const string GetActualSalesData = "dbsp_GetActualSalesData";
        public const string GetGuestCountData = "dbsp_GetGuestCountData";
        public const string GetBudgetVsProjectedExpense = "dbsp_GetBudgetVsProjectedExpense";

        public const string GetActualSalesData_Chart = "dbsp_GetActualSalesData_Chart";
        public const string GetSalesVsBudget_chart = "dbsp_GetSalesVsBudget_chart";
        public const string GetSalesVsBudgetPart2_chart = "dbsp_GetSalesVsBudgetPart2_chart";
        public const string GetAllSales_chart = "dbsp_GetAllSales_chart";
        public const string GetSalesTrendAnalysis_chart = "dbsp_GetSalesTrendAnalysis_Trend_chart";
        public const string GetSalesTrendAnalysis_Part2_chart = "dbsp_GetTrendAnalysis_TrendPart2_chart";
        public const string GetCashFlowBreakup_Chart = "dbsp_GetCashFlowBreakup_Chart";
        public const string CashFlowBreakup_Trend = "dbsp_GetCashFlowBreakup_Trend_Chart";
        public const string GetCostOfGoods = "dbsp_GetCostOfGoods";
        public const string GetCostOfOther = "dbsp_GetOtherCostDetails_Chart";


        /************ History Chart ****************************/
        public const string GetCashFlowBreakup_Chart_History = "dbsp_GetCashFlowBreakup_History_Chart";
        public const string GetCashFlowBreakup_Trend_Chart_History = "dbsp_GetCashFlowBreakup_Trend_History_Chart";
        public const string GetSalesTrendAnalysis_chart_History = "dbsp_GetSalesTrendAnalysis_Trend_History_chart";
        public const string GetGuestTrendAnalysis_History = "dbsp_GetGuestTrendAnalysis_History_chart";
        /*******************************************************/

        public const string GetAllExchageRates = "dbsp_GetExchangeRatesAll";
        public const string GetExchangeRateByMonth = "dbsp_GetExchangeRateByMonth";
        public const string SaveExchangeRate = "dbsp_SaveExchangeRate";
       public const string SaveUploadDocument = "dbsp_Dms_SaveDocument";
        public const string SaveUploadDocumentPath = "dbsp_DMS_SaveUploadDocumentPath";
        public const string SavePropertyContent = "dbsp_DMS_SaveDocumentValues";
        public const string GetVendor = "dbsp_GetVendorByCityId";
        public const string GetSubCategoryDoc = "dbsp_GetSubCategoryDoc";
        public const string GetAllDocument = "dbsp_Dms_GetAllDocument";
        public const string GetAllScanDocument = "dbsp_Dms_GetAllDocumentValues";
        public const string GetAllAttachement = "dbsp_Dms_GetAllAttachement";
        public const string GetScanAttachement = "dbsp_Dms_GetDocumentById";

        public const string SaveDocStatus = "dbsp_Dms_SaveDocStatus";
        public const string DeleteDocStatus = "dbsp_Dms_DeleteDocStatus";
        public const string RevokeDocStatus = "dbsp_Dms_RevokeDocStatus";
        public const string GetFilePath = "dbsp_Dms_GetFilePath";
        public const string GetVendorList = "dbsp_GetVendorList";
        public const string SaveVendor = "dbsp_SaveVendor";
        public const string ChangePassword = "dbsp_ChangePassword";


        //*********************IKMG -Guest ********************************/
        public const string GetGuestVisitingTime = "dbsp_Guest_GetGuestVisitingTime";
        public const string GetGuestType = "dbsp_Guest_GetGuestType";
        public const string SaveGuestData = "dbsp_Guest_SaveDetails";
        public const string GetGuestList = "dbsp_Guest_GetGuestList";
        public const string GetImagePath = "dbsp_Guest_GetImagePath";
        public const string GetAllAuthCash = "dbsp_Cash_GetAllCashAuth";
        public const string Authorize = "dbsp_DepositAuthorization";
        public const string GetGuestAge = "dbsp_Guest_GetGuestAge";
        public const string GetGuestBatch = "dbsp_Guest_GetGuestBatch";
        public const string GetBatchwiseGuest = "dbsp_Guest_GetBatchwiseGuest";
        public const string DeleteGuestOutletAccess = "dbsp_Guest_DeleteOutletAccess";
        public const string getLinkedOutlet = "dbsp_Guest_getLinkedOutlet";


       /// ************ Cash Deposit Status****************************/
        public const string GetAllCashDeposites = "dbsp_Get_Pending_CashDeposites";
        public const string DeleteCashDeposits = "dbsp_Delete_CashDeposits";
        public const string GetCashDepositImagePath = "dbsp_CashDeposit_GetImagePath";
        public const string SaveCashDeposits = "dbsp_SAVE_CashDeposits";

        public const string GetCashDepositStatus = "dbsp_GetCashDepositStatus";

        /// ************************** Prospect ****************************/
        public const string GetBrand = "dbsp_Prospect_GetBrand";
        public const string GetRegion = "dbsp_Prospect_GetRegion";
        public const string GetState = "dbsp_Prospect_GetState";
        public const string GetSource = "dbsp_Prospect_GetSource";
        public const string GetPrefix = "dbsp_Prospect_GetPrefix";
        public const string GetPersonType = "dbsp_Prospect_GetPersonType";
        public const string GetServiceType = "dbsp_Prospect_GetServiceType";
        public const string GetSite = "dbsp_Prospect_GetSite";
        public const string GetInvestment = "dbsp_Prospect_GetInvestment";
        public const string SaveProspectData = "dbsp_Prospect_SaveProspectData";
        public const string getAllProspect = "dbsp_Prospect_GetAllProspect";
        //Prospect's Follow Up Forms 
        public const string SaveFirstFollowUp = "dbsp_Prospect_SaveFirstFollowUp";
        public const string GetSiteFeedback = "dbsp_Prospect_GetSiteFeedback";
        public const string GetPotentialOfFranchisee = "dbsp_Prospect_GetPotentialOfFranchisee";
        public const string GetFinacialCapacity = "dbsp_Prospect_GetFinacialCapacity";
        public const string GetFollowUpProspect = "dbsp_Prospect_GetFollowUpProspect";
        public const string SaveSecondFollowUp = "dbsp_Prospect_SaveSecondFollowUp";
        public const string SavePostFollowUp = "dbsp_Prospect_SavePostFollowUp";
        public const string GetFollowUpDetails = "dbsp_Prospect_GetFollowUpDetails";
        public const string FollowUpReminder = "dbsp_Prospect_GetFollowUpReminder";
        public const string getProstectStatus = "dbsp_Prospect_GetProspectStatus";
        public const string GetAllProspectForStatus = "dbsp_Prospect_GetAllProspectForStatus";
        public const string GetDashboardData = "dbsp_Prospect_GetDashboardData";
        public const string GetProspectForAllocation = "dbsp_Prospect_GetProspectForAllocation";
        public const string SaveProspectAllocation = "dbsp_Prospect_SaveProspectAllocation"; 
        public const string GetProspectAllocation = "dbsp_Prospect_GetProspectAllocation";


        // ********************************* SITE MASTER ***************************************

        public const string GetCity = "dbsp_Prospect_GetCity";
        public const string SaveSiteDetails = "dbsp_Site_SaveSiteDetails";
        public const string SaveSiteAttachment = "dbsp_Site_SaveSiteAttachment";
        public const string GetSiteDetails = "dbsp_Site_GetSiteDetails";
        public const string DeleteSiteImageVideo = "dbsp_Site_DeleteSiteImageVideo";
        public const string GetPropertyBySite = "dbsp_Site_GetPropertyBySite";

        //********************************** B2C Billing ***************************************

        public const string GetDashboardCity = "dbsp_Bill_GetDashboardCity";  
        public const string GetDashboardCluster = "dbsp_Bill_GetDashboardCluster";  
        public const string GetDashboardOutlet = "dbsp_Bill_GetDashboardOutlet";  
        public const string GetFunctionForStatus = "dbsp_Bill_GetFunctionForStatus";  
        public const string GetAllInvoiceUpload = "dbsp_Bill_GetAllInvoiceUpload";  
        public const string GetFunctionForExport = "dbsp_Bill_GetFunctionForExport";  

        public const string GetOutletforBilling = "dbsp_Bill_GetOutletforBilling";  
        public const string SaveBillingDetails = "dbsp_Bill_SaveBillingDetails"; 
        public const string updateBalance = "dbsp_Bill_UpdateBalance";
        public const string GetBillDetailsbyId = "dbsp_Bill_GetBillDetailsbyId";
        public const string GetAllBillData = "dbsp_Bill_GetAllBillData";      
        public const string AuthourizeBillReceipt = "dbsp_Bill_AuthourizeBillReceipt";
        public const string SaveFunctionEntry = "dbsp_Bill_SaveFunctionEntry";
        public const string UpdateFunctionEntry = "dbsp_Bill_UpdateFunctionEntry";
        public const string GetFunctionEntry = "dbsp_Bill_GetFunctionEntry";
        public const string SaveInvoiceDetails = "dbsp_Bill_SaveInvoiceDetails";
        public const string GetInvoiceDetailsById = "dbsp_Bill_GetInvoiceDetailsById";
        public const string SaveInvoiceAttachments = "dbsp_Bill_SaveInvoiceAttachments";
        public const string VerfApprFunctionInvoice = "dbsp_Bill_VerfApprFunctionInvoice";
        public const string SaveFunctionAttachments = "dbsp_Bill_SaveFunctionAttachments";
        public const string GetBillAttachments = "dbsp_Bill_GetBillAttachments";
        public const string DeleteBillAttachment = "dbsp_Bill_DeleteBillAttachment";
        public const string SaveEInvoiceUpload = "dbsp_Bill_SaveEInvoiceUpload";
        public const string GetEInvoiceUpload = "dbsp_Bill_GetEInvoiceUpload";
        public const string DeleteEInvoiceUpload = "dbsp_Bill_DeleteEInvoiceUpload";
        public const string SaveBillingAttachment = "dbsp_Bill_SaveBillingAttachment";
        public const string DeleteBillingReceipt = "dbsp_Bill_DeleteBillingReceipt";


        // ********************************* Inventory ***************************************

        public const string GetSubCategory = "dbsp_Inv_GetSubCategory";
        public const string GetItemPackSize = "dbsp_Inv_GetItemPackSize";
        public const string SaveItem = "dbsp_Inv_SaveItem";
        public const string GetItem = "dbsp_Inv_GetItem";
        public const string GetOutletItemMapping = "dbsp_Inv_GetOutletItemMapping";
        public const string SaveOutletItemMapping = "dbsp_Inv_SaveOutletItemMapping";
        public const string GetFinancialYear = "dbsp_Inv_GetFinancialYear";
        public const string SaveScheduleStockCount = "dbsp_Inv_SaveScheduleStockCount";
        public const string GetScheduleStockCount = "dbsp_Inv_GetScheduleStockCount";
        public const string SaveInventoryEntry = "dbsp_Inv_SaveInventoryEntry";
        public const string GetInventoryEntry = "dbsp_Inv_GetInventoryEntry";
        public const string GetEntryDetails = "dbsp_Inv_GetEntryDetails";
        public const string GetEntryAtt = "dbsp_Inv_GetEntryAtt";
        public const string DeleteEntryAtt = "dbsp_Inv_DeleteEntryAtt";
        public const string EntryVfyAndAuth = "dbsp_Inv_EntryVfyAndAuth";
    }
}