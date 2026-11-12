using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICharactersRepository
    {
        Task<Guid> Create(Character character, string ownerId);
        Task<Guid> Delete(Guid id, string ownerId);
        Task<List<Character>> Get(string userId);
        Task<Character> GetById(Guid id, string userId);
        Task<Guid> Update(Guid id, string ownerId, string name, string description, string race);
    }
}