using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models.ManPower;
using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/ManPower")]
    public class ManPowerController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ManPowerController));
        IManPowerRepository _IRepo;

        public ManPowerController(IManPowerRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("GetManPowerBudgetByOutletID")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetManPowerBudgetByOutletID(int? OutletID)
        {
            List<ManPowerBudgetDetailsModel> _result = _IRepo.GetManPowerBudgetByOutletID(OutletID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetManPowerBudgetByOutletID"));
        }

        [Route("SaveManPowerCounts")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveManPowerCounts(ManPowerBudgetModel model)
        {
            if (_IRepo.SaveManPowerCounts(model)) return Ok(new { IsSuccess = true, Message = "ManPowerBudget Save Successfully." });
            else return BadRequest("ManPowerBudget Save Failed");
        }

        [Route("GetManPowerBudgetHistory")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetManPowerBudgetHistory(int? OutletID, int? Latest)
        {
            List<ManPowerBudgetDetailsModel> _result = _IRepo.GetManPowerBudgetHistory(OutletID, Latest).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetManPowerBudgetHistory"));
        }

        [Route("GetManPowerBudgetForDashBoard")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetManPowerBudgetForDashBoard()
        {
            List<ManPowerBudgetDashboardModel> _result = _IRepo.GetManPowerBudgetForDashBoard().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetManPowerBudgetForDashBoard"));
        }

    }
}