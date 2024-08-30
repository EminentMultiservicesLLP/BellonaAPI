using BellonaAPI.Models.Masters;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetCurrencies(int? iCurrencyId = 0);
        bool UpdateCurrency(Currency _data);
        bool DeleteCurrency(int CurrencyId);
    }
}
