using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecoporto.Agendamento.Portal.Application.Interfaces;
using Ecoporto.Agendamento.Portal.App_Start;
using System.Web.Security;
using Newtonsoft.Json;
using Ecoporto.Agendamento.Portal.Helpers;
using Ecoporto.Agendamento.Portal.Extensions; 


namespace Ecoporto.Agendamento.Portal.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly ILoginAppServices _loginAppServices;
        private readonly ITiposAgendamentosAppServices _tiposAgendamentosAppServices;

        public HomeController(ILoginAppServices loginAppServices, ITiposAgendamentosAppServices tiposAgendamentosAppServices)
        {
            _loginAppServices = loginAppServices;
            _tiposAgendamentosAppServices = tiposAgendamentosAppServices;
        }

        public ActionResult Index(int? id, bool? login)
        {
            ViewBag.MainClass = "container-fluid top-content";
#if DEBUG
            id = 10853305;
#endif

            if (login == null)
            if (id == 0 || id == null)
            {
                if (User.ObterDespachanteId() == 0)
                    return Redirect(Config.UrlICC());
            }
            else {
                return RedirectToAction("Login", "Home", new { id = id }); 
            }

            ViewBag.despachanteDescricao = User.ObterDespachanteDescricao();
            ViewBag.despachanteCnpj = User.ObterDespachanteCnpj();

            ViewBag.ListarSites = _tiposAgendamentosAppServices.GetListarTiposAgendamentos();

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(int? id)
        {
            ViewBag.MainClass = "container-fluid top-content";
            if (id == null)
                return Redirect(Config.UrlICC());

            var user = _loginAppServices.GetLoginUsuario(id.Value);

            if (user != null)
            {
                if (!user.Ativo)
                    return Redirect(Config.UrlICC());

                user.TransportadoraID = id.Value;

                var usuarioJson = JsonConvert
                       .SerializeObject(new
                       {
                           user.Id,
                           user.Nome,
                           user.Email,
                           user.CPF,
                           user.Ativo,
                           user.DespachanteId,
                           user.DespachanteDescricao,
                           user.DespachanteCNPJ,
                           user.TransportadoraID,
                           user.CNPJTRANSPORTADORA
                       });

                Identity.Logout();

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    user.Nome,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(480),
                    true,
                    usuarioJson);

                Response.Cookies.Add(
                    new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(authTicket)));

              
                return RedirectToAction("Index", "Home", new { login = true });
            }

            else {

                return Redirect(Config.UrlICC());
            }

            
        }
        public ActionResult Logout()
        {
            ViewBag.despachanteDescricao = User.ObterDespachanteDescricao();
            ViewBag.despachanteCnpj = User.ObterDespachanteCnpj();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetOpenAgendamento(string link, string descricao)
        {
            try
            {
                var caminhoCompleto = link + User.ObterTransportadoraID();
                if (link == "veiculos/index")
                    caminhoCompleto = link;

                if (link == "motoristas/index")
                    caminhoCompleto = link;

                if (User.ObterDespachanteId() == 0)
                {
                    retornoJson.Mensagem = "Não foi possivel logar no site, por favor tente novamennte";
                    retornoJson.objetoRetorno = null;
                    retornoJson.possuiDados = false;
                    retornoJson.statusRetorno = "500";


                    return Json (retornoJson, JsonRequestBehavior.AllowGet);
                }

                retornoJson.Mensagem = "Aguarde enquanto você está sendo direcionado para o " + descricao;
                retornoJson.objetoRetorno = new {
                    url = caminhoCompleto
                };
                retornoJson.possuiDados = false;
                retornoJson.statusRetorno = "200";


                return Json(retornoJson, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                retornoJson.Mensagem = "Não foi possivel logar no site, por favor tente novamennte";
                retornoJson.objetoRetorno = null;
                retornoJson.possuiDados = false;
                retornoJson.statusRetorno = "500";


                return Json(retornoJson, JsonRequestBehavior.AllowGet);
            }
        }
    }
}