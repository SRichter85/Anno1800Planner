using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainVM : ModelDataVM<ProductionChain>
    {
        public ProductionChainVM(ProductionChain chain) : base(chain)
        {
            Buildings = new(chain.Buildings, b =>
            {
                var vm = new IdCountPairVM<BuildingId, Building>(b, Game.BuildingResolver);
                vm.PropertyChanged += (_, _) => RefreshCalculated();
                return vm;
            });
        }

        private ProductionChainOverview _overview = new();
        public ProductionChainOverview Overview
        {
            get => _overview;
            private set => Set(ref _overview, value);
        }

        public string Name
        {
            get => Model.Name;
            set => Set(() => Model.Name, value);
        }

        public SyncedCollection<IdCountPair<BuildingId>, IdCountPairVM<BuildingId, Building>> Buildings { get; }
        public void RefreshCalculated()
        {
            Overview = new ProductionChainOverview(this);
        }

        public ICommand RemoveBuildingCommand => new RelayCommand<IdCountPairVM<BuildingId, Building>>(RemoveBuilding);

        private void RemoveBuilding(IdCountPairVM<BuildingId, Building> vm)
        {
            Buildings.Remove(vm);
        }

    }
}
