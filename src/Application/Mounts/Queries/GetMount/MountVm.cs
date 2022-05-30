
using System.Text.Json.Serialization;

namespace Application.Mounts.Queries.GetMount
{
    public class MountVm
    {
        public string Name { get; set; } = string.Empty;
        public int Speed { get; set; }
        public List<Hero> Heroes { get; set; } = new List<Hero>();
    }
}
