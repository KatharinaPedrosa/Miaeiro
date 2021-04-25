using controleFinanceiro.Models;
using controleFinanceiro.Services;
using controleFinanceiro.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace controleFinanceiro.Pages
{
    public class EntradasBase : ComponentBase
    {
        [Inject]
        public IControleFincanceiroService controleFinanceiroService { get; set; }

        public ListaTransacoes lista;
        public bool exibirForm = false;
        public Transacao transacao = new Transacao();

        public void adicionar(double valor)
        {
            transacao.Valor += valor;
        }

        public void adicionarSalario()
        {
            transacao = new Transacao();
            transacao.Data = DateTime.Now;
            transacao.Origem = "Salário";
            transacao.Tipo = TipoTransacao.entrada;
            exibirForm = true;
        }

        public void adicionarExtra()
        {
            transacao = new Transacao();
            transacao.Data = DateTime.Now;
            transacao.Origem = "Extra";
            transacao.Tipo = TipoTransacao.entrada;
            exibirForm = true;
        }

        public async Task confirmar()
        {
            if (transacao.Valor == 0) return;
            controleFinanceiroService.Transacoes.Add(transacao);
            await controleFinanceiroService.Salvar();
            transacao = new Transacao();
            exibirForm = false;
            lista.Atualizar();
        }
    }
}