using FluentAssertions;
using Xunit;
using ONGColab.Domain;
using ONGColab.Domain.Entities;
using System.Linq;
using ONGColab.Tests.Common.Fixtures;

namespace ONGColab.Unit.Tests.DomainTests
{
    [Collection(nameof(VoluntarixFixtureCollection))]
    public class DomainNotificationServiceTests: IClassFixture<VoluntarixFixture>
    {
        private readonly VoluntarixFixture _voluntarixFixture;
        private readonly IDomainNotificationService _domainNotificationService;

        public DomainNotificationServiceTests(VoluntarixFixture fixture)
        {
            _voluntarixFixture = fixture;
            _domainNotificationService = new DomainNotificationService();
        }

        [Trait("DomainNotificationService", "DomainNotificationService_NovaClasse_NaoDevePossuirNotificacoes")]
        [Fact]
        public void DomainNotificationService_NovaClasse_NaoDevePossuirNotificacoes()
        {
            // Arrange & Act
            var domainNotification = new DomainNotificationService();

            // Assert
            domainNotification.PossuiErros.Should().BeFalse(because:"ainda não foi adicionado nenhuma notificação de dominino");
        }
        
        [Trait("DomainNotificationService", "DomainNotificationService_AdicionarNotificacao_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AdicionarNotificacao_HasNotificationsTrue()
        {
            // Arrange
            var domainNotification = new DomainNotification("RequiredField", "O campo Nome é obrigatório");

            // Act
            _domainNotificationService.Adicionar(domainNotification);

            // Assert            
            _domainNotificationService.PossuiErros.Should().BeTrue(because: "foi adicionado a notificacao de codigo RequiredField");

            var notifications = _domainNotificationService.RecuperarErrosDominio().Select(a => a.MensagemErro);
            notifications.Should().Contain("O campo Nome é obrigatório", because: "foi adicionado a notificacao de codigo RequiredField");
        }

        [Trait("DomainNotificationService", "DomainNotificationService_AdicionarEntidade_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AdicionarEntidade_HasNotificationsTrue()
        {
            // Arrange
            var voluntarix = _pessoaFixture.VoluntarixVazio();
            voluntarix.Valido();

            // Act
            _domainNotificationService.Adicionar(voluntarix);

            // Assert
            var notifications = _domainNotificationService.RecuperarErrosDominio().Select(a => a.HabilidadesVaga);

            notifications.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado.");
            notifications.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            notifications.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");

            _domainNotificationService.PossuiErros.Should().BeTrue(because: "foi adicionado a entidade voluntário inválida");
        }
    }
}