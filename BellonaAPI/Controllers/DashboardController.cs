using CommonLayer;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BellonaAPI.Models.Dashboard;
using System;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(DashboardController));
        IDashboardRepository _iRepo;

        public DashboardController(IDashboardRepository irepo)
        {
            _iRepo = irepo;
        }

        [Route("GetSaleDineIn")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSaleDineIn(string userId, int menuid, int? Month = 0, int? OutletId = 0)
        {
            List<SaleDineIn> _result = _iRepo.GetSaleDineIn(new Guid(userId), menuid, Month, OutletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSaleDineIn data"));
        }

        [Route("GetSaleBreakUp")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSaleBreakUp(string userId, int menuid, int? Month = 0, int? OutletId = 0)
        {
            List<SaleBreakUp> _result = _iRepo.GetSaleBreakUp(new Guid(userId), menuid, Month, OutletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSaleBreakUp data"));
        }


        [Route("GetSaleDelivery")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSaleDelivery(string userId, int menuid, int? Month = 0, int? OutletId = 0)
        {
            List<SaleDelivery> _result = _iRepo.GetSaleDelivery(new Guid(userId), menuid, Month, OutletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSaleDelivery data"));
        }       


    }
}
