using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{
    public class ResidentTier : ContainsId<TierId>
    {
        public ResidentTier()
        {
        }

        [SetsRequiredMembers]
        public ResidentTier(TierId id, string name, int residentsPerHouse, RegionId region = RegionId.Global) : base(id)
        {
            Name = name;
            Region = region;
            BaseResidentsPerHouse = residentsPerHouse;
        }

        public string Name { get; set; } = string.Empty;

        public RegionId Region { get; set; }

        public List<Need> Needs { get; } = new();

        public int BaseResidentsPerHouse { get; set; }

        public override string ToString() => Name;

        internal ResidentTier AddNeed(Need need)
        {
            Needs.Add(need);
            return this;
        }
        public static IEnumerable<ResidentTier> CreateDefault()
        {
            yield return new ResidentTier(TierId.Farmers, "Farmers", 10, RegionId.OldWorld);
            yield return new ResidentTier(TierId.Workers, "Workers", 20, RegionId.OldWorld);
            yield return new ResidentTier(TierId.Artisans, "Artisans", 1, RegionId.OldWorld);
            yield return new ResidentTier(TierId.Engineers, "Engineers", 1, RegionId.OldWorld);
            yield return new ResidentTier(TierId.Investors, "Investors", 1, RegionId.OldWorld);

            yield return new ResidentTier(TierId.Jornaleros, "Investors", 1, RegionId.NewWorld);
            yield return new ResidentTier(TierId.Obreros, "Investors", 1, RegionId.NewWorld);
            yield return new ResidentTier(TierId.Artistas, "Artistas", 1, RegionId.NewWorld);

            yield return new ResidentTier(TierId.Explorers, "Explorers", 1, RegionId.Arctic);
            yield return new ResidentTier(TierId.Technicians, "Technicians", 1, RegionId.Arctic);

            yield return new ResidentTier(TierId.Shephards, "Shephards", 1, RegionId.Enbesa);
            yield return new ResidentTier(TierId.Elders, "Elders", 1, RegionId.Enbesa);

            yield return new ResidentTier(TierId.Scholars, "Scholars", 1);
            yield return new ResidentTier(TierId.Tourists, "Tourists", 1);
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
