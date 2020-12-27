using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NToastNotify;
using ONGColab.Domain;
using ONGColab.Domain.Entities;
using ONGColab.Domain.ViewModels;
using ONGColab.MVC.Controllers;
using ONGColab.Service;
using ONGColab.Tests.Common.Fixtures;
using Xunit;

namespace ONGColab.Unit.Tests.ControllerTests
{
    [Collection(nameof(VoluntariadoFixtureCollection))]
    public class VoluntariadoControllerTests : IClassFixture<VoluntariadoFixture>,
                                        IClassFixture<ExperienciaFixture>,
    {
        private readonly Mock<IVoluntariadoRepository> _voluntariadoRepository = new Mock<IVoluntariadoRepository>();        
        private readonly Mock<GloballAppConfig> _globallAppConfig = new Mock<GloballAppConfig>();

        private readonly VoluntariadoFixture _voluntariadoFixture;
        private readonly ExperienciaFixture _experienciaFixture;

        private VoluntariadoController _voluntariadoController;
        private readonly IVoluntariadoService _voluntariadoService;

        private Mock<IMapper> _mapper;
        private Mock<ILogger<VoluntariadoController>> _logger = new Mock<ILogger<VoluntariadoController>>();

        private IDomainNotificationService _domainNotificationService = new DomainNotificationService();

        private Mock<IToastNotification> _toastNotification = new Mock<IToastNotification>();

        private readonly Voluntariado _voluntariadoValida;
        private readonly VoluntariadoViewModel _voluntariadoModelValida;

        public VoluntariadoControllerTests(VoluntariadoFixture voluntariadoFixture, ExperienciaFixture experienciaFixture)
        {
            _voluntariadoFixture = voluntariadoFixture;
            _experienciaFixture = experienciaFixture;

            _mapper = new Mock<IMapper>();

            _voluntariadoValido = voluntariadoFixture.VoluntariadoValido();
            _voluntariadoValido.AdicionarExperiencia(experienciaFixture.ExperienciaValido());

            _voluntariadoModelValido = voluntariadoFixture.VoluntariadoModelValido();
            _voluntariadoModelValido.Experiencia = experienciaFixture.ExperienciaModelValida();

            _mapper.Setup(a => a.Map<VoluntariadoViewModel, Voluntariado>(_voluntariadoModelValida)).Returns(_voluntariadoValida);

            _voluntariadoService = new VoluntariadoService(_mapper.Object, _voluntariadoRepository.Object, _domainNotificationService);
        }

        #region HTTPPOST

        [Trait("VoluntariadoController", "VoluntariadoController_Adicionar_RetornaDadosComSucesso")]
        [Fact]
        public void VoluntariadoController_Adicionar_RetornaDadosComSucesso()
        {
            // Arrange            
            _voluntariadoController = new VoluntariadoController(_voluntariadoService, _domainNotificationService, _toastNotification.Object);

            // Act
            var retorno = _voluntariadoController.Create(_voluntariadoModelValida);

            _mapper.Verify(a => a.Map<VoluntariadoViewModel, Doacao>(_voluntariadoModelValida), Times.Once);
            _toastNotification.Verify(a => a.AddSuccessToastMessage(It.IsAny<string>(), It.IsAny<LibraryOptions>()), Times.Once);

            retorno.Should().BeOfType<RedirectToActionResult>();

            ((RedirectToActionResult)retorno).ActionName.Should().Be("Index");
            ((RedirectToActionResult)retorno).ControllerName.Should().Be("Home");
        }

        [Trait("VoluntariadoController", "VoluntariadoController_AdicionarDadosInvalidos_BadRequest")]
        [Fact]
        public void VoluntariadoController_AdicionarDadosInvalidos_BadRequest()
        {
            // Arrange          
            var voluntariado = _doacaoFixture.VoluntariadoInvalido();
            var voluntariadoModelInvalido = new VoluntariadoViewModel();
            _mapper.Setup(a => a.Map<VoluntariadoViewModel, Voluntariado>(voluntariadoModelInvalida)).Returns(voluntariado);

            _voluntariadoController = new VoluntariadoController(_voluntariadoService, _domainNotificationService, _toastNotification.Object);

            // Act
            var retorno = _voluntariadoController.Create(voluntariadoModelInvalido);

            // Assert                   
            retorno.Should().BeOfType<ViewResult>();

            _mapper.Verify(a => a.Map<VoluntariadoViewModel, Voluntariado>(voluntariadoModelInvalido), Times.Once);
            _voluntariadoRepository.Verify(a => a.AdicionarAsync(doacao), Times.Never);
            _toastNotification.Verify(a => a.AddErrorToastMessage(It.IsAny<string>(), It.IsAny<LibraryOptions>()), Times.Once);

            var viewResult = ((ViewResult)retorno);

            viewResult.Model.Should().BeOfType<VoluntariadoViewModel>();

            ((VoluntariadoViewModel)viewResult.Model).Should().Be(voluntaraidoModelInvalido);
        }

        #endregion
    }
}

