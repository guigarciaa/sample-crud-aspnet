using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Entities;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Data.Tests.Repositories;

public class PersonRepositoryTests
{
    private readonly Mock<SampleCrudDbContext> _mockSampleCrudDbContext;
    private readonly Mock<ILogger<PersonRepository>> _mockLogger;

    public PersonRepositoryTests()
    {
        _mockSampleCrudDbContext = new Mock<SampleCrudDbContext>();
        _mockLogger = new Mock<ILogger<PersonRepository>>();
    }

    [Fact]
    public void Should_Be_True_If_Get_All_Persons()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var persons = service.GetPersons();

        // Assert   
        Assert.NotNull(persons);
    }

    [Fact]
    public void Should_Be_Throw_If_Try_Get_All_Persons()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);
        // mockContext.Setup(m => m.Person.ToListAsync(It.IsAny<CancellationToken>())).Throws(new Exception());

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);

        // Assert
        Assert.ThrowsAsync<Exception>(() => service.GetPersons());
    }

    [Fact]
    public async void Should_Be_True_If_Get_Person_By_Id()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);
        mockContext.Setup(m => m.Person.FindAsync(typeof(Guid), It.IsAny<Guid>())).ReturnsAsync(new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        });

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = await service.GetById(Guid.NewGuid());

        // Assert
        Assert.NotNull(person);
    }

    [Fact]
    public void Should_Be_Throw_If_Try_Get_Person_By_Id()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);
        mockContext.Setup(m => m.Person.FindAsync(typeof(Guid), It.IsAny<Guid>())).Throws(new Exception());

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        
        // Assert
        Assert.ThrowsAsync<Exception>(() => service.GetById(Guid.NewGuid()));
    }

    [Fact]
    public void Should_Be_True_If_Add_a_Person_in_DBSet_Person()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        service.Add(person);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void Should_Be_Throw_If_Try_Add_a_Person_in_DBSet_Person()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);
        mockContext.Setup(m => m.SaveChanges()).Throws(new Exception());

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };

        // Assert
        Assert.Throws<Exception>(() => service.Add(person));
    }

    [Fact]
    public void Should_Be_True_If_Update_a_Person_in_DBSet_Person() {
        // Arrange
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        // Act
        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        service.Update(person);

        mockSet.Verify(m => m.Update(It.IsAny<Person>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void Should_Be_Throw_If_Try_Update_a_Person_in_DBSet_Person()
    {
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);
        mockContext.Setup(m => m.SaveChanges()).Throws(new Exception());

        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };

        Assert.Throws<Exception>(() => service.Update(person));
    }

    [Fact]
    public void Should_Be_True_If_Remove_a_Person_in_DBSet_Person()
    {
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        service.Remove(person);

        mockSet.Verify(m => m.Remove(It.IsAny<Person>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void Should_Be_Throw_If_Try_Remove_a_Person_in_DBSet_Person()
    {
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<SampleCrudDbContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);
        mockContext.Setup(m => m.SaveChanges()).Throws(new Exception());

        var service = new PersonRepository(_mockLogger.Object, mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };

        Assert.Throws<Exception>(() => service.Remove(person));
    }
}