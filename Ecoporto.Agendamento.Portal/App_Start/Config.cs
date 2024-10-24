using System.Configuration;

namespace Ecoporto.Agendamento.Portal.App_Start
{
    public class Config
    {
        public static string UrlICC()
            => ConfigurationManager.AppSettings["UrlICC"].ToString();
    }
}