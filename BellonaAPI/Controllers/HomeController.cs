using CommonLayer;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using BellonaAPI.Models;
using System;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(HomeController));
        IRegionRepository _Iregion; IBrandRepository _IBrand; ICityRepository _ICity; IClusterRepository _ICluster; ICurrencyRepository _ICurrency; IUserMenuRightsRepository _IUserMenuRightsRepository;
        IUserProfilRepository _IUserProfilRepository; IDeliver_GuestPartners _IDeliver_GuestPartners; IMastersRepository _IMastersRepository;

        public HomeController(IRegionRepository region, ICurrencyRepository currency, IBrandRepository brand, ICityRepository city, IClusterRepository cluster, IUserMenuRightsRepository userMenuRightsRepository, 
            IDeliver_GuestPartners deliver_GuestPartners, IUserProfilRepository userProfilRepository, IMastersRepository mastersRepository)
        {
            _Iregion = region;
            _IBrand = brand;
            _ICity = city;
            _ICluster = cluster;
            _ICurrency = currency;
            _IDeliver_GuestPartners = deliver_GuestPartners;
            _IUserMenuRightsRepository = userMenuRightsRepository;
            _IUserProfilRepository = userProfilRepository;
            _IMastersRepository = mastersRepository;
        }

        [Route("getTimeZones")]
        [ValidationActionFilter]
        public IHttpActionResult GetTimeZones()
        {
            Logger.LogInfo("Received GetTimeZones call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                List<TimeZones> _result = new List<TimeZones>();
                _result = _IUserProfilRepository.GetTimeZones().ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve Time Zones"));
            }
            finally
            {
                Logger.LogInfo("Completed GetTimeZones call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("getRentTypes")]
        [ValidationActionFilter]
        public IHttpActionResult GetRentTypes()
        {
            Logger.LogInfo("Received GetRentTypes call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                List<RentTypes> _result = new List<RentTypes>();
                _result = _IMastersRepository.GetRentTypes().ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve Rent Types"));
            }
            finally
            {
                Logger.LogInfo("Completed GetRentTypes call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("getAllMenuRights")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllMenuRights(Guid userId)
        {
            Logger.LogInfo("Received GetAllMenuRights call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                List<MenuUserRightsModel> _result = new List<MenuUserRightsModel>();
                _result = _IUserMenuRightsRepository.GetAllUserMenRights(userId).ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve Menu Rights data"));
            }
            finally
            {
                Logger.LogInfo("Completed GetAllMenuRights call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("verifyLogin")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult VerifyLogin(string loginId, string pwd)
        {
            Logger.LogInfo("Received VerifyLogin call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                UserInfo _result = new UserInfo();
                var nloginID = System.Uri.UnescapeDataString(loginId);
                var npwd = System.Uri.UnescapeDataString(pwd);
                _result = _IUserMenuRightsRepository.VerifyLogin(nloginID, npwd);
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve User Information"));
            }
            finally
            {
                Logger.LogInfo("Completed VerifyLogin call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("GetForgotPassword")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetForgotPassword(string loginId)
        {
            Logger.LogInfo("Received GetForgotPassword call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                UserInfo _result = new UserInfo();
                var nloginID = System.Uri.UnescapeDataString(loginId);                
                _result = _IUserMenuRightsRepository.GetForgotPassword(nloginID);
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve User Information"));
            }
            finally
            {
                Logger.LogInfo("Completed GetForgotPassword call at :" + System.DateTime.Now.ToLongDateString());
            }
        }
        

        [Route("getRegion")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllRegions(int? regionId = 0)
        {
            Logger.LogInfo("Received GetRegion call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                List<Region> _result = new List<Region>();
                _result = _Iregion.GetRegions(regionId).ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve Region data"));
            }
            finally
            {
                Logger.LogInfo("Completed GetRegion call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("getBrand")]
        [ValidationActionFilter]
        public IHttpActionResult GetBrand(int? BrandId = 0)
        {
            List<Brand> _result = new List<Brand>();
            Logger.LogInfo("Received GetBrand call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                _result = _IBrand.GetBrands(BrandId).ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve Brand data"));
            }
            finally
            {
                Logger.LogInfo("Completed GetBrand call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("getCity")]
        [ValidationActionFilter]
        public IHttpActionResult GetCity(int? cityId = 0)
        {
            List<City> _result = new List<City>();
            Logger.LogInfo("Received GetCity call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                _result = _ICity.GetCities(cityId).ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve City data"));
            }
            finally
            {
                Logger.LogInfo("Completed GetCity call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("getCluster")]
        [ValidationActionFilter]
        public IHttpActionResult GetCluster(int? ClusterId = 0)
        {
            List<Cluster> _result = new List<Cluster>();
            Logger.LogInfo("Received GetCluster call - Starting at :" + System.DateTime.Now.ToLongDateString());
            try
            {
                _result = _ICluster.GetClusters(ClusterId).ToList();
                if (_result != null) return Ok(_result);
                else return InternalServerError(new System.Exception("Failed to retrieve Cluster data"));
            }
            finally
            {
                Logger.LogInfo("Completed GetCluster call at :" + System.DateTime.Now.ToLongDateString());
            }
        }

        [Route("getCurrency")]
        [ValidationActionFilter]
        public IHttpActionResult GetCurrency(int? currencyId = 0)
        {
            List<Currency> _result = new List<Currency>();
            _result = _ICurrency.GetCurrencies(currencyId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Currency data"));
        }

        [Route("getDeliveryPartners")]
        [ValidationActionFilter]
        public IHttpActionResult GetDeliveryPartners(int? outletId = 0)
        {
            List<DeliveryPartners> _result = new List<DeliveryPartners>();
            _result = _IDeliver_GuestPartners.GetDeliveryPartners(outletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Delivery Partners List"));
        }

        [Route("getGuestPartners")]
        [ValidationActionFilter]
        public IHttpActionResult GetGuestPartners(int? outletId = 0)
        {
            List<GuestPartners> _result = new List<GuestPartners>();
            _result = _IDeliver_GuestPartners.GetGuestPartners(outletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Guest Partners List"));
        }

        [Route("saveMenuSettings")]
        [ValidationActionFilter]
        public IHttpActionResult SaveMenuSettings(UserInfo userInfo)
        {
            bool _isSuccess = false;
            _isSuccess = _IUserMenuRightsRepository.SaveMenuSettings(userInfo);
            if (_isSuccess) return Ok(new { IsSuccess = true, Message = "Successfully Saved User Settings." });
            else return InternalServerError(new System.Exception("Failed to Save user settings."));
            
        }

        [Route("GetExchangeRateDetailByMonthYear")]
        [ValidationActionFilter]
        public IHttpActionResult GetExchangeRateDetailByMonthYear(int ExchangeRateYear, int ExchangeRateMonth)
        {
            List<ExchangeRates> _result = new List<ExchangeRates>();
            _result = _IMastersRepository.GetExchangeRateDetailByMonthYear(ExchangeRateYear, ExchangeRateMonth).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to Save user settings."));
        }

        [Route("GetExchangeRatesAll")]
        [ValidationActionFilter]
        public IHttpActionResult GetExchangeRatesAll()
        {
            List<ExchangeRatesMonthWise> _result = new List<ExchangeRatesMonthWise>();
            _result = _IMastersRepository.GetExchangeRatesAll().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to get Exchange Rates All."));
        }


        [Route("SaveExchangeRate")]
        [ValidationActionFilter]
        public IHttpActionResult SaveExchangeRate(ExchangeRatesMonthWise exchangeRates)
        {
            bool _isSuccess = false;
            _isSuccess = _IMastersRepository.SaveExchangeRate(exchangeRates);
            if (_isSuccess) return Ok(new { IsSuccess = true, Message = "Successfully Saved User Settings." });
            else return InternalServerError(new System.Exception("Failed to Save user settings."));

        }


    }
}
