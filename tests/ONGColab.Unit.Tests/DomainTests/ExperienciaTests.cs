using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ONGColab.Tests.Common.Fixtures;
namespace ONGColab.Unit.Tests.DomainTests
{
    [Collection(nameof(EnderecoFixtureCollection))]
    public class ExperienciaTests: IClassFixture<ExperienciaFixture>
    {
        private readonly ExperienciaFixture _fixture;

        public ExperienciaTests(ExperienciaFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Experiencia", "Experiencia_CorretamentePreenchida_ExperienciaValida")]
        public void Experiencia_CorretamentePreenchida_ExperienciaValida()
        {
            // Arrange
            var experiencia = _fixture.ExperienciaValida();

            // Act
            var valido = experiencia.Valida();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            experiencia.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Experiencia", "Experiencia_NenhumDadoPreenchido_ExperienciaInvalida")]
        public void Experiencia_NenhumDadoPreenchido_ExperienciaInvalida()
        {
            // Arrange
            var experiencia = _fixture.ExperienciaVazia();

            // Act
            var valido = experiencia.Valida();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de preenchimento");
            experiencia.ErrorMessages.Should().HaveCount(6, because: "nenhum dos 4 campos obrigatórios foi informado ou estão incorretos.");

            experiencia.ErrorMessages.Should().Contain("O campo Experiências profissionais deve ser preenchido", because: "o campo Experiências profissionais é obrigatório e não foi preenchido.");
            experiencia.ErrorMessages.Should().Contain("O campo Atividades exercidas deve ser preenchido", because: "o campo Atividades exercidas é obrigatório e não foi preenchido.");
            experiencia.ErrorMessages.Should().Contain("O campo Tempo de experiência deve ser preenchido", because: "o campo Tempo de experiência é obrigatório e não foi preenchido.");
            experiencia.ErrorMessages.Should().Contain("O campo Formacao deve ser preenchido", because: "o campo Formacao é obrigatório e não foi preenchido.");
        }
    }
}
