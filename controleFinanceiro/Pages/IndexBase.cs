using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor;
using ChartJs.Blazor.Util;
using controleFinanceiro.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using controleFinanceiro.Models;
using ChartJs.Blazor.PieChart;
using System;

namespace controleFinanceiro.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IControleFincanceiroService ControleFincanceiroService { get; set; }

        public PieConfig ConfigEntrada { get; set; }
        public PieConfig ConfigGasto { get; set; }
        public PieConfig ConfigTotal { get; set; }

        private PieDataset<double> gastosPorDia;
        private PieDataset<double> entradasPorDia;
        private PieDataset<double> total;

        protected override async Task OnInitializedAsync()
        {
            ConfigGraficos();
            await CarregarDados();
        }

        private async Task CarregarDados()
        {
            await ControleFincanceiroService.Carregar();

            ControleFincanceiroService.Transacoes
                .Where(transacao => transacao.Tipo == TipoTransacao.saida && transacao.Data.Month == DateTime.Now.Month)
                .GroupBy(transacao => transacao.Origem)
                .OrderBy(grupo => grupo.Key)
                .Select(grupo => grupo.Key)
                .ToList()
                .ForEach(label => ConfigGasto.Data.Labels.Add(label));

            ControleFincanceiroService.Transacoes
                .Where(transacao => transacao.Tipo == TipoTransacao.entrada && transacao.Data.Month == DateTime.Now.Month)
                .GroupBy(transacao => transacao.Origem)
                .OrderBy(grupo => grupo.Key)
                .Select(grupo => grupo.Key)
                .ToList()
                .ForEach(label => ConfigEntrada.Data.Labels.Add(label));

            ConfigTotal.Data.Labels.Add("Saídas");
            ConfigTotal.Data.Labels.Add("Entradas");

            gastosPorDia.AddRange(ControleFincanceiroService.Transacoes
               .Where(transacao => transacao.Tipo == TipoTransacao.saida && transacao.Data.Month == DateTime.Now.Month)
               .GroupBy(transacao => transacao.Origem)
               .OrderBy(grupo => grupo.Key)
               .Select(grupo => grupo.Sum(transacao => transacao.Valor)));

            entradasPorDia.AddRange(ControleFincanceiroService.Transacoes
               .Where(transacao => transacao.Tipo == TipoTransacao.entrada && transacao.Data.Month == DateTime.Now.Month)
               .GroupBy(transacao => transacao.Origem)
               .OrderBy(grupo => grupo.Key)
               .Select(grupo => grupo.Sum(transacao => transacao.Valor)));

            total.AddRange(ControleFincanceiroService.Transacoes
               .Where(transacao => transacao.Data.Month == DateTime.Now.Month)
               .GroupBy(transacao => transacao.Tipo)
               .OrderBy(grupo => grupo.Key)
               .Select(grupo => grupo.Sum(transacao => transacao.Valor)));
        }

        private void ConfigGraficos()
        {
            gastosPorDia = new PieDataset<double>()
            {
                BackgroundColor = new[]
                {
                    ColorUtil.ColorHexString(255, 99, 132),
                    ColorUtil.ColorHexString(255, 205, 86),
                    ColorUtil.ColorHexString(255, 93, 27),
                    ColorUtil.ColorHexString(150, 27, 196),
                }
            };

            entradasPorDia = new PieDataset<double>()
            {
                BackgroundColor = new[]
                {
                    ColorUtil.ColorHexString(75, 192, 192),
                    ColorUtil.ColorHexString(54, 162, 235)
                }
            };

            total = new PieDataset<double>()
            {
                BackgroundColor = new[]
                {
                    ColorUtil.ColorHexString(192, 0, 0),
                    ColorUtil.ColorHexString(0, 192, 0)
                }
            };

            ConfigEntrada = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Entradas"
                    }
                }
            };

            ConfigGasto = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Saídas"
                    }
                }
            };

            ConfigTotal = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Total"
                    }
                }
            };

            ConfigEntrada.Data.Datasets.Add(entradasPorDia);
            ConfigGasto.Data.Datasets.Add(gastosPorDia);
            ConfigTotal.Data.Datasets.Add(total);
        }
    }
}