using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Mount
    {
        public int MountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Speed { get; set; }
        [JsonIgnore]
        public List<Hero> Heroes { get; set; } = new List<Hero>();
    }
}
