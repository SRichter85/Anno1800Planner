using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Anno1800Planner.Common;
using System.Windows.Input;
using Anno1800Planner.GameData;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainConfigurationViewVM : MainContentViewModelBase
    {

        public ProductionChainConfigurationViewVM(MainVM mainVM) : base(mainVM)
        {

            var buildingVms = Game.AllBuildings().Select(b => BuildingVM.Create(b.Id));
            AllBuildings = new SelectableCollection<BuildingVM>(buildingVms);

            AddChainCommand = new RelayCommand(() => _productionChains?.AddItem(new ProductionChain() { Name = "New Chain" }));
            DeleteSelectedChainCommand = new RelayCommand(() => _productionChains?.RemoveSelectedItem(), () => _productionChains?.SelectedItem != null);
            AddBuildingToSelectedChainCommand = new RelayCommand(
                execute: () =>
                {
                    // The logic now operates on the selected chain in the main list.
                    if (ProductionChains?.SelectedItem != null && AllBuildings.SelectedItem != null)
                    {
                        ProductionChains.SelectedItem.AddBuilding(AllBuildings.SelectedItem);
                    }
                },
                canExecute: () => ProductionChains?.SelectedItem != null && AllBuildings.SelectedItem != null
            );
        }

        private SyncedCollection<ProductionChain, ProductionChainVM>? _productionChains;
        public SyncedCollection<ProductionChain, ProductionChainVM>? ProductionChains
        {
            get => _productionChains;
            set => Set(ref _productionChains, value);
        }
        // This collection holds all available buildings the user can add.
        public SelectableCollection<BuildingVM> AllBuildings { get; }

        public ICommand AddChainCommand { get; }
        public ICommand DeleteSelectedChainCommand { get; }
        public ICommand AddBuildingToSelectedChainCommand { get; }
    }
}
