using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecoporto.Agendamento.Portal.Domain.Entities
{
    public class TiposAgendamentos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
}
