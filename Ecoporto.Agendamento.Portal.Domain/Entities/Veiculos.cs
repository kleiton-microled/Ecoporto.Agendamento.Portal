using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Ecoporto.Agendamento.Portal.Domain.Entities
{
    public class Veiculos
    {


        public int Id { get; set; }
        public int TransportadoraId { get; set; }

        public int Cadastro { get; set; }

        public int TipoCaminhaoId { get; set; }

        public string TipoCaminhaoDescricao { get; set; }

        public string Cavalo { get; set; }

        public string Carreta { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Tara { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        public string Descricao { get; set; }

        public DateTime UltimaAlteracao { get; set; }
        public string Marca { get; set; }
        public string Tanque { get; set; }
        public bool Rastreador { get; set; }
        public string Ano_Modelo { get; set; }
        public string Ano_Fabricacao {get;set;}
        public bool Flag_API { get; set; }
    

      

     
    
    }
}
