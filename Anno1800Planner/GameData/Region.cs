using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{

    public class Region : ContainsId<RegionId>
    {
        public Region()
        {
        }

        [SetsRequiredMembers]
        public Region(RegionId id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;

        public override string ToString() => Name;

        public static IEnumerable<Region> CreateDefault()
        {
            yield return new Region(RegionId.Global, "Global");
            yield return new Region(RegionId.OldWorld, "Old World");
            yield return new Region(RegionId.NewWorld, "New World");
            yield return new Region(RegionId.Arctic, "Arctic");
            yield return new Region(RegionId.Enbesa, "Enbesa");
            yield return new Region(RegionId.CapeTrelawney, "Cape Trelawney");
        }
    }

    public enum RegionId
    {
        Global = 0,
        OldWorld = 1,
        NewWorld = 2,
        Arctic = 3,
        Enbesa = 4,
        CapeTrelawney = 5
    }
}
