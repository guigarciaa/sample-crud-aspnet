using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Repositories
{
    /// <summary>
    /// Represents a repository for managing persons.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Retrieves all persons.
        /// </summary>
        /// <returns>An asynchronous operation that represents the retrieval of all persons.</returns>
        Task<IEnumerable<Person>> GetPersons();

        /// <summary>
        /// Retrieves a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>An asynchronous operation that represents the retrieval of the person. Returns null if the person is not found.</returns>
        Task<Person?> GetById(Guid? id);

        /// <summary>
        /// Adds a new person.
        /// </summary>
        /// <param name="person">The person to add.</param>
        void Add(Person person);

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="person">The person to update.</param>
        void Update(Person person);

        /// <summary>
        /// Removes a person.
        /// </summary>
        /// <param name="person">The person to remove.</param>
        void Remove(Person person);
    }
}