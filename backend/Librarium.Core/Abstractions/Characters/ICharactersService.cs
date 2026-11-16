using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICharactersService
    {
        Task<Character?> CreateCharacter(Character character, string ownerId);
        Task<Character?> DeleteCharacter(Guid id, string ownerId);
        Task<List<Character>> GetAllCharacters(string userId);
        Task<Character?> GetCharacterById(Guid id, string userId);
        Task<Character?> UpdateCharacter(Guid id, string ownerId, string name, string description, string race);
    }
}