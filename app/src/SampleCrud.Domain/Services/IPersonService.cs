using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Services
{
    /// <summary>
    /// Represents a service for managing persons.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves all persons.
        /// </summary>
        /// <returns>An asynchronous operation that returns a collection of persons.</returns>
        Task<IEnumerable<Person>> GetPersons();

        /// <summary>
        /// Retrieves a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>An asynchronous operation that returns the person with the specified ID, or null if not found.</returns>
        Task<Person?> GetById(Guid id);

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
        /// Removes a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to remove.</param>
        void Remove(Guid id);
    }
}