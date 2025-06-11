using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{

    public class Building : ContainsId<BuildingId>
    {
        public Building() { }

        [SetsRequiredMembers]
        public Building(BuildingId id,
            string name,
            TierId tier,
            int maintenance,
            int workforce,
            RegionId region = RegionId.Global,
            double productionTime = 60.0) : base(id)
        {
            Name = name;
            Tier = tier;
            Maintenance = maintenance;
            Workforce = workforce;
            Region = region;
            ProductionTime = productionTime;
        }
        public string Name { get; set; } = string.Empty;
        public RegionId Region { get; set; } = RegionId.Global;

        public TierId Tier { get; set; }
        public int Maintenance { get; set; }

        public int Workforce { get; set; }

        public double ProductionTime { get; set; } = 60.0;

        public List<ResourceId> Produces { get; } = new();

        public List<ResourceId> Requires { get; } = new();


        public bool CanUseTractor { get; set; }
        public bool CanUseSilo { get; set; }
        public bool CanUseFertilizer { get; set; }
        public bool CanUseEletricity { get; set; }

        public bool RequiresElectricity { get; set; }

        public override string ToString() => Name;


        private Building AddProducedResource(ResourceId id)
        {
            Produces.Add(id);
            return this;
        }

        private Building AddRequiredResource(ResourceId id)
        {
            Requires.Add(id);
            return this;
        }

        public static IEnumerable<Building> CreateDefault()
        {
            yield return new Building(BuildingId.Marketplace, "Marketplace", TierId.Farmers, 20, 0);
            yield return new Building(BuildingId.SmallWarehouse, "Small Warehouse", TierId.Farmers, 20, 0);
            yield return new Building(BuildingId.Lumberjack, "Lumberjack's Hut", TierId.Farmers, 10, 5, productionTime: 15.0)
                .AddProducedResource(ResourceId.Wood);
            yield return new Building(BuildingId.Sawmill, "Sawmill", TierId.Farmers, 10, 10, productionTime: 15.0)
                .AddRequiredResource(ResourceId.Wood)
                .AddProducedResource(ResourceId.Timber);
            yield return new Building(BuildingId.Pub, "Pub", TierId.Farmers, 1, 0, RegionId.OldWorld);



            yield return new Building(BuildingId.GrainFarm, "Grain Farm", TierId.Farmers, 10, 50, RegionId.OldWorld, 60.0)
            {
                CanUseTractor = true,
                CanUseFertilizer = true
            }
            .AddProducedResource(ResourceId.Wheat);
            yield return new Building(BuildingId.Mill, "Mill", TierId.Farmers, 30, 20, RegionId.OldWorld, 30.0)
            {
                CanUseEletricity = true
            }
            .AddRequiredResource(ResourceId.Wheat)
            .AddProducedResource(ResourceId.Cotton);

            yield return new Building(BuildingId.CottonPlantation, "Cotton Plantation", TierId.Jornaleros, 20, 100, RegionId.NewWorld, 60.0)
            {
                CanUseTractor = true,
                CanUseFertilizer = true
            }
            .AddProducedResource(ResourceId.Cotton);
        }
    }

    public enum BuildingId
    {
        Marketplace,
        SmallWarehouse,
        Lumberjack,
        Sawmill,
        GrainFarm,
        CottonPlantation,
        Mill,
        Pub
    }
}
