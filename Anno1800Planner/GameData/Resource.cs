using Anno1800Planner.Common;
using System.Diagnostics.CodeAnalysis;

namespace Anno1800Planner.GameData
{
    public class Resource : ContainsId<ResourceId>
    {
        public Resource()
        {
        }

        [SetsRequiredMembers]
        public Resource(ResourceId id, string name, RegionId region = RegionId.Global, bool reqFertility = false) : base(id)
        {
            Name = name;
            ProducingRegions.Add(region);
            RequiresFertility = reqFertility;
        }

        public string Name { get; set; } = string.Empty;

        public List<RegionId> ProducingRegions { get; } = new();

        public bool RequiresFertility { get; set; } = false;

        public override string ToString() => Name;

        private Resource AddProducingRegion(RegionId id)
        {
            ProducingRegions.Add(id);
            return this;
        }

        public static IEnumerable<Resource> CreateDefault()
        {
            yield return new Resource(ResourceId.Wood, "Wood");
            yield return new Resource(ResourceId.Timber, "Timber");
            yield return new Resource(ResourceId.Fish, "Fish", RegionId.OldWorld);
            yield return new Resource(ResourceId.WorkClothes, "Work Clothes", RegionId.OldWorld);
            yield return new Resource(ResourceId.Schnapps, "Schnapps", RegionId.OldWorld);
            yield return new Resource(ResourceId.Flour, "Flour", RegionId.OldWorld);
            yield return new Resource(ResourceId.Wheat, "Wheat", RegionId.OldWorld, true);
            yield return new Resource(ResourceId.Cotton, "Cotton", RegionId.NewWorld, true);
            yield return new Resource(ResourceId.GoldOre, "Gold Ore", RegionId.NewWorld, true);
        }
    }

    public enum ResourceId
    {
        Wood,
        Timber,
        Wheat,
        Cotton,
        GoldOre,
        Fish,
        WorkClothes,
        Schnapps,
        Flour
    }
}
