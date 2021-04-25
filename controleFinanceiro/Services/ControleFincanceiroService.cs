using controleFinanceiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace controleFinanceiro.Services
{
    public class ControleFincanceiroService : IControleFincanceiroService
    {
        private ILocalStorageHelper localStorageHelper;

        public ControleFincanceiroService(ILocalStorageHelper localStorageHelper)
        {
            this.localStorageHelper = localStorageHelper;
        }

        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public List<Meta> Metas { get; set; } = new List<Meta>();

        public async Task Salvar()
        {
            await localStorageHelper.SetItem<List<Transacao>>("transacoes", Transacoes);
            await localStorageHelper.SetItem<List<Meta>>("metas", Metas);
        }

        public async Task Carregar()
        {
            Transacoes = await localStorageHelper.GetItem<List<Transacao>>("transacoes");
            if (Transacoes == null)
            {
                Transacoes = new List<Transacao>();
                await localStorageHelper.SetItem<List<Transacao>>("transacoes", Transacoes);
            }

            Metas = await localStorageHelper.GetItem<List<Meta>>("metas");
            if (Metas == null)
            {
                Metas = new List<Meta>();
                await localStorageHelper.SetItem<List<Meta>>("metas", Metas);
            }
        }
    }
}