using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace controleFinanceiro.Models
{
    public class Meta
    {
        public string Tag { get; set; }

        private DateTime data;

        public Meta()
        {
            Tag = Guid.NewGuid().ToString();
            DataInicio = DateTime.Now;
            data = DateTime.Now;
            Tipo = TipoMeta.Nenhum;
        }

        public TipoMeta Tipo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                switch (Tipo)
                {
                    case TipoMeta.CurtoPrazo:
                        if (value > DateTime.Now.AddYears(1))
                        {
                            data = DateTime.Now.AddYears(1);
                        }
                        else
                        {
                            data = value;
                        }
                        break;

                    case TipoMeta.MedioPrazo:
                        if (value > DateTime.Now.AddYears(5))
                        {
                            data = DateTime.Now.AddYears(5);
                        }
                        else
                        {
                            data = value;
                        }
                        break;

                    case TipoMeta.LongoPrazo:
                        if (value > DateTime.Now.AddYears(10))
                        {
                            data = DateTime.Now.AddYears(10);
                        }
                        else
                        {
                            data = value;
                        }
                        break;
                }
            }
        }

        public double Valor { get; set; }

        public double TotalMeses => ((Data.Year - DataInicio.Year) * 12) + (Data.Month - DataInicio.Month);

        public double MesesAtuais => ((DateTime.Now.Year - DataInicio.Year) * 12) + (DateTime.Now.Month - DataInicio.Month) + 1;

        public double Progresso => TotalMeses > 0d ? ((MesesAtuais / TotalMeses) * 100) : 0d;
    }
}