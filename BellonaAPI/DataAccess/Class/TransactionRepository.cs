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
using BellonaAPI.Models.Masters;

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

        public IEnumerable<Cluster> getCluster(string userId, int? CityID)
        {
            List<Cluster> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CityID", CityID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDashboardCluster, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Cluster
                    {
                        ClusterID = row.Field<int>("ClusterID"),
                        ClusterName = row.Field<string>("ClusterName"),
                    }).OrderBy(o => o.CityName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Dashboard Repository GetCluster:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        //public IEnumerable<DSREntry> GetDSREntries(Guid userId, int menuId, int? dsrEntryId = null)
        //{
        //    List<DSREntry> _result = null;
        //    TryCatch.Run(() =>
        //    {
        //        using (DBHelper Dbhelper = new DBHelper())
        //        {
        //            DBParameterCollection dbCol = new DBParameterCollection();
        //            dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
        //            dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
        //            if (dsrEntryId != null && dsrEntryId > 0) dbCol.Add(new DBParameter("DSREntryId", dsrEntryId, DbType.Int32));

        //            DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetDSREntryList, dbCol, CommandType.StoredProcedure);

        //            var guestPartnersList = (dsData != null && dsData.Tables.Count > 2) ? dsData.Tables[2].AsEnumerable().Select(row => new GuestPartnersDailySales
        //            {
        //                DSREntryID = row.Field<int>("DSREntryID"),
        //                GuestPartnerName = row.Field<string>("GuestPartnerName"),
        //                GuestPartnerID = row.Field<int>("GuestPartnerID"),
        //                GuestCount = row.Field<int>("GuestCount")
        //            }).ToArray() : new GuestPartnersDailySales[] { };

        //            var deliveryPartnersList = (dsData != null && dsData.Tables.Count > 1) ? dsData.Tables[1].AsEnumerable().Select(row => new DeliveryPartnersDailySales
        //            {
        //                DSREntryID = row.Field<int>("DSREntryID"),
        //                DeliveryPartnerID = row.Field<int>("DeliveryPartnerID"),
        //                DeliveryPartnerName = row.Field<string>("DeliveryPartnerName"),
        //                SaleAmount = row.Field<decimal>("SaleAmount")
        //            }).ToArray() : new DeliveryPartnersDailySales[] { };

        //            _result = dsData.Tables[0].AsEnumerable().Select(row => new DSREntry
        //            {
        //                DSREntryID = row.Field<int>("DSREntryID"),
        //                OutletID = row.Field<int>("OutletID"),
        //                DSREntryDate = row.Field<DateTime>("DSREntryDate"),
        //                SaleLunchDinein = row.Field<decimal>("SaleLunchDinein"),
        //                SaleEveningDinein = row.Field<decimal>("SaleEveningDinein"),

        //                SaleDinnerDinein = row.Field<decimal>("SaleDinnerDinein"),
        //                SaleTakeAway = row.Field<decimal>("SaleTakeAway"),
        //                SaleFood = row.Field<decimal>("SaleFood"),
        //                SaleBeverage = row.Field<decimal>("SaleBeverage"),
        //                SaleWine = row.Field<decimal>("SaleWine"),
        //                SaleBeer = row.Field<decimal>("SaleBeer"),
        //                SaleLiquor = row.Field<decimal>("SaleLiquor"),
        //                SaleTobacco = row.Field<decimal>("SaleTobacco"),
        //                SaleOther = row.Field<decimal>("SaleOther"),
        //                ItemsPerBill = row.Field<int>("ItemsPerBill"),
        //                CTCSalary = row.Field<decimal>("CTCSalary"),
        //                ServiceCharge = row.Table.Columns.Contains("ServiceCharge") == false ? 0 : (row.Field<decimal?>("ServiceCharge") == null ? 0 : row.Field<decimal>("ServiceCharge")),

        //                TotalNoOfBills = row.Field<int>("TotalNoOfBills"),
        //                GuestCountLunch = row.Field<int>("GuestCountLunch"),
        //                GuestCountEvening = row.Field<int>("GuestCountEvening"),
        //                GuestCountDinner = row.Field<int>("GuestCountDinner"),
        //                CashCollected = row.Field<decimal>("CashCollected"),
        //                CashStatus = row.Field<int>("CashStatus"),
        //                DeliveryPartners = dsrEntryId != null && dsrEntryId > 0 ? deliveryPartnersList : new DeliveryPartnersDailySales[] { },
        //                GuestPartners = dsrEntryId != null && dsrEntryId > 0 ? guestPartnersList : new GuestPartnersDailySales[] { },
        //                TotalDelivery = (deliveryPartnersList.Where(w => w.DSREntryID == row.Field<int>("DSREntryID")).Sum(s => s.SaleAmount) + row.Field<decimal>("SaleTakeAway")),
        //                TotalGuestByPartners = guestPartnersList.Where(w => w.DSREntryID == row.Field<int>("DSREntryID")).Sum(s => s.GuestCount),
        //            }).OrderBy(o => o.DSREntryDate).ToList();

        //            if (_result != null && _result.Count > 0)
        //            {
        //                _result.ForEach(f =>
        //                {
        //                    f.TotalDinein = (f.SaleFood + f.SaleLiquor + f.SaleBeer + f.SaleWine + f.SaleTobacco + f.SaleOther + f.SaleBeverage);
        //                    //f.TotalDinein = (f.SaleLunchDinein + f.SaleEveningDinein + f.SaleDinnerDinein);  CHANGED BECAUSE NOW NOT ALL OUTLET PROVIDES LUNCH/EVEING/DINNER DETAILS
        //                    f.TotalGuestDirect = (f.GuestCountLunch + f.GuestCountEvening + f.GuestCountDinner);
        //                    //f.TotalDelivery = (f.DeliveryPartners.Where(w=> w.DSREntryID == f.DSREntryID).Sum(s => s.SaleAmount) + f.SaleTakeAway);
        //                    f.TotalSale = f.TotalDinein + f.TotalDelivery;
        //                    //f.TotalGuestByPartners = f.GuestPartners.Where(w => w.DSREntryID == f.DSREntryID).Sum(s => s.GuestCount);
        //                });
        //            }
        //        }
        //    }).IfNotNull((ex) =>
        //    {
        //        Logger.LogError("Error in TransactionRepository GetDSREnties:" + ex.Message + Environment.NewLine + ex.StackTrace);
        //    });

        //    return _result;
        //}

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
                MonthlyExpenseID = row.Table.Columns.Contains("MonthlyExpenseID") == false ? 0 : (row.Field<int?>("MonthlyExpenseID") == null ? 0 : row.Field<int>("MonthlyExpenseID")),
                DeliveryPartnerID = row.Table.Columns.Contains("DeliveryPartnerID") == false ? 0 : (row.Field<int?>("DeliveryPartnerID") == null ? 0 : row.Field<int>("DeliveryPartnerID")),
                DeliveryPartnerName = row.Field<string>("DeliveryPartnerName"),
                Amount = row.Table.Columns.Contains("Amount") == false ? 0 : (row.Field<decimal?>("Amount") == null ? 0 : row.Field<decimal>("Amount"))
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
                Property_RentPerc = row.Table.Columns.Contains("Property_RentPerc") == false ? 0 : (row.Field<decimal?>("Property_RentPerc") == null ? 0 : row.Field<decimal>("Property_RentPerc")),
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

        public IEnumerable<MonthlyExpenseList> GetMonthlyExpensesEntries(Guid userId, int menuId, int? outletID = 0, int? expenseMonth = 0, int? expenseYear = 0, bool isActualExpense = false)
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
                    }).OrderByDescending(o => o.ExpenseYear).ThenByDescending(o => o.ExpenseMonth).ToList();


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

        public List<DailyExpense> GetDailyExpenseEntries(Guid userId, int menuId, int outletID, int expenseMonth, int expenseYear, int week)
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
                    dbCol.Add(new DBParameter("Week", week, DbType.Int32));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", isSuccess, DbType.Int32, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", returnMessage, DbType.String, ParameterDirection.Output));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetDailyExpenseEntries, dbCol, CommandType.StoredProcedure);
                    _result = dsData.Tables[0].AsEnumerable().Select(row => new DailyExpense
                    {
                        DailyExpenseId = row.Field<int>("DailyExpenseId"),
                        OutletID = row.Field<int>("OutletID"),
                        Week = row.Field<int>("WeekNo"),
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

        public List<WeekModel> GetAllWeeks(Guid userId, string year, int outletId)
        {
            List<WeekModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("Year", year, DbType.String));
                    dbCol.Add(new DBParameter("OutletID", outletId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllWeeks, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new WeekModel
                    {
                        DateRangeId = row.Field<int>("DateRangeId"),
                        WeekRange = row.Field<string>("WeekRange"),
                        Period = row.Field<string>("Period"),
                        Days = row.Field<string>("Days"),
                        Dates = row.Field<string>("Dates"),
                        WeekNo = row.Field<string>("WeekNo"),
                        FinancialYear = row.Field<string>("FinancialYear"),
                        IsExist = row.Field<int>("IsExist"),
                        WeekDate = Convert.ToDateTime(row.Field<DateTime>("WeekDate")).ToString("yyyy-MM-dd"),

                    }).OrderBy(o => o.DateRangeId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetAllWeeks:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public List<financialYear> GetFinancialYear(Guid userId)
        {
            List<financialYear> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDistinctYear, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new financialYear
                    {
                        Year = row.Field<string>("Year"),
                        IsCurrentYear = row.Field<int>("IsCurrentYear")
                    }).OrderBy(o => o.Year).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetFinancialYear:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool SaveWeeklyExpense(WeeklyExpenseModel _data)
        {

            bool IsSuccess = false; string ReturnMessage = string.Empty;
            var modelData = Common.ToXML(_data);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("WeeklyExpenseDetails", modelData, DbType.Xml));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", IsSuccess, DbType.Boolean, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", ReturnMessage, DbType.String, ParameterDirection.Output));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.SaveWeeklyExpense, dbCol, CommandType.StoredProcedure));
                    if (IsSuccess) Logger.LogInfo("Output message of UpdateMonthlyExpense from Repository TransactionRepository : " + ReturnMessage);
                    else Logger.LogInfo("    Failed Output message of UpdateMonthlyExpense from Repository TransactionRepository : " + ReturnMessage);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository WeeklyExpenses :" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed execution of UpdateMonthlyExpense from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            });
            return IsSuccess;
        }

        #region SalesBudget
        public List<SalesCategoryModel> GetSalesCategory()
        {
            List<SalesCategoryModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSalesCategory, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SalesCategoryModel
                    {
                        SalesCategoryID = row.Field<int>("SalesCategoryID"),
                        SalesCategory = row.Field<string>("SalesCategory")
                    }).OrderBy(o => o.SalesCategory).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSalesCategory:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public SalesBudget GetSalesBudget(int? OutletID, int? Year, int? Month)
        {
            SalesBudget _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection pm = new DBParameterCollection();
                    pm.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    pm.Add(new DBParameter("Year", Year, DbType.Int32));
                    pm.Add(new DBParameter("Month", Month, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSalesBudget, pm, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SalesBudget
                    {
                        SalesBudgetID = row.Field<int>("SalesBudgetID"),
                        TotalBudgetAmount = row.Field<decimal>("TotalBudgetAmount"),
                    }).FirstOrDefault();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSalesBudget:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public List<SalesCategoryBudget> GetSalesCategoryBudget(int? OutletID, int? Year, int? Month)
        {
            List<SalesCategoryBudget> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection pm = new DBParameterCollection();
                    pm.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    pm.Add(new DBParameter("Year", Year, DbType.Int32));
                    pm.Add(new DBParameter("Month", Month, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSalesCategoryBudget, pm, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SalesCategoryBudget
                    {
                        SalesCategoryID = row.Field<int>("SalesCategoryID"),
                        SalesCategory = row.Field<string>("SalesCategory"),
                        CategorySalesPercentage = row.Field<decimal?>("CategorySalesPercentage"),
                        CategorySalesAmount = row.Field<decimal?>("CategorySalesAmount"),
                    }).OrderBy(o => o.SalesCategory).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSalesCategoryBudget:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public List<SalesDayBudget> GetSalesDayBudget(int? OutletID, int? Year, int? Month)
        {
            List<SalesDayBudget> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection pm = new DBParameterCollection();
                    pm.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    pm.Add(new DBParameter("Year", Year, DbType.Int32));
                    pm.Add(new DBParameter("Month", Month, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSalesDayBudget, pm, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SalesDayBudget
                    {
                        Day = row.Field<string>("Day"),
                        NumberOfDays = row.Field<int>("NumberOfDays"),
                        DaySalesPercentage = row.Field<decimal?>("DaySalesPercentage"),
                        DaySalesAmount = row.Field<decimal?>("DaySalesAmount"),
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSalesDayBudget:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool SaveSalesBudget(SalesBudget model)
        {
            var SalesCategoryBudgetXML = model.SalesCategoryBudget != null ? Common.ToXML(model.SalesCategoryBudget) : string.Empty;
            var SalesDayBudgetXML = model.SalesDayBudget != null ? Common.ToXML(model.SalesDayBudget) : string.Empty;
            int SalesBudgetID = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletID", model.OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Year", model.Year, DbType.Int32));
                    paramCollection.Add(new DBParameter("Month", model.Month, DbType.Int32));
                    paramCollection.Add(new DBParameter("TotalBudgetAmount", model.TotalBudgetAmount, DbType.Decimal));
                    paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedBy", model.UpdatedBy, DbType.String));
                    paramCollection.Add(new DBParameter("SalesCategoryBudgetXML", SalesCategoryBudgetXML, DbType.Xml));
                    paramCollection.Add(new DBParameter("SalesDayBudgetXML", SalesDayBudgetXML, DbType.Xml));

                    var iResult = dbHelper.ExecuteScalar(QueryList.SaveSalesBudget, paramCollection, transaction, CommandType.StoredProcedure);
                    SalesBudgetID = Convert.ToInt32(iResult.ToString());

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });

                return SalesBudgetID > 0;
            }
        }
        public List<SalesBudgetDetail> GetSalesBudgetDetails(int? OutletID, int? Year, int? Month)
        {
            List<SalesBudgetDetail> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection pm = new DBParameterCollection();
                    pm.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    pm.Add(new DBParameter("Year", Year, DbType.Int32));
                    pm.Add(new DBParameter("Month", Month, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSalesBudgetDetails, pm, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SalesBudgetDetail
                    {
                        SalesBudgetDetailsID = row.Field<long>("SalesBudgetDetailsID"),
                        SalesBudgetID = row.Field<int>("SalesBudgetID"),
                        SalesCategoryID = row.Field<int>("SalesCategoryID"),
                        SalesCategory = row.Field<string>("SalesCategory"),
                        CategoryAmount = row.Field<decimal>("CategoryAmount"),
                        Date = row.Field<DateTime>("Date"),
                        strDate = row.Field<string>("strDate"),
                        Year = row.Field<int>("Year"),
                        Month = row.Field<int>("Month"),
                        WeekNo = row.Field<int>("WeekNo"),
                        DayID = row.Field<int>("DayID"),
                        DayName = row.Field<string>("DayName"),
                        DayNo = row.Field<int>("DayNo"),
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSalesBudgetDetails:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        #endregion SalesBudget

        #region TBUpload
        public List<TBErrorLog> CheckTBErrorLog()
        {
            List<TBErrorLog> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.CheckTBErrorLog, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new TBErrorLog
                    {
                        Id = row.Field<int>("Id"),
                        ErrorProcess = row.Field<string>("ErrorProcess"),
                        FileId = row.Field<int>("FileId"),
                        ErrorMessage = row.Field<string>("ErrorMessage"),
                        RowNumber = row.Field<int>("RowNumber"),
                        ColNumber = row.Field<int>("ColNumber"),
                        ColName = row.Field<string>("ColName"),
                        ErrorTime = row.Field<DateTime>("ErrorTime"),
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository CheckTBErrorLog:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion TBUpload


        public List<WeeklyExpense> GetWeeklyExpense(Guid userId, int menuId, int outletID, string expenseYear, string week)
        {
            List<WeeklyExpense> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletID", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("FinancialYear", expenseYear, DbType.String));
                    dbCol.Add(new DBParameter("Weeks", week, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetWeeklyExpense, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new WeeklyExpense
                    {
                        WeeklyExpenseId = row.Field<int>("WeeklyExpenseId"),
                        OutletID = row.Field<int>("OutletID"),
                        ExpenseMonth = row.Field<int>("ExpenseMonth"),
                        ExpenseYear = row.Field<int>("ExpenseYear"),
                        FinancialYear = row.Field<string>("FinancialYear"),
                        ExpenseWeek = row.Field<int>("ExpenseWeek"),
                        WeekNo = row.Field<string>("Weeks"),
                        EquipmentHireCharges_DJEvents = row.Field<decimal>("EquipmentHireCharges_DJEvents"),
                        EquipmentHireCharges_Kitchen = row.Field<decimal>("EquipmentHireCharges_Kitchen"),
                        EquipmentHireCharges_Banquet = row.Field<decimal>("EquipmentHireCharges_Banquet"),
                        Total_EquipmentHireCharges = row.Field<decimal>("Total_EquipmentHireCharges"),

                        BusinessPromotion_AdvertisementExpenses = row.Field<decimal>("BusinessPromotion_AdvertisementExpenses"),
                        BusinessPromotion_CommissionBrokerageExpenses = row.Field<decimal>("BusinessPromotion_CommissionBrokerageExpenses"),
                        BusinessPromotion_Entertainment = row.Field<decimal>("BusinessPromotion_Entertainment"),
                        BusinessPromotion_SalesPromotion = row.Field<decimal>("BusinessPromotion_SalesPromotion"),
                        Total_BusinessPromotionMarketing = row.Field<decimal>("Total_BusinessPromotionMarketing"),

                        FinanceCost_BankCharges = row.Field<decimal>("FinanceCost_BankCharges"),
                        FinanceCost_CommissionOnAggregators = row.Field<decimal>("FinanceCost_CommissionOnAggregators"),
                        FinanceCost_CommissionOnCardSettlement = row.Field<decimal>("FinanceCost_CommissionOnCardSettlement"),
                        Total_FinanceCost = row.Field<decimal>("Total_FinanceCost"),

                        NonFoodConsumable_GeneralSupplies = row.Field<decimal>("NonFoodConsumable_GeneralSupplies"),
                        NonFoodConsumable_LiquidContainerNitrogen = row.Field<decimal>("NonFoodConsumable_LiquidContainerNitrogen"),
                        NonFoodConsumable_PackingMaterials = row.Field<decimal>("NonFoodConsumable_PackingMaterials"),
                        Total_NonFoodConsumable = row.Field<decimal>("Total_NonFoodConsumable"),

                        LegalFees_LegalCharges = row.Field<decimal>("LegalFees_LegalCharges"),
                        LegalFees_ProfessionalCharges = row.Field<decimal>("LegalFees_ProfessionalCharges"),
                        LegalFees_StatutoryAuditFees = row.Field<decimal>("LegalFees_StatutoryAuditFees"),
                        Total_LegalFees = row.Field<decimal>("Total_LegalFees"),

                        LicenseFees_FranchiseFees = row.Field<decimal>("LicenseFees_FranchiseFees"),
                        LicenseFees_LicensePermitCharges = row.Field<decimal>("LicenseFees_LicensePermitCharges"),
                        Total_LicenseFees = row.Field<decimal>("Total_LicenseFees"),

                        ManPowerCost_BonusToStaff = row.Field<decimal>("ManPowerCost_BonusToStaff"),
                        ManPowerCost_CasualLabourCharges = row.Field<decimal>("ManPowerCost_CasualLabourCharges"),
                        ManPowerCost_ConveyanceAllowance = row.Field<decimal>("ManPowerCost_ConveyanceAllowance"),
                        ManPowerCost_ESIC = row.Field<decimal>("ManPowerCost_ESIC"),
                        ManPowerCost_PF = row.Field<decimal>("ManPowerCost_PF"),
                        ManPowerCost_HousekeepingExpenses = row.Field<decimal>("ManPowerCost_HousekeepingExpenses"),
                        ManPowerCost_Insurance = row.Field<decimal>("ManPowerCost_Insurance"),
                        ManPowerCost_LeaveEncashment = row.Field<decimal>("ManPowerCost_LeaveEncashment"),
                        ManPowerCost_MedicalExpenses = row.Field<decimal>("ManPowerCost_MedicalExpenses"),
                        ManPowerCost_PFAdmin = row.Field<decimal>("ManPowerCost_PFAdmin"),
                        ManPowerCost_RecruitmentCost = row.Field<decimal>("ManPowerCost_RecruitmentCost"),
                        ManPowerCost_SalaryAllowances = row.Field<decimal>("ManPowerCost_SalaryAllowances"),
                        ManPowerCost_ServiceChargePayOut = row.Field<decimal>("ManPowerCost_ServiceChargePayOut"),
                        ManPowerCost_AccommodationElectricityCharges = row.Field<decimal>("ManPowerCost_AccommodationElectricityCharges"),
                        ManPowerCost_AccommodationWaterCharges = row.Field<decimal>("ManPowerCost_AccommodationWaterCharges"),
                        ManPowerCost_StaffFoodExpense = row.Field<decimal>("ManPowerCost_StaffFoodExpense"),
                        ManPowerCost_StaffRoomRent = row.Field<decimal>("ManPowerCost_StaffRoomRent"),
                        ManPowerCost_StaffWelfareExpenses = row.Field<decimal>("ManPowerCost_StaffWelfareExpenses"),
                        ManPowerCost_Uniforms = row.Field<decimal>("ManPowerCost_Uniforms"),
                        ManPowerCost_SecurityExpenses = row.Field<decimal>("ManPowerCost_SecurityExpenses"),
                        Total_ManPowerCost = row.Field<decimal>("Total_ManPowerCost"),

                        OtherOperational_CCGPurchase = row.Field<decimal>("OtherOperational_CCGPurchase"),
                        OtherOperational_ConveyanceExpenses = row.Field<decimal>("OtherOperational_ConveyanceExpenses"),
                        OtherOperational_FreightCharges = row.Field<decimal>("OtherOperational_FreightCharges"),
                        OtherOperational_MiscellaneousCharges = row.Field<decimal>("OtherOperational_MiscellaneousCharges"),
                        OtherOperational_LaundryCharges = row.Field<decimal>("OtherOperational_LaundryCharges"),
                        OtherOperational_LodgingBoarding = row.Field<decimal>("OtherOperational_LodgingBoarding"),
                        OtherOperational_OfficeExpense = row.Field<decimal>("OtherOperational_OfficeExpense"),
                        OtherOperational_OfficeExpensesGuest = row.Field<decimal>("OtherOperational_OfficeExpensesGuest"),
                        OtherOperational_PrintingStationary = row.Field<decimal>("OtherOperational_PrintingStationary"),
                        OtherOperational_Transportation = row.Field<decimal>("OtherOperational_Transportation"),
                        OtherOperational_TravellingLocal = row.Field<decimal>("OtherOperational_TravellingLocal"),
                        Total_OtherOperational = row.Field<decimal>("Total_OtherOperational"),

                        PrintingStationery_PostageCourier = row.Field<decimal>("PrintingStationery_PostageCourier"),

                        RentOccupationCost_CAMCharges = row.Field<decimal>("RentOccupationCost_CAMCharges"),
                        RentOccupationCost_PropertyTax = row.Field<decimal>("RentOccupationCost_PropertyTax"),
                        RentOccupationCost_PropertyTaxMall = row.Field<decimal>("RentOccupationCost_PropertyTaxMall"),
                        RentOccupationCost_RentRevenueCharges = row.Field<decimal>("RentOccupationCost_RentRevenueCharges"),
                        Total_RentOccupationCost = row.Field<decimal>("Total_RentOccupationCost"),

                        RepairMaintenance_AMCComputerSoftware = row.Field<decimal>("RepairMaintenance_AMCComputerSoftware"),
                        RepairMaintenance_PestControlAC = row.Field<decimal>("RepairMaintenance_PestControlAC"),
                        RepairMaintenance_Civil = row.Field<decimal>("RepairMaintenance_Civil"),
                        RepairMaintenance_Others = row.Field<decimal>("RepairMaintenance_Others"),
                        Total_RepairMaintenance = row.Field<decimal>("Total_RepairMaintenance"),

                        TelephoneInternet_CableCharges = row.Field<decimal>("TelephoneInternet_CableCharges"),
                        TelephoneInternet_InternetExpenses = row.Field<decimal>("TelephoneInternet_InternetExpenses"),
                        TelephoneInternet_TelephoneExpenses = row.Field<decimal>("TelephoneInternet_TelephoneExpenses"),
                        TelephoneInternet_TelephoneExpensesMobile = row.Field<decimal>("TelephoneInternet_TelephoneExpensesMobile"),
                        Total_TelephoneInternet = row.Field<decimal>("Total_TelephoneInternet"),

                        UtilityEnergyCost_DGCharges = row.Field<decimal>("UtilityEnergyCost_DGCharges"),
                        UtilityEnergyCost_ElectricityCharges = row.Field<decimal>("UtilityEnergyCost_ElectricityCharges"),
                        UtilityEnergyCost_ElectricityInfraCharges = row.Field<decimal>("UtilityEnergyCost_ElectricityInfraCharges"),
                        UtilityEnergyCost_GasCharges = row.Field<decimal>("UtilityEnergyCost_GasCharges"),
                        UtilityEnergyCost_HVACCharges = row.Field<decimal>("UtilityEnergyCost_HVACCharges"),
                        UtilityEnergyCost_WaterCharges = row.Field<decimal>("UtilityEnergyCost_WaterCharges"),
                        Total_UtilityEnergyCost = row.Field<decimal>("Total_UtilityEnergyCost"),

                        Total_WeeklyExpense = row.Field<decimal>("Total_WeeklyExpense"),


                    }).OrderBy(o => o.WeeklyExpenseId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetWeeklyExpense:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        #region GET_DSR_Summary
        //public List<DSR_Summary> GetDSR_Summary(String outletCode, string startDate, string endDate)
        //{
        //    List<DSR_Summary> _result = null;
        //    TryCatch.Run(() =>
        //    {
        //        using (DBHelper Dbhelper = new DBHelper())
        //        {
        //            DBParameterCollection dbCol = new DBParameterCollection();
        //            dbCol.Add(new DBParameter("branchCode", outletCode, DbType.String));
        //            dbCol.Add(new DBParameter("Enddt", endDate, DbType.String));
        //            dbCol.Add(new DBParameter("Startdt", startDate, DbType.String));

        //            DataTable dsData = Dbhelper.ExecuteDataTable(QueryList.GetDSR_Summary, dbCol, CommandType.StoredProcedure);
        //            _result = dsData.AsEnumerable().Select(row => new DSR_Summary
        //            {
        //            //OutletID = row.Field<int>("OutletID"),
        //            BranchName = row.Field<string>("BranchName"),
        //                BranchCode = row.Field<string>("BranchCode"),

        //                FoodSaleNet = row.Field<decimal>("FoodSaleNet"),
        //                BeverageSaleNet = row.Field<decimal>("BeverageSaleNet"),
        //                LiquorSaleNet = row.Field<decimal>("LiquorSaleNet"),
        //                TobaccoSaleNet = row.Field<decimal>("TobaccoSaleNet"),
        //                OtherSale1Net = row.Field<decimal>("OtherSale1Net"),
        //                DiscountAmount = row.Field<decimal>("DiscountAmount"),
        //                ServiceChargeAmount = row.Field<decimal>("ServiceChargeAmount"),
        //                DirectCharge = row.Field<decimal>("DirectCharge"),
        //                SalesNetTotal = row.Field<decimal>("SalesNetTotal"),
        //                SalesTotalWithSC = row.Field<decimal>("SalesTotalWithSC"),

        //                DeliveryFoodSaleNet = row.Field<decimal>("DeliveryFoodSaleNet"),
        //                DeliveryBeverageSaleNet = row.Field<decimal>("DeliveryBeverageSaleNet"),
        //                DineInFoodSaleNet = row.Field<decimal>("DineInFoodSaleNet"),
        //                DineInBeverageSaleNet = row.Field<decimal>("DineInBeverageSaleNet"),
        //                DineInLiquorSaleNet = row.Field<decimal>("DineInLiquorSaleNet"),
        //                DineInTobaccoNet = row.Field<decimal>("DineInTobaccoNet"),
        //                DineInOthersNet = row.Field<decimal>("DineInOthersNet"),
        //                DineInCovers = row.Field<decimal>("DineInCovers"),
        //                ApcDineIn = row.Field<decimal>("ApcDineIn"),

        //                ZomatoDeliveryBillsNo = row.Field<decimal>("ZomatoDeliveryBillsNo"),
        //                ZomatoDeliverySaleNet = row.Field<decimal>("ZomatoDeliverySaleNet"),
        //                SwiggyDeliveryBillsNo = row.Field<decimal>("SwiggyDeliveryBillsNo"),
        //                SwiggyDeliverySaleNet = row.Field<decimal>("SwiggyDeliverySaleNet"),
        //                DeliveryChannel3BillsNo = row.Field<decimal>("DeliveryChannel3BillsNo"),
        //                DeliveryChannel3SaleNet = row.Field<decimal>("DeliveryChannel3SaleNet"),
        //                DeliveryBillsTotalNo = row.Field<decimal>("DeliveryBillsTotalNo"),
        //                DeliveryBillsAmountTotal = row.Field<decimal>("DeliveryBillsAmountTotal"),

        //                ZomatoDineInSaleNet = row.Field<decimal>("ZomatoDineInSaleNet"),
        //                ZomatoDineInCovers = row.Field<decimal>("ZomatoDineInCovers"),
        //                ZomatoDineInBills = row.Field<decimal>("ZomatoDineInBills"),
        //                AvgBillAmountZomato = row.Field<decimal>("AvgBillAmountZomato"),

        //                DineOutDineInSaleNet = row.Field<decimal>("DineOutDineInSaleNet"),
        //                DineOutDineInCovers = row.Field<decimal>("DineOutDineInCovers"),
        //                DineOutDineInBills = row.Field<decimal>("DineOutDineInBills"),
        //                AvgBillAmountDineOut = row.Field<decimal>("AvgBillAmountDineOut"),

        //                EazyDinerDineInSaleNet = row.Field<decimal>("EazyDinerDineInSaleNet"),
        //                EazyDinerDineInCovers = row.Field<decimal>("EazyDinerDineInCovers"),
        //                EazyDinerDineInBills = row.Field<decimal>("EazyDinerDineInBills"),
        //                AvgBillAmountEazyDiner = row.Field<decimal>("AvgBillAmountEazyDiner"),

        //                OtherAggregatorDineInSaleNet = row.Field<decimal>("OtherAggregatorDineInSaleNet"),
        //                OtherAggregatorDineInCovers = row.Field<decimal>("OtherAggregatorDineInCovers"),
        //                OtherAggregatorDineInBills = row.Field<decimal>("OtherAggregatorDineInBills"),
        //                AvgBillAmountOtherAggregator = row.Field<decimal>("AvgBillAmountOtherAggregator"),

        //            }).OrderBy(o => o.BranchName).ToList();
        //        }
        //    }).IfNotNull((ex) =>
        //            {
        //                Logger.LogError("Error in TransactionRepository GET DRS_Summery:" + ex.Message + Environment.NewLine + ex.StackTrace);
        //            });

        //    return _result;

        //}

        public List<DSR_Summary> GetDSR_Summary(string outletCode, string startDate, string endDate, int cityId, int clusterId)
        {
            List<DSR_Summary> _result = null;
            TryCatch.Run(() =>
            {

                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("Enddt", endDate, DbType.String));
                    dbCol.Add(new DBParameter("Startdt", startDate, DbType.String));

                    if (outletCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", outletCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dsData = Dbhelper.ExecuteDataTable(QueryList.GetDSR_Summary, dbCol, CommandType.StoredProcedure);
                    _result = dsData.AsEnumerable().Select(row => CreateSummaryFromRow(row)).OrderBy(o => o.BranchName).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GET DRS_Summery:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        private DSR_Summary CreateSummaryFromRow(DataRow row)
        {
            var summary = new DSR_Summary();
            foreach (var prop in typeof(DSR_Summary).GetProperties())
            {
                if (row.Table.Columns.Contains(prop.Name))
                {
                    if (row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(summary, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }
            }
            return summary;
        }
        #endregion GET_DSR_Summary    

        #region weeklyMIS
        public List<WeeklyMIS> GetWeeklySaleDetails(string FinancialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<WeeklyMIS> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetWeeklySaleDetails, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new WeeklyMIS
                    {
                        InvoiceDay = row.Field<string>("InvoiceDay"),
                        FoodSale = row.Field<decimal>("FoodSale"),
                        BeverageSale = row.Field<decimal>("BeverageSale"),
                        LiquorSale = row.Field<decimal>("LiquorSale"),
                        TobaccoSale = row.Field<decimal>("TobaccoSale"),
                        OtherSale = row.Field<decimal>("OtherSale")
                    }).OrderBy(o => o.InvoiceDay).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetWeeklyExpense:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<SalesVsBudget> GetLast12Weeks_SalesVsBudget(string financialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<SalesVsBudget> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("FINANCIALYEAR", financialYear, DbType.String));
                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetLast12Weeks_SalesVsBudget, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new SalesVsBudget
                    {
                        Date = row.Field<string>("Date"),
                        NetAmount = row.Field<decimal>("NetAmount"),
                        BudgetAmount = row.Field<decimal>("BudgetAmount"),
                    }).OrderBy(o => o.Date).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetLast12Weeks_SalesVsBudget:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<WeeklyCoversTrend> GetWeeklyCoversTrend(string financialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<WeeklyCoversTrend> result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    var dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    dbCol.Add(new DBParameter("FINANCIALYEAR", financialYear, DbType.String));

                    if (!string.IsNullOrEmpty(branchCode))
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    // Execute the stored procedure and retrieve data
                    DataTable dtData = dbHelper.ExecuteDataTable(QueryList.GetWeekly_CoversTrend, dbCol, CommandType.StoredProcedure);

                    // Get date columns, excluding "SessionName"
                    var dateColumns = dtData.Columns.Cast<DataColumn>()
                        .Where(col => col.ColumnName != "SessionName")
                        .Select(col => col.ColumnName)
                        .ToList();

                    // Process the data into a list of WeeklyCoversTrend
                    result = dtData.AsEnumerable()
                        .Select(row => new WeeklyCoversTrend
                        {
                            SessionName = row.Field<string>("SessionName"),
                            SessionDetails = dateColumns.ToDictionary(
                                date => date,
                                date => row.IsNull(date) ? 0 : Convert.ToInt32(row[date])
                            )
                        })
                        .ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in MealRepository GetWeeklyCoversTrend: " + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return result;
        }

        public List<BeverageVsBudgetTrend> GetBeverageVsBudgetTrend(string financialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<BeverageVsBudgetTrend> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("FINANCIALYEAR", financialYear, DbType.String));
                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBeverageVsBudgetTrend, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new BeverageVsBudgetTrend
                    {
                        Date = row.Field<string>("Date"),
                        NetAmount = row.Field<decimal>("NETAMOUNT"),
                        BudgetAmount = row.Field<decimal>("BudgetAmount"),
                    }).OrderBy(o => o.Date).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetBeverageVsBudgetTrend:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<TobaccoVsBudgetTrend> GetTobaccoVsBudgetTrend(string financialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<TobaccoVsBudgetTrend> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("FINANCIALYEAR", financialYear, DbType.String));
                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetTobaccoVsBudgetTrend, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new TobaccoVsBudgetTrend
                    {
                        Date = row.Field<string>("Date"),
                        NetAmount = row.Field<decimal>("NETAMOUNT"),
                        BudgetAmount = row.Field<decimal>("BudgetAmount"),
                    }).OrderBy(o => o.Date).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetTobaccoVsBudgetTrend:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<TimeWiseSalesBreakup> GetTimeWiseSalesBreakup(string FinancialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<TimeWiseSalesBreakup> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    dbCol.Add(new DBParameter("FINANCIALYEAR", FinancialYear, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetTimeWiseSalesBreakup, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new TimeWiseSalesBreakup
                    {
                        SessionName = row.Field<string>("SessionName"),
                        Session_NetAmount = row.Field<decimal>("SESSION_NETAMOUNT"),
                        Total_NetAmount = row.Field<decimal>("TOTAL_NETAMOUNT"),
                        Percentage = row.Field<decimal>("Percentage")                       
                    }).OrderBy(o => o.SessionName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetWeeklyExpense:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<AverageCoverTrend> GetAvgCoversTrend(string FinancialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<AverageCoverTrend> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("FINANCIALYEAR", FinancialYear, DbType.String));
                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAvgCoversTrend, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new AverageCoverTrend
                    {
                        InvoiceDay = row.Field<string>("InvoiceDay"),
                        ApcDineIn = row.Field<decimal>("ApcDineIn")
                    }).OrderBy(o => o.InvoiceDay).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository Get Average Covers Trend for 12weeks:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public List<LiquorVsBudgetTrend> GetLiquorVsBudgetTrend(string FinancialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<LiquorVsBudgetTrend> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("FINANCIALYEAR", FinancialYear, DbType.String));
                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetLiquorVsBudgetTrend, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new LiquorVsBudgetTrend
                    {
                        Date = row.Field<string>("Date"),
                        NetAmount = row.Field<decimal>("NETAMOUNT"),
                        BudgetAmount = row.Field<decimal>("BudgetAmount"),
                    }).OrderBy(o => o.Date).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository Get Liquor Vs Budget Trend for 12weeks:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<FoodVsBudgetTrend> GetFoodVsBudgetTrend(string FinancialYear, string week, string branchCode, int cityId, int clusterId)
        {
            List<FoodVsBudgetTrend> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("FINANCIALYEAR", FinancialYear, DbType.String));
                    dbCol.Add(new DBParameter("WEEK", week, DbType.String));
                    if (branchCode != "")
                    {
                        dbCol.Add(new DBParameter("branchCode", branchCode, DbType.String));
                    }
                    else if (clusterId > 0)
                    {
                        dbCol.Add(new DBParameter("clusterId", clusterId, DbType.Int32));
                    }
                    else if (cityId > 0)
                    {
                        dbCol.Add(new DBParameter("cityId", cityId, DbType.Int32));
                    }

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFoodVsBudgetTrend, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new FoodVsBudgetTrend
                    {
                        Date = row.Field<string>("Date"),
                        NetAmount = row.Field<decimal>("NETAMOUNT"),
                        BudgetAmount = row.Field<decimal>("BudgetAmount"),
                    }).OrderBy(o => o.Date).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository Get Food Vs Budget Trend for 12weeks:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion weeklyMIS

        #region DSR Snapshot
        public List<WeeklySnapshot> GetSanpshotWeeklyData(int WeekNo, string Year, int OutletId)
        {
            List<WeeklySnapshot> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("WeekNo", WeekNo, DbType.Int32));
                    dbCol.Add(new DBParameter("FinancialYear", Year, DbType.String));
                    dbCol.Add(new DBParameter("OutletId", OutletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSanpshotWeeklyData, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new WeeklySnapshot
                    {
                        SnapshotType = row.Field<string>("SnapshotType"),
                        Monday = row.Field<string>("Monday"),
                        Tuesday = row.Field<string>("Tuesday"),
                        Wednesday = row.Field<string>("Wednesday"),
                        Thursday = row.Field<string>("Thursday"),
                        Friday = row.Field<string>("Friday"),
                        Saturday = row.Field<string>("Saturday"),
                        Sunday = row.Field<string>("Sunday"),
                    }).OrderBy(o => o.SnapshotType).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSanpshotWeeklyData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool SaveSnapshotEntry(SnapshotModel SnapshotEntry)
        {

            bool IsSuccess = false; string ReturnMessage = string.Empty;
            var modelData = Common.ToXML(SnapshotEntry);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("SnapshotEntryDetails", modelData, DbType.Xml));
                    dbCol.Add(new DBParameter("IsSuccessFullyExecuted", IsSuccess, DbType.Boolean, ParameterDirection.Output));
                    dbCol.Add(new DBParameter("ReturnMessage", ReturnMessage, DbType.String, ParameterDirection.Output));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.SaveSnapshotEntry, dbCol, CommandType.StoredProcedure));
                    if (IsSuccess) Logger.LogInfo("Output message of UpdateMonthlyExpense from Repository TransactionRepository : " + ReturnMessage);
                    else Logger.LogInfo("    Failed Output message of UpdateMonthlyExpense from Repository TransactionRepository : " + ReturnMessage);
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository WeeklyExpenses :" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed execution of UpdateMonthlyExpense from Repository TransactionRepository at " + DateTime.Now.ToLongDateString());
            });
            return IsSuccess;
        }



        public List<WeeklySalesSnapshot> GetWeeklySalesSnapshot(string Week, string Year, int OutletId)
        {
            List<WeeklySalesSnapshot> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("Week", Week, DbType.String));
                    dbCol.Add(new DBParameter("Year", Year, DbType.String));
                    dbCol.Add(new DBParameter("OutletID", OutletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetWeeklySalesData, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new WeeklySalesSnapshot
                    {
                        Category = row.Field<string>("Category"),
                        Monday = row.Field<decimal>("Monday"),
                        Tuesday = row.Field<decimal>("Tuesday"),
                        Wednesday = row.Field<decimal>("Wednesday"),
                        Thursday = row.Field<decimal>("Thursday"),
                        Friday = row.Field<decimal>("Friday"),
                        Saturday = row.Field<decimal>("Saturday"),
                        Sunday = row.Field<decimal>("Sunday"),
                    }).OrderBy(o => o.Category).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetWeeklySalesSnapshot:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }



        public List<WeeklySnapshot> GetItem86SnapshotDetails(int WeekNo, string Year)
        {
            List<WeeklySnapshot> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("Week", WeekNo, DbType.Int32));
                    dbCol.Add(new DBParameter("FinancialYear", Year, DbType.String));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetItem86SnapshotDetails, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new WeeklySnapshot
                    {
                        SnapshotType = row.Field<string>("SnapshotType"),
                        ClusterName = row.Field<string>("ClusterName"),
                        OutletName = row.Field<string>("OutletName"),
                        Monday = row.Field<string>("Monday"),
                        Tuesday = row.Field<string>("Tuesday"),
                        Wednesday = row.Field<string>("Wednesday"),
                        Thursday = row.Field<string>("Thursday"),
                        Friday = row.Field<string>("Friday"),
                        Saturday = row.Field<string>("Saturday"),
                        Sunday = row.Field<string>("Sunday"),
                    }).OrderBy(o => o.SnapshotType).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetSanpshotWeeklyData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        #endregion DSR Snapshot
    }
}


