using FluentValidation;
using System;
using ONGColab.Domain.Base;

namespace ONGColab.Domain.Entities
{
    public class Voluntariado : Entity
    {
        private Voluntariado() { }

        public Voluntariado(Guid id, Guid dadosPessoaisId, Guid enderecoId,
                      Voluntarix dadosPessoais, Causa funcaoTrabalho, Experiencia xp)
        {
            Id = id;
            DataHora = DateTime.Now;

            DadosPessoaisId = dadosPessoaisId;
            EnderecoId = enderecoId;

            Xp = xp;

            DadosPessoais = dadosPessoais;
            FuncaoTrabalho = funcaoTrabalho;
        }

        public Guid DadosPessoaisId { get; private set; }
        public Guid EnderecoId { get; private set; }

        public DateTime DataHora { get; private set; }

        public Voluntarix DadosPessoais { get; private set; }
        public Causa FuncaoTrabalho { get; private set; }

        public void AtualizarDataInscricao()
        {
            DataHora = DateTime.Now;
        }

        public void AdicionarPessoa(Voluntarix voluntarix)
        {
            DadosPessoais = voluntarix;
        }

        public void AdicionarEndereco(Endereco endereco) {
            Endereco = endereco;
        }

        public void AdicionarXp(Experiencia xp) {
            Xp = xp;
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
            RuleFor(a => a.DadosPessoais).NotNull().WithMessage("Os Dados Pessoais são obrigatórios").SetValidator(new VoluntarixValidacao());
            RuleFor(a => a.Xp).NotNull().WithMessage("Insira a sua formação.").SetValidator(new ExperienciaValidacao());
            RuleFor(a => a.FuncaoTrabalho).NotNull().WithMessage("Escolha uma função para o trabalho voluntário.").SetValidator(new CausaValidacao());
        }
    }
}