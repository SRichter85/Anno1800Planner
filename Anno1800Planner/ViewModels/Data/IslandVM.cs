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
    public class IslandVM : WrapperVM<Island>, ICreatable<IslandVM, Island>
    {

        public IslandVM(Island island) : base(island)
        {
            Buildings = new SyncedCollection<IslandBuildingData, IslandBuildingVM>(island.Buildings);
            ImportedResources = new SyncedCollection<ResourceId, ResourceVM>(island.ImportedResources);
            ProductionChains = new SyncedCountPairCollection<Guid, ProductionChainVM>(island.ProductionChains);

            LifestyleNeeds = Game.AllNeedLifestyle()
                .Select(x => new LifestyleToggleVM(x, island.EnabledLifestyleNeeds))
                .ToList();
        }

        public SyncedCollection<IslandBuildingData, IslandBuildingVM> Buildings { get; }

        public SyncedCountPairCollection<Guid, ProductionChainVM> ProductionChains { get; }

        public SyncedCollection<ResourceId, ResourceVM> ImportedResources { get; }

        public List<LifestyleToggleVM> LifestyleNeeds { get; }

        public static IslandVM Create(Island data) => new IslandVM(data);
    }
}
