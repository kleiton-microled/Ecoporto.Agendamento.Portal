using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecoporto.Agendamento.Portal.Domain.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToNumero(this decimal valor)
        {
            if (Decimal.TryParse(valor.ToString(), out decimal resultado))
                return string.Format("{0:N2}", resultado);

            return string.Empty;
        }
    }
}
