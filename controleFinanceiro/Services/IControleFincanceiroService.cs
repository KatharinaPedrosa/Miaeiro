using controleFinanceiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace controleFinanceiro.Services
{
    public interface IControleFincanceiroService
    {
        List<Transacao> Transacoes { get; set; }

        List<Meta> Metas { get; set; }

        Task Carregar();

        Task Salvar();
    }
}