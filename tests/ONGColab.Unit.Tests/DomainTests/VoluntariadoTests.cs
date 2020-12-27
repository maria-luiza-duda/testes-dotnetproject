using FluentAssertions;
using Xunit;
using ONGColab.Tests.Common.Fixtures;

namespace ONGColab.Unit.Tests.DomainTests
{
    [Collection(nameof(DoacaoFixtureCollection))]    
    public class VoluntariadoTests: IClassFixture<VoluntariadoFixture>, 
                              IClassFixture<ExperienciaFixture>
    {
        private readonly VoluntariadoFixture _voluntariadoFixture;
        private readonly ExperienciaFixture _experienciaFixture;

        public VoluntariadoTests(VoluntariadoFixture voluntariadoFixture, ExperienciaFixture experienciaFixture)
        {
            _voluntariadoFixture = voluntariadoFixture;
            _experienciaFixture = experienciaFixture;
        }

        [Fact]
        [Trait("Voluntariado", "Voluntariado_CorretamentePreenchido_VoluntariadoValido")]
        public void Voluntariado_CorretamentePreenchido_VoluntariadoValido()
        {           
            // Arrange
            var voluntariado = _voluntariadoFixture.VoluntariadoValido();
            voluntariado.AdicionarExperiencia(_experienciaFixture.ExperienciaValida());

            // Act
            var valido = voluntariado.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            voluntariado.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Voluntariado", "Voluntariado_DadosPessoaisInvalidos_VoluntariadoInvalido")]
        public void Voluntariado_DadosPessoaisInvalidos_VoluntariadoInvalido()
        {
            // Arrange
            const bool EMAIL_INVALIDO = true;
            var voluntariado = _doacaoFixture.VoluntariadoValido(EMAIL_INVALIDO);
            voluntariado.AdicionarExperiencia(_experienciaFixture.ExperienciaValida());

            // Act
            var valido = voluntariado.Valido();

            // Assert
            valido.Should().BeFalse(because: "o campo email está inválido");
            voluntariado.ErrorMessages.Should().Contain("O campo Email é inválido.");
            voluntariado.ErrorMessages.Should().HaveCount(1, because: "somente o campo email está inválido.");
        }

        [Fact]
        [Trait("Voluntariado", "Voluntariado_HabilidadesVagaMaxlenghtExecido_VoluntariadoInvalido")]
        public void Voluntariado_HabilidadesVagaMaxlenghtExecido_VoluntariadoInvalido()
        {
            // Arrange
            const bool EXCEDER_MAX_LENTH_HABILIDADESVAGA = true;
            var voluntariado = _voluntariadoFixture.VoluntariadoValido(false, null, EXCEDER_MAX_LENTH_HABILIDADESVAGA);
            voluntariado.AdicionarExperiencia(_experienciaFixture.ExperienciaValida());

            // Act
            var valido = voluntariado.Valido();

            // Assert
            valido.Should().BeFalse(because: "O campo Habilidades para a vaga possui mais caracteres do que o permitido");
            voluntariado.ErrorMessages.Should().HaveCount(1, because: "somente o campo Habilidades para a vaga está inválido.");
            voluntariado.ErrorMessages.Should().Contain("O campo Habilidades para a vaga deve possuir no máximo 500 caracteres.");
        }
    }
}
