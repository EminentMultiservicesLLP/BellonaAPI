using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.Controllers;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace BellonaAPI.DataAccess.Class
{
    public class TransactionRepository : ITransactionRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(TransactionController));

        public bool DeleteBudget(int BudgetId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDSRENtry(int DSREntryID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMonthlyExpense(int MonthlyExpenseID, bool isActualExpense = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DSREntry> GetDSREntries(Guid userId,int menuId, int? dsrEntryId = null)
        {
            List<DSREntry> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32)); 
                    if (dsrEntryId != null && dsrEntryId > 0) dbCol.Add(new DBParameter("DSREntryId", dsrEntryId, DbType.Int32));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetDSREntryList, dbCol, CommandType.StoredProcedure);

                    var guestPartnersList = (dsData != null && dsData.Tables.Count >2) ?  dsData.Tables[2].AsEnumerable().Select(row => new GuestPartnersDailySales
                    {
                        DSREntryID = row.Field<int>("DSREntryID"),
                        GuestPartnerName = row.Field<string>("GuestPartnerName"),
                        GuestPartnerID = row.Field<int>("GuestPartnerID"),
                        GuestCount = row.Field<int>("GuestCount")
                    }).ToArray() : new GuestPartnersDailySales[] { };

                    var deliveryPartnersList = (dsData != null && dsData.Tables.Count > 1) ? dsData.Tables[1].AsEnumerable().Select(row => new DeliveryPartnersDailySales
                    {
                        DSREntryID = row.Field<int>("DSREntryID"),
                        DeliveryPartnerID = row.Field<int>("DeliveryPartnerID"),
                        DeliveryPartnerName = row.Field<string>("DeliveryPartnerName"),
                        SaleAmount = row.Field<decimal>("SaleAmount")
                    }).ToArray() : new DeliveryPartnersDailySales[] { };

                    _result = dsData.Tables[0].AsEnumerable().Select(row => new DSREntry
                    {
                        DSREntryID = row.Field<int>("DSREntryID"),
                        OutletID = row.Field<int>("OutletID"),
                        DSREntryDate = row.Field<DateTime>("DSREntryDate"),
                        SaleLunchDinein = row.Field<decimal>("SaleLunchDinein"),
                        SaleEveningDinein = row.Field<decimal>("SaleEveningDinein"),

                        SaleDinnerDinein = row.Field<decimal>("SaleDinnerDinein"),
                        SaleTakeAway = row.Field<decimal>("SaleTakeAway"),
                        SaleFood = row.Field<decimal>("SaleFood"),
                        SaleBeverage = row.Field<decimal>("SaleBeverage"),
                        SaleWine = row.Field<decimal>("SaleWine"),
                        SaleBeer = row.Field<decimal>("SaleBeer"),
                        SaleLiquor = row.Field<decimal>("SaleLiquor"),
                        SaleTobacco = row.Field<decimal>("SaleTobacco"),
                        SaleOther = row.Field<decimal>("SaleOther"),
                        ItemsPerBill = row.Field<int>("ItemsPerBill"),
                        CTCSalary = row.Field<decimal>("CTCSalary"),
                        ServiceCharge = row.Table.Columns.Contains("ServiceCharge") == false ? 0 : (row.Field<decimal?>("ServiceCharge") == null ? 0 : row.Field<decimal>("ServiceCharge")),

                        TotalNoOfBills = row.Field<int>("TotalNoOfBills"),
                        GuestCountLunch = row.Field<int>("GuestCountLunch"),
                        GuestCountEvening = row.Field<int>("GuestCountEvening"),
                        GuestCountDinner = row.Field<int>("GuestCountDinner"),
                        CashCollected = row.Field<decimal>("CashCollected"),
                        CashStatus= row.Field<int>("CashStatus"),
                        DeliveryPartners = dsrEntryId != null && dsrEntryId > 0 ? deliveryPartnersList : new DeliveryPartnersDailySales[] { } ,
                        GuestPartners= dsrEntryId != null && dsrEntryId > 0 ? guestPartnersList : new GuestPartnersDailySales[] {},
                        TotalDelivery = (deliveryPartnersList.Where(w => w.DSREntryID == row.Field<int>("DSREntryID")).Sum(s => s.SaleAmount) + row.Field<decimal>("SaleTakeAway")),
                        TotalGuestByPartners = guestPartnersList.Where(w => w.DSREntryID == row.Field<int>("DSREntryID")).Sum(s => s.GuestCount),
                    }).OrderBy(o => o.DSREntryDate).ToList();

                    if (_result != null && _result.Count > 0) 
                    {
                        _result.ForEach(f =>
                        {
                            f.TotalDinein = (f.SaleFood + f.SaleLiquor + f.SaleBeer + f.SaleWine + f.SaleTobacco + f.SaleOther + f.SaleBeverage);
                            //f.TotalDinein = (f.SaleLunchDinein + f.SaleEveningDinein + f.SaleDinnerDinein);  CHANGED BECAUSE NOW NOT ALL OUTLET PROVIDES LUNCH/EVEING/DINNER DETAILS
                            f.TotalGuestDirect = (f.GuestCountLunch + f.GuestCountEvening + f.GuestCountDinner);
                            //f.TotalDelivery = (f.DeliveryPartners.Where(w=> w.DSREntryID == f.DSREntryID).Sum(s => s.SaleAmount) + f.SaleTakeAway);
                            f.TotalSale = f.TotalDinein + f.TotalDelivery;
                            //f.TotalGuestByPartners = f.GuestPartners.Where(w => w.DSREntryID == f.DSREntryID).Sum(s => s.GuestCount);
                        });
                    }
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetDSREnties:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public MonthlyExpense GetMonthlyExpensesByID(Guid userId, int monthlyExpenseId, bool isActualExpense = false)
        {
            MonthlyExpense _result = null; int isSuccess = 0; string returnMessage = "";
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MonthlyExpenseId", monthlyExpenseId, DbType.Int32));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", isSuccess, DbType.Int32, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", returnMessage, DbType.String, ParameterDirection.Output));

                    DataSet dsData = Dbhelper.ExecuteDataSet((isActualExpense ? QueryList.GetActualMonthlyExpenseDetailByID : QueryList.GetMonthlyExpenseDetailByID), dbCol, CommandType.StoredProcedure);
                    _result = ProcessMonthlyExpenseDataFromSQL(dsData);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetMonthlyExpensesByID:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public MonthlyExpense GetMonthlyExpensesByOutlet_Month(Guid userId, int outletID, int monthlyExpenseYear, int monthlyExpenseMonth, bool isActualExpense = false)
        {
            MonthlyExpense _result = null; int isSuccess = 0; string returnMessage = "";
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("MonthlyExpenseMonth", monthlyExpenseMonth, DbType.Int32));
                    dbCol.Add(new DBParameter("MonthlyExpenseYear", monthlyExpenseYear, DbType.Int32));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", isSuccess, DbType.Int32, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", returnMessage, DbType.String, ParameterDirection.Output));

                    DataSet dsData = Dbhelper.ExecuteDataSet((isActualExpense ? QueryList.GetActualMonthlyExpenseDetailByOutlet_Month : QueryList.GetMonthlyExpenseDetailByOutlet_Month), dbCol, CommandType.StoredProcedure);
                    _result = ProcessMonthlyExpenseDataFromSQL(dsData);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetMonthlyExpensesByID:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        private MonthlyExpense ProcessMonthlyExpenseDataFromSQL(DataSet dsData)
        {
            MonthlyExpense _result = null;

            var deliveryPartnersList = (dsData != null && dsData.Tables.Count > 1) ? dsData.Tables[1].AsEnumerable().Select(row => new DeliveryPartnersMonthlyExpense
            {
                MonthlyExpenseID = row.Table.Columns.Contains("MonthlyExpenseID")== false? 0 : (row.Field<int?>("MonthlyExpenseID")  == null ? 0: row.Field<int>("MonthlyExpenseID")),
                DeliveryPartnerID = row.Table.Columns.Contains("DeliveryPartnerID")== false? 0 : (row.Field<int?>("DeliveryPartnerID") == null ? 0 :row.Field<int>("DeliveryPartnerID")),
                DeliveryPartnerName = row.Field<string>("DeliveryPartnerName"),
                Amount = row.Table.Columns.Contains("Amount")== false? 0 : (row.Field<decimal?>("Amount") == null ? 0: row.Field<decimal>("Amount"))
            }).ToArray() : new DeliveryPartnersMonthlyExpense[] { };

            _result = dsData.Tables[0].AsEnumerable().Select(row => new MonthlyExpense
            {
                MonthlyExpenseId = row.Field<int>("MonthlyExpenseId"),
                OutletID = row.Field<int>("OutletID"),
                OutletName = row.Field<string>("OutletName"),
                ExpenseMonth = row.Field<int>("ExpenseMonth"),
                ExpenseYear = row.Field<int>("ExpenseYear"),

                Equipement_Other = row.Field<decimal>("Equipement_Other"),
                Equipement_Rental = row.Field<decimal>("Equipement_Rental"),
                Equipement_SmallWare = row.Field<decimal>("Equipement_SmallWare"),

                FinanceCharge_BankFees = row.Field<decimal>("FinanceCharge_BankFees"),
                FinanceCharge_BankInterest_Loan = row.Field<decimal>("FinanceCharge_BankInterest_Loan"),
                FinanceCharge_CashPickUp = row.Field<decimal>("FinanceCharge_CashPickUp"),
                FinanceCharge_CreditCard = row.Field<decimal>("FinanceCharge_CreditCard"),
                FinanceCharge_Depreciation = row.Field<decimal>("FinanceCharge_Depreciation"),
                FinanceCharge_Insurance = row.Field<decimal>("FinanceCharge_Insurance"),
                FinanceCharge_Permit = row.Field<decimal>("FinanceCharge_Permit"),
                FinanceCharge_ProfessionalFees = row.Table.Columns.Contains("FinanceCharge_ProfessionalFees") == false ? 0 : (row.Field<decimal?>("FinanceCharge_ProfessionalFees") == null ? 0 : row.Field<decimal>("FinanceCharge_ProfessionalFees")),
                FinanceCharge_LegalFees = row.Table.Columns.Contains("FinanceCharge_LegalFees") == false ? 0 : (row.Field<decimal?>("FinanceCharge_LegalFees") == null ? 0 : row.Field<decimal>("FinanceCharge_LegalFees")),
                FinanceCharge_Accounting_Admin = row.Table.Columns.Contains("FinanceCharge_Accounting_Admin") == false ? 0 : (row.Field<decimal?>("FinanceCharge_Accounting_Admin") == null ? 0 : row.Field<decimal>("FinanceCharge_Accounting_Admin")),


                ITSoftware_AMC = row.Field<decimal>("ITSoftware_AMC"),
                ITSoftware_Cloud = row.Field<decimal>("ITSoftware_Cloud"),
                ITSoftware_ITHardware = row.Field<decimal>("ITSoftware_ITHardware"),
                ITSoftware_License = row.Field<decimal>("ITSoftware_License"),
                ITSoftware_Rental = row.Field<decimal>("ITSoftware_Rental"),
                ITSoftware_WebSite = row.Field<decimal>("ITSoftware_WebSite"),

                LabourCost_Other_Complimentary_Manag = row.Table.Columns.Contains("LabourCost_Other_Complimentary_Manag") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_Complimentary_Manag") == null ? 0 : row.Field<decimal>("LabourCost_Other_Complimentary_Manag")),
                LabourCost_Other_Complimentary_Staff = row.Table.Columns.Contains("LabourCost_Other_Complimentary_Staff") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_Complimentary_Staff") == null ? 0 : row.Field<decimal>("LabourCost_Other_Complimentary_Staff")),
                LabourCost_Other_Icentive = row.Table.Columns.Contains("LabourCost_Other_Icentive") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_Icentive") == null ? 0 : row.Field<decimal>("LabourCost_Other_Icentive")),
                LabourCost_Other_Other = row.Table.Columns.Contains("LabourCost_Other_Other") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_Other") == null ? 0 : row.Field<decimal>("LabourCost_Other_Other")),
                LabourCost_Other_Recuit_Training = row.Table.Columns.Contains("LabourCost_Other_Recuit_Training") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_Recuit_Training") == null ? 0 : row.Field<decimal>("LabourCost_Other_Recuit_Training")),
                LabourCost_Other_Staff_AccommodationCost = row.Table.Columns.Contains("LabourCost_Other_Staff_AccommodationCost") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_Staff_AccommodationCost") == null ? 0 : row.Field<decimal>("LabourCost_Other_Staff_AccommodationCost")),
                LabourCost_Other_StaffMeal = row.Table.Columns.Contains("LabourCost_Other_StaffMeal") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_StaffMeal") == null ? 0 : row.Field<decimal>("LabourCost_Other_StaffMeal")),
                LabourCost_Other_PayRollTax = row.Table.Columns.Contains("LabourCost_Other_PayRollTax") == false ? 0 : (row.Field<decimal?>("LabourCost_Other_PayRollTax") == null ? 0 : row.Field<decimal>("LabourCost_Other_PayRollTax")),
                LabourCost_CTC = row.Table.Columns.Contains("LabourCost_CTC") == false ? 0 : (row.Field<decimal?>("LabourCost_CTC") == null ? 0 : row.Field<decimal>("LabourCost_CTC")),
                LabourCost_CTC_Service = row.Table.Columns.Contains("LabourCost_CTC_Service") == false ? 0 : (row.Field<decimal?>("LabourCost_CTC_Service") == null ? 0 : row.Field<decimal>("LabourCost_CTC_Service")),
                LabourCost_CTC_Kitchen = row.Table.Columns.Contains("LabourCost_CTC_Kitchen") == false ? 0 : (row.Field<decimal?>("LabourCost_CTC_Kitchen") == null ? 0 : row.Field<decimal>("LabourCost_CTC_Kitchen")),
                LabourCost_CTC_Management = row.Table.Columns.Contains("LabourCost_CTC_Management") == false ? 0 : (row.Field<decimal?>("LabourCost_CTC_Management") == null ? 0 : row.Field<decimal>("LabourCost_CTC_Management")),
                LabourCost_CTC_MIT = row.Table.Columns.Contains("LabourCost_CTC_MIT") == false ? 0 : (row.Field<decimal?>("LabourCost_CTC_MIT") == null ? 0 : row.Field<decimal>("LabourCost_CTC_MIT")),
                LabourCost_CTC_VACA_BONUS_13Month = row.Table.Columns.Contains("LabourCost_CTC_VACA_BONUS_13Month") == false ? 0 : (row.Field<decimal?>("LabourCost_CTC_VACA_BONUS_13Month") == null ? 0 : row.Field<decimal>("LabourCost_CTC_VACA_BONUS_13Month")),
                LabourCost_Other_HealthInsurance_Medical = row.Table.Columns.Contains("LabourCost_HealthInsurance_Medical") == false ? 0 : (row.Field<decimal?>("LabourCost_HealthInsurance_Medical") == null ? 0 : row.Field<decimal>("LabourCost_HealthInsurance_Medical")),
                LabourCost_Other_WorkerCompensation = row.Table.Columns.Contains("LabourCost_WorkerCompensation") == false ? 0 : (row.Field<decimal?>("LabourCost_WorkerCompensation") == null ? 0 : row.Field<decimal>("LabourCost_WorkerCompensation")),
                LabourCost_Other_EmployeeBenefit = row.Table.Columns.Contains("LabourCost_EmployeeBenefit") == false ? 0 : (row.Field<decimal?>("LabourCost_EmployeeBenefit") == null ? 0 : row.Field<decimal>("LabourCost_EmployeeBenefit")),


                Maintenance_Annual = row.Field<decimal>("Maintenance_Annual"),
                Maintenance_Cleaning = row.Field<decimal>("Maintenance_Cleaning"),
                Maintenance_Repair = row.Field<decimal>("Maintenance_Repair"),
                Maintenance_Waste = row.Field<decimal>("Maintenance_Waste"),

                MarketingCost_Advertising = row.Field<decimal>("MarketingCost_Advertising"),
                MarketingCost_BusinessTieUp = row.Field<decimal>("MarketingCost_BusinessTieUp"),
                MarketingCost_Commission = row.Field<decimal>("MarketingCost_Commission"),
                MarketingCost_CommissonOnline = row.Field<decimal>("MarketingCost_CommissonOnline"),
                MarketingCost_ComplimentaryGuest = row.Field<decimal>("MarketingCost_ComplimentaryGuest"),
                MarketingCost_Dues = row.Field<decimal>("MarketingCost_Dues"),
                MarketingCost_PubRelation = row.Field<decimal>("MarketingCost_PubRelation"),
                MarketingCost_CreditCardDiscount = row.Table.Columns.Contains("MarketingCost_CreditCardDiscount") == false ? 0 : (row.Field<decimal?>("MarketingCost_CreditCardDiscount") == null ? 0 : row.Field<decimal>("MarketingCost_CreditCardDiscount")),


                OtherExpense_Courier = row.Field<decimal>("OtherExpense_Courier"),
                OtherExpense_Freight = row.Field<decimal>("OtherExpense_Freight"),
                OtherExpense_Laundry = row.Field<decimal>("OtherExpense_Laundry"),
                OtherExpense_Misc = row.Field<decimal>("OtherExpense_Misc"),
                OtherExpense_Travel = row.Field<decimal>("OtherExpense_Travel"),
                OtherExpense_OtherDirect = row.Table.Columns.Contains("OtherExpense_OtherDirect") == false ? 0 : (row.Field<decimal?>("OtherExpense_OtherDirect") == null ? 0 : row.Field<decimal>("OtherExpense_OtherDirect")),
                OtherExpense_SuperVisionFees = row.Table.Columns.Contains("OtherExpense_SuperVisionFees") == false ? 0 : (row.Field<decimal?>("OtherExpense_SuperVisionFees") == null ? 0 : row.Field<decimal>("OtherExpense_SuperVisionFees")),
                //OtherExpense_OfficeSupplies = row.Field<decimal>("OtherExpense_OfficeSupplies"),

                Utilities_Electricity = row.Field<decimal>("Utilities_Electricity"),
                Utilities_GasCoal = row.Field<decimal>("Utilities_GasCoal"),
                Utilities_Telephone = row.Field<decimal>("Utilities_Telephone"),
                Utilities_Water = row.Field<decimal>("Utilities_Water"),

                Property_Cam = row.Field<decimal>("Property_Cam"),
                Property_LateFees = row.Field<decimal>("Property_LateFees"),
                Property_Rent = row.Field<decimal>("Property_Rent"),
                Property_RentPerc = row.Table.Columns.Contains("Property_RentPerc")== false? 0 : (row.Field<decimal?>("Property_RentPerc") == null ? 0: row.Field<decimal>("Property_RentPerc")),
                Property_Tax = row.Field<decimal>("Property_Tax"),

                Royalty_Charge = row.Field<decimal>("Royalty_Charge"),
                Royalty_Others = row.Field<decimal>("Royalty_Others"),
                Royalty_Penalty = row.Field<decimal>("Royalty_Penalty"),

                SaleDinein_Food = row.Table.Columns.Contains("SaleDinein_Food") == false ? 0 : (row.Field<decimal?>("SaleDinein_Food") == null ? 0 : row.Field<decimal>("SaleDinein_Food")),
                SaleDinein_Beverage = row.Table.Columns.Contains("SaleDinein_Beverage") == false ? 0 : (row.Field<decimal?>("SaleDinein_Beverage") == null ? 0 : row.Field<decimal>("SaleDinein_Beverage")),
                SaleDinein_Beer = row.Table.Columns.Contains("SaleDinein_Beer") == false ? 0 : (row.Field<decimal?>("SaleDinein_Beer") == null ? 0 : row.Field<decimal>("SaleDinein_Beer")),
                SaleDinein_Wine = row.Table.Columns.Contains("SaleDinein_Wine") == false ? 0 : (row.Field<decimal?>("SaleDinein_Wine") == null ? 0 : row.Field<decimal>("SaleDinein_Wine")),
                SaleDinein_Liquor = row.Table.Columns.Contains("SaleDinein_Liquor") == false ? 0 : (row.Field<decimal?>("SaleDinein_Liquor") == null ? 0 : row.Field<decimal>("SaleDinein_Liquor")),
                SaleDinein_TOBACCO = row.Table.Columns.Contains("SaleDinein_TOBACCO") == false ? 0 : (row.Field<decimal?>("SaleDinein_TOBACCO") == null ? 0 : row.Field<decimal>("SaleDinein_TOBACCO")),
                SaleDinein_Other = row.Table.Columns.Contains("SaleDinein_Other") == false ? 0 : (row.Field<decimal?>("SaleDinein_Other") == null ? 0 : row.Field<decimal>("SaleDinein_Other")),
                SaleDinein_Delivery = row.Table.Columns.Contains("SaleDinein_Delivery") == false ? 0 : (row.Field<decimal?>("SaleDinein_Delivery") == null ? 0 : row.Field<decimal>("SaleDinein_Delivery")),
                SaleDinein_TakeAway = row.Table.Columns.Contains("SaleDinein_TakeAway") == false ? 0 : (row.Field<decimal?>("SaleDinein_TakeAway") == null ? 0 : row.Field<decimal>("SaleDinein_TakeAway")),
                SaleDinein_CashierShortOver = row.Table.Columns.Contains("SaleDinein_CashierShortOver") == false ? 0 : (row.Field<decimal?>("SaleDinein_CashierShortOver") == null ? 0 : row.Field<decimal>("SaleDinein_CashierShortOver")),

                CustomerCount_Lunch = row.Table.Columns.Contains("CustomerCount_Lunch") == false ? 0 : (row.Field<decimal?>("CustomerCount_Lunch") == null ? 0 : row.Field<decimal>("CustomerCount_Lunch")),
                CustomerCount_Evening = row.Table.Columns.Contains("CustomerCount_Evening") == false ? 0 : (row.Field<decimal?>("CustomerCount_Evening") == null ? 0 : row.Field<decimal>("CustomerCount_Evening")),
                CustomerCount_Dinner = row.Table.Columns.Contains("CustomerCount_Dinner") == false ? 0 : (row.Field<decimal?>("CustomerCount_Dinner") == null ? 0 : row.Field<decimal>("CustomerCount_Dinner")),

                PurchaseSupplies_CUTLERY = row.Table.Columns.Contains("PurchaseSupplies_CUTLERY") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_CUTLERY") == null ? 0 : row.Field<decimal>("PurchaseSupplies_CUTLERY")),
                PurchaseSupplies_Office = row.Table.Columns.Contains("PurchaseSupplies_Office") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_Office") == null ? 0 : row.Field<decimal>("PurchaseSupplies_Office")),
                PurchaseSupplies_CLEANINGMATERIAL = row.Table.Columns.Contains("PurchaseSupplies_CLEANINGMATERIAL") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_CLEANINGMATERIAL") == null ? 0 : row.Field<decimal>("PurchaseSupplies_CLEANINGMATERIAL")),
                PurchaseSupplies_PACKAGINGMATERIAL = row.Table.Columns.Contains("PurchaseSupplies_PACKAGINGMATERIAL") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_PACKAGINGMATERIAL") == null ? 0 : row.Field<decimal>("PurchaseSupplies_PACKAGINGMATERIAL")),
                PurchaseSupplies_PRINTINGSTATIONARY = row.Table.Columns.Contains("PurchaseSupplies_PRINTINGSTATIONARY") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_PRINTINGSTATIONARY") == null ? 0 : row.Field<decimal>("PurchaseSupplies_PRINTINGSTATIONARY")),
                PurchaseSupplies_FOH = row.Table.Columns.Contains("PurchaseSupplies_FOH") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_FOH") == null ? 0 : row.Field<decimal>("PurchaseSupplies_FOH")),
                PurchaseSupplies_BOH = row.Table.Columns.Contains("PurchaseSupplies_BOH") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_BOH") == null ? 0 : row.Field<decimal>("PurchaseSupplies_BOH")),
                PurchaseSupplies_UNIFORM = row.Table.Columns.Contains("PurchaseSupplies_UNIFORM") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_UNIFORM") == null ? 0 : row.Field<decimal>("PurchaseSupplies_UNIFORM")),
                PurchaseSupplies_OTHER = row.Table.Columns.Contains("PurchaseSupplies_OTHER") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_OTHER") == null ? 0 : row.Field<decimal>("PurchaseSupplies_OTHER")),

                PurchaseDinein_Food = row.Table.Columns.Contains("PurchaseDinein_Food") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Food") == null ? 0 : row.Field<decimal>("PurchaseDinein_Food")),
                PurchaseDinein_Beverage = row.Table.Columns.Contains("PurchaseDinein_Beverage") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Beverage") == null ? 0 : row.Field<decimal>("PurchaseDinein_Beverage")),
                PurchaseDinein_Beer = row.Table.Columns.Contains("PurchaseDinein_Beer") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Beer") == null ? 0 : row.Field<decimal>("PurchaseDinein_Beer")),
                PurchaseDinein_Wine = row.Table.Columns.Contains("PurchaseDinein_Wine") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Wine") == null ? 0 : row.Field<decimal>("PurchaseDinein_Wine")),
                PurchaseDinein_Liquor = row.Table.Columns.Contains("PurchaseDinein_Liquor") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Liquor") == null ? 0 : row.Field<decimal>("PurchaseDinein_Liquor")),
                PurchaseDinein_TOBACCO = row.Table.Columns.Contains("PurchaseDinein_TOBACCO") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_TOBACCO") == null ? 0 : row.Field<decimal>("PurchaseDinein_TOBACCO")),
                PurchaseDinein_Other = row.Table.Columns.Contains("PurchaseDinein_Other") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Other") == null ? 0 : row.Field<decimal>("PurchaseDinein_Other")),
                PurchaseDinein_Delivery = row.Table.Columns.Contains("PurchaseDinein_Delivery") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_Delivery") == null ? 0 : row.Field<decimal>("PurchaseDinein_Delivery")),
                PurchaseDinein_TakeAway = row.Table.Columns.Contains("PurchaseDinein_TakeAway") == false ? 0 : (row.Field<decimal?>("PurchaseDinein_TakeAway") == null ? 0 : row.Field<decimal>("PurchaseDinein_TakeAway")),

                Totals = new MonthlyExpenseList
                {
                    Equipement_Total = row.Table.Columns.Contains("Equipement_Total") == false ? 0 : (row.Field<decimal?>("Equipement_Total") == null ? 0 : row.Field<decimal>("Equipement_Total")),
                    FinanceCharge_Total = row.Table.Columns.Contains("FinanceCharge_Total") == false ? 0 : (row.Field<decimal?>("FinanceCharge_Total") == null ? 0 : row.Field<decimal>("FinanceCharge_Total")),
                    ITSoftware_Total = row.Table.Columns.Contains("ITSoftware_Total") == false ? 0 : (row.Field<decimal?>("ITSoftware_Total") == null ? 0 : row.Field<decimal>("ITSoftware_Total")),
                    LabourCost_Total = row.Table.Columns.Contains("LabourCost_Total") == false ? 0 : (row.Field<decimal?>("LabourCost_Total") == null ? 0 : row.Field<decimal>("LabourCost_Total")),
                    Maintenance_Total = row.Table.Columns.Contains("Maintenance_Total") == false ? 0 : (row.Field<decimal?>("Maintenance_Total") == null ? 0 : row.Field<decimal>("Maintenance_Total")),
                    MarketingCost_Total = row.Table.Columns.Contains("MarketingCost_Total") == false ? 0 : (row.Field<decimal?>("MarketingCost_Total") == null ? 0 : row.Field<decimal>("MarketingCost_Total")),
                    OtherExpense_Total = row.Table.Columns.Contains("OtherExpense_Total") == false ? 0 : (row.Field<decimal?>("OtherExpense_Total") == null ? 0 : row.Field<decimal>("OtherExpense_Total")),
                    Sale_Total = row.Table.Columns.Contains("Sale_Total") == false ? 0 : (row.Field<decimal?>("Sale_Total") == null ? 0 : row.Field<decimal>("Sale_Total")),
                    Purchase_Total = row.Table.Columns.Contains("Purchase_Total") == false ? 0 : (row.Field<decimal?>("Purchase_Total") == null ? 0 : row.Field<decimal>("Purchase_Total")),
                    PurchaseSupplies_Total = row.Table.Columns.Contains("PurchaseSupplies_Total") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_Total") == null ? 0 : row.Field<decimal>("PurchaseSupplies_Total")),
                    Property_Total = row.Table.Columns.Contains("Property_Total") == false ? 0 : (row.Field<decimal?>("Property_Total") == null ? 0 : row.Field<decimal>("Property_Total")),
                    Royalty_Total = row.Table.Columns.Contains("Royalty_Total") == false ? 0 : (row.Field<decimal?>("Royalty_Total") == null ? 0 : row.Field<decimal>("Royalty_Total")),
                    Utilities_Total = row.Table.Columns.Contains("Utilities_Total") == false ? 0 : (row.Field<decimal?>("Utilities_Total") == null ? 0 : row.Field<decimal>("Utilities_Total")),
                },
                MarketingCost_DeliveryPartners = deliveryPartnersList
            }).FirstOrDefault();
            return _result;
        }

        public IEnumerable<MonthlyExpenseList> GetMonthlyExpensesEntries(Guid userId,int menuId, int? outletID = 0, int? expenseMonth = 0, int? expenseYear = 0, bool isActualExpense = false)
        {
            List<MonthlyExpenseList> _result = new List<MonthlyExpenseList>(); int isSuccess = 0; string returnMessage = "";
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    if (outletID != null && outletID > 0) dbCol.Add(new DBParameter("OutetId", outletID, DbType.Int32));
                    if (expenseMonth != null && expenseMonth > 0) dbCol.Add(new DBParameter("ExpenseMonth", expenseMonth, DbType.Int32));
                    if (expenseYear != null && expenseYear > 0) dbCol.Add(new DBParameter("ExpenseYear", expenseYear, DbType.Int32));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", isSuccess, DbType.Int32, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", returnMessage, DbType.String, ParameterDirection.Output));

                    DataSet dsData = Dbhelper.ExecuteDataSet((isActualExpense ? QueryList.GetActualMonthlyExpenseEntryList : QueryList.GetMonthlyExpenseEntryList), dbCol, CommandType.StoredProcedure);
                    _result = dsData.Tables[0].AsEnumerable().Select(row => new MonthlyExpenseList
                    {
                        MonthlyExpenseId = row.Field<int>("MonthlyExpenseId"),
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        ExpensePeriod = row.Field<string>("ExpensePeriod"),
                        ExpenseMonth = row.Field<int>("ExpenseMonth"),
                        ExpenseYear = row.Field<int>("ExpenseYear"),

                        Equipement_Total = row.Table.Columns.Contains("Equipement_Total") == false ? 0 : (row.Field<decimal?>("Equipement_Total") == null ? 0 : row.Field<decimal>("Equipement_Total")),
                        FinanceCharge_Total = row.Table.Columns.Contains("FinanceCharge_Total") == false ? 0 : (row.Field<decimal?>("FinanceCharge_Total") == null ? 0 : row.Field<decimal>("FinanceCharge_Total")),
                        ITSoftware_Total = row.Table.Columns.Contains("ITSoftware_Total") == false ? 0 : (row.Field<decimal?>("ITSoftware_Total") == null ? 0 : row.Field<decimal>("ITSoftware_Total")),
                        LabourCost_Total = row.Table.Columns.Contains("LabourCost_Total") == false ? 0 : (row.Field<decimal?>("LabourCost_Total") == null ? 0 : row.Field<decimal>("LabourCost_Total")),
                        Maintenance_Total = row.Table.Columns.Contains("Maintenance_Total") == false ? 0 : (row.Field<decimal?>("Maintenance_Total") == null ? 0 : row.Field<decimal>("Maintenance_Total")),
                        MarketingCost_Total = row.Table.Columns.Contains("MarketingCost_Total") == false ? 0 : (row.Field<decimal?>("MarketingCost_Total") == null ? 0 : row.Field<decimal>("MarketingCost_Total")),
                        OtherExpense_Total = row.Table.Columns.Contains("OtherExpense_Total") == false ? 0 : (row.Field<decimal?>("OtherExpense_Total") == null ? 0 : row.Field<decimal>("OtherExpense_Total")),
                        Purchase_Total = row.Table.Columns.Contains("Purchase_Total") == false ? 0 : (row.Field<decimal?>("Purchase_Total") == null ? 0 : row.Field<decimal>("Purchase_Total")),
                        Property_Total = row.Table.Columns.Contains("Property_Total") == false ? 0 : (row.Field<decimal?>("Property_Total") == null ? 0 : row.Field<decimal>("Property_Total")),
                        Royalty_Total = row.Table.Columns.Contains("Royalty_Total") == false ? 0 : (row.Field<decimal?>("Royalty_Total") == null ? 0 : row.Field<decimal>("Royalty_Total")),
                        Utilities_Total = row.Table.Columns.Contains("Utilities_Total") == false ? 0 : (row.Field<decimal?>("Utilities_Total") == null ? 0 : row.Field<decimal>("Utilities_Total")),
                        Sale_Total = row.Table.Columns.Contains("Sale_Total") == false ? 0 : (row.Field<decimal?>("Sale_Total") == null ? 0 : row.Field<decimal>("Sale_Total")),
                        PurchaseSupplies_Total = row.Table.Columns.Contains("PurchaseSupplies_Total") == false ? 0 : (row.Field<decimal?>("PurchaseSupplies_Total") == null ? 0 : row.Field<decimal>("PurchaseSupplies_Total")),

                        //Sale_Total = row.Field<decimal>("Sale_Total"),
                        //ASPG_Total = row.Field<decimal>("ASPG_Total"),
                        //SaleBreakUp_Total = row.Field<decimal>("SaleBreakUp_Total"),
                        //Utilization_Total = row.Field<decimal>("Utilization_Total"),
                    }).OrderByDescending(o => o.ExpenseYear).ThenByDescending(o=> o.ExpenseMonth).ToList();

                    
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetMonthlyExpensesEntries:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool UpdateBudget(BudgetModel _data, out string resultOutputMessage)
        {
            Logger.LogInfo("Started execution of UpdateYearlyBudget from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            bool IsSuccess = false; string ReturnMessage = string.Empty;
            var modelData = Common.ToXML(_data);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("BudgetDetails", modelData, DbType.Xml));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", IsSuccess, DbType.Boolean, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", ReturnMessage, DbType.String, 255, ParameterDirection.Output));

                    var _output = Dbhelper.ExecuteNonQueryForOutParameter(QueryList.UpdateYearlyBudget, dbCol, CommandType.StoredProcedure);

                    IsSuccess = Convert.ToBoolean(_output["IsSuccessFullyExecuted"].ToString());
                    ReturnMessage = _output["ReturnMessage"].ToString();

                    if (IsSuccess == true) Logger.LogInfo("    Output message of UpdateYearlyBudget from Repository TransactionRepository : " + ReturnMessage);
                    else Logger.LogInfo("    Failed Output message of UpdateYearlyBudget from Repository TransactionRepository : " + ReturnMessage);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository UpdateYearlyBudget:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed execution of UpdateYearlyBudget from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            });
            resultOutputMessage = ReturnMessage;
            return IsSuccess;
        }

        public bool UpdateDSREntry(DSREntry _data)
        {
            bool IsSuccess = false;
            var modelData = Common.ToXML(_data);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("DSREntryDetails", modelData, DbType.Xml));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateDSREntry, dbCol, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository UpdateDSREntry:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return IsSuccess;
        }

        public bool UpdateMonthlyExpense(MonthlyExpense _data, bool isActualExpense = false)
        {
            Logger.LogInfo("Started execution of UpdateMonthlyExpense from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            bool IsSuccess = false; string ReturnMessage = string.Empty;
            var modelData = Common.ToXML(_data);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("MonthlyExpenseDetails", modelData, DbType.Xml));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", IsSuccess, DbType.Boolean, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", ReturnMessage, DbType.String, ParameterDirection.Output));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery((isActualExpense ? QueryList.UpdateActualMonthlyExpenseEntry : QueryList.UpdateMonthlyExpenseEntry), dbCol, CommandType.StoredProcedure));
                    if (IsSuccess == true) Logger.LogInfo("    Output message of UpdateMonthlyExpense from Repository TransactionRepository : " + ReturnMessage);
                    else Logger.LogInfo("    Failed Output message of UpdateMonthlyExpense from Repository TransactionRepository : " + ReturnMessage);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository UpdateMonthlyExpense:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed execution of UpdateMonthlyExpense from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            });
            return IsSuccess;
        }

        public BudgetModel GetBudgetDetailsByID(int BudgetId)
        {
            BudgetModel _result = null; string outputXML = string.Empty;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("BudgetId", BudgetId, DbType.Int32));
                    dbCol.Add(new DBParameter("Output", outputXML, DbType.Xml, ParameterDirection.Output));

                    var _params = Dbhelper.ExecuteNonQueryForOutParameter(QueryList.GetBudgetDetailByID, dbCol, CommandType.StoredProcedure);
                    outputXML = _params["Output"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(outputXML))
                    _result = Common.XMLToObject<BudgetModel>(outputXML);

            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetMonthlyExpensesEntries:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        

        public List<BudgetList_ForGrid> GetBudgetList(Guid userId, int menuId)
        {
            List<BudgetList_ForGrid> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {                    
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBudgetList, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new BudgetList_ForGrid
                    {
                        BudgetId = row.Field<int>("BudgetId"),
                        OutletId = row.Field<int>("OutletId"),
                        OutletName = row.Field<string>("OutletName"),
                        SeatingCapacity = row.Field<int>("SeatingCapacity"),
                        CashFlow = row.Field<decimal>("CashFlow"),
                        StartFinancialMonthYear = row.Field<string>("StartFinancialMonthYear"),
                        EndFinancialMonthYear = row.Field<string>("EndFinancialMonthYear")
                    }).OrderBy(o => o.BudgetId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetDSREnties:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<DailyExpense> GetDailyExpenseEntries(Guid userId, int menuId, int outletID, int expenseMonth, int expenseYear)
        {
            List<DailyExpense> _result = null; int isSuccess = 0; string returnMessage = "";
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletID", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("ExpenseMonth", expenseMonth, DbType.Int32));
                    dbCol.Add(new DBParameter("ExpenseYear", expenseYear, DbType.Int32));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", isSuccess, DbType.Int32, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", returnMessage, DbType.String, ParameterDirection.Output));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetDailyExpenseEntries, dbCol, CommandType.StoredProcedure);
                    _result = dsData.Tables[0].AsEnumerable().Select(row => new DailyExpense
                    {
                        DailyExpenseId = row.Field<int>("DailyExpenseId"),
                        OutletID = row.Field<int>("OutletID"),                        
                        ExpenseDay = row.Field<int>("ExpenseDay"),
                        ExpenseMonth = row.Field<int>("ExpenseMonth"),
                        ExpenseYear = row.Field<int>("ExpenseYear"),                       

                        D_LabourCost_CTC = row.Field<decimal>("D_LabourCost_CTC"),
                      
                        D_ProductionCost_Beer = row.Field<decimal>("D_ProductionCost_Beer"),
                        D_ProductionCost_Beverage = row.Field<decimal>("D_ProductionCost_Beverage"),
                        D_ProductionCost_Food = row.Field<decimal>("D_ProductionCost_Food"),
                        D_ProductionCost_Liquor = row.Field<decimal>("D_ProductionCost_Liquor"),
                        D_ProductionCost_Other = row.Field<decimal>("D_ProductionCost_Other"),
                        D_ProductionCost_Tobacco = row.Field<decimal>("D_ProductionCost_Tobacco"),
                        D_ProductionCost_Wine = row.Field<decimal>("D_ProductionCost_Wine"),
                        D_ProductionCost_Total = row.Field<decimal>("D_ProductionCost_Wine"),

                        D_Supplies_BOH = row.Field<decimal>("D_Supplies_BOH"),
                        D_Supplies_Cleaning = row.Field<decimal>("D_Supplies_Cleaning"),
                        D_Supplies_Cutlery_Glassware = row.Field<decimal>("D_Supplies_Cutlery_Glassware"),
                        D_Supplies_FOH = row.Field<decimal>("D_Supplies_FOH"),
                        D_Supplies_Office = row.Field<decimal>("D_Supplies_Office"),
                        D_Supplies_Packaging = row.Field<decimal>("D_Supplies_Packaging"),
                        D_Supplies_Stationary = row.Field<decimal>("D_Supplies_Stationary"),
                        D_Supplies_Uniform = row.Field<decimal>("D_Supplies_Uniform"),
                        D_Supplies_Total = row.Field<decimal>("D_Supplies_Total"),
                        ExpenseDateStr = row.Field<string>("ExpenseDateStr"),
                        IsStock = row.Field<bool>("IsStock"),
                        IsOpeningStock = row.Field<bool>("IsOpeningStock")

                    }).OrderBy(o => o.ExpenseDay).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetMonthlyExpensesByID:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;

        }

        public bool SaveDailyExpense(DailyExpense DailyExpenseEntries)
        {
            Logger.LogInfo("Started execution of UpdateMonthlyExpense from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            bool IsSuccess = false; string ReturnMessage = string.Empty;
            var DailyEntries = DailyExpenseEntries.DailyExpenseEntries;
            
            var modelData = Common.ToXML(DailyEntries);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("DailyExpenses", modelData, DbType.Xml));
                    dbCol.Add(new DBParameter("IsEdit", DailyExpenseEntries.IsEdit, DbType.Boolean));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", IsSuccess, DbType.Boolean, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", ReturnMessage, DbType.String, ParameterDirection.Output));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.DailyExpenseEntries, dbCol, CommandType.StoredProcedure));
                    if (IsSuccess == true) Logger.LogInfo("Output message of SaveDailyExpense from Repository TransactionRepository : " + ReturnMessage);
                    else Logger.LogInfo("Failed Output message of SaveDailyExpense from Repository TransactionRepository : " + ReturnMessage);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository SaveDailyExpense:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed execution of SaveDailyExpense from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            });
            return IsSuccess;
        }

        
    }
}