using Ecoporto.Agendamento.Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecoporto.Agendamento.Portal.Domain.Entities
{
    public class Login: Entidade<Login>
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public int DespachanteId { get; set; }

        public string DespachanteDescricao { get; set; }

        public string DespachanteCNPJ { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }

        public long TransportadoraID { get; set; }
        public string CNPJTRANSPORTADORA { get; set; }

        public override void Validar()
        {
            ValidationResult = Validate(this);
        }
    }
}
