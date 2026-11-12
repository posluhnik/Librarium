using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICharactersService
    {
        Task<Guid> CreateCharacter(Character character, string ownerId);
        Task<Guid> DeleteCharacter(Guid id, string ownerId);
        Task<List<Character>> GetAllCharacters(string userId);
        Task<Character> GetCharacterById(Guid id, string userId);
        Task<Guid> UpdateCharacter(Guid id, string ownerId, string name, string description, string race);
    }
}