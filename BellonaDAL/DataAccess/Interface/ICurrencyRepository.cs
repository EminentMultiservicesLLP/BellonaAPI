using BellonaDAL.Models.Masters;
using System.Collections.Generic;

namespace BellonaDAL.DataAccess.Interface
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetCurrencies(int? iCurrencyId = 0);
        bool UpdateCurrency(Currency _data);
        bool DeleteCurrency(int CurrencyId);
    }
}
