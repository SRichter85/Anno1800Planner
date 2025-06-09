using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1800Planner.GameData;
using Anno1800Planner.Common;

namespace Anno1800Planner.Models
{
    public class ProductionChain : ContainsId<Guid>
    {

        public ProductionChain()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; } = string.Empty;

        public List<IdCountPair<BuildingId>> Buildings { get; set; } = new();

    }
}
