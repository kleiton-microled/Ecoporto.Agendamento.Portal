
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Extensions;
using Ecoporto.Agendamento.Portal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecoporto.Agendamento.Portal.App_Start;
using System.Web.Security;
using Newtonsoft.Json;
using Ecoporto.Agendamento.Portal.Helpers;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;
using Ecoporto.Agendamento.Portal.Domain.Extensions;
using Ecoporto.Agendamento.Portal.Models;
using Ecoporto.Agendamento.Portal.Application.Interfaces;


namespace Ecoporto.Agendamento.Portal.Controllers
{
    [Authorize]
    public class MotoristasController : BaseController
    {
        private readonly IMotoristaAppServices _motoristaAppService;
        private readonly IVeiculosAppServices _veiculoAppServices;

        public MotoristasController(IMotoristaAppServices motoristaRepositorio, IVeiculosAppServices veiculoRepositorio)
        {
            _motoristaAppService = motoristaRepositorio;
            _veiculoAppServices = veiculoRepositorio;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var idTranportadora = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());
            var resultado = _motoristaAppService
                .ObterMotoristas(idTranportadora).ToList();

            return View(resultado);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var cadastrar = new MotoristaViewModel();
            cadastrar.Paises = _motoristaAppService.ObterPaises().ToList();

            return View(cadastrar);
        }

        [HttpPost]
        public ActionResult Cadastrar(MotoristaViewModel viewModel)
        {

          
            try
            {

                if (viewModel.Data_Emissao == null)
                    return RetornarErro($"Data Emissão deve ser preenchida  !");
                if (Convert.ToDateTime(viewModel.Data_Emissao) > DateTime.Now)
                {
                    return RetornarErro($"Data inválida, emissão nao pode ser maior que hoje !");

                }



                if (viewModel.DT_Nascimento == null)
                    return RetornarErro($"Data Nascimento deve ser preenchida  !");

                if (Convert.ToDateTime(viewModel.DT_Nascimento) > DateTime.Now.AddYears(-18))
                {
                    return RetornarErro($"Motorista com menos de 18 anos");

                }

                if (Convert.ToDateTime(viewModel.DT_Nascimento) < DateTime.Now.AddYears(-100))
                {
                    return RetornarErro($"Data de nascimento invalida");

                }

                string[] motoristas = viewModel.Nome.Split(' ');

                if (motoristas[0].Length < 3)
                    return RetornarErro($"O primeiro nome do motorista não pode ter menos que três caracteres");

                if (motoristas.Count() == 1)
                    return RetornarErro($"Favor preencher o nome e o sobrenome do motorista");

                if (viewModel.Nome.Length < 3)
                    return RetornarErro($"O nome do motorista não pode ter menos que três caracteres");

                if (viewModel.Chkestrangeiro == false)
                {


                    var motoristaComCNH = _motoristaAppService.ObterMotoristaPorCNH(viewModel.CNH,Convert.ToInt32( User.ObterTransportadoraID()));

                    if (motoristaComCNH != null)
                        return RetornarErro($"Já existe um outro motorista cadastrado com a CNH {viewModel.CNH}");

                    var motoristaComCPF = _motoristaAppService.ObterMotoristaPorCPF(viewModel.CPF, Convert.ToInt32(User.ObterTransportadoraID()));

                    if (motoristaComCPF != null)
                        return RetornarErro($"Já existe um outro motorista cadastrado com o CPF {viewModel.CPF}");

                    var valido = Validation.CPFValido(viewModel.CPF);

                    if (!Validation.CPFValido(viewModel.CPF))
                    {
                        return RetornarErro($"CPF inválido");

                      
                    }

                    if(Convert.ToDateTime(viewModel.ValidadeCNH) <= DateTime.Now.AddDays(-30))
                        return RetornarErro($" A Validade da CNH deverá ser no mínimo 30 dias inferior à data atual");

                    if (Convert.ToDateTime(viewModel.ValidadeCNH) >= DateTime.Now.AddYears(10))
                        return RetornarErro($" A Validade da CNH deverá ser no máximo 10 anos");

                


                }
                else
                {
                    if (viewModel.Passaport == null)
                    {
                        return RetornarErro($"Passaport não pode ser em branco !");

                    }

                    if (Convert.ToDateTime(viewModel.Dt_passaport) < DateTime.Now)
                    {
                        return RetornarErro($"Data do Passaport não pode ser menor que hoje!");

                    }

                 

                    if (viewModel.Bigrama == "")
                    {
                        return RetornarErro($"Pais emissor obrigatório !");

                    }
                    if (viewModel.Carteira_Habilitacao == "")
                    {
                        return RetornarErro($"Carteira de Habilitação não pode ser em branco!");

                    }
                }

                var idTranportadora = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());
                var motorista = new Motorista
                {

                    Celular = viewModel.Celular,
                    CNH = viewModel.CNH,
                    CPF = viewModel.CPF,
                    Nextel = viewModel.Nextel,
                    Nome = viewModel.Nome,
                    Data_Emissao = viewModel.Data_Emissao,
                    ValidadeCNH = viewModel.ValidadeCNH,
                    TransportadoraId = idTranportadora,
                    MOP = viewModel.MOP,
                    RG = viewModel.RG,
                    Orgao_Emissor = viewModel.Orgao_Emissor,
                    DT_Passaport = viewModel.Dt_passaport,
                    Bigrama = viewModel.Bigrama,
                    Carteira_Habilitacao = viewModel.Carteira_Habilitacao,
                    DT_Carteira_Habilitacao = viewModel.DT_Carteira_Habilitacao,
                    Estrangeiro = viewModel.Chkestrangeiro ? 1 : 0,
                    Genero = "M",
                    Passaport = viewModel.Passaport,
                    DT_NASCIMENTO = viewModel.DT_Nascimento

                };

                if (viewModel.F)
                    motorista.Genero = "F";


                _motoristaAppService.Cadastrar(motorista);
            }
            catch (Exception ex)
            {
                return RetornarErro(ex.Message);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public ActionResult Atualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var motoristaBusca = _motoristaAppService.ObterMotoristaPorId(id.Value);

            var idTranportadora = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());


            if (motoristaBusca.TransportadoraId != idTranportadora)
                return RedirectToAction(nameof(Index));

            var viewModel = new MotoristaViewModel
            {
                Id = motoristaBusca.Id,
                Nome = motoristaBusca.Nome,
                CNH = motoristaBusca.CNH,
                ValidadeCNH = motoristaBusca.ValidadeCNH,
                RG = motoristaBusca.RG,
                CPF = motoristaBusca.CPF,
                Celular = motoristaBusca.Celular,
                Nextel = motoristaBusca.Nextel,
                MOP = motoristaBusca.MOP,
                Passaport = motoristaBusca.Passaport,
                Orgao_Emissor = motoristaBusca.Orgao_Emissor,
                Data_Emissao = motoristaBusca.Data_Emissao,
                Dt_passaport = motoristaBusca.DT_Passaport,
                Carteira_Habilitacao = motoristaBusca.Carteira_Habilitacao,
                Bigrama = motoristaBusca.Bigrama,
                DT_Nascimento = motoristaBusca.DT_NASCIMENTO,
                Chkestrangeiro = false,
                M = true,

                UltimaAlteracao = motoristaBusca.UltimaAlteracao.DataHoraFormatada()
            };
            viewModel.Paises = _motoristaAppService.ObterPaises().ToList();


            if (motoristaBusca.Estrangeiro == 1)
                viewModel.Chkestrangeiro = true;

            if (motoristaBusca.Genero == "F")
            {
                viewModel.F = true;
                viewModel.M = false;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Atualizar([Bind(Include = "Id, Nome, CNH, ValidadeCNH, RG, CPF, Celular, Nextel, MOP,Orgao_emissor,Data_emissao,dt_nascimento,TransportadoraId,carteira_habilitacao,Passaport,Bigrama,chkestrageiro,dt_passaport,M,F")] MotoristaViewModel viewModel, int? id)
        {
            try
            {
                viewModel.Paises = _motoristaAppService.ObterPaises().ToList();
                if (id == null)
                    return RedirectToAction(nameof(Index));

                string[] motoristas = viewModel.Nome.Split(' ');

                if (motoristas[0].Length < 3)
                {
                    ModelState.AddModelError(string.Empty, $"O primeiro nome do motorista não pode ter menos que três caracteres");

                    return View(viewModel);
                }

                if (viewModel.DT_Nascimento == null)
                {
                    ModelState.AddModelError(string.Empty, $"Data Nascimento deve ser preenchida  !");
                    return View(viewModel);
                }

                if (Convert.ToDateTime(viewModel.DT_Nascimento) > DateTime.Now.AddYears(-18))
                {
                    ModelState.AddModelError(string.Empty, $"Motorista com menos de 18 anos");
                    return View(viewModel);
                }


                if (Convert.ToDateTime(viewModel.DT_Nascimento) < DateTime.Now.AddYears(-100))
                {
                    ModelState.AddModelError(string.Empty, $"Data de nascimento invalida");
                    return View(viewModel);
                }


                if (viewModel.Data_Emissao == null)
                {
                    ModelState.AddModelError(string.Empty, $"Data Emissão deve ser preenchida  !");
                    return View(viewModel);
                }
                if (Convert.ToDateTime(viewModel.Data_Emissao) > DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, $"Data inválida, emissão nao pode ser maior que hoje !");
                    return View(viewModel);
                }


                if (motoristas.Count() == 1)
                {
                    ModelState.AddModelError(string.Empty, $"Favor preencher o nome e o sobrenome do motorista");

                    return View(viewModel);
                }

                if (viewModel.Nome.Length < 3)
                {
                    ModelState.AddModelError(string.Empty, $"O nome do motorista não pode ter menos que três caracteres");

                    return View(viewModel);
                }

           

                var motoristaBusca = _motoristaAppService.ObterMotoristaPorId(id.Value);

                if (motoristaBusca == null)
                    return RegistroNaoEncontrado();



                if (viewModel.Chkestrangeiro == false)
                {

                    var motoristaComCNH = _motoristaAppService.ObterMotoristaPorCNH(viewModel.CNH, Convert.ToInt32(User.ObterTransportadoraID()));
                    if (motoristaComCNH != null)
                    {
                        if (motoristaComCNH.Id != motoristaBusca.Id)
                        {
                            ModelState.AddModelError(string.Empty, $"Já existe um outro motorista cadastrado com a CNH {viewModel.CNH}");

                            return View(viewModel);
                        }
                    }

                    var valido = Validation.CPFValido(viewModel.CPF);

                    if (!Validation.CPFValido(viewModel.CPF))
                    {
                        ModelState.AddModelError(string.Empty, $"CPF inválido");

                        return View(viewModel);
                    }


                    if (Convert.ToDateTime(viewModel.ValidadeCNH) <= DateTime.Now.AddDays(-30))
                    {
                        ModelState.AddModelError(string.Empty, $" A Validade da CNH deverá ser no mínimo 30 dias inferior à data atual");
                        return View(viewModel);
                    }
                    if (Convert.ToDateTime(viewModel.ValidadeCNH) <= DateTime.Now.AddDays(-30)) {
                        ModelState.AddModelError(string.Empty, $" A Validade da CNH deverá ser no mínimo 30 dias inferior à data atual");
                    return View(viewModel);
                      }

                if (Convert.ToDateTime(viewModel.ValidadeCNH) >= DateTime.Now.AddYears(10))
                        return RetornarErro($" A Validade da CNH deverá ser no máximo 10 anos");

                    var motoristaComCPF = _motoristaAppService.ObterMotoristaPorCPF(viewModel.CPF, Convert.ToInt32(User.ObterTransportadoraID()));

                    if (motoristaComCPF != null)
                    {
                        if (motoristaComCPF.Id != motoristaBusca.Id)
                        {
                            ModelState.AddModelError(string.Empty, $"Já existe um outro motorista cadastrado com o CPF {viewModel.CPF}");

                            return View(viewModel);
                        }
                    }
                }
                else
                {
                    if (viewModel.Passaport == "")
                    {
                        ModelState.AddModelError(string.Empty, $"Passaport não pode ser em branco !");
                        return View(viewModel);
                    }


                    if (viewModel.Bigrama == "")
                    {
                        ModelState.AddModelError(string.Empty, $"Pais emissor obrigatório !");
                        return View(viewModel);
                    }


                    if (viewModel.Carteira_Habilitacao == "")
                    {
                        ModelState.AddModelError(string.Empty, $"Carteira de Habilitação não pode ser em branco!");
                        return View(viewModel);
                    }

                }

                motoristaBusca.Passaport = viewModel.Passaport;
                motoristaBusca.DT_Passaport = viewModel.Dt_passaport;
                motoristaBusca.Carteira_Habilitacao = viewModel.Carteira_Habilitacao;
                motoristaBusca.Bigrama = viewModel.Bigrama;
                motoristaBusca.DT_NASCIMENTO = viewModel.DT_Nascimento;
                motoristaBusca.Orgao_Emissor = viewModel.Orgao_Emissor;
                motoristaBusca.Data_Emissao = viewModel.Data_Emissao;
                motoristaBusca.ValidadeCNH = viewModel.ValidadeCNH;
                motoristaBusca.Nextel = viewModel.Nextel;
                motoristaBusca.MOP = viewModel.MOP;
                motoristaBusca.Celular = viewModel.Celular;
           
                motoristaBusca.Estrangeiro = 0;
                if (viewModel.Chkestrangeiro == true)
                    motoristaBusca.Estrangeiro = 1;

                motoristaBusca.Genero = "M";
                if (viewModel.F == true)
                {
                    motoristaBusca.Genero = "F";
                }


                if (motoristaBusca != null)
                {
                    _motoristaAppService.Atualizar(motoristaBusca);
                    TempData["Sucesso"] = true;
                     motoristaBusca = _motoristaAppService.ObterMotoristaPorId(id.Value);


                    viewModel.Nome = motoristaBusca.Nome;
                    viewModel.CNH = motoristaBusca.CNH;
                    viewModel.ValidadeCNH = motoristaBusca.ValidadeCNH;
                    viewModel.RG = motoristaBusca.RG;
                    viewModel.CPF = motoristaBusca.CPF;
                    viewModel.Celular = motoristaBusca.Celular;
                    viewModel.Nextel = motoristaBusca.Nextel;
                    viewModel.MOP = motoristaBusca.MOP;
                    viewModel.UltimaAlteracao = motoristaBusca.UltimaAlteracao;
                    viewModel.DT_Nascimento = motoristaBusca.DT_NASCIMENTO;
                    viewModel.Orgao_Emissor = motoristaBusca.Orgao_Emissor;
                    viewModel.Data_Emissao = motoristaBusca.Data_Emissao;


                    return View(viewModel);

                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return RetornarErro(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var motoristaBusca = _motoristaAppService.ObterMotoristaPorId(id);

                if (motoristaBusca == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Motorista não encontrado");

                if (motoristaBusca.TransportadoraId != User.ObterTransportadoraID())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Motorista não pertence a transportadora");

                _motoristaAppService.Excluir(motoristaBusca.Id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public ActionResult PesquisarCNH(string cnh)
        {
            if (string.IsNullOrWhiteSpace(cnh))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "CNH não informada");

            var motoristaChronos = _motoristaAppService.ObterMotoristaChronosPorCNH(cnh);

            if (motoristaChronos != null)
            {
                return Json(new
                {
                    motoristaChronos.Nome,
                    motoristaChronos.CNH,
                    motoristaChronos.ValidadeCNH,
                    motoristaChronos.RG,
                    motoristaChronos.CPF,
                    motoristaChronos.Celular,
                    UltimaAlteracao = motoristaChronos.UltimaAlteracao.DataHoraFormatada()
                }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}