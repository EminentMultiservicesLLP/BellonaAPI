using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
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
    [RoutePrefix("api/BillDashboard")]
    public class BillingDashboardController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(BillingDashboardController));
        IBillingDashboardRepository _IRepo;

        public BillingDashboardController(IBillingDashboardRepository _irepo)
        {
            _IRepo = _irepo;
        }

        #region FilterData
        [Route("getCity")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCity(string userId , int? CountryID = null)
        {
            List<BillingDashboard> _result = _IRepo.getCity(userId, CountryID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Cities"));
        }

        [Route("getCluster")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCluster(string userId, int? CityID = null)
        {
            List<BillingDashboard> _result = _IRepo.getCluster(userId,CityID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Clusters"));
        }

        [Route("getOutlet")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOutlet(string userId, int? ClusterID = null)
        {
            List<BillingDashboard> _result = _IRepo.getOutlet(userId,ClusterID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Outlet"));
        }
        #endregion FilterData
        
        [Route("getFunctionForStatus")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getFunctionForStatus(string userId, int? ID = null)
        {
            List<FunctionEntryModel> _result = _IRepo.getFunctionForStatus(userId, ID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Functions"));
        }
        [Route("GetAllnvoiceUpload")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllnvoiceUpload(string userId)
        {
            List<Attachments>_result = _IRepo.getAllnvoiceUpload().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Functions"));
        }
        [Route("getFunctionForExport")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetFunctionForExport(string userId, int? ID = null)
        {
            List<FunctionEntryModel> _result = _IRepo.getFunctionForExport(userId, ID).ToList();            
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Functions"));
        }
    }
}
