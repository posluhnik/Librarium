using Librarium.API.Contacts;
using Librarium.Application.Services;
using Librarium.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Librarium.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignsService _campaignsService;

        public CampaignsController(ICampaignsService campaignsService)
        {
            _campaignsService = campaignsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CampaignsResponse>>> GetCampaigns()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }
            
            var campaigns = await _campaignsService.GetAllCampaigns(userId);

            var response = campaigns.Select(b => new CampaignsResponse(b.Id, b.ownerId, b.Name, b.Description, b.Players, b.Characters));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CampaignsResponse>> GetCampaignById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }

            var campaign = await _campaignsService.GetCampaignById(id, userId);

            if (campaign == null)
            {
                return NotFound($"Campaign with ID {id} not found for user {userId}");
            }
            
            return Ok(campaign);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCampaign([FromBody] CampaignsRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }

            var (campaign, error) = Campaign.Create(
                Guid.NewGuid(),
                userId,
                request.Name,
                request.Description,
                players: new List<string>(),
                characters: new List<Guid>()
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var resp = await _campaignsService.CreateCampaign(campaign, userId);

            return Ok(resp);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<bool>> UpdateCampaigns(Guid id, [FromBody] CampaignsRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }

            var resp = await _campaignsService.UpdateCampaign(id, userId, request.Name, request.Description);

            if (!resp)
            {
                return NotFound($"Campaign with ID {id} not found for user {userId}");
            }
            
            return Ok(resp);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteCampaign(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }
            
            bool resp = await _campaignsService.DeleteCampaign(id, userId);
            
            if (!resp)
            {
                return NotFound($"Campaign with ID {id} not found for user {userId}");
            }

            return Ok(resp);
        }

        [HttpPost("join/{id:guid}")]
        public async Task<ActionResult<bool>> JoinCampaign(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }

            bool resp = await _campaignsService.JoinCampaign(id, userId);

            if (!resp)
            {
                return NotFound($"Campaign with ID {id} not found for user {userId}");
            }
            
            return Ok(resp);
        }

        [HttpPost("addchar/{id:guid}")]
        public async Task<ActionResult<bool>> AddCharacter(Guid campaignId, Guid characterId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid");
            }

            return await _campaignsService.AddCharacter(campaignId, characterId, userId);
        }

    }
}
