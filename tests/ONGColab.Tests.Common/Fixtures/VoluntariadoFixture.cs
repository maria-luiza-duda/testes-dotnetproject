using Bogus;
using Bogus.DataSets;
using System;
using Xunit;
using ONGColab.Domain.Entities;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(VoluntariadoFixtureCollection))]
    public class VoluntariadoFixtureCollection : ICollectionFixture<VoluntariadoFixture>, ICollectionFixture<ExperienciaFixture>
    {
    }

    public class VoluntariadoFixture
    {
        public VoluntariadoViewModel VoluntariadoModelValida()
        {
            var faker = new Faker<VoluntariadoViewModel>("pt_BR");

            const int MIN_VALUE = 1;
            const int MAX_VALUE = 500;
            const int DECIMALS = 2;

            faker.RuleFor(c => c.Valor, (f, c) => f.Finance.Amount(MIN_VALUE, MAX_VALUE, DECIMALS));
            
            var retorno = faker.Generate();

            retorno.DadosPessoais = VoluntarixModelValida();

            return retorno;
        }

        public Voluntariaod VoluntariadoValida(bool emailInvalido = false, double? valor = 5, bool maxLenghField = false)
        {            
            var faker = new Faker<Voluntariado>("pt_BR");

            const int MIN_VALUE = 1;
            const int MAX_VALUE = 500;
            const int DECIMALS = 2;

            faker.CustomInstantiator(f => new Voluntariado(Guid.Empty, Guid.Empty, Guid.Empty, valor ?? (double)f.Finance.Amount(MIN_VALUE, MAX_VALUE, DECIMALS), 
                                                        VoluntarixValida(emailInvalido, maxLenghField), null, null));

            return faker.Generate();
        }

        public VoluntariadoViewModel VoluntariadoModelInvalidaValida()
        {
            return new VoluntariadoViewModel();
        }

        public Voluntariado VoluntariadoInvalida(bool voluntariadoAnonima = false)
        {
            var voluntarixInvalida = new Voluntarix(Guid.Empty, string.Empty, string.Empty, voluntariadoCandidatura, string.Empty);
            return new Voluntariado(Guid.Empty, Guid.Empty, Guid.Empty, 0, voluntarixInvalida, null, null );
        }

        public Voluntarix VoluntarixValida(bool emailInvalido = false,bool maxLenghField = false)
        {            
            var voluntarix = new Faker().Person;

            var faker = new Faker<Voluntarix>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Voluntarix(Guid.NewGuid(), voluntarix.FullName, string.Empty, false, maxLenghField ? f.Lorem.Sentence(501) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate();
        }

        public VoluntarixViewModel VoluntarixModelValida(bool emailInvalido = false)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<VoluntarixViewModel>("pt_BR");

            faker.RuleFor(a => a.Nome, (f, c) => f.Name.FirstName(genero));
            faker.RuleFor(a => a.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate();
        }
    }
}