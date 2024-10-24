using Ecoporto.Agendamento.Portal.Domain.Extensions;
using System.Security.Claims;
using System.Security.Principal;

namespace Ecoporto.Agendamento.Portal.Extensions
{
    public static class UserExtensions
    {
        public static int ObterId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Id");

            return claim == null
                ? 0
                : claim.Value.ToInt();
        }

        public static string ObterNome(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Nome");

            return claim?.Value;
        }

        public static string ObterEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Email");

            return claim?.Value;
        }

        public static int ObterDespachanteId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("DespachanteId");

            return claim?.Value != null
                ? claim.Value.ToInt()
                : 0;
        }

        public static string ObterDespachanteDescricao(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("DespachanteDescricao");

            return claim?.Value ?? string.Empty;
        }

        public static string ObterCNPJTransportadora(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("CNPJTransportadora");

            return claim?.Value ?? string.Empty;
        }

        public static string ObterDespachanteCnpj(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("DespachanteCNPJ");

            return claim?.Value ?? string.Empty;
        }


        public static string ObterCriptoID(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("cID");

            return claim?.Value ?? string.Empty;
        }

        public static byte[] ObterByteId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("cByteId");

            return claim?.Value != null
               ? claim.Value.ToByteArray()
               : null;
        }
        public static long ObterTransportadoraID(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("TransportadoraID");

            return claim?.Value != null
                ? claim.Value.ToInt()
                : 0;
        }
    }
}