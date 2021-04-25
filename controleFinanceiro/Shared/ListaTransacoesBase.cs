using controleFinanceiro.Models;
using controleFinanceiro.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace controleFinanceiro.Shared
{
    public class ListaTransacoesBase : ComponentBase
    {
        [Inject]
        public IControleFincanceiroService ControleFinanceiroService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ControleFinanceiroService.Carregar();
        }

        public async Task Excluir(Transacao transacao)
        {
            ControleFinanceiroService.Transacoes.Remove(transacao);
            await ControleFinanceiroService.Salvar();
        }

        public void Atualizar()
        {
            StateHasChanged();
        }
    }
}