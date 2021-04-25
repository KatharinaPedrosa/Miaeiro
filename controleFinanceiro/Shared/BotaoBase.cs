using controleFinanceiro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace controleFinanceiro.Shared
{
    public class BotaoBase : ComponentBase
    {
        [Parameter]
        public string Texto { get; set; }

        [Parameter]
        public Action Acao { get; set; }

        [Parameter]
        public Icone Icone { get; set; }

        [Parameter]
        public Func<bool> Selecionado { get; set; }
    }
}