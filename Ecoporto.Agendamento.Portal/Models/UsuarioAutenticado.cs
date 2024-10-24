using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecoporto.Agendamento.Portal.Models
{
    public class UsuarioAutenticado
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public int DespachanteId { get; set; }

        public string DespachanteDescricao { get; set; }

        public string DespachanteCNPJ { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }

        public string cID { get; set; }

        public byte[] cByteId { get; set; }
        public long TransportadoraID { get; set; }
        public string CNPJTransportadora { get; set; }
    }
}