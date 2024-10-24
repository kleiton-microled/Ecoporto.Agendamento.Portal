using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;


namespace Ecoporto.Agendamento.Portal.Domain.Services
{
   public class VeiculosServices: IVeiculosServices
    {
        private readonly IVeiculosRepository _veiculosRepositorio;

        public VeiculosServices(IVeiculosRepository veiculosRepositorio)
        {
            _veiculosRepositorio = veiculosRepositorio;
        
        }

        public int  Atualizar(Veiculos Veiculos)
        {
       var id=    _veiculosRepositorio.Atualizar(Veiculos);
            return id;
        }

        public int Cadastrar(Veiculos Veiculos)
        {
            return _veiculosRepositorio.Cadastrar(Veiculos);
        }

        public void Excluir(int id)
        {
             _veiculosRepositorio.Excluir(id);
        }

        public int JaCadastrado(int transportadoraIdl, string carreta, string cavalo)
        {
            return _veiculosRepositorio.JaCadastrado(transportadoraIdl, carreta, cavalo);
        }

        public IEnumerable<TipoVeiculo> ObterTiposVeiculos()
        {
            return _veiculosRepositorio.ObterTiposVeiculos();
        }                             

        public IEnumerable<Veiculos> ObterUltimos5VeiculosAgendados(int transportadoraId)
        {
            return _veiculosRepositorio.ObterUltimos5VeiculosAgendados(transportadoraId);
        }

        public Veiculos ObterVeiculosPorId(int id)
        {
            return _veiculosRepositorio.ObterVeiculosPorId(id);
        }

        public IEnumerable<Veiculos> ObterVeiculos(int transportadoraId)
        {
            return _veiculosRepositorio.ObterVeiculos(transportadoraId);
        }
      
       public int ObterIdTransportadora(string cnpjtransp)
        {
            return _veiculosRepositorio.ObterIdTransportadora(cnpjtransp);
        }

        public IEnumerable<Veiculos> ObterVeiculosPorPlaca(string descricao, int transportadoraId)
        {
            return _veiculosRepositorio.ObterVeiculosPorPlaca(descricao, transportadoraId);
        }




    }
}
