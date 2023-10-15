using Moq;
using SampleCrud.Application.Services;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;

namespace SampleCrud.Application.Tests;

public class PersonServicesTests
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;

    public PersonServicesTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
    }

    [Fact]
    public void Test1()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };

        _mockPersonRepository.Setup(x => x.Add(person));

        var personServices = new PersonServices(_mockPersonRepository.Object);

        // Act
        personServices.Add(person);

        // Assert
        _mockPersonRepository.Verify(x => x.Add(person), Times.Once);
    }
}