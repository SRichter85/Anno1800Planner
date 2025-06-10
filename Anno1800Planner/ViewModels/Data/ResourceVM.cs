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
        public ResourceVM(ResourceId id) : base(id, Game.ResourceResolver) { }

        public static ResourceVM Create(ResourceId data) => new ResourceVM(data);
    }
    public class ShipVM : WrapperVM<ShipId, Ship>, ICreatable<ShipVM, ShipId>
    {
        public ShipVM(ShipId id) : base(id, Game.ShipResolver) { }
        public static ShipVM Create(ShipId data) => new ShipVM(data);
    }

    public class BuildingVM : WrapperVM<BuildingId, Building>, ICreatable<BuildingVM, BuildingId>
    {
        public BuildingVM(BuildingId id) : base(id, Game.BuildingResolver) { }
        public static BuildingVM Create(BuildingId data) => new BuildingVM(data);
    }

    public class ResidentTierVM : WrapperVM<TierId, ResidentTier>, ICreatable<ResidentTierVM, TierId>
    {
        public ResidentTierVM(TierId id) : base(id, Game.TierResolver) { }
        public static ResidentTierVM Create(TierId data) => new ResidentTierVM(data);
    }

    public class WorldRegionVM : WrapperVM<RegionId, Region>, ICreatable<WorldRegionVM, RegionId>
    {
        public WorldRegionVM(RegionId id) : base(id, Game.RegionResolver) { }
        public static WorldRegionVM Create(RegionId data) => new WorldRegionVM(data);
    }
}
