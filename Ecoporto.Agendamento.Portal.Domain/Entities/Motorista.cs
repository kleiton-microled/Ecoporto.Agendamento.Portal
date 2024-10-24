using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Helpers;

namespace Ecoporto.Agendamento.Portal.Domain.Entities
{
  public  class Motorista
    {
        public Motorista()
        {

        }

  

  

        public int TransportadoraId { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }

        public string CNH { get; set; }

        public string ValidadeCNH { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Celular { get; set; }

        public string Nextel { get; set; }

        public string MOP { get; set; }

        public string Descricao { get; set; }

        public string UltimaAlteracao { get; set; }

        public bool Inativo { get; set; }
        public bool CNHVencida => !Validation.CNHEmDia(Convert.ToDateTime(ValidadeCNH));

        public int Estrangeiro { get; set; }
        public string Passaport { get; set; }
        public string Carteira_Habilitacao { get; set; }
        public string Bigrama { get; set; }
        public string DT_Passaport { get; set; }
        public string Genero { get; set; }

        public string DT_NASCIMENTO { get; set; }
        public string Orgao_Emissor { get; set; }

        public string Data_Emissao { get; set; }

        public string DT_Carteira_Habilitacao { get; set; }

        public bool F { get; set; }
        public bool M { get; set; }

    }
}
