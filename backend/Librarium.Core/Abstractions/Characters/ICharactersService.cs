using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICharactersService
    {
        Task<Guid> CreateCharacter(Character character, string userId);
        Task<bool> DeleteCharacter(Guid id, string userId);
        Task<List<Character>> GetAllCharacters(string userId);
        Task<Character?> GetCharacterById(Guid id, string userId);
        Task<bool> UpdateCharacter(Guid id, string userId, string name, string description, string race);
    }
}