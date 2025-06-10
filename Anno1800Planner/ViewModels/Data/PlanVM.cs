using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.ViewModels
{
    public class PlanVM : WrapperVM<Plan>
    {
        public PlanVM(Plan model) : base(model)
        {
            Ships = new SyncedCountPairCollection<ShipId, ShipVM>(model.Ships);
            Ships.ModelDataChanged += (_, _) => RefreshCalculated();
            Islands = new SyncedCollection<Island, IslandVM>(model.Islands);
        }

        public string Name
        {
            get => Data.Name;
            set => Set(() => Data.Name, value);
        }

        public SyncedCountPairCollection<ShipId, ShipVM> Ships { get; }

        public SyncedCollection<Island, IslandVM> Islands { get; }


        public int TotalShipMaintenance => Ships.Sum(vm => vm.ChildViewModel.Reference.Maintenance * vm.Count);

        private void RefreshCalculated()
        {
            OnPropertyChanged(nameof(TotalShipMaintenance));
        }
    }
}
