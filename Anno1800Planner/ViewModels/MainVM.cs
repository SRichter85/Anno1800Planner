using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class MainVM : NotifyPropertyChangedBase
    {    
        // Navigation target
        private MainContentViewModelBase? _currentContent;

        private List<PlanViewVM> _plans = new();

        public MainVM()
        {
            Resources = new ResourcesViewVM(this);
            ProductionChains = new ProductionChainConfigurationViewVM(this);
        }

        public MainContentViewModelBase? CurrentContent
        {
            get => _currentContent;
            set => Set(ref _currentContent, value);
        }

        // Exposed sub-view models

        public ProductionChainConfigurationViewVM ProductionChains { get; }

        public ResourcesViewVM Resources { get; }

        public ICommand NavigateToResources => new RelayCommand(() => NavigateTo(Resources));
        public void NavigateTo(MainContentViewModelBase vm)
        {
            CurrentContent = vm;
        }
    }
}
