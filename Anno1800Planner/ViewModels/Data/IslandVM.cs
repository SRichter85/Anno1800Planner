using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class IslandVM : ModelDataVM<Island>
    {

        public IslandVM(Island island) : base(island)
        {
            Buildings = new SyncedCollection<IslandBuildingData, IslandBuildingVM>(
                island.Buildings,
                b => new IslandBuildingVM(b)
            );

            ImportedResources = new SyncedCollection<ResourceId, ResourceVM>(
                island.ImportedResources,
                r => new ResourceVM(r)
            );

            ProductionChains = new SyncedCollection<IdCountPair<Guid>, IdCountPairVM<Guid, ProductionChain>>(
                island.ProductionChains,
                pc => new IdCountPairVM<Guid, ProductionChain>(pc, Database.ProductionChainResolver)
            );

            LifestyleNeeds = Game.AllNeedLifestyle()
                .Select(x => new LifestyleToggleVM(x, island.EnabledLifestyleNeeds))
                .ToList();
        }

        public SyncedCollection<IslandBuildingData, IslandBuildingVM> Buildings { get; }

        public SyncedCollection<IdCountPair<Guid>, IdCountPairVM<Guid, ProductionChain>> ProductionChains { get; }

        public SyncedCollection<ResourceId, ResourceVM> ImportedResources { get; }

        public List<LifestyleToggleVM> LifestyleNeeds { get; }
    }
}
