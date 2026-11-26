namespace Librarium.Core.Models
{
    public class Campaign
    {
        public const int MAX_TITLE_LENGTH = 250;

        private Campaign(Guid id, string? ownerid, string name, string description, List<string> players, List<Guid> characters)
        {
            Id = id;
            ownerId = ownerid;
            Name = name;
            Description = description;
            Players = players;
            Characters = characters;
        }
        
        public Guid Id { get; }
        public string? ownerId { get; set; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public List<string> Players { get; } = new List<string>();
        public List<Guid> Characters { get; } = new List<Guid>();

        public static (Campaign campaign, string Error) Create(Guid id, string? ownerId, string name, string description, List<string> players, List<Guid> characters)
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

            var сampaign = new Campaign(id, ownerId, name, description, players, characters);

            return (сampaign, error);
        }

    }
}