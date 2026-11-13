using Librarium.API.Contacts;
using Librarium.Application.Services;
using Librarium.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Librarium.DataAccess.Repositories;

namespace Librarium.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersService _charactersService;

        public CharactersController(ICharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CharactersResponse>>> GetCharacters()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var characters = await _charactersService.GetAllCharacters(userId);

            var response = characters.Select(b => new CharactersResponse(b.Id, b.ownerId, b.Name, b.Description, b.Race));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CharactersResponse>> GetCharacterById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var character = await _charactersService.GetCharacterById(id, userId);
            
            if (character == null)
            {
                return NotFound($"Character with ID {id} not found for user {userId}.");
            }
            
            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCharacter([FromBody] CharactersRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var (character, error) = Character.Create(
                Guid.NewGuid(),
                userId,
                request.Name,
                request.Description,
                request.Race
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var characterId = await _charactersService.CreateCharacter(character, userId);

            return Ok(characterId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCharacters(Guid id, [FromBody] CharactersRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var characterId = await _charactersService.UpdateCharacter(id, userId, request.Name, request.Description, request.Race);

            return Ok(characterId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCharacter(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _charactersService.DeleteCharacter(id, userId));
        }
    }
}
