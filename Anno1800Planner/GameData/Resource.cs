using Anno1800Planner.Common;

namespace Anno1800Planner.GameData
{
    public class Resource : ContainsId<ResourceId>
    {
        public Resource()
        {
        }

        public Resource(string name) : this(name, RegionId.Global)
        {
        }

        public Resource(string name, RegionId region)
        {
            Name = name;
            ProducingRegions.Add(region);
        }

        public string Name { get; set; }

        private List<RegionId> ProducingRegions = new();

        public bool RequiresFertility { get; set; } = false;


        public override string ToString() => Name;


        private Resource AddProducingRegion(RegionId id)
        {
            ProducingRegions.Add(id);
            return this;
        }

        public static void FillDict(Dictionary<ResourceId, Resource> dict)
        {
            dict[ResourceId.Wood] = new Resource("Wood");
            dict[ResourceId.Timber] = new Resource("Timber");

            dict[ResourceId.Wheat] = new Resource
            {
                Name = "Wheat",
                RequiresFertility = true
            }
            .AddProducingRegion(RegionId.OldWorld);
            dict[ResourceId.Wheat] = new Resource
            {
                Name = "Wheat",
                RequiresFertility = true
            }
            .AddProducingRegion(RegionId.OldWorld);

            dict[ResourceId.Cotton] = new Resource
            {
                Name = "Cotton",
                RequiresFertility = true
            }
            .AddProducingRegion(RegionId.NewWorld);

            dict[ResourceId.GoldOre] = new Resource
            {
                Name = "Gold Ore",
                RequiresFertility = true
            }
            .AddProducingRegion(RegionId.NewWorld);

            dict[ResourceId.Fish] = new Resource
            {
                Name = "Fish",
                RequiresFertility = false
            }
            .AddProducingRegion(RegionId.OldWorld)
            .AddProducingRegion(RegionId.NewWorld);
        }
    }

    public enum ResourceId
    {
        Wood,
        Timber,
        Wheat,
        Cotton,
        GoldOre,
        Fish
    }
}
