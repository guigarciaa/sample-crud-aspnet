using Moq;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;

namespace SampleCrud.Data.Tests.Repositories;

public class PersonRepositoryTests
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;

    public PersonRepositoryTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
    }

    [Fact]
    public async Task Should_Be_True_With_Returned_An_List_Of_Persons()
    {
        _mockPersonRepository.Setup(x => x.GetPersons()).ReturnsAsync(new List<Person>()
        {
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Teste 1",
                Email = ""
            },
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Teste 2",
                Email = ""
            },
        });

        var persons = await _mockPersonRepository.Object.GetPersons();

        Assert.NotNull(persons);
        Assert.True(persons.Any());
    }

    [Fact]
    public async Task Should_Be_True_With_Returned_An_Person_By_Id()
    {
        var id = Guid.NewGuid();

        _mockPersonRepository.Setup(x => x.GetById(id)).ReturnsAsync(new Person
        {
            Id = id,
            Name = "Teste 1",
            Email = ""
        });

        var person = await _mockPersonRepository.Object.GetById(id);

        Assert.NotNull(person);
        Assert.Equal(id, person.Id);
    }
}