using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{

    public class Region : ContainsId<RegionId>
    {
        public string DisplayName { get; set;  }

        public override string ToString() => DisplayName;

        public static void FillDict(Dictionary<RegionId, Region> dict)
        {
            dict[RegionId.Global] = new Region { DisplayName = "Global" };
            dict[RegionId.OldWorld] = new Region { DisplayName = "Old World" };
            dict[RegionId.NewWorld] = new Region { DisplayName = "New World" };
            dict[RegionId.Arctic] = new Region { DisplayName = "Arctic" };
            dict[RegionId.Enbesa] = new Region { DisplayName = "Enbesa" };
            dict[RegionId.CapeTrelawney] = new Region { DisplayName = "Cape Trelawney" };
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
