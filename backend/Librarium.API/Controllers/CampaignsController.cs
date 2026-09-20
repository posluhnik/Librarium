using Librarium.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Librarium.API.Contacts;
using Librarium.Core.Models;

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
            var campaigns = await _campaignsService.GetAllCampaigns();

            var response = campaigns.Select(b => new CampaignsResponse(b.Id, b.Name, b.Description));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCampaign([FromBody] CampaignsRequest request)
        {
            var (campaign, error) = Campaign.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var campaignId = await _campaignsService.CreateCampaign(campaign);

            return Ok(campaignId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCampaigns(Guid id, [FromBody] CampaignsRequest request)
        {
            var campaignId = await _campaignsService.UpdateCampaign(id, request.Name, request.Description);

            return Ok(campaignId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCampaign(Guid id)
        {
            return Ok(await _campaignsService.DeleteCampaign(id));
        }
    }
}
