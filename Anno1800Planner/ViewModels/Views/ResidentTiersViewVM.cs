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
    public class ResidentTiersViewVM : MainContentViewModelBase
    {
        public ResidentTiersViewVM(MainVM mainVM) : base(mainVM)
        {
            Tiers = [.. Enum.GetValues<TierId>().Select(ResidentTierVM.Create)];
        }

        public ObservableCollection<ResidentTierVM> Tiers { get; }
    }
}
