using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICharactersRepository
    {
        Task<Guid> Create(Character character, string? userId);
        Task<bool> Delete(Guid id, string userId);
        Task<List<Character>> Get(string userId);
        Task<Character?> GetById(Guid id, string userId);
        Task<bool> Update(Guid id, string userId, string name, string description, string race);
    }
}