using FluentValidation;
using System;
using System.Collections.Generic;
using ONGColab.Domain.Base;

namespace ONGColab.Domain.Entities
{
    public class Experiencia : Entity
    {
        public Experiencia() { }

        public Experiencia(Guid id, string formacao, string experienciaProfissional, string tempoTrabalho, string atividadesExercidas)
        {
            Id = id;
            Formacao = formacao;
            ExperienciaProfissional = experienciaProfissional;
            TempoTrabalho = tempoTrabalho;
            AtividadesExercidas = atividadesExercidas;
        }
        
        public string Formacao { get; private set; }
        public string ExperienciaProfissional { get; private set; }
        public string TempoTrabalho { get; private set; }
        public string AtividadesExercidas { get; private set; }

        public ICollection<Voluntariado> Voluntariado { get; set; }

        public override bool Valido()
        {
            ValidationResult = new ExperienciaValidacao().Validate( this);
            return ValidationResult.IsValid;
        }
    }

    public class ExperienciaValidacao : AbstractValidator<Experiencia>
    {
        private const int MAX_LENGHT_EXPERIENCIA = 250;
        private const int MAX_LENGHT_FORMACAO = 150;
        private const int MAX_LENGHT_EXPERIENCIAPROFISSIONAL = 500;
        private const int MAX_LENGHT_TEMPOTRABALHO = 16;
        private const int MAX_LENGHT_ATIVIDADESEXERCIDAS = 500;

        public ExperienciaValidacao()
        {
            RuleFor(o => o.Formacao)
                .NotEmpty().WithMessage("O campo Formação deve ser preenchido")
                .MaximumLength(MAX_LENGHT_FORMACAO).WithMessage($"O campo Formação deve possuir no máximo {MAX_LENGHT_FORMACAO} caracteres");

            RuleFor(o => o.ExperienciaProfissional)
                .NotEmpty().WithMessage("O campo Experiência Profissional deve ser preenchido")
                .MaximumLength(MAX_LENGHT_EXPERIENCIAPROFISSIONAL).WithMessage($"O campo Experiência Profissional deve possuir no máximo {MAX_LENGHT_EXPERIENCIAPROFISSIONAL} caracteres");

            RuleFor(o => o.TempoTrabalho)
                .NotEmpty().WithMessage("O campo Tempo de trabalho deve ser preenchido")
                .MaximumLength(MAX_LENGHT_TEMPOTRABALHO).WithMessage($"O campo Tempo de Trabalho deve possuir no máximo {MAX_LENGHT_TEMPOTRABALHO} caracteres");

            RuleFor(o => o.AtividadesExercidas)
                .NotEmpty().WithMessage("O campo Atividades Exercidas deve ser preenchido")
                .MaximumLength(MAX_LENGHT_ATIVIDADESEXERCIDAS).WithMessage($"O campo Atividades Exercidas deve possuir no máximo {MAX_LENGHT_ATIVIDADESEXERCIDAS} caracteres");
        }
    }
}