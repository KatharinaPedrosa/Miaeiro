using controleFinanceiro.Models;
using controleFinanceiro.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace controleFinanceiro.Shared
{
    public class ListaMetasBase : ComponentBase
    {
        [Inject]
        public IControleFincanceiroService ControleFinanceiroService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ControleFinanceiroService.Carregar();
        }

        public async Task Excluir(Meta meta)
        {
            ControleFinanceiroService.Metas.Remove(meta);
            ControleFinanceiroService.Transacoes.RemoveAll(t => t.Tag == meta.Tag);
            await ControleFinanceiroService.Salvar();
        }

        public void Atualizar()
        {
            StateHasChanged();
        }
    }
}