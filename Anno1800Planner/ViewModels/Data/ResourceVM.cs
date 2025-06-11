using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.ViewModels
{
    public class ResourceVM : WrapperVM<ResourceId, Resource>, ICreatable<ResourceVM, ResourceId>
    {
        public ResourceVM(ResourceId id) : base(Game.ResourceResolver(id)) { }

        public static ResourceVM Create(ResourceId data) => new ResourceVM(data);

        public ResourceId GetDbEntry() => Data;
    }
    public class ShipVM : WrapperVM<ShipId, Ship>, ICreatable<ShipVM, ShipId>
    {
        public ShipVM(ShipId id) : base(Game.ShipResolver(id)) { }
        public static ShipVM Create(ShipId data) => new ShipVM(data);
        public ShipId GetDbEntry() => Data;
    }

    public class BuildingVM : WrapperVM<BuildingId, Building>, ICreatable<BuildingVM, BuildingId>
    {
        public BuildingVM(BuildingId id) : base(Game.BuildingResolver(id)) { }
        public static BuildingVM Create(BuildingId data) => new BuildingVM(data);
        public BuildingId GetDbEntry() => Data;
    }

    public class ResidentTierVM : WrapperVM<TierId, ResidentTier>, ICreatable<ResidentTierVM, TierId>
    {
        public ResidentTierVM(TierId id) : base(Game.TierResolver(id))
        {
            Needs.AddRange(Reference.Needs.Select(NeedVM.Create));
        }

        public static ResidentTierVM Create(TierId data) => new ResidentTierVM(data);
        public TierId GetDbEntry() => Data;


        public List<NeedVM> Needs { get; } = new();
    }

    public class WorldRegionVM : WrapperVM<RegionId, Region>, ICreatable<WorldRegionVM, RegionId>
    {
        public WorldRegionVM(RegionId id) : base(Game.RegionResolver(id)) { }
        public static WorldRegionVM Create(RegionId data) => new WorldRegionVM(data);
        public RegionId GetDbEntry() => Data;
    }
}
