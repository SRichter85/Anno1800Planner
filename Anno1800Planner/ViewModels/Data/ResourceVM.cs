using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.ViewModels
{
    public class ResourceVM : ModelDataVM<ResourceId, Resource>
    {
        public ResourceVM(ResourceId id) : base(id, Game.ResourceResolver) { }
    }
    public class ShipVM : ModelDataVM<ShipId, Ship>
    {
        public ShipVM(ShipId id) : base(id, Game.ShipResolver) { }
    }

    public class BuildingVM : ModelDataVM<BuildingId, Building>
    {
        public BuildingVM(BuildingId id) : base(id, Game.BuildingResolver) { }
    }

    public class ResidentTierVM : ModelDataVM<TierId, ResidentTier>
    {
        public ResidentTierVM(TierId id) : base(id, Game.TierResolver) { }
    }

    public class WorldRegionVM : ModelDataVM<RegionId, Region>
    {
        public WorldRegionVM(RegionId id) : base(id, Game.RegionResolver) { }
    }
}
