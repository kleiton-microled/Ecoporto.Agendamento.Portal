
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
    public class VeiculosController : BaseController
    {
        private readonly IVeiculosAppServices _veiculoAppServices;

        public VeiculosController(IVeiculosAppServices veiculoRepositorio)
        {
            _veiculoAppServices = veiculoRepositorio;
        }

        [HttpGet]
        public ActionResult Index()
        {

            var idTranportadora = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());
            var resultado = _veiculoAppServices.ObterVeiculos(idTranportadora).ToList();
            var veiculos = new Veiculo();

            foreach(var item in resultado)
            {
                veiculos.Id = item.Id;
                veiculos.Carreta = item.Carreta;
                veiculos.Cavalo = item.Cavalo;
                veiculos.Chassi = item.Chassi;
                veiculos.Cor = item.Cor;
                veiculos.Modelo = item.Modelo;
                veiculos.Renavam = item.Renavam;
                veiculos.Chassi = item.Chassi;
            }
                           
            return View(resultado);
        }

        private void ObterTiposVeiculos(VeiculoViewModel viewModel)
        {
            viewModel.TiposVeiculos = _veiculoAppServices
                .ObterTiposVeiculos().ToList();
        }



        [HttpGet]
        public ActionResult Cadastrar()
        {
            var viewModel = new VeiculoViewModel();

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar([Bind(Include = "TipoCaminhaoId, Cavalo, Carreta, Chassi, Renavam, Tara, Modelo, Cor,rastreador,tanque,marca,ano_fabricacao,ano_modelo")] VeiculoViewModel viewModel)
        {
            if (String.IsNullOrEmpty(viewModel.Carreta))
            {
                ModelState.AddModelError(string.Empty, "Preencha a placa carreta");
                return View(viewModel);
            }

            if (!viewModel.Carreta.Contains("-"))
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (!viewModel.Cavalo.Contains("-"))
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (viewModel.Carreta.Length < 8)
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (viewModel.Cavalo.Length < 8)
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Cavalo))
            {
                ModelState.AddModelError(string.Empty, "Preencha a placa cavalo");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Tara))
            {
                ModelState.AddModelError(string.Empty, "Preencha o valor da tara do veículo");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Chassi))
            {
                ModelState.AddModelError(string.Empty, "Preencha o chassi");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Tanque))
            {
                ModelState.AddModelError(string.Empty, "Preencha da capacidade do tanque é obigatória");
                return View(viewModel);
            }

            if (viewModel.Ano_Fabricacao == null)
            {
                ModelState.AddModelError(string.Empty, "Informe o ano de Fabricação");
                return View(viewModel);
            }

            if (Convert.ToInt16(viewModel.Ano_Fabricacao) >= DateTime.Now.Year)
            {
                ModelState.AddModelError(string.Empty, "Ano de fabricação invalido, ano dever ser menor ou igual a  " + DateTime.Now.Year + "");
                return View(viewModel);
            }

            if (Convert.ToInt16(viewModel.Ano_Fabricacao) <= 1970)
            {
                ModelState.AddModelError(string.Empty, "Ano de fabricação invalido");
                return View(viewModel);
            }

            if (viewModel.Ano_Modelo == null)
            {
                ModelState.AddModelError(string.Empty, "Informe o ano do Modelo");
                return View(viewModel);
            }

            if (viewModel.Renavam == null)
            {
                ModelState.AddModelError(string.Empty, "Informe o renavam ");
                return View(viewModel);
            }

            if (viewModel.TipoCaminhaoId == 0)
            {
                ModelState.AddModelError(string.Empty, "Informe o tipo de Carreta ");
                return View(viewModel);
            }
            if (String.IsNullOrEmpty(viewModel.Tanque))
            {
                ModelState.AddModelError(string.Empty, "Preencha a capacidade to tanque");
                return View(viewModel);
            }

            if (viewModel.Tanque.ToInt() < 30)
            {
                ModelState.AddModelError(string.Empty, "Valor muito baixo");
                return View(viewModel);
            }




            if (Convert.ToInt16(viewModel.Ano_Modelo) > Convert.ToInt16(viewModel.Ano_Fabricacao) + 1)
            {
                ModelState.AddModelError(string.Empty, "Ano do modelo invalido, ano dever ser menor ou igual a  " + DateTime.Now.AddYears(1).Year + "");
                return View(viewModel);
            }

            if (Convert.ToInt16(viewModel.Ano_Modelo) < Convert.ToInt16(viewModel.Ano_Fabricacao))
            {
                ModelState.AddModelError(string.Empty, "Ano do modelo invalido, não pode ser menor que ano de fabricação");
                return View(viewModel);
            }

            var trans = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());
            var cad = _veiculoAppServices.JaCadastrado(Convert.ToInt32(trans), viewModel.Carreta, viewModel.Cavalo);


            var veiculo = new Veiculos();

            veiculo.TipoCaminhaoId = viewModel.TipoCaminhaoId;
            veiculo.Cavalo = viewModel.Cavalo;
            veiculo.Carreta = viewModel.Carreta;
            veiculo.Chassi = viewModel.Chassi;
            veiculo.Renavam = viewModel.Renavam;
            veiculo.Tara = viewModel.Tara.ToString();
            veiculo.Modelo = viewModel.Modelo;
            veiculo.Cor = viewModel.Cor;
            veiculo.Marca = viewModel.Marca;
            veiculo.Tanque = viewModel.Tanque;
            veiculo.Ano_Fabricacao = viewModel.Ano_Fabricacao;
            veiculo.Ano_Modelo = viewModel.Ano_Modelo;
            veiculo.Rastreador = viewModel.Rastreador;
            veiculo.TransportadoraId = trans;




            if (veiculo != null)
            {
                _veiculoAppServices.Cadastrar(veiculo);
                TempData["Sucesso"] = true;
            }

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Atualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var veiculoBusca = _veiculoAppServices.ObterVeiculosPorId(id.Value);

            var idTranportadora = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());

            if (veiculoBusca == null)
                return RegistroNaoEncontrado();

            if (veiculoBusca.TransportadoraId != idTranportadora)
                return RedirectToAction(nameof(Index));

            var viewModel = new VeiculoViewModel
            {
                Id = veiculoBusca.Id,
                TipoCaminhaoId = veiculoBusca.TipoCaminhaoId,
                Cavalo = veiculoBusca.Cavalo,
                Carreta = veiculoBusca.Carreta,
                Chassi = veiculoBusca.Chassi,
                Renavam = veiculoBusca.Renavam,
                Tara = veiculoBusca.Tara,
                Modelo = veiculoBusca.Modelo,
                Cor = veiculoBusca.Cor,
                Tanque = veiculoBusca.Tanque,
                Marca = veiculoBusca.Marca,
                Ano_Fabricacao = veiculoBusca.Ano_Fabricacao,
                Ano_Modelo = veiculoBusca.Ano_Modelo,
                UltimaAlteracao = veiculoBusca.UltimaAlteracao.DataHoraFormatada()
            };

            if (veiculoBusca.Rastreador)
                viewModel.Rastreador = true;

            
            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Atualizar(VeiculoViewModel viewModel, int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(Index));

            if (String.IsNullOrEmpty(viewModel.Carreta))
            {
                ModelState.AddModelError(string.Empty, "Preencha a placa carreta");
                return View(viewModel);
            }

            if (!viewModel.Carreta.Contains("-"))
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (!viewModel.Cavalo.Contains("-"))
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (viewModel.Carreta.Length < 8)
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (viewModel.Cavalo.Length < 8)
            {
                ModelState.AddModelError(string.Empty, "Placa invalida");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Cavalo))
            {
                ModelState.AddModelError(string.Empty, "Preencha a placa cavalo");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Tara))
            {
                ModelState.AddModelError(string.Empty, "Preencha o valor da tara do veículo");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Chassi))
            {
                ModelState.AddModelError(string.Empty, "Preencha o chassi");
                return View(viewModel);
            }

            if (String.IsNullOrEmpty(viewModel.Tanque))
            {
                ModelState.AddModelError(string.Empty, "Preencha da capacidade do tanque é obigatória");
                return View(viewModel);
            }

            if (viewModel.Ano_Fabricacao == null)
            {
                ModelState.AddModelError(string.Empty, "Informe o ano de Fabricação");
                return View(viewModel);
            }

            if (Convert.ToInt16(viewModel.Ano_Fabricacao) > DateTime.Now.Year)
            {
                ModelState.AddModelError(string.Empty, "Ano de fabricação invalido, ano dever ser menor ou igual a  " + DateTime.Now.Year + "");
                return View(viewModel);
            }

            if (Convert.ToInt16(viewModel.Ano_Fabricacao) < 1970)
            {
                ModelState.AddModelError(string.Empty, "Ano de fabricação invalido");
                return View(viewModel);
            }

            if (viewModel.Ano_Modelo == null)
            {
                ModelState.AddModelError(string.Empty, "Informe o ano do Modelo");
                return View(viewModel);
            }

            if (viewModel.Renavam == null)
            {
                ModelState.AddModelError(string.Empty, "Informe o renavam ");
                return View(viewModel);
            }

            if (viewModel.TipoCaminhaoId == 0)
            {
                ModelState.AddModelError(string.Empty, "Informe o tipo de Carreta ");
                return View(viewModel);
            }
            if (String.IsNullOrEmpty(viewModel.Tanque))
            {
                ModelState.AddModelError(string.Empty, "Preencha a capacidade to tanque");
                return View(viewModel);
            }

            if (viewModel.Tanque.ToInt() < 30)
            {
                ModelState.AddModelError(string.Empty, "Valor muito baixo");
                return View(viewModel);
            }




            if (Convert.ToInt16(viewModel.Ano_Modelo) > Convert.ToInt16(viewModel.Ano_Fabricacao) + 1)
            {
                ModelState.AddModelError(string.Empty, "Ano do modelo invalido, ano dever ser menor ou igual a  " + DateTime.Now.AddYears(1).Year + "");
                return View(viewModel);
            }

            if (Convert.ToInt16(viewModel.Ano_Modelo) < Convert.ToInt16(viewModel.Ano_Fabricacao))
            {
                ModelState.AddModelError(string.Empty, "Ano do modelo invalido, não pode ser menor que ano de fabricação");
                return View(viewModel);
            }

            var trans = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());
            var veiculoBusca = _veiculoAppServices.ObterVeiculosPorId(id);

            if (veiculoBusca == null)
                return RegistroNaoEncontrado();

            veiculoBusca = new Veiculos();

             veiculoBusca.TipoCaminhaoId = viewModel.TipoCaminhaoId;
             veiculoBusca.Cavalo = viewModel.Cavalo;
             veiculoBusca.Carreta = viewModel.Carreta;
             veiculoBusca.Chassi = viewModel.Chassi;
             veiculoBusca.Renavam = viewModel.Renavam;
             veiculoBusca.Tara = viewModel.Tara.ToString();
             veiculoBusca.Modelo = viewModel.Modelo;
             veiculoBusca.Cor = viewModel.Cor;
             veiculoBusca.Marca = viewModel.Marca;
             veiculoBusca.Tanque = viewModel.Tanque;
             veiculoBusca.Ano_Fabricacao = viewModel.Ano_Fabricacao;
             veiculoBusca.Ano_Modelo = viewModel.Ano_Modelo;
             veiculoBusca.Rastreador = viewModel.Rastreador;
             veiculoBusca.TransportadoraId = trans;
            veiculoBusca.Id = id;

            if (veiculoBusca != null)
            {
           var atualizar =     _veiculoAppServices.Atualizar(veiculoBusca);
                if(atualizar >0)
                TempData["Sucesso"] = true;
            }

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var veiculoBusca = _veiculoAppServices.ObterVeiculosPorId(id);

                if (veiculoBusca == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Veículo não encontrado");
                var trans = _veiculoAppServices.ObterIdTransportadora(User.ObterCNPJTransportadora());
                if (veiculoBusca.TransportadoraId != trans)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Veículo não pertence a transportadora");

                _veiculoAppServices.Excluir(veiculoBusca.Id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}