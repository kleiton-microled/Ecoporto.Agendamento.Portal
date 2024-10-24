using Ecoporto.Agendamento.Portal.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecoporto.Agendamento.Portal.Application.Interfaces
{
    public interface IVeiculosAppServices
    {
        int Cadastrar(Veiculos veiculo);
       int Atualizar(Veiculos veiculo);
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
