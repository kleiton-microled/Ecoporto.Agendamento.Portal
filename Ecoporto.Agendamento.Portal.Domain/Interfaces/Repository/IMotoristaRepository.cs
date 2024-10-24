using Ecoporto.Agendamento.Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository
{
   public interface IMotoristaRepository
    {
        int Cadastrar(Motorista motorista);
        void Atualizar(Motorista motorista);
        void Excluir(int id);
        IEnumerable<Motorista> ObterMotoristas(int transportadoraId);
        IEnumerable<Motorista> ObterMotoristasPorNomeOuCNH(string descricao, int transportadoraId);
        Motorista ObterMotoristaPorId(int id);
        Motorista ObterMotoristaPorCNH(string cnh, int transportadoraId);
        Motorista ObterMotoristaPorCPF(string cpf, int transportadoraId);
        IEnumerable<Motorista> ObterUltimos5MotoristasAgendados(int transportadoraId);
        Motorista ObterMotoristaChronosPorCNH(string cnh);

        IEnumerable<Paises> ObterPaises();
    }
}
