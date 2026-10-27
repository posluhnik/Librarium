namespace Librarium.API.Contacts
{
    public record CampaignsResponse(
        Guid Id,
        string ownerId,
        string Name,
        string Description);
}
