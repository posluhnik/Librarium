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

        public async Task<List<Character>> Get(string userId)
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

        public async Task<Character> GetById(Guid id, string userId)
        {
            var characterEntities = await _context.Characters
                .AsNoTracking()
                .ToListAsync();

            var characters = characterEntities
                .Select(b => Character.Create(b.Id, b.ownerId, b.Name, b.Description, b.Race).character)
                .Where(b => b != null && (b.ownerId == userId) && (b.Id == id))
                .ToList();

            if (characters.Count == 0)
            {
                return null;
            }

            return characters[0];
        }

        public async Task<Guid> Create(Character character, string ownerId)
        {
            if (string.IsNullOrEmpty(ownerId))
            {
                throw new ArgumentException("Owner ID cannot be null or empty.", nameof(ownerId));
            }
            else
            {
                var characterEntity = new CharacterEntity
                {
                    Id = character.Id,
                    ownerId = ownerId,
                    Name = character.Name,
                    Description = character.Description,
                    Race = character.Race
                };

                await _context.Characters.AddAsync(characterEntity);
                await _context.SaveChangesAsync();

                return character.Id;
            }
                
        }

        public async Task<Guid> Update(Guid id, string ownerId, string name, string description, string race)
        {
            if (string.IsNullOrEmpty(ownerId))
            {
                throw new ArgumentException("Owner ID cannot be null or empty.", nameof(ownerId));
            }

            if (ownerId == _context.Characters.Where(b => b.Id == id).Select(b => b.ownerId).FirstOrDefault())
            {
                await _context.Characters
                    .Where(b => b.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(b => b.Name, b => name)
                        .SetProperty(b => b.Description, b => description)
                        .SetProperty(b => b.Race, b => race));
            }

            return id;
        }

        public async Task<Guid> Delete(Guid id, string ownerId)
        {
            if (ownerId == _context.Characters.Where(b => b.Id == id).Select(b => b.ownerId).FirstOrDefault())
            {
                await _context.Characters
                    .Where(b => b.Id == id)
                    .ExecuteDeleteAsync();
            }

            return id;
        }
    }
}
