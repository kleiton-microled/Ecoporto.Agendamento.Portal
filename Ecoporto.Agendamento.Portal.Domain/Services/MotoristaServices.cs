﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;

namespace Ecoporto.Agendamento.Portal.Domain.Services
{
   public class MotoristaServices : IMotoristaServices
    {
        private readonly IMotoristaRepository _motoristaServices;

        public MotoristaServices(IMotoristaRepository veiculosServices)
        { 
            _motoristaServices = veiculosServices;
        }

        public int Cadastrar(Motorista motorista)
        {
            return _motoristaServices.Cadastrar(motorista);
        }
        public void Atualizar(Motorista motorista)
        {
            _motoristaServices.Atualizar(motorista);
        }
        public void Excluir(int id)
        {
            _motoristaServices.Excluir(id);
        }
        public IEnumerable<Motorista> ObterMotoristas(int transportadoraId)
        {
            return _motoristaServices.ObterMotoristas(transportadoraId);
        }
        public IEnumerable<Motorista> ObterMotoristasPorNomeOuCNH(string descricao, int transportadoraId)
        {
            return _motoristaServices.ObterMotoristasPorNomeOuCNH(descricao, transportadoraId);
        }
        public Motorista ObterMotoristaPorId(int id)
        {
            return _motoristaServices.ObterMotoristaPorId(id);
        }
        public Motorista ObterMotoristaPorCNH(string cnh, int transportadoraId)
        {
            return _motoristaServices.ObterMotoristaPorCNH(cnh, transportadoraId);
        }
        public Motorista ObterMotoristaPorCPF(string cpf, int transportadoraId)
        {
            return _motoristaServices.ObterMotoristaPorCPF(cpf, transportadoraId);
        }
        public IEnumerable<Motorista> ObterUltimos5MotoristasAgendados(int transportadoraId)
        {
            return _motoristaServices.ObterUltimos5MotoristasAgendados(transportadoraId);
        }
        public Motorista ObterMotoristaChronosPorCNH(string cnh)
        {
            return _motoristaServices.ObterMotoristaChronosPorCNH(cnh);
        }

        public IEnumerable<Paises> ObterPaises()
        {
            return _motoristaServices.ObterPaises();
        }
    }
}