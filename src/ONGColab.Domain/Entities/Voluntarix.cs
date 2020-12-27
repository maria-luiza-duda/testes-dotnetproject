using FluentValidation;
using System;
using System.Collections.Generic;
using ONGColab.Domain.Base;

namespace ONGColab.Domain.Entities
{
    public class Voluntarix : Entity
    {
        public Voluntarix() { }

        public Voluntarix(Guid id, string nome, string email, bool candidatura, string habilidadesVaga)
        {
            Id = id;
            _nome = nome;
            Email = email;
            Candidatura = candidatura;
            HabilidadesVaga = habilidadesVaga;
        }

        private string _nome;

        public string Nome
        {
            get { return Candidatura ? Email : _nome; }
            private set { _nome = value; }
        }

        public bool Candidatura { get; private set; }
        public string HabilidadesVaga { get; private set; }

        public string Email { get; private set; }
        public ICollection<Voluntariado> Voluntariados { get; set; }

        public override bool Valido()
        {
            ValidationResult = new VoluntarixValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class VoluntarixValidacao : AbstractValidator<Voluntarix>
    {
        private const int MAX_LENTH_CAMPOS = 150;
        private const int MAX_LENTH_MENSAGEM = 500;

        public VoluntarixValidacao()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .When(a => a.Anonima == false)
                .MaximumLength(MAX_LENTH_CAMPOS).WithMessage("O campo Nome deve possuir no máximo 150 caracteres.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .MaximumLength(MAX_LENTH_CAMPOS).WithMessage($"O campo Email deve possuir no máximo {MAX_LENTH_CAMPOS} caracteres.");

            RuleFor(a => a.Email).EmailAddress()
                .When(a => !string.IsNullOrEmpty(a.Email))
                .When(a => a?.Email?.Length <= MAX_LENTH_CAMPOS)
                .WithMessage("O campo Email é inválido.");

            RuleFor(a => a.HabilidadesVaga)                
                .MaximumLength(MAX_LENTH_MENSAGEM).WithMessage($"O campo Habilidades para vaga deve possuir no máximo {MAX_LENTH_MENSAGEM} caracteres.");
        }
    }
}