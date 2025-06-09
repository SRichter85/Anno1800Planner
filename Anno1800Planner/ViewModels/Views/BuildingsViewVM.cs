using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Anno1800Planner.Common;
using Anno1800Planner.GameData;

namespace Anno1800Planner.ViewModels
{
    public class BuildingsViewVM : MainContentViewModelBase
    {
        public BuildingsViewVM(MainVM mainVM) : base(mainVM)
        {
            Resources = [.. Game.AllBuildings()];
        }

        public ObservableCollection<Building> Resources { get; }
    }
}
