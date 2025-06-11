using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Anno1800Planner.Common;
using Anno1800Planner.GameData;

namespace Anno1800Planner.Models
{
    public class IslandBuildingData : IdCountPair<BuildingId>
    {

        public IslandBuildingData(BuildingId id, int count = 0) : base(id, count) { }
        // Module upgrades (only apply to eligible buildings)
        public int WithTractor { get; set; }
        public int WithSilo { get; set; }
        public int WithFertilizer { get; set; }

        // Electricity applies to entire building type, not per instance
        public bool WithElectricity { get; set; }
    }
}
