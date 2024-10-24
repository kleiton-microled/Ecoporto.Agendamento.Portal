using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Application.Interfaces;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;
using Ecoporto.Agendamento.Portal.Domain.Entities;


namespace Ecoporto.Agendamento.Portal.Application.Services
{
    public class VeiculosAppServices : IVeiculosAppServices
    {
        private readonly IVeiculosServices _veiculosServices;

        public VeiculosAppServices(IVeiculosServices veiculosServices)
        {
            _veiculosServices = veiculosServices;
        }


        public int Atualizar(Veiculos veiculo)
        {
       var id =   _veiculosServices.Atualizar(veiculo);
            return id;
        }

        public int Cadastrar(Veiculos veiculo)
        {
            return _veiculosServices.Cadastrar(veiculo);
        }

        public void Excluir(int id)
        {
            _veiculosServices.Excluir(id);
        }

        public int JaCadastrado(int transportadoraIdl, string carreta, string cavalo)
        {
            return _veiculosServices.JaCadastrado(transportadoraIdl, carreta, cavalo);
        }

        public IEnumerable<TipoVeiculo> ObterTiposVeiculos()
        {
            return _veiculosServices.ObterTiposVeiculos();
        }

        public IEnumerable<Veiculos> ObterUltimos5VeiculosAgendados(int transportadoraId)
        {
            return _veiculosServices.ObterUltimos5VeiculosAgendados(transportadoraId);
        }

        public Veiculos ObterVeiculosPorId(int id)
        {
            return _veiculosServices.ObterVeiculosPorId(id);
        }

        public IEnumerable<Veiculos> ObterVeiculos(int transportadoraId)
        {
            return _veiculosServices.ObterVeiculos(transportadoraId);
        }

        public int ObterIdTransportadora(string cnpjtransp)
        {
            return _veiculosServices.ObterIdTransportadora(cnpjtransp);
        }

        public IEnumerable<Veiculos> ObterVeiculosPorPlaca(string descricao, int transportadoraId)
        {
            return _veiculosServices.ObterVeiculosPorPlaca(descricao, transportadoraId);
        }
    }
}
