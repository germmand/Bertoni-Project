using Bertoni.Web.Controllers;
using Bertoni.Core.Services;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Bertoni.Tests.Controllers {
    public class HomeControllerTests {
        public readonly HomeController _homeController;
        public readonly Mock<ITypicodeService> _typicodeServiceMock;
        public readonly Mock<IMapper> _mapperMock;
        public readonly Mock<ILogger<HomeController>> _loggerMock;

        public HomeControllerTests()
        {
            _typicodeServiceMock = new Mock<ITypicodeService>();
            _mapperMock          = new Mock<IMapper>();
            _loggerMock          = new Mock<ILogger<HomeController>>();
            _homeController = new HomeController(
                _loggerMock.Object, 
                _typicodeServiceMock.Object, 
                _mapperMock.Object
            );
        }
    }
}