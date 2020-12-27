using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ONGColab.Domain.Entities;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(ExperienciaFixtureCollection))]
    public class ExperienciaFixtureCollection : ICollectionFixture<ExperienciaFixture>
    {
    }
    public class ExperienciaFixture
    {
        public ExperienciaViewModel ExperienciaModelValido()
        {
            var experiencia = new Faker().Experience;

            var faker = new Faker<ExperienciaViewModel>("pt_BR");

            faker.RuleFor(c => c.AtividadesExercidas, (f, c) => experiencia.Activities());
            faker.RuleFor(c => c.TempoTrabalho, (f, c) => experiencia.Years());
            faker.RuleFor(c => c.ExperienciaProfissional, (f, c) => experiencia.Knowhow());
            faker.RuleFor(c => c.Formacao, (f, c) => experiencia.Graduate());            

            return faker.Generate();
        }

        public Experiencia ExperienciaValida()
        {
            var experiencia = new Faker("pt_BR").Experience;
            
            var faker = new Faker<Experiencia>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Experiencia(Guid.NewGuid(), experiencia.Activities(), string.Empty, experiencia.Years(), experiencia.Knowhow(), experiencia.Graduate()));

            return faker.Generate();
        }

        public Experiencia ExperienciaVazia()
        {
            return new Experiencia(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}
