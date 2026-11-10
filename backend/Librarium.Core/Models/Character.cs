namespace Librarium.Core.Models
{
    public class Character
    {
        public const int MAX_TITLE_LENGTH = 250;

        private Character(Guid id, string ownerid, string name, string description, string race)
        {
            Id = id;
            ownerId = ownerid;
            Name = name;
            Description = description;
            Race = race;
        }
        
        public Guid Id { get; }
        public string ownerId { get; set; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public string Race { get; } = string.Empty;

        public static (Character character, string Error) Create(Guid id, string ownerId, string name, string description, string race)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > MAX_TITLE_LENGTH)
            {
                error = "Invalid name";
            }
            if (string.IsNullOrEmpty(ownerId))
            {
                error = $"You're not registered";
            }

            var character = new Character(id, ownerId, name, description, race);

            return (character, error);
        }

    }
}