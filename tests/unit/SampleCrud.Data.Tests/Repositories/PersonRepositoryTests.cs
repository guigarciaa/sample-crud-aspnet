using Microsoft.EntityFrameworkCore;
using Moq;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Entities;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Data.Tests.Repositories;

public class PersonRepositoryTests
{
    private readonly Mock<ApplicationContext> _mockApplicationContext;

    public PersonRepositoryTests()
    {
        _mockApplicationContext = new Mock<ApplicationContext>();
    }

    [Fact]
    public void Should_Be_True_If_Add_a_Person_in_DBSet_Person()
    {
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<ApplicationContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        var service = new PersonRepository(mockContext.Object);
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = ""
        };
        service.Add(person);

        mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void Should_Be_True_If_Update_a_Person_in_DBSet_Person() {
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<ApplicationContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        var service = new PersonRepository(mockContext.Object);
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
    public void Should_Be_True_If_Remove_a_Person_in_DBSet_Person()
    {
        var mockSet = new Mock<DbSet<Person>>();

        var mockContext = new Mock<ApplicationContext>();
        mockContext.Setup(m => m.Person).Returns(mockSet.Object);

        var service = new PersonRepository(mockContext.Object);
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
}