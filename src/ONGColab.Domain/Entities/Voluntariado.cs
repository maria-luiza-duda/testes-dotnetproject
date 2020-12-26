using FluentValidation;
using System;
using ONGColab.Domain.Base;

namespace ONGColab.Domain.Entities
{
    public class Voluntariado : Entity
    {
        private Voluntariado() { }

        public Voluntariado(Guid id, Guid dadosPessoaisId, Guid enderecoId,
                      Voluntario dadosPessoais, Causa funcaoTrabalho, Formacao nivelXP)
        {
            Id = id;
            DataHora = DateTime.Now;

            DadosPessoaisId = dadosPessoaisId;
            EnderecoId = enderecoId;

            NivelXP = nivelXP;

            DadosPessoais = dadosPessoais;
            FuncaoTrabalho = funcaoTrabalho;
        }

        public Guid DadosPessoaisId { get; private set; }
        public Guid EnderecoId { get; private set; }

        public DateTime DataHora { get; private set; }

        public Voluntario DadosPessoais { get; private set; }
        public Causa FuncaoTrabalho { get; private set; }

        public void AtualizarDataInscricao()
        {
            DataHora = DateTime.Now;
        }

        public void AdicionarPessoa(Voluntario voluntario)
        {
            DadosPessoais = voluntario;
        }

        public void AdicionarEndereco(Endereco endereco) {
            Endereco = endereco;
        }

        public void AdicionarNivelXP(Formacao nivelXP) {
            NivelXP = nivelXP;
        }

        public void AdicionarFuncaoTrabalho(Causa funcaoTrabalho) {
            FuncaoTrabalho = funcaoTrabalho;
        }

        public override bool Valido()
        {
            ValidationResult = new VoluntariadoValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class VoluntariadoValidacao : AbstractValidator<Voluntariado>
    {
        public VoluntariadoValidacao()
        {
            RuleFor(a => a.DadosPessoais).NotNull().WithMessage("Os Dados Pessoais são obrigatórios").SetValidator(new VoluntarioValidacao());
            RuleFor(a => a.NivelXP).NotNull().WithMessage("Insira a sua formação.").SetValidator(new FormacaoValidacao());
            RuleFor(a => a.FuncaoTrabalho).NotNull().WithMessage("Escolha uma função para o trabalho voluntário.").SetValidator(new CausaValidacao());
        }
    }
}