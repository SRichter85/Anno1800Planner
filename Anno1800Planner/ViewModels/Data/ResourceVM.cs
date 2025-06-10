using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.ViewModels
{
    public class ResourceVM : WrapperVM<ResourceId, Resource>
    {
        public ResourceVM(ResourceId id) : base(id, Game.ResourceResolver) { }
    }
    public class ShipVM : WrapperVM<ShipId, Ship>
    {
        public ShipVM(ShipId id) : base(id, Game.ShipResolver) { }
    }

    public class BuildingVM : WrapperVM<BuildingId, Building>
    {
        public BuildingVM(BuildingId id) : base(id, Game.BuildingResolver) { }
    }

    public class ResidentTierVM : WrapperVM<TierId, ResidentTier>
    {
        public ResidentTierVM(TierId id) : base(id, Game.TierResolver) { }
    }

    public class WorldRegionVM : WrapperVM<RegionId, Region>
    {
        public WorldRegionVM(RegionId id) : base(id, Game.RegionResolver) { }
    }
}
