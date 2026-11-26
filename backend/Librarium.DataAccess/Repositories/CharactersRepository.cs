using Librarium.Core.Models;
using Librarium.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.DataAccess.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly ApplicationDbContext _context;

        public CharactersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Character>> Get(string? userId)
        {
            var characterEntities = await _context.Characters
                .AsNoTracking()
                .ToListAsync();

            var characters = characterEntities
                .Select(b => Character.Create(b.Id, b.ownerId, b.Name, b.Description, b.Race).character)
                .Where(b => b != null && (b.ownerId == userId))
                .ToList();

            return characters;
        }

        public async Task<Character?> GetById(Guid id, string? userId)
        {
            var query = _context.Characters
                .Where(b => b.Id == id && b.ownerId == userId);

            return await query
                .Select(b => Character.Create(b.Id, b.ownerId, b.Name, b.Description, b.Race).character)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> Create(Character character, string? userId)
        {
            var characterEntity = new CharacterEntity
            {
                Id = character.Id,
                ownerId = userId,
                Name = character.Name,
                Description = character.Description,
                Race = character.Race
            };

            await _context.Characters.AddAsync(characterEntity);
            await _context.SaveChangesAsync();

            return character.Id;
        }

        public async Task<bool> Update(Guid id, string? userId, string name, string description, string race)
        {
            var query = _context.Characters
                .Where(b => b.Id == id && b.ownerId == userId);

            int updatedCount = await query.ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Name, name)
                .SetProperty(b => b.Description, description)
                .SetProperty(b => b.Race, race));

            if (updatedCount == 0)
                return false;

            return true;
        }

        public async Task<bool> Delete(Guid id, string? userId)
        {
            var query = _context.Characters
                .Where(b => b.Id == id && b.ownerId == userId);
            
            int deletedCount = await query.ExecuteDeleteAsync();
            
            if (deletedCount == 0)
                return false;

            return true;
        }
    }
}