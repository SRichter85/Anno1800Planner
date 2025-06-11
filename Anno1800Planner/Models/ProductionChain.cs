using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1800Planner.GameData;
using Anno1800Planner.Common;
using System.Diagnostics.CodeAnalysis;

namespace Anno1800Planner.Models
{
    public class ProductionChain : ContainsId<Guid>
    {
        [SetsRequiredMembers]
        public ProductionChain() : base(Guid.NewGuid())
        {
        }

        public string Name { get; set; } = string.Empty;

        public List<IdCountPair<BuildingId>> Buildings { get; set; } = new();

    }
}
