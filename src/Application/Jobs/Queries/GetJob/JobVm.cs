using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobs.Queries.GetJob
{
    public class JobVm
    {
        public string Name { get; set; } = string.Empty;
        public List<Hero> Heroes { get; set; } = new List<Hero>();
    }
}
