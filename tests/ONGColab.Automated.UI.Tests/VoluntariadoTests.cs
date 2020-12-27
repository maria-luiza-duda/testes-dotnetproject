using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using ONGColab.Tests.Common.Fixtures;
using Xunit;

namespace ONGColab.AutomatedUITests
{
	public class VoluntariadoTests : IDisposable, IClassFixture<VoluntariadoFixture>, 
                                               IClassFixture<ExperienciaFixture>, 
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly VoluntariadoFixture _voluntariadoFixture;
		private readonly ExperienciaFixture _experienciaFixture;

		public VoluntariadoTests(VoluntariadoFixture voluntariadoFixture, ExperienciaFixture experienciaFixture)
        {
            _voluntariadoFixture = voluntariadoFixture;
            _experienciaFixture = experienciaFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void VoluntariadoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://ONGColab.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("ongcolab-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void VoluntariadoUI_CriacaoDoacao()
		{
			//Arrange
			var voluntariado = _voluntariadoFixture.VoluntariadoValida();
            voluntariado.AdicionarExperienciaCobranca(_experienciaFixture.ExperienciaValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Voluntariado/Create");
		}
	}
}