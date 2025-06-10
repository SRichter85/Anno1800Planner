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
        public ProductionChainViewVM(MainVM main) : base(main) { }

        private ProductionChainVM? _chain;
        public ProductionChainVM? Chain
        {
            get => _chain;
            set => Set(ref _chain, value);
        }

        public SelectableCollection<Building> AllBuildings { get; } = new(Game.AllBuildings());

        public ICommand AddBuildingCommand => new RelayCommand(AddBuilding, () => AllBuildings.SelectedItem != null);

        private void AddBuilding()
        {
            var chain = Chain;
            var selectedBuilding = AllBuildings.SelectedItem;
            if (chain == null || selectedBuilding == null)
                return;

            var existing = chain.Buildings.FirstOrDefault(vm => vm.ChildViewModel.Reference == selectedBuilding);
            if (existing != null)
            {
                existing.Count++;
            }
            else
            {
                var model = new IdCountPair<BuildingId>(selectedBuilding.Id, 1);
                chain.Buildings.AddItem(model);
            }

            Database.Instance.MarkDirty();
        }
    }
}
