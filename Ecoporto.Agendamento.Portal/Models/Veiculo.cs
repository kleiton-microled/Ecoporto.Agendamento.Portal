using System;
using FluentValidation;
using Ecoporto.Agendamento.Portal;
using Ecoporto.Agendamento.Portal.Helpers;

namespace Ecoporto.Agendamento.Portal.Models
{
    public class Veiculo 
    {
     
        public int TransportadoraId { get; set; }

        public int Id { get; set; }

        public int Cadastro { get; set; }

        public int TipoCaminhaoId { get; set; }

        public string TipoCaminhaoDescricao { get; set; }

        public string Cavalo { get; set; }

        public string Carreta { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Tara { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        public string Descricao { get; set; }

        public DateTime UltimaAlteracao { get; set; }

    

        //public override void Validar()
        //{
        //    RuleFor(c => c.TransportadoraId)
        //        .GreaterThan(0)
        //        .WithMessage("Transportadora não informada");

        //    RuleFor(c => c.TipoCaminhaoId)
        //        .GreaterThan(0)
        //        .WithMessage("Tipo de Veículo não informado");

        //    RuleFor(c => c.Cavalo)
        //        .NotEmpty()
        //        .WithMessage("Placa do Cavalo não informada corretamente")
        //        .Length(8)
        //        .WithMessage("Placa do Cavalo é inválida");

        //    RuleFor(c => c.Carreta)
        //        .NotEmpty()
        //        .WithMessage("Placa da Carreta não informada corretamente")
        //        .Length(8)
        //        .WithMessage("Placa da Carreta é inválida");

        //    RuleFor(c => c.Cadastro)
        //          .GreaterThan(1)
        //        .When(c => c.Cadastro == 1)
        //        .WithMessage("Carreta já cadastrado com o mesmo Cavalo");

        //    RuleFor(c => c.Renavam)
        //        .NotEmpty()
        //        .WithMessage("Renavam não informado");

        //    RuleFor(c => c.Renavam)
        //        .Must(Validation.RenavamValido)
        //        .When(c => !string.IsNullOrEmpty(c.Renavam))
        //        .WithMessage("Renavam inválido");

        //    ValidationResult = Validate(this);
        //}

        public static bool CNHValida(DateTime validadeCNH)
        {
            if (DateTimeHelpers.IsDate(validadeCNH))
            {
                return validadeCNH > DateTime.Now.AddDays(-30);
            }

            return false;
        }
    }
}