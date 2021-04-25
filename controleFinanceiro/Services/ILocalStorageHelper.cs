using System.Threading.Tasks;

namespace controleFinanceiro.Services
{
    public interface ILocalStorageHelper
    {
        Task<T> GetItem<T>(string key);

        Task RemoveItem(string key);

        Task SetItem<T>(string key, T value);
    }
}