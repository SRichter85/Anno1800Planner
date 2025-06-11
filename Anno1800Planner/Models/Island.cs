
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Anno1800Planner.GameData;
using Anno1800Planner.Common;

namespace Anno1800Planner.Models
{
    public class Island
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Population per tier
        public List<IdCountPair<TierId>> Residents { get; set; } = new();

        // Buildings per type, with module status
        public List<IslandBuildingData> Buildings { get; set; } = new();
        public List<IdCountPair<Guid>> ProductionChains { get; set; } = new();

        // Ships stationed here
        public List<IdCountPair<ShipId>> Ships { get; set; } = new();

        // Resources being imported from outside
        public List<ResourceId> ImportedResources { get; set; } = new();

        // Which lifestyle needs are enabled (per tier)
        public List<string> EnabledLifestyleNeeds { get; set; } = new();
    }
}
