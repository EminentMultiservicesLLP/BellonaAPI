using BellonaAPI.Models.Masters;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IMastersRepository
    {
        IEnumerable<RentTypes> GetRentTypes();
        IEnumerable<ExchangeRates> GetExchangeRateDetailByMonthYear(int ExchangeRateYear, int ExchangeRateMonth);
        IEnumerable<ExchangeRatesMonthWise> GetExchangeRatesAll();
        bool SaveExchangeRate(ExchangeRatesMonthWise exchangeRates);

    }
}
