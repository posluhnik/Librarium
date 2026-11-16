using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICharactersRepository
    {
        Task<Character?> Create(Character character, string ownerId);
        Task<Character?> Delete(Guid id, string ownerId);
        Task<List<Character>> Get(string userId);
        Task<Character?> GetById(Guid id, string userId);
        Task<Character?> Update(Guid id, string ownerId, string name, string description, string race);
    }
}