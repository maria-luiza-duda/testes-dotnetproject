using FluentAssertions;
using System.Threading.Tasks;
using ONGColab.Domain.Extensions;
using ONGColab.Integration.Tests.Fixtures;
using ONGColab.MVC;
using Xunit;

namespace ONGColab.Integration.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class HomeTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _integrationTestsFixture;

        public HomeTests(IntegrationTestsFixture<StartupWebTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }

        [Trait("HomeControllerIntegrationTests", "HomeController_CarregarPaginaInicial_TotalVoluntarixsDeveSerZero")]
        [Fact]
        public async Task HomeController_CarregarPaginaInicial_TotalVoluntarixsDeveSerZero()
        {
            // Arrange & Act
            var home = await _integrationTestsFixture.Client.GetAsync("Home");

            // Assert
            home.EnsureSuccessStatusCode();
            var dadosHome = await home.Content.ReadAsStringAsync();
            
            dadosHome.Should().Contain(expected: "Quntos voluntarixs inscritos?");
            dadosHome.Should().Contain(expected: QuantidadeVoluntarixs);
        }
    }
}