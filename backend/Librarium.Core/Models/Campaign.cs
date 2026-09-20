namespace Librarium.Core.Models
{
    public class Campaign
    {
        public const int MAX_TITLE_LENGTH = 250;

        private Campaign(Guid id, string name, string description)
        {
            Id = id; 
            Name = name;
            Description = description; 
        }
        
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;

        public static (Campaign campaign, string Error) Create(Guid id, string name, string description)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > MAX_TITLE_LENGTH)
            {
                error = "Invalid name";
            }

            var сampaign = new Campaign(id, name, description);

            return (сampaign, error);
        }

    }
}