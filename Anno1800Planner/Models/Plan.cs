
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Anno1800Planner.GameData;
using Anno1800Planner.Common;

namespace Anno1800Planner.Models
{
    public class Plan
    {
        public Guid Id { get; set; }
        public Plan()
        {
            // Initialize ships with all known types at 0
            Ships.AddRange(Enum.GetValues<ShipId>().Select(x => new IdCountPair<ShipId>(x)));
        }

        public string Name { get; set; }

        public List<IdCountPair<ShipId>> Ships { get; set; } = new();

        public List<Island> Islands { get; set; } = new();
    }
}
