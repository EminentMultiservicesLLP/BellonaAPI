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

        [Route("getCluster")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCluster(string userId, int? CityID = null)
        {
            List<Cluster> _result = _iRepo.getCluster(userId, CityID).ToList();
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
        public IHttpActionResult GetDSR_Summary(string startDate, string endDate, string outletCode = null, int? cityId = null, int? clusterId = null)
        {
            List<DSR_Summary> _result = _iRepo.GetDSR_Summary(outletCode ?? "", startDate, endDate, cityId  ?? 0, clusterId ?? 0);
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
        public IHttpActionResult GetWeeklySaleDetails(string week, string branchCode= null, int? cityId=null, int? clusterId=null)
        {
            List<WeeklyMIS> _result = _iRepo.GetWeeklySaleDetails(week, branchCode??"", cityId?? 0, clusterId??0);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekly Sales Details."));
        }
        #endregion WeeklyMIS 

        #region DSR Sanpshot
        [Route("GetSanpshotWeeklyData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSanpshotWeeklyData(int WeekNo, string Year, int OutletId)
        {
            List<WeeklySnapshot> _result = _iRepo.GetSanpshotWeeklyData(WeekNo, Year, OutletId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Weekly Snapshot Data."));
        }
        #endregion  DSR Sanpshot
    }
}
