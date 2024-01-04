using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleCrud.API.Controllers;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Services;

namespace SampleCrud.API.Tests.Controllers;

public class PersonControllerTests
{
    private readonly Mock<ILogger<PersonController>> _logger;
    private readonly Mock<IPersonService> _mockPersonService;

    public PersonControllerTests()
    {
        _mockPersonService = new Mock<IPersonService>();
        _logger = new Mock<ILogger<PersonController>>();
    }

    [Fact]
    public void Get_ReturnsOk_For_Get_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.GetPersons()).ReturnsAsync(
            new List<Person>
            {
                new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Teste",
                    Birthday = DateTime.Now,
                    Email = ""
                }
            });
        
        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Get().Result;

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void Get_ReturnsNotFound_For_Get_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.GetPersons()).ReturnsAsync(
            new List<Person>());
        
        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Get().Result;

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void GetById_ReturnsOk_For_Get_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.GetById(It.IsAny<Guid>())).ReturnsAsync(
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                Birthday = DateTime.Now,
                Email = ""
            });
        
        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Get(Guid.NewGuid()).Result;

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetById_ReturnsNotFound_For_Get_Person()
    {
        // Arrange
        var mockService = new Mock<IPersonService>();
        mockService.Setup(service => service.GetById(It.IsAny<Guid>())).ReturnsAsync(
            null as Person);

        var controller = new PersonController(_logger.Object, mockService.Object);

        // Act
        var result = controller.Get(Guid.NewGuid()).Result;

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void Post_ReturnsCreated_For_Add_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.Add(It.IsAny<Person>()));
        
        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Post(new Person()).Result;

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void Put_ReturnsOk_For_Update_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.Update(It.IsAny<Person>()));
        
        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Put(new Person());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsOk_For_Delete_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.Remove(It.IsAny<Guid>()));
        _mockPersonService.Setup(service => service.GetById(It.IsAny<Guid>())).ReturnsAsync(
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                Birthday = DateTime.Now,
                Email = ""
            });

        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Delete(Guid.NewGuid()).Result;

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNotFound_For_Delete_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.Remove(It.IsAny<Guid>()));
        _mockPersonService.Setup(service => service.GetById(It.IsAny<Guid>())).ReturnsAsync(
            null as Person);
        
        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Delete(Guid.NewGuid()).Result;

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsBadRequest_For_Delete_Person()
    {
        // Arrange
        _mockPersonService.Setup(service => service.Remove(It.IsAny<Guid>()));
        _mockPersonService.Setup(service => service.GetById(It.IsAny<Guid>())).ReturnsAsync(
            null as Person);

        var controller = new PersonController(_logger.Object, _mockPersonService.Object);

        // Act
        var result = controller.Delete(Guid.Empty).Result;

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}