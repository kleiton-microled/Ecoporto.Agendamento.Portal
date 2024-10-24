using Ecoporto.Agendamento.Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecoporto.Agendamento.Portal.Models.ViewModels
{
    public class VeiculoViewModel
    {
        public VeiculoViewModel()
        {
            TiposVeiculos = new List<TipoVeiculo>();
        }

        public int Id { get; set; }

        public int TransportadoraId { get; set; }

        [Display(Name = "Tipo Veículo")]
        public int TipoCaminhaoId { get; set; }

        public int TipoCaminhaoDescricao { get; set; }

        public string Cavalo { get; set; }

        public string Carreta { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Tara { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        [Display(Name = "Última Alteração")]
        public string UltimaAlteracao { get; set; }

        [Display(Name ="Marca")]
        public string Marca { get; set; }
        [Display(Name ="Ano Fabricação")]
        public string Ano_Fabricacao { get; set; }
        [Display(Name ="Ano Modelo")]
        public string Ano_Modelo { get; set; }
        public string Tanque { get; set; }
        public bool Rastreador { get; set; }

        public List<TipoVeiculo> TiposVeiculos { get; set; }

    }
}