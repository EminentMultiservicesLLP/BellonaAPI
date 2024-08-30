using CommonLayer;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CountryController));
        ICountryRepository _ICountry;

        public CountryController(ICountryRepository icountry)
        {
           _ICountry = icountry;
        }

        [Route("getCountry")]
        [AcceptVerbs("GET", "POST")]
        [ValidationActionFilter]
        public IHttpActionResult GetCountries(int? countryId = null, int? regionId = null)
        {
            List<Country> _result = _ICountry.GetCountries(regionId, countryId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Country data"));
        }

        [Route("saveCountry")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult UpdateCountry(Country _data)
        {
            if (_ICountry.UpdateCountry(_data)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Data" });
            else return BadRequest("Failed to Save Data");
        }

    }
}
