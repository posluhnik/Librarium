namespace Librarium.DataAccess.Entities
{
    public class CharacterEntity
    {
        public Guid Id { get; set; }
        public string ownerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
    }
}