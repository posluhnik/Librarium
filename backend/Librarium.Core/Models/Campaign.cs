namespace Librarium.Core.Models
{
    public class Campaign
    {
        public const int MAX_TITLE_LENGTH = 250;

        private Campaign(Guid id, string ownerid, string name, string description)
        {
            Id = id;
            ownerId = ownerid;
            Name = name;
            Description = description;
        }
        
        public Guid Id { get; }
        public string ownerId { get; set; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;

        public static (Campaign campaign, string Error) Create(Guid id, string ownerId, string name, string description)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > MAX_TITLE_LENGTH)
            {
                error = "Invalid name";
            }

            var сampaign = new Campaign(id, ownerId, name, description);

            return (сampaign, error);
        }

    }
}