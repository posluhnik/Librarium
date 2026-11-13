using Librarium.Core.Models;
using Librarium.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Librarium.Application.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersRepository _charactersRepository;

        public CharactersService(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }

        public async Task<List<Character>> GetAllCharacters(string userId)
        {
            return await _charactersRepository.Get(userId);
        }

        public async Task<Character> GetCharacterById(Guid id, string userId)
        {
            return await _charactersRepository.GetById(id, userId);
        }

        public async Task<Guid> CreateCharacter(Character character, string ownerId)
        {
            return await _charactersRepository.Create(character, ownerId);
        }

        public async Task<Guid> UpdateCharacter(Guid id, string ownerId, string name, string description, string race)
        {
            return await _charactersRepository.Update(id, ownerId, name, description, race);
        }

        public async Task<Guid> DeleteCharacter(Guid id, string ownerId)
        {
            return await _charactersRepository.Delete(id, ownerId);
        }
    }
}
