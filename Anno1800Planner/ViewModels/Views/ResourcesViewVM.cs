using Anno1800Planner.GameData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class ResourcesViewVM : MainContentViewModelBase
    {
        public ResourcesViewVM(MainVM mainVM) : base(mainVM)
        {
            Resources = [.. Game.AllResources() ];
        }

        public ObservableCollection<Resource> Resources { get; }
    }
}
