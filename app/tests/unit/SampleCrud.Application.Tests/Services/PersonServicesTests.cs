using Microsoft.Extensions.Logging;
using Moq;
using SampleCrud.Application.Services;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;

namespace SampleCrud.Application.Tests;

public class PersonServicesTests
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly Mock<ILogger<PersonService>> _mockLogger;

    public PersonServicesTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _mockLogger = new Mock<ILogger<PersonService>>();
    }

    [Fact]
    public void Should_Be_True_If_Add_Person_In_Repository()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Nickname = "Teste 1",
            Email = "unitemail@unit.com",
            Birthday = DateTime.Now,
            Stack = new List<string>()
        };
        _mockPersonRepository.Setup(x => x.Add(person));
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Act
        personServices.Add(person);

        // Assert
        _mockPersonRepository.Verify(x => x.Add(person), Times.Once);
    }

    [Fact]
    public void Should_Be_True_If_Update_Person_In_Repository()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        _mockPersonRepository.Setup(x => x.Update(person));
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Act
        personServices.Update(person);

        // Assert
        _mockPersonRepository.Verify(x => x.Update(person), Times.Once);
    }

    [Fact]
    public void Should_Be_True_If_Delete_Person_In_Repository()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        _mockPersonRepository.Setup(x => x.Remove(person));
        _mockPersonRepository.Setup(x => x.GetById(person.Id)).ReturnsAsync(person);
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Act
        personServices.Remove(person.Id);

        // Assert
        _mockPersonRepository.Verify(x => x.Remove(person), Times.Once);
    }

    [Fact]
    public async Task Should_Be_True_If_Get_Person_By_Id_In_Repository()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        _mockPersonRepository.Setup(x => x.GetById(person.Id)).ReturnsAsync(person);
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Act
        var result = await personServices.GetById(person.Id);

        // Assert
        Assert.Equal(person.Id, result?.Id);
    }

    [Fact]
    public async Task Should_Be_True_If_Get_Persons_In_Repository()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        var persons = new List<Person>
        {
            person
        };
        _mockPersonRepository.Setup(x => x.GetPersons()).ReturnsAsync(persons);
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Act
        var result = await personServices.GetPersons();

        // Assert
        Assert.Equal(persons, result);
    }

    [Fact]
    public void Should_Be_True_If_Get_Persons_Throws_Exception()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.GetPersons()).Throws(new Exception());

        // Act
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Assert
        Assert.ThrowsAsync<Exception>(() => personServices.GetPersons());

    }

    [Fact]
    public void Should_Be_True_If_Get_Person_By_Id_Throws_Exception()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Throws(new Exception());

        // Act
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Assert
        Assert.ThrowsAsync<Exception>(() => personServices.GetById(It.IsAny<Guid>()));
    }

    [Fact]
    public void Should_Be_True_If_Remove_Person_Throws_Exception()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Throws(new Exception());

        // Act
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Assert
        Assert.ThrowsAsync<Exception>(() => personServices.GetById(It.IsAny<Guid>()));
    }

    [Fact]
    public void Should_Be_True_If_Update_Person_Throws_Exception()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.Update(It.IsAny<Person>())).Throws(new Exception());

        // Act
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        // Assert
        Assert.Throws<Exception>(() => personServices.Update(person));
    }

    [Fact]
    public void Should_Be_True_If_Add_Person_Throws_Exception()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.Add(It.IsAny<Person>())).Throws(new Exception());

        // Act
        var personServices = new PersonService(_mockLogger.Object, _mockPersonRepository.Object);

        // Assert
        Assert.Throws<Exception>(() => personServices.Add(It.IsAny<Person>()));
    }
}