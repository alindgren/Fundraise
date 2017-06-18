using Fundraise.Core.Entities;

namespace Fundraise.Core.Services
{
    public interface ICurrencyRepository
    {
        Currency Create(string code, string symbol, string name);
        Currency FindByCode(string currencyCode);
    }
}
