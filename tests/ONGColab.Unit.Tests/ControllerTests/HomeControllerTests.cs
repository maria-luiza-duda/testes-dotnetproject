using Microsoft.Extensions.Logging;
using Moq;
using ONGColab.MVC.Controllers;
using ONGColab.Domain;

namespace ONGColab.Unit.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly IHomeInfoService _homeInfoService;
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {

        }
    }
}
