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
    public class PlanVM : ModelDataVM<Plan>
    {
        public PlanVM(Plan model) : base(model)
        {
            Ships = new SyncedCollection<IdCountPair<ShipId>, IdCountPairVM<ShipId, Ship>>(
                model.Ships,
                s => new IdCountPairVM<ShipId, Ship>(s, Game.ShipResolver)
            );

            foreach (var shipVM in Ships.ViewModels) shipVM.PropertyChanged += (_, _) => RefreshCalculated();

            Islands = new SyncedCollection<Island, IslandVM>(
                model.Islands,
                i => new IslandVM(i)
            );
        }

        public string Name
        {
            get => Model.Name;
            set => Set(() => Model.Name, value);
        }

        public SyncedCollection<IdCountPair<ShipId>, IdCountPairVM<ShipId, Ship>> Ships { get; }

        public SyncedCollection<Island, IslandVM> Islands { get; }


        public int TotalShipMaintenance => Ships.ViewModels.Sum(vm => vm.Reference.Maintenance * vm.Count);

        private void RefreshCalculated()
        {
            OnPropertyChanged(nameof(TotalShipMaintenance));
        }
    }
}
