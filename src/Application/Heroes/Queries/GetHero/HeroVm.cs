
namespace Application.Heroes.Queries.GetHero
{
    public class HeroVm
    {
        public int HeroId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }

        public Job Job { get; set; } = null!;
        public List<Mount> Mounts { get; set; } = new List<Mount>();
    }
}
