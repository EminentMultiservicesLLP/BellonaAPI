using CommonLayer;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using BellonaAPI.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/ProspectDashboard")]
    public class ProspectDashboardController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ProspectDashboardController));
        IProspectDashboardRepository _iRepo;

        public ProspectDashboardController(IProspectDashboardRepository _irepo)
        {
            _iRepo = _irepo;
        }

        #region Prospect Dashboard

        [Route("getDashboardData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getDashboardData()
        {
            List<ProspectDashboardModel> _result = _iRepo.getDashboardData().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getDashboardData"));
        }
        #endregion Prospect Dashboard
    }
}