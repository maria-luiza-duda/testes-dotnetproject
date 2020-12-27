using FluentAssertions;
using Xunit;
using ONGColab.Tests.Common.Fixtures;

namespace ONGColab.Unit.Tests.DomainTests
{
    [Collection(nameof(VoluntarixFixtureCollection))]
    public class VoluntarixTests : IClassFixture<VoluntarixFixture>
    {
        private readonly VoluntarixFixture _fixture;

        public VoluntarixTests(VoluntarixFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Voluntarix", "Voluntarix_CorretamentePreenchidos_VoluntarixValido")]
        public void Voluntarix_CorretamentePreenchidos_VoluntarixValido()
        {
            // Arrange
            var voluntarix = _fixture.VoluntarixValido();
            
            // Act
            var valido = voluntarix.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            voluntarix.ErrorMessages.Should().BeEmpty();            
        }

        [Fact]
        [Trait("Voluntarix", "Voluntarix_NenhumDadoPreenchido_VoluntarixInvalido")]
        public void Voluntarix_NenhumDadoPreenchido_VoluntarixInvalido()
        {
            // Arrange
            var voluntarix = _fixture.VoluntarixVazia();

            // Act
            var valido = voluntarix.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");

            voluntarix.ErrorMessages.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado.");

            voluntarix.ErrorMessages.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            voluntarix.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");
        }

        [Fact]
        [Trait("Voluntarix", "Voluntarix_EmailInvalido_VoluntarixInvalido")]
        public void Voluntarix_EmailInvalido_VoluntarixInvalido()
        {
            // Arrange
            const bool EMAIL_INVALIDO = true;
            var voluntarix = _fixture.VoluntarixValido(EMAIL_INVALIDO);

            // Act
            var valido = voluntarix.Valido();

            // Assert
            valido.Should().BeFalse(because: "o campo email está inválido");
            voluntarix.ErrorMessages.Should().HaveCount(1, because: "somente o campo email está inválido.");

            voluntarix.ErrorMessages.Should().Contain("O campo Email é inválido.");
        }

        [Fact]
        [Trait("Voluntarix", "Voluntarix_CamposMaxLenghtExcedidos_VoluntarixInvalido")]
        public void Voluntarix_CamposMaxLenghtExcedidos_VoluntarixInvalido()
        {
            // Arrange            
            var voluntarix = _fixture.VoluntarixMaxLenth();

            // Act
            var valido = voluntarix.Valido();

            // Assert
            valido.Should().BeFalse(because: "os campos nome e email possuem mais caracteres do que o permitido.");
            voluntarix.ErrorMessages.Should().HaveCount(2, because: "os dados estão inválidos.");

            voluntarix.ErrorMessages.Should().Contain("O campo Nome deve possuir no máximo 150 caracteres.");
            voluntarix.ErrorMessages.Should().Contain("O campo Email deve possuir no máximo 150 caracteres.");
        }
    }
}