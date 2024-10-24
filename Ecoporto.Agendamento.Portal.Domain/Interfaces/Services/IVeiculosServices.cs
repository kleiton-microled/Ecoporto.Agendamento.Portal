using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecoporto.Agendamento.Portal.Domain.Interfaces.Services
{
    public interface IVeiculosServices
    {
        int Cadastrar(Veiculos Veiculos);
       int Atualizar(Veiculos Veiculos);
        void Excluir(int id);
        IEnumerable<Veiculos> ObterVeiculos(int transportadoraId);
        IEnumerable<Veiculos> ObterVeiculosPorPlaca(string descricao, int transportadoraId);
        Veiculos ObterVeiculosPorId(int id);
        int JaCadastrado(int transportadoraIdl, string carreta, string cavalo);

        IEnumerable<TipoVeiculo> ObterTiposVeiculos();
        IEnumerable<Veiculos> ObterUltimos5VeiculosAgendados(int transportadoraId);
        int ObterIdTransportadora(string cnpjtransp);
    }
}
