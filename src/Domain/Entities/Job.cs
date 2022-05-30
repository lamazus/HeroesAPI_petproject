using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Job
    {
        public int JobId { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Hero> Heroes { get; set; } = new List<Hero>();
    }
}
