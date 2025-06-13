using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using CommonLayer;
using CommonLayer.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/Trans")]
    public class TransactionController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(TransactionController));
        ITransactionRepository _iRepo;

        public TransactionController(ITransactionRepository irepo)
        {
            _iRepo = irepo;
        }

        //[Route("GetDSREntries")]
        //[AcceptVerbs("GET")]
        //[ValidationActionFilter]
        //public IHttpActionResult GetDSREntries(string userId, int menuId, int? dsrEntryID = null)
        //{
        //    List<DSREntry> _result = _iRepo.GetDSREntries(new Guid(userId), menuId, dsrEntryID).ToList();
        //    if (_result != null) return Ok(_result);
        //    else return InternalServerError(new System.Exception("Failed to retrieve DSREntry data"));
        //}
        [Route("getCity")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCity(string userId, int? BrandID = null)
        {
            List<City> _result = _iRepo.getCity(userId, BrandID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Citys"));
        }

        [Route("getCluster")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCluster(string userId, int? BrandID = null, int? CityID = null)
        {
            List<Cluster> _result = _iRepo.getCluster(userId, BrandID, CityID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Clusters"));
        }

        [Route("SaveDSREntry")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveDSREntry(DSREntry _data)
        {
            if (_iRepo.UpdateDSREntry(_data)) return Ok(new { IsSuccess = true, Message = "Successfully Saved DSR Entry" });
            else return BadRequest("Failed to Save DSR Entry");
        }

        [Route("GetMonthlyExpenseEntries")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMonthlyExpenseEntries(Guid userId, int menuId, int? outletId = 0, int? expenseMonth = 0, int? expenseYear = 0)
        {
            List<MonthlyExpenseList> _result = _iRepo.GetMonthlyExpensesEntries(userId, menuId, outletId, expenseMonth, expenseYear).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetMonthlyExpenseEntries data"));
        }

        [Route("GetActualMonthlyExpenseEntries")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetActualMonthlyExpenseEntries(Guid userId, int menuId, int? outletId = 0, int? expenseMonth = 0, int? expenseYear = 0)
        {
            List<MonthlyExpenseList> _result = _iRepo.GetMonthlyExpensesEntries(userId, menuId, outletId, expenseMonth, expenseYear, true).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetActualMonthlyExpenseEntries data"));
        }


        [Route("GetExpenseDetailbyOutlet_Month")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetExpenseDetailbyOutlet_Month(Guid userId, int outletID, int monthlyExpenseYear, int monthlyExpenseMonth)
        {
            MonthlyExpense _result = _iRepo.GetMonthlyExpensesByOutlet_Month(userId, outletID, monthlyExpenseYear, monthlyExpenseMonth);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetExpenseDetailbyOutlet_Month data"));
        }

        [Route("GetActualExpenseDetailbyOutlet_Month")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetActualExpenseDetailbyOutlet_Month(Guid userId, int outletID, int monthlyExpenseYear, int monthlyExpenseMonth)
        {
            MonthlyExpense _result = _iRepo.GetMonthlyExpensesByOutlet_Month(userId, outletID, monthlyExpenseYear, monthlyExpenseMonth, true);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetActualExpenseDetailbyOutlet_Month (Actual) data"));
        }

        [Route("GetExpenseDetailbyID")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetExpenseDetailbyID(Guid userId, int monthlyExpenseID)
        {
            MonthlyExpense _result = _iRepo.GetMonthlyExpensesByID(userId, monthlyExpenseID);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetExpenseDetailbyID data"));
        }

        [Route("GetActualExpenseDetailbyID")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetActualExpenseDetailbyID(Guid userId, int monthlyExpenseID)
        {
            MonthlyExpense _result = _iRepo.GetMonthlyExpensesByID(userId, monthlyExpenseID, true);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetActualExpenseDetailbyID data"));
        }

        [Route("SaveMonthlyExpense")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveMonthlyExpense(MonthlyExpense _data)
        {
            if (_iRepo.UpdateMonthlyExpense(_data)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Monthly Expense Entry" });
            else return BadRequest("Failed to Save Monthly Expense Entry");
        }

        [Route("SaveActualMonthlyExpense")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveActualMonthlyExpense(MonthlyExpense _data)
        {
            if (_iRepo.UpdateMonthlyExpense(_data, true)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Actual Monthly Expense Entry" });
            else return BadRequest("Failed to Save Actual Monthly Expense Entry");
        }

        [Route("GetBudgetEntryList")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBudgetEntryList(Guid userId, int menuId)
        {
            List<BudgetList_ForGrid> _result = _iRepo.GetBudgetList(userId, menuId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetBudgetEntryList data"));
        }


        [Route("GetBudgetDetailByID")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBudgetDetailByID(int BudgetId)
        {
            BudgetModel _result = _iRepo.GetBudgetDetailsByID(BudgetId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetBudgetDetailByID data"));
        }

        [Route("SaveYearlyBudget")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveYearlyBudget(BudgetModel _data)
        {
            string resultOutputMessage = string.Empty;
            if (_iRepo.UpdateBudget(_data, out resultOutputMessage)) return Ok(new { IsSuccess = true, Message = resultOutputMessage });
            else return BadRequest(resultOutputMessage);
        }

        [Route("GetDailyExpenseEntries")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDailyExpenseEntries(string userId, int menuId, int outletID, int expenseMonth, int expenseYear, int week)
        {
            List<DailyExpense> _result = _iRepo.GetDailyExpenseEntries(new Guid(userId), menuId, outletID, expenseMonth, expenseYear, week);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetBudgetDetailByID data"));
        }

        [Route("SaveDailyExpense")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveDailyExpense(DailyExpense DailyExpenseEntries)
        {
            if (_iRepo.SaveDailyExpense(DailyExpenseEntries)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Daily Expense Entries" });
            else return BadRequest("Failed to Save Daily Expense Entries");
        }

        [Route("GetAllWeeks")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllWeeks(Guid userId, string year, int outletId)
        {
            List<WeekModel> _result = _iRepo.GetAllWeeks(userId, year, outletId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  All Weeks data"));
        }

        [Route("SaveWeeklyExpense")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveWeeklyExpense(WeeklyExpenseModel _data)
        {
            if (_iRepo.SaveWeeklyExpense(_data)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Weekly Expense Entry" });
            else return BadRequest("Failed to Save Weekly Expense Entry");
        }

        [Route("GetWeeklyExpense")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklyExpense(Guid userId, int menuId, int outletID, string expenseYear, string week)
        {
            List<WeeklyExpense> _result = _iRepo.GetWeeklyExpense(userId, menuId, outletID, expenseYear, week);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  Expense Weeks data"));
        }

        [Route("GetFinancialYear")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetFinancialYear(Guid userId)
        {
            List<financialYear> _result = _iRepo.GetFinancialYear(userId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  Distinct Financial Year"));
        }
        #region APCBudget
        [Route("GetAPC_BudgetWeekwise")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAPC_BudgetWeekwise(Guid userId, int menuId, string financialYear, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<APCBudgetModel> _result = _iRepo.GetAPC_BudgetWeekwise(userId, menuId, financialYear, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  APc Budget Data"));
        }
        [Route("SaveAPC_BudgetWeekwise")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveAPC_BudgetWeekwise(APCBudgetList model)
        {
            if (_iRepo.SaveAPC_BudgetWeekwise(model)) return Ok(new { IsSuccess = true, Message = "APC Budget Save Successfully." });
            else return BadRequest("APC Budget Save Failed");
        }
        #endregion APCBudget

        #region SalesBudget
        [Route("GetSalesCategory")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSalesCategory()
        {
            List<SalesCategoryModel> _result = _iRepo.GetSalesCategory();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  SalesCategoryModel"));
        }

        [Route("GetSalesBudget")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSalesBudget(int? OutletID = null, int? Year = null, int? Month = null)
        {
            SalesBudget _result = _iRepo.GetSalesBudget(OutletID, Year, Month);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  SalesBudget"));
        }

        [Route("GetSalesCategoryBudget")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSalesCategoryBudget(int? OutletID = null, int? Year = null, int? Month = null)
        {
            List<SalesCategoryBudget> _result = _iRepo.GetSalesCategoryBudget(OutletID, Year, Month);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  SalesCategoryBudget"));
        }

        [Route("GetSalesDayBudget")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSalesDayBudget(int? OutletID = null, int? Year = null, int? Month = null)
        {
            List<SalesDayBudget> _result = _iRepo.GetSalesDayBudget(OutletID, Year, Month);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  SalesDayBudget"));
        }

        [Route("SaveSalesBudget")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveSalesBudget(SalesBudget model)
        {
            if (_iRepo.SaveSalesBudget(model)) return Ok(new { IsSuccess = true, Message = "SalesBudget Save Successfully." });
            else return BadRequest("SalesBudget Save Failed");
        }


        [Route("GetSalesBudgetDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSalesBudgetDetails(int? OutletID = null, int? Year = null, int? Month = null)
        {
            List<SalesBudgetDetail> _result = _iRepo.GetSalesBudgetDetails(OutletID, Year, Month);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  SalesBudgetDetail"));
        }

        #endregion SalesBudget

        #region TBUpload
        [Route("SaveTBUpload")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveTBUpload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var files = System.Web.HttpContext.Current.Request.Files;

            if (files.Count > 0)
            {
                var relativePath = ConfigurationManager.AppSettings["FileSavePathForTBUploads"];
                string fileSavePath = Path.IsPathRooted(relativePath) ? relativePath : HttpContext.Current.Server.MapPath(relativePath);

                if (!Directory.Exists(fileSavePath))
                {
                    Directory.CreateDirectory(fileSavePath);
                }

                string filePath = string.Empty;

                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    TryCatch.Run(() =>
                    {
                        if (file != null)
                        {
                            filePath = Path.Combine(fileSavePath, file.FileName);
                            file.SaveAs(filePath);
                        }
                    }).IfNotNull(ex =>
                    {
                        Logger.LogError($"Error in TransactionController SaveTBUpload: Error saving file: {file.FileName}\n{ex.StackTrace}");
                    });
                }

                return Content(HttpStatusCode.OK, new { file_path = filePath });




            }

            return BadRequest("Failed to Save TBUpload");
        }

        [Route("CheckTBErrorLog")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult CheckTBErrorLog()
        {
            List<TBErrorLog> _result = _iRepo.CheckTBErrorLog();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  TBErrorLog"));
        }
        #endregion TBUpload

        #region DSR 
        [Route("GetDSR_Summary")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDSR_Summary(Guid userId, int menuId, string startDate, string endDate, string outletCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<DSR_Summary> _result = _iRepo.GetDSR_Summary(userId, menuId, outletCode ?? "", startDate, endDate, cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null)
                return Ok(_result);
            else
                return InternalServerError(new System.Exception("Failed to retrieve GET_Summary"));
        }
        #endregion DSR

        #region WeeklyMIS
        [Route("GetWeeklySaleDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklySaleDetails(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<WeeklyMIS> _result = _iRepo.GetWeeklySaleDetails(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekly Sales Details."));
        }

        [Route("GetLast12Weeks_SalesVsBudget")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12Weeks_SalesVsBudget(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<SalesVsBudget> _result = _iRepo.GetLast12Weeks_SalesVsBudget(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Last12Weeks_SalesVsBudget Details."));
        }

        [Route("GetWeeklyCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklyCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<WeeklyCoversTrend> _result = _iRepo.GetWeeklyCoversTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);

            if (_result != null)
                return Ok(_result);
            else
                return InternalServerError(new System.Exception("Failed to retrieve Weekly Covers Trend details."));
        }

        [Route("GetWeekly_DaywiseSaleTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeekly_DaywiseSaleTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<WeeklyCoversTrend> _result = _iRepo.GetWeekly_DaywiseSaleTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);

            if (_result != null)
                return Ok(_result);
            else
                return InternalServerError(new System.Exception("Failed to retrieve Weekly Sale Trend details."));
        }

        [Route("GetBeverageVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBeverageVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<BeverageVsBudgetTrend> _result = _iRepo.GetBeverageVsBudgetTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve BeverageVsBudgetTrend Details."));
        }

        [Route("GetTobaccoVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetTobaccoVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<TobaccoVsBudgetTrend> _result = _iRepo.GetTobaccoVsBudgetTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve TobaccoVsBudgetTrend Details."));
        }

        [Route("GetTimeWiseSalesBreakup")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetTimeWiseSalesBreakup(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<TimeWiseSalesBreakup> _result = _iRepo.GetTimeWiseSalesBreakup(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetTimeWiseSalesBreakup Details."));
        }

        [Route("GetAvgCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAvgCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<AverageCoverTrend> _result = _iRepo.GetAvgCoversTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAvgCoversTrend Details."));
        }

        [Route("GetDayWise_AvgCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDayWise_AvgCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<AverageCoverTrend> _result = _iRepo.GetDayWise_AvgCoversTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetDayWise_AvgCoversTrend Details."));
        }

        [Route("GetLiquorVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLiquorVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<LiquorVsBudgetTrend> _result = _iRepo.GetLiquorVsBudgetTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetLiquorVsBudgetTrend Details."));
        }

        [Route("GetFoodVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetFoodVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<FoodVsBudgetTrend> _result = _iRepo.GetFoodVsBudgetTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetFoodVsBudgetTrend Details."));
        }

        [Route("GetDailySaleTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDailySaleTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<SaleTrendModel> _result = _iRepo.GetDailySaleTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Daily Sale Trend Details."));
        }

        [Route("GetGrossProfitTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetGrossProfitTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<SaleTrendModel> _result = _iRepo.GetGrossProfitTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Gross Profit Trend Details."));
        }

        [Route("GetNetProfitTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetNetProfitTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<SaleTrendModel> _result = _iRepo.GetNetProfitTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Net Profit Trend Details."));
        }

        [Route("GetWeeklyMISData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklyMISData(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            //  Logger.LogInfo("GetWeeklyMISData in TransactionController before send call to repo  :" + DateTime.UtcNow);
            List<MISWeeklyDataModel> _result = _iRepo.GetWeeklyMISData(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            // Logger.LogInfo("GetWeeklyMISData in TransactionController after  call return from repo  :" + DateTime.UtcNow);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MISData Details."));
        }

        [Route("GetWeeklyMISData_Part_I")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklyMISData_Part_I(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            //  Logger.LogInfo("GetWeeklyMISData in TransactionController before send call to repo  :" + DateTime.UtcNow);
            List<MISWeeklyDataModel> _result = _iRepo.GetWeeklyMISData_Part_I(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            // Logger.LogInfo("GetWeeklyMISData in TransactionController after  call return from repo  :" + DateTime.UtcNow);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MISData Part I Details."));
        }

        [Route("GetWeeklyMISData_Part_II")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklyMISData_Part_II(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            //  Logger.LogInfo("GetWeeklyMISData in TransactionController before send call to repo  :" + DateTime.UtcNow);
            List<MISWeeklyDataModel> _result = _iRepo.GetWeeklyMISData_Part_II(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            // Logger.LogInfo("GetWeeklyMISData in TransactionController after  call return from repo  :" + DateTime.UtcNow);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MISData Part II Details."));
        }

        [Route("GetWeeklyMISData_Part_III")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklyMISData_Part_III(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            //  Logger.LogInfo("GetWeeklyMISData in TransactionController before send call to repo  :" + DateTime.UtcNow);
            List<MISWeeklyDataModel> _result = _iRepo.GetWeeklyMISData_Part_III(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            // Logger.LogInfo("GetWeeklyMISData in TransactionController after  call return from repo  :" + DateTime.UtcNow);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MISData Part III Details."));
        }
        [Route("GetCogsBreakUp")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCogsBreakUp(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<CogsBreakUp> _result = _iRepo.GetCogsBreakUp(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetCogsBreakUp Details."));
        }

        [Route("GetUtilityCost")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetUtilityCost(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<UtilityCostModel> _result = _iRepo.GetUtilityCost(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get UtilityCost Details."));
        }

        [Route("GetMarketingPromotionCost")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMarketingPromotionCost(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<MarketingPromotion> _result = _iRepo.GetMarketingPromotionCost(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get MarketingPromotionCost Details."));
        }

        [Route("GetOtherOperationalCost")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOtherOperationalCost(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<OtherOperationalCostModel> _result = _iRepo.GetOtherOperationalCost(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Other Operational Cost Details."));
        }

        [Route("GetOccupationalCost")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOccupationalCost(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<OccupationalCostModel> _result = _iRepo.GetOccupationalCost(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Rent and Occupational Cost Details."));
        }

        [Route("GetCostBreakUp")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCostBreakUp(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<CostBreakUpModel> _result = _iRepo.GetCostBreakUp(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Cost BreakUp Details."));
        }
        [Route("GetLast12Weeks_CoverTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12Weeks_CoverTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<WeeklyCoversTrend> _result = _iRepo.GetLast12Weeks_CoverTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetLast12Weeks_CoverTrend Details."));
        }
        [Route("GetWeekDays_CoverCapicityUtilization")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeekDays_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<TimeWiseSalesBreakup> _result = _iRepo.GetWeekDays_CoverCapicityUtilization(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetWeekDays_CoverCapicityUtilization Details."));
        }

        [Route("GetWeekend_CoverCapicityUtilization")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeekend_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<TimeWiseSalesBreakup> _result = _iRepo.GetWeekend_CoverCapicityUtilization(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetWeekend_CoverCapicityUtilization Details."));
        }

        [Route("GetLast12Weeks_WeekendCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12Weeks_WeekendCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<WeeklyCoversTrend> _result = _iRepo.GetLast12Weeks_WeekendCoversTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetLast12Weeks_WeekendCoversTrend Details."));
        }

        [Route("GetLast12Weeks_WeekDaysCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12Weeks_WeekDaysCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<WeeklyCoversTrend> _result = _iRepo.GetLast12Weeks_WeekDaysCoversTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetLast12Weeks_WeekDaysCoversTrend Details."));
        }

        [Route("GetDeliveySaleTrends")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDeliveySaleTrends(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<DeliverySaleTrend> _result = _iRepo.GetDeliveySaleTrends(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve DeliveySaleTrends Details."));
        }

        [Route("GetDeliveySale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDeliveySale(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<DeliverySaleBreakup> _result = _iRepo.GetDeliveySale(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Delivey Sales Breakup Details."));
        }

        [Route("GetFoodCostTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetFoodCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<CostTrend> _result = _iRepo.GetFoodCostTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Food Cost Trend Details."));
        }

        [Route("GetLiquorCostTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLiquorCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<CostTrend> _result = _iRepo.GetLiquorCostTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Liquor Cost Trend Details."));
        }

        [Route("GetBeverageCostTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBeverageCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<CostTrend> _result = _iRepo.GetBeverageCostTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Beverage Cost Trend Details."));
        }

        [Route("GetCogsCostTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCogsCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<CostTrend> _result = _iRepo.GetCogsCostTrend(userId, menuId, financialYear, week, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve COGS Cost Trend Details."));
        }
        #endregion WeeklyMIS 
        #region DSR Sanpshot
        [Route("GetSanpshotWeeklyData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSanpshotWeeklyData(int WeekNo, string Year, int OutletId, Guid UserId, int MenuId)
        {
            List<WeeklySnapshot> _result = _iRepo.GetSanpshotWeeklyData(WeekNo, Year, OutletId, UserId, MenuId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekly Snapshot Data."));
        }

        [Route("SaveSnapshotEntry")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveSnapshotEntry(SnapshotModel SnapshotEntry)
        {
            if (_iRepo.SaveSnapshotEntry(SnapshotEntry)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Snapshot Entries" });
            else return BadRequest("Failed to Save Daily Expense Entries");
        }

        [Route("GetWeeklySalesSnapshot")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeeklySalesSnapshot(string Week, string Year, int OutletId, Guid UserId, int MenuId)
        {
            List<WeeklySalesSnapshot> _result = _iRepo.GetWeeklySalesSnapshot(Week, Year, OutletId, UserId, MenuId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekly Sales Data."));
        }

        [Route("GetItem86SnapshotDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetItem86SnapshotDetails(int WeekNo, string Year, Guid UserId, int MenuId)
        {
            List<WeeklySnapshot> _result = _iRepo.GetItem86SnapshotDetails(WeekNo, Year, UserId, MenuId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekly Sales Data."));
        }
        #endregion  DSR Sanpshot
        #region  DSR Comparison
        [Route("GetWeekDays")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetWeekDays()
        {
            List<Weekdays> _result = _iRepo.GetWeekDays();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekdays Data."));
        }

        [Route("GetDSRComparisonForSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDSRComparisonForSale(string Week, string Day, string FinancialYear, string BranchCode, Guid UserId, int MenuId)
        {
            List<DsrComparisonModel> _result = _iRepo.Get_DSRComparisonForSale(Week, Day, FinancialYear, BranchCode, UserId, MenuId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve DSRComparisonForSale."));
        }

        [Route("GetDailySnapshotforComparison")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDailySnapshotforComparison(Guid UserId, int MenuId, int Week, string Day, string FinancialYear, string BranchCode)
        {
            List<WeeklySnapshotsViewModel> _result = _iRepo.Get_DailySnapshotforComparison(Week, Day, FinancialYear, BranchCode, UserId, MenuId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  DailySnapshotforComparison."));
        }
        #endregion  DSR Comparison

        #region Monthly_MIS
        [Route("getAllMonths")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getAllMonths()
        {
            List<Months> _result = _iRepo.GetAllMonths().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Months"));
        }


        [Route("getMonthlyMISData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMonthlyMISData(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<MonthlyMISDataModel> _result = _iRepo.GetMonthlyMISData(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MIS Data Details."));
        }

        [Route("GetMonthlyMISData_PartI")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMonthlyMISData_PartI(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<MonthlyMISDataModel> _result = _iRepo.GetMonthlyMISData_PartI(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MIS Data PartI Details."));
        }


        [Route("GetMonthlyMISData_PartII")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMonthlyMISData_PartII(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<MonthlyMISDataModel> _result = _iRepo.GetMonthlyMISData_PartII(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MIS Data PartII Details."));
        }

        [Route("GetMonthlyMISData_PartIII")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMonthlyMISData_PartIII(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<MonthlyMISDataModel> _result = _iRepo.GetMonthlyMISData_PartIII(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Weekly MIS Data PartIII Details."));
        }

        [Route("getYTDSalesVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetYTDSalesVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_YTDChartModel> _result = _iRepo.GetYTDSalesVsBudgetTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get YTDSalesVsBudget Trend Details."));
        }

        [Route("getLast12MonthsSalesVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12MonthsSalesVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Last12MonthBudgetSaleComparison> _result = _iRepo.GetLast12MonthsSalesVsBudgetTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Last12MonthsSalesVsBudget Trend Details."));
        }

        [Route("getLast12MonthsFoodVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12MonthsFoodVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Last12MonthBudgetSaleComparison> _result = _iRepo.GetLast12MonthsFoodVsBudgetTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get GetLast12MonthsFoodVsBudget Trend Details."));
        }
        [Route("getLast12MonthsLiquorVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12MonthsLiquorVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Last12MonthBudgetSaleComparison> _result = _iRepo.GetLast12MonthsLiquorVsBudgetTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get getLast12MonthsLiquorVsBudget Trend Details."));
        }
        [Route("getLast12MonthsBeverageVsBudgetTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetLast12MonthsBeverageVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Last12MonthBudgetSaleComparison> _result = _iRepo.GetLast12MonthsBeverageVsBudgetTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get GetLast12MonthsBeverageVsBudgetTrend Trend Details."));
        }

        [Route("getMonthlyMTDSalesvsBudget")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMonthlyMTDSalesvsBudget(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<MonthlyMTDSalesVsBudget> _result = _iRepo.GetMonthlyMTDSalesvsBudget(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get MonthlyChart_MTD_SalesvsBudget Trend Details."));
        }

        [Route("Monthly_GetDaywiseSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetDaywiseSale(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<DayWiseSaleTrend> _result = _iRepo.Monthly_GetDaywiseSale(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetDaywiseSale Trend Details."));
        }

        [Route("Monthly_GetSalesBreakup")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetSalesBreakup(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_SalesBreakup> _result = _iRepo.Monthly_GetSalesBreakup(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetSalesBreakup Trend Details."));
        }

        [Route("Monthly_GetTimeWiseSalesBreakup")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetTimeWiseSalesBreakup(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_TimeWiseSalesBreakup> _result = _iRepo.Monthly_GetTimeWiseSalesBreakup(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetTimeWiseSalesBreakup Trend Details."));
        }

        [Route("Monthly_GetWeekDays_CoverCapicityUtilization")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetWeekDays_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_TimeWiseSalesBreakup> _result = _iRepo.Monthly_GetWeekDays_CoverCapicityUtilization(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetWeekDays_CoverCapicityUtilization Trend Details."));
        }

        [Route("Monthly_GetWeekend_CoverCapicityUtilization")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetWeekend_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_TimeWiseSalesBreakup> _result = _iRepo.Monthly_GetWeekend_CoverCapicityUtilization(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetWeekend_CoverCapicityUtilization Trend Details."));
        }

        [Route("Monthly_GetDeliveySale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetDeliveySale(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_DeliverySaleBreakup> _result = _iRepo.Monthly_GetDeliveySale(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetDeliveySale Trend Details."));
        }

        [Route("Monthly_GetDayWiseAvgCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetDayWiseAvgCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_AverageCoverTrend> _result = _iRepo.Monthly_GetDayWiseAvgCoversTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetDayWiseAvgCovers Trend Details."));
        }

        [Route("Monthly_GetAvgCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetAvgCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_AverageCoverTrend> _result = _iRepo.Monthly_GetAvgCoversTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetAvgCovers Trend Details."));
        }

        [Route("Monthly_GetDeliveySaleTrends")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetDeliveySaleTrends(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_DeliverySaleTrend> _result = _iRepo.Monthly_GetDeliveySaleTrends(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetDeliveySale Trend Details."));
        }

        [Route("Monthly_DayWise_GetMonthlyCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_DayWise_GetMonthlyCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_CoversTrend> _result = _iRepo.Monthly_DayWise_GetMonthlyCoversTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_DayWise_GetMonthlyCoversTrend Details."));
        }

        [Route("Monthly_GetLast12Weeks_CoverTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetLast12Weeks_CoverTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_CoversTrend> _result = _iRepo.Monthly_GetLast12Weeks_CoverTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetLast12Weeks_CoverTrend Details."));
        }

        [Route("Monthly_GetLast12Weeks_WeekendCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetLast12Weeks_WeekendCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_CoversTrend> _result = _iRepo.Monthly_GetLast12Weeks_WeekendCoversTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetLast12Weeks_WeekendCoversTrend Details."));
        }

        [Route("Monthly_GetLast12Weeks_WeekDaysCoversTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Monthly_GetLast12Weeks_WeekDaysCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Monthly_CoversTrend> _result = _iRepo.Monthly_GetLast12Weeks_WeekDaysCoversTrend(userId, menuId, financialYear, month, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Monthly_GetLast12Weeks_WeekDaysCoversTrend Details."));
        }

        #endregion Monthly_MIS

        #region Item Analysis Module

        [Route("GetAccountNames")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAccountNames()
        {
            List<DropdownFilterModel> _result = _iRepo.GetAccountNames();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAccountNames."));
        }

        [Route("GetCategoryNames")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCategoryNames(string AccountName = null)
        {
            List<DropdownFilterModel> _result = _iRepo.GetCategoryNames(AccountName);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetCategoryNames."));
        }

        [Route("GetItemAnalysisReport")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetItemAnalysisReport(Guid userId, int menuId, string fromDate, string toDate, string AccountName = null, string CategoryName = null, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<ItemAnalysisModel> _result = _iRepo.GetItemAnalysisReport(userId, menuId, fromDate, toDate, AccountName ?? "", CategoryName ?? "", branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetItemAnalysisReport."));
        }
        #endregion Item Analysis Module

        #region Dashboard_ Item Analysis

        [Route("Dashboard_GetTotalBillNumbers")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Dashboard_GetTotalBillNumbers(Guid userId, int menuId, string fromDate, string toDate, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Dashboard_BillCount> _result = _iRepo.Dashboard_GetTotalBillNumbers(userId, menuId, fromDate, toDate, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Dashboard_GetTotalBillNumbers."));
        }

        [Route("ItemChart_GetCategoryWiseSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ItemChart_GetCategoryWiseSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Dashboard_PieChart> _result = _iRepo.ItemChart_GetCategoryWiseSale(userId, menuId, fromDate, toDate, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ItemChart_GetCategoryWiseSale."));
        }

        [Route("ItemChart_GetFoodSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ItemChart_GetFoodSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Dashboard_MutliChart> _result = _iRepo.ItemChart_GetFoodSale(userId, menuId, fromDate, toDate, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ItemChart_GetFoodSale."));
        }

        [Route("ItemChart_GetBeverageSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ItemChart_GetBeverageSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Dashboard_MutliChart> _result = _iRepo.ItemChart_GetBeverageSale(userId, menuId, fromDate, toDate, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ItemChart_GetBeverageSale."));
        }

        [Route("ItemChart_GetLiquorSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ItemChart_GetLiquorSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Dashboard_MutliChart> _result = _iRepo.ItemChart_GetLiquorSale(userId, menuId, fromDate, toDate, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ItemChart_GetLiquorSale."));
        }

        [Route("ItemChart_GetTobaccoSale")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ItemChart_GetTobaccoSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode = null, int? cityId = null, int? clusterId = null, int? brandId = null)
        {
            List<Dashboard_MutliChart> _result = _iRepo.ItemChart_GetTobaccoSale(userId, menuId, fromDate, toDate, branchCode ?? "", cityId ?? 0, clusterId ?? 0, brandId ?? 0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ItemChart_GetTobaccoSale."));
        }
        #endregion Dashboard_ Item Analysis
    }
}
