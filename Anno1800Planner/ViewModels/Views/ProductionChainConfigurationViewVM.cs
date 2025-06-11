using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Anno1800Planner.Common;
using System.Windows.Input;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainConfigurationViewVM : MainContentViewModelBase
    {

        public ProductionChainConfigurationViewVM(MainVM mainVM) : base(mainVM)
        {
            AddChainCommand = new RelayCommand(() => _productionChains?.AddItem(new ProductionChain() { Name = "New Chain" }));
            DeleteSelectedChainCommand = new RelayCommand(() => _productionChains?.RemoveSelectedItem());
            EditSelectedChainCommand = new RelayCommand(() => {
                var sel = _productionChains?.SelectedItem;
                if (sel != null)
                {
                    Main.ProductionChain.Chain = sel;
                    Main.NavigateTo(Main.ProductionChain);
                }
            });
        }

        private SyncedCollection<ProductionChain, ProductionChainVM>? _productionChains;
        public SyncedCollection<ProductionChain, ProductionChainVM>? ProductionChains
        {
            get => _productionChains;
            set => Set(ref _productionChains, value);
        }

        public ICommand AddChainCommand { get; }
        public ICommand DeleteSelectedChainCommand { get; }
        public ICommand EditSelectedChainCommand { get; }
    }
}
