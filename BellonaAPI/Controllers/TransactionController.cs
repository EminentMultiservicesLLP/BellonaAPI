using CommonLayer;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using BellonaAPI.Models;

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

        [Route("GetDSREntries")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetDSREntries(string userId,int menuId, int? dsrEntryID = null)
        {
            List<DSREntry> _result = _iRepo.GetDSREntries(new Guid(userId), menuId, dsrEntryID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve DSREntry data"));
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
        public IHttpActionResult GetMonthlyExpenseEntries(Guid userId,int menuId, int? outletId = 0, int? expenseMonth =0, int? expenseYear =0)
        {
            List<MonthlyExpenseList> _result = _iRepo.GetMonthlyExpensesEntries(userId,menuId, outletId, expenseMonth, expenseYear).ToList();
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
        public IHttpActionResult GetDailyExpenseEntries(string userId, int menuId, int outletID , int expenseMonth , int expenseYear )
        {            
            List<DailyExpense> _result = _iRepo.GetDailyExpenseEntries(new Guid(userId), menuId, outletID, expenseMonth, expenseYear);
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

    }
}
