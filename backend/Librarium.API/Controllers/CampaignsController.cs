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
            
            var campaigns = await _campaignsService.GetAllCampaigns(userId);

            var response = campaigns.Select(b => new CampaignsResponse(b.Id, b.ownerId, b.Name, b.Description, b.Players));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CampaignsResponse>> GetCampaignById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var campaign = await _campaignsService.GetCampaignById(id, userId);

            if (campaign == null)
            {
                return NotFound($"Campaign with ID {id} not found for user {userId}.");
            }
            
            return Ok(campaign);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCampaign([FromBody] CampaignsRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var (campaign, error) = Campaign.Create(
                Guid.NewGuid(),
                userId,
                request.Name,
                request.Description,
                players: new List<string>()
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var campaignId = await _campaignsService.CreateCampaign(campaign, userId);

            return Ok(campaignId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCampaigns(Guid id, [FromBody] CampaignsRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var campaignId = await _campaignsService.UpdateCampaign(id, userId, request.Name, request.Description);

            return Ok(campaignId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCampaign(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _campaignsService.DeleteCampaign(id, userId));
        }

        [HttpPost("join/{id:guid}")]
        public async Task<ActionResult> JoinCampaign(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _campaignsService.JoinCampaign(id, userId);

            return Ok();
        }
    }
}
