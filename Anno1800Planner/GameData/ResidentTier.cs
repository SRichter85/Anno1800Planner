using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{
    public class ResidentTier : ContainsId<TierId>
    {
        public string Name { get; set; }

        public RegionId Region { get; set; }

        public readonly List<Need> Needs = new();
        public int BaseResidentsPerHouse { get; set; } // Add this property

        public override string ToString() => Name;

        internal ResidentTier AddNeed(Need need)
        {
            Needs.Add(need);
            return this;
        }

        public static void FillDict(Dictionary<TierId, ResidentTier> dict)
        {
            dict[TierId.Farmers] = new ResidentTier { Name = "Farmers", Region = RegionId.OldWorld, BaseResidentsPerHouse = 10 };
            dict[TierId.Workers] = new ResidentTier { Name = "Workers", Region = RegionId.OldWorld, BaseResidentsPerHouse = 20 };
            dict[TierId.Artisans] = new ResidentTier { Name = "Artisans", Region = RegionId.OldWorld };
            dict[TierId.Engineers] = new ResidentTier { Name = "Engineers", Region = RegionId.OldWorld };
            dict[TierId.Investors] = new ResidentTier { Name = "Investors", Region = RegionId.OldWorld };
            dict[TierId.Jornaleros] = new ResidentTier { Name = "Jornaleros", Region = RegionId.NewWorld };
            dict[TierId.Obreros] = new ResidentTier { Name = "Obreros", Region = RegionId.NewWorld };
            dict[TierId.Explorers] = new ResidentTier { Name = "Explorers", Region = RegionId.Arctic };
            dict[TierId.Technicians] = new ResidentTier { Name = "Technicians", Region = RegionId.Arctic };
            dict[TierId.Shephards] = new ResidentTier { Name = "Shephards", Region = RegionId.Enbesa };
            dict[TierId.Elders] = new ResidentTier { Name = "Elders", Region = RegionId.Enbesa };
            dict[TierId.Scholars] = new ResidentTier { Name = "Scholars" };
            dict[TierId.Tourists] = new ResidentTier { Name = "Tourists" };
            dict[TierId.Artistas] = new ResidentTier { Name = "Artistas", Region = RegionId.NewWorld };
        }
    }

    public enum TierId
    {
        Farmers,
        Workers,
        Artisans,
        Engineers,
        Investors,
        Jornaleros,
        Obreros,
        Explorers,
        Technicians,
        Shephards,
        Elders,
        Scholars,
        Tourists,
        Artistas
    }
}
