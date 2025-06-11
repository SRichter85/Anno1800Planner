using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Anno1800Planner.GameData
{
    public class Game
    {
        private static Game _instance = new Game();

        private Dictionary<RegionId, Region> _worldRegions = new();
        private Dictionary<ResourceId, Resource> _resources = new();
        private Dictionary<ShipId, Ship> _ships = new();
        private Dictionary<TierId, ResidentTier> _tiers = new();
        private Dictionary<BuildingId, Building> _buildings = new();

        private List<Need> _needs = new();

        public static void Initialize()
        {
            _instance._worldRegions.Clear();
            Region.FillDict(_instance._worldRegions);

            _instance._resources.Clear();
            Resource.FillDict(_instance._resources);

            _instance._ships.Clear();
            Ship.FillDict(_instance._ships);

            _instance._tiers.Clear();
            ResidentTier.FillDict(_instance._tiers);

            _instance._buildings.Clear();
            Building.FillDict(_instance._buildings);

            InitializeNeeds();
        }

        private static void InitializeNeeds()
        {
            AddBasicNeed(TierId.Farmers, BuildingId.Marketplace, workforce: 5);
            AddBasicNeed(TierId.Farmers, ResourceId.Fish, 40, income: 1, workforce: 3);
            AddBasicNeed(TierId.Farmers, ResourceId.WorkClothes, 32.5, income: 3, workforce: 2);
            AddLuxuryNeed(TierId.Farmers, ResourceId.Schnapps, 30, income: 3);
            AddLuxuryNeed(TierId.Farmers, BuildingId.Pub, income: 1.2);
            AddLifestyleNeed(TierId.Farmers, ResourceId.Flour, 41.67, workforce: 1, income: 0.6);
        }

        private static void AddNeed(TierId tier, Need need)
        {
            _instance._tiers[tier].AddNeed(need);
            need.Id = Need.GetId(tier, need);
            _instance._needs.Add(need);
        }

        private static void AddBasicNeed(TierId tier, ResourceId resource, double consumption, double income = 0, int workforce = 0)
            => AddNeed(tier, new Need { Resource = resource, Category = NeedCategory.Basic, ConsumptionIntervalMinutes = consumption, IncomeBonus = income, WorkforceBonus = workforce });
        private static void AddBasicNeed(TierId tier, BuildingId building, double income = 0, int workforce = 0)
            => AddNeed(tier, new Need { Building = building, Category = NeedCategory.Basic, ConsumptionIntervalMinutes = 0, IncomeBonus = income, WorkforceBonus = workforce });
        private static void AddLuxuryNeed(TierId tier, ResourceId resource, double consumption, double income = 0, int workforce = 0)
            => AddNeed(tier, new Need { Resource = resource, Category = NeedCategory.Luxury, ConsumptionIntervalMinutes = consumption, IncomeBonus = income, WorkforceBonus = workforce });
        private static void AddLuxuryNeed(TierId tier, BuildingId building, double income = 0, int workforce = 0)
            => AddNeed(tier, new Need { Building = building, Category = NeedCategory.Luxury, ConsumptionIntervalMinutes = 0, IncomeBonus = income, WorkforceBonus = workforce });
        private static void AddLifestyleNeed(TierId tier, ResourceId resource, double consumption, double income = 0, int workforce = 0)
            => AddNeed(tier, new Need { Resource = resource, Category = NeedCategory.Lifestyle, ConsumptionIntervalMinutes = consumption, IncomeBonus = income, WorkforceBonus = workforce });
        private static void AddLifestyleNeed(TierId tier, BuildingId building, double income = 0, int workforce = 0)
            => AddNeed(tier, new Need { Building = building, Category = NeedCategory.Lifestyle, ConsumptionIntervalMinutes = 0, IncomeBonus = income, WorkforceBonus = workforce });


        // Helper Lookup Classes
        public static Region Get(RegionId id) => _instance._worldRegions[id];
        public static Resource Get(ResourceId id) => _instance._resources[id];
        public static Ship Get(ShipId id) => _instance._ships[id];
        public static ResidentTier Get(TierId id) => _instance._tiers[id];
        public static Building Get(BuildingId id) => _instance._buildings[id];


        public static IEnumerable<Region> AllWorldRegions() => _instance._worldRegions.Values;
        public static IEnumerable<Resource> AllResources() => _instance._resources.Values;
        public static IEnumerable<Ship> AllShips() => _instance._ships.Values;
        public static IEnumerable<ResidentTier> AllResidentTiers() => _instance._tiers.Values;
        public static IEnumerable<Building> AllBuildings() => _instance._buildings.Values;
        public static IEnumerable<Need> AllNeeds() => _instance._needs;
        public static IEnumerable<Need> AllNeeds(NeedCategory category) => AllNeeds().Where(n => n.Category == category);
        public static IEnumerable<Need> AllNeedLifestyle() => AllNeeds(NeedCategory.Lifestyle);

        public static Func<RegionId, Region> RegionResolver => ((id) => _instance._worldRegions[id]);
        public static Func<ResourceId, Resource> ResourceResolver => ((id) => _instance._resources[id]);
        public static Func<ShipId, Ship> ShipResolver => ((id) => _instance._ships[id]);
        public static Func<TierId, ResidentTier> TierResolver => ((id) => _instance._tiers[id]);
        public static Func<BuildingId, Building> BuildingResolver => ((id) => _instance._buildings[id]);


    }
}
