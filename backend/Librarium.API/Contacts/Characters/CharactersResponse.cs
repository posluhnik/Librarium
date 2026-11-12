namespace Librarium.API.Contacts
{
    public record CharactersResponse(
        Guid Id,
        string ownerId,
        string Name,
        string Description,
        string Race
    );
}