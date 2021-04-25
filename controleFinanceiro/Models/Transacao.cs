using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace controleFinanceiro.Models
{
    public class Transacao
    {
        public string Tag { get; set; }

        public DateTime Data { get; set; }

        public string Origem { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public TipoTransacao Tipo { get; set; }
    }
}