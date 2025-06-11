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
            foreach (var item in Region.CreateDefault()) _instance._worldRegions[item.Id] = item;

            _instance._resources.Clear();
            foreach (var item in Resource.CreateDefault()) _instance._resources[item.Id] = item;

            _instance._ships.Clear();
            foreach (var item in Ship.CreateDefault()) _instance._ships[item.Id] = item;

            _instance._tiers.Clear();
            foreach (var item in ResidentTier.CreateDefault()) _instance._tiers[item.Id] = item;

            _instance._buildings.Clear();
            foreach (var item in Building.CreateDefault()) _instance._buildings[item.Id] = item;
            

            InitializeNeeds();
        }

        private static void InitializeNeeds()
        {
            AddBasicNeed(TierId.Farmers, BuildingId.Marketplace, 5, 0);
            AddBasicNeed(TierId.Farmers, ResourceId.Fish, 0.025, 3, 1);
            AddBasicNeed(TierId.Farmers, ResourceId.WorkClothes, 0.03075, 3, 2);
            AddLuxuryNeed(TierId.Farmers, ResourceId.Schnapps, 0.0333, 0, 3);
            AddLuxuryNeed(TierId.Farmers, BuildingId.Pub, 0, 1.2);
            AddLifestyleNeed(TierId.Farmers, ResourceId.Flour, 0.024, 1, 0.6);
        }

        private static void AddNeed(Need need)
        {
            _instance._tiers[need.Tier].AddNeed(need);
            _instance._needs.Add(need);
        }

        private static void AddBasicNeed(TierId tier, ResourceId resource, double consumption, int workforce, double income)
            => AddNeed(new Need(tier, resource, NeedCategory.Basic, consumption, workforce, income));
        private static void AddBasicNeed(TierId tier, BuildingId building, int workforce = 0, double income = 0)
            => AddNeed(new Need(tier, building, NeedCategory.Basic, workforce, income));
        private static void AddLuxuryNeed(TierId tier, ResourceId resource, double consumption, int workforce, double income)
            => AddNeed(new Need(tier, resource, NeedCategory.Luxury, consumption, workforce, income));
        private static void AddLuxuryNeed(TierId tier, BuildingId building, int workforce = 0, double income = 0)
            => AddNeed(new Need(tier, building, NeedCategory.Luxury, workforce, income));
        private static void AddLifestyleNeed(TierId tier, ResourceId resource, double consumption, int workforce, double income)
            => AddNeed(new Need(tier, resource, NeedCategory.Lifestyle, consumption, workforce, income));
        private static void AddLifestyleNeed(TierId tier, BuildingId building, int workforce = 0, double income = 0)
            => AddNeed(new Need(tier, building, NeedCategory.Lifestyle, workforce, income));
       

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
