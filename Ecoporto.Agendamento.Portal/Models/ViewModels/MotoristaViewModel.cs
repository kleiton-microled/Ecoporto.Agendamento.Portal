using Ecoporto.Agendamento.Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecoporto.Agendamento.Portal.Models.ViewModels
{
    public class MotoristaViewModel
    {
        public int Id { get; set; }

        public int TransportadoraId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode ter mais que 50 caracteres.")]
        [MinLength(3, ErrorMessage = "O primeiro nome do motorista não pode ter menos que três caracteres.")]
        public string Nome { get; set; }

        public string CNH { get; set; }

        [Display(Name = "Validade Habilitação")]
        public string ValidadeCNH { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Celular { get; set; }

        public string Nextel { get; set; }

        public string MOP { get; set; }

        [Display(Name = "Última Alteração")]
        public string UltimaAlteracao { get; set; }

        [Display(Name = "Emissor-UF")]
        public string Bigrama { get; set; }
        public List<Paises> Paises { get; set; }

        public bool Chkestrangeiro { get; set; }

        [Display(Name = "Validade do Passaport")]
        public string Dt_passaport { get; set; }
        public bool F { get; set; }
        public bool M { get; set; }

    
        [Display(Name = "Orgão Emissor")]
        public string Orgao_Emissor { get; set; }

        [Display(Name = "Data de Emissão")]
        public string Data_Emissao { get; set; }

   
        [Display(Name = "Data de Nascimento")]
        public string DT_Nascimento { get; set; }

     
        [Display(Name = "Carteira de Habilitação")]
        public string Carteira_Habilitacao { get; set; }

        [Display(Name = "Passaporte")]
        public string Passaport { get; set; }
        //public string DT_Passaport { get; set; }
        public string DT_Carteira_Habilitacao { get; set; }
        //public string DataNascimento { get; set; }
    }
}