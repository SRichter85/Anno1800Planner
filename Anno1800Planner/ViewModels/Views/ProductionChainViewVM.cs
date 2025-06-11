using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainViewVM : MainContentViewModelBase
    {
        public ProductionChainViewVM(MainVM main) : base(main)
        {            
            
            // Populate the list of all buildings using our ICreatable pattern.
            var buildingVms = Game.AllBuildings().Select(b => BuildingVM.Create(b.Id));
            AllBuildings = new SelectableCollection<BuildingVM>(buildingVms);

            AddBuildingToChainCommand = new RelayCommand(
                execute: () =>
                {
                    // Call the AddBuilding method on the ProductionChainVM itself.
                    if (AllBuildings.SelectedItem != null)
                    {
                        Chain?.AddBuilding(AllBuildings.SelectedItem);
                    }
                },
                canExecute: () => AllBuildings.SelectedItem != null && Chain != null
            );
        }

        private ProductionChainVM? _chain;
        public ProductionChainVM? Chain
        {
            get => _chain;
            set => Set(ref _chain, value);
        }

        // The list of all available buildings the user can choose from.
        // It's now a collection of BuildingVMs for consistency.
        public SelectableCollection<BuildingVM> AllBuildings { get; }


        // This command is triggered when the user double-clicks an available building.
        public ICommand AddBuildingToChainCommand { get; }

    }
}
