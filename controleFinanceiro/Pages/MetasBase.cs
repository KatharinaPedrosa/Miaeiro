using controleFinanceiro.Models;
using controleFinanceiro.Services;
using controleFinanceiro.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace controleFinanceiro.Pages
{
    public class MetasBase : ComponentBase
    {
        [Inject]
        public IControleFincanceiroService controleFinanceiroService { get; set; }

        public Meta meta = new Meta();
        public bool exibirForm = false;
        public ListaMetas lista;

        public async Task AdicionarMeta(TipoMeta tipo)
        {
            meta = new Meta();
            meta.Tipo = tipo;
            switch (tipo)
            {
                case TipoMeta.CurtoPrazo:
                    meta.Data = DateTime.Now.AddYears(1);
                    break;

                case TipoMeta.MedioPrazo:
                    meta.Data = DateTime.Now.AddYears(5);
                    break;

                case TipoMeta.LongoPrazo:
                    meta.Data = DateTime.Now.AddYears(10);
                    break;

                default:
                    break;
            }
            exibirForm = true;
        }

        public async Task confirmar()
        {
            if (meta.Valor == 0) return;

            controleFinanceiroService.Metas.Add(meta);
            var dataTransacao = new DateTime(meta.DataInicio.Year, meta.DataInicio.Month, 1);
            for (int i = 0; i < meta.TotalMeses; i++)
            {
                controleFinanceiroService.Transacoes.Add(new Transacao()
                {
                    Tag = meta.Tag,
                    Data = dataTransacao,
                    Origem = "Meta",
                    Tipo = TipoTransacao.saida,
                    Descricao = meta.Descricao,
                    Valor = meta.Valor / meta.TotalMeses
                });
                dataTransacao = dataTransacao.AddMonths(1);
            }
            await controleFinanceiroService.Salvar();
            meta = new Meta();
            lista.Atualizar();
            exibirForm = false;
        }
    }
}