using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class PlanViewVM : MainContentViewModelBase
    {

        public PlanViewVM(MainVM main) : base(main)
        {
        }

        private PlanVM? _plan;
        public PlanVM? Plan
        {
            get => _plan;
            set => Set(ref _plan, value);
        }

    }

}
