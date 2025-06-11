using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{

    public class Building : ContainsId<BuildingId>
    {
        public string Name { get; set; }
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

        public static void FillDict(Dictionary<BuildingId, Building> dict)
        {
            dict[BuildingId.Marketplace] = new Building
            {
                Name = "Marketplace",
                Tier = TierId.Farmers,
                Maintenance = 20,
            };

            dict[BuildingId.SmallWarehouse] = new Building
            {
                Name = "Small Warehouse",
                Tier = TierId.Farmers,
                Maintenance = 20,
            };

            dict[BuildingId.Lumberjack] = new Building
            {
                Name = "Lumberjack's Hut",
                Tier = TierId.Farmers,
                Maintenance = 10,
                Workforce = 5,
                ProductionTime = 15.0,
            }
            .AddProducedResource(ResourceId.Wood);
            

            dict[BuildingId.Sawmill] = new Building
            {
                Name = "Sawmill",
                Tier = TierId.Farmers,
                Maintenance = 10,
                Workforce = 10,
                ProductionTime = 15.0,
            }
            .AddRequiredResource(ResourceId.Wood)
            .AddProducedResource(ResourceId.Timber);

            dict[BuildingId.GrainFarm] = new Building
            {
                Name = "Grain Farm",
                Tier = TierId.Farmers,
                Maintenance = 10,
                Workforce = 50,
                ProductionTime = 60,
                Region = RegionId.OldWorld,
                CanUseTractor = true,
                CanUseFertilizer = true
            }
            .AddProducedResource(ResourceId.Wheat);

            dict[BuildingId.CottonPlantation ] = new Building
            {
                Name = "Cotton Plantation",
                Tier = TierId.Jornaleros,
                Maintenance = 20,
                Workforce = 100,
                ProductionTime = 60,
                Region = RegionId.NewWorld,
                CanUseTractor = true,
                CanUseFertilizer = true
            }
            .AddProducedResource(ResourceId.Cotton);


            dict[BuildingId.Mill] = new Building
            {
                Name = "Mill",
                Maintenance = 30,
                Workforce = 20,
                Region = RegionId.OldWorld,
                Tier = TierId.Farmers,
                ProductionTime = 30,
                CanUseEletricity = true
            }
            .AddRequiredResource(ResourceId.Wheat)
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
        Mill
    }
}
