using Anno1800Planner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public class MainContentViewModelBase : NotifyPropertyChangedBase
    {
        private MainVM _mainVM;
        public MainContentViewModelBase(MainVM mainVM)
        {
            _mainVM = mainVM;
        }

        public MainVM Main { get => _mainVM; }
    }
}
