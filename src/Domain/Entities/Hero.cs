
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Hero
    {
        public int HeroId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }

        [JsonIgnore]
        public int JobId { get; set; }
        public Job Job { get; set; } = null!; 
        public List<Mount> Mounts { get; set; } = new List<Mount>();

    }
}
