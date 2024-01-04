using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Tests.Entities
{
    public class PersonTests
    {
        [Fact]
        public void Should_Success_If_Person_Created()
        {
            // Arrange
            var nickname = "somename1";
            var name = "somename";
            var email = "unitemail@unit.com";
            var birthday = new DateTime();
            var stack = new List<string>();

            // Act
            var act = new Person(nickname, name, email, birthday, stack);
            var result = act.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_Fail_If_Trying_Create_Person_Without_Name()
        {
            // Arrange
            var nickname = "teste";
            var name = "";
            var email = "unitemail@unit.com";
            var birthday = new DateTime();
            var stack = new List<string>();

            // Act
            var act = () => new Person(nickname, name, email, birthday, stack);
            var ex = Assert.Throws<AggregateException>(() => act.Invoke());

            // Assert
            Assert.Equal("One or more errors occurred. (Name is required) (Name must be between 3 and 100 characters)", ex.Message);
        }

        [Fact]
        public void Should_Fail_If_Trying_Create_Person_With_Name_Less_Than_Three_Characters()
        {
            // Arrange
            var nickname = "teste";
            var name = "te";
            var email = "unitemail@unit.com";
            var birthday = new DateTime();
            var stack = new List<string>();

            // Act
            var act = () => new Person(nickname, name, email, birthday, stack);
            var ex = Assert.Throws<AggregateException>(() => act.Invoke());

            // Assert
            Assert.Equal("One or more errors occurred. (Name must be between 3 and 100 characters)", ex.Message);
        }

        [Fact]
        public void Should_Fail_If_Trying_Create_Person_With_Name_Greater_Than_OneHundred_Characters()
        {
            // Arrange
            var nickname = "teste";
            var name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod, nisl eget ultricies ultricies, nisl nisl ultricies elit, eget ultricies elit elit eget elit.";
            var email = "unitemail@unit.com";
            var birthday = new DateTime();
            var stack = new List<string>();

            // Act
            var act = () => new Person(nickname, name, email, birthday, stack);
            var ex = Assert.Throws<AggregateException>(() => act.Invoke());

            // Assert
            Assert.Equal("One or more errors occurred. (Name must be between 3 and 100 characters)", ex.Message);
        }

        [Fact]
        public void Should_Fail_If_Trying_Create_Person_With_Stack_Grater_Than_ThrityTwo_Characters()
        {
            // Arrange
            var nickname = "somename1";
            var name = "somename";
            var email = "unitemail@unit.com";
            var birthday = new DateTime();
            var stack = new List<string>() { "Lorem ipsum dolor sit amet, consectetur adipiscing elit." };

            // Act
            var act = () => new Person(nickname, name, email, birthday, stack);
            var ex = Assert.Throws<AggregateException>(() => act.Invoke());

            // Assert
            Assert.Equal("One or more errors occurred. (Stack must be max 32 characters)", ex.Message);
        }
    }
}