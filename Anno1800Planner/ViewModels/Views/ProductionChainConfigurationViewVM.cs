using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainConfigurationViewVM : MainContentViewModelBase
    {
        public ObservableCollection<ProductionChainViewVM> ProductionChains { get; } = new();

        private ProductionChainViewVM? _selectedChain;

        public ProductionChainConfigurationViewVM(MainVM mainVM) : base(mainVM)
        {
        }

        public ProductionChainViewVM? SelectedChain
        {
            get => _selectedChain;
            set => Set(ref _selectedChain, value);
        }

        public void RefreshData()
        {
            /*
            ProductionChains.Clear();
            foreach (var model in Database.ProductionChains)
                ProductionChains.Add(new ProductionChainVM(model));

            // Optional: select the first plan
            if (ProductionChains.Count > 0)
                SelectedChain = ProductionChains[0];
            else
                SelectedChain = null;
            */
        }

        internal void AddChain()
        {
            /*
            var newChain = new ProductionChain { Name = $"Chain {ProductionChains.Count + 1}" };
            Database.ProductionChains.Add(newChain);

            var newVM = new ProductionChainVM(newChain);
            ProductionChains.Add(newVM);
            SelectedChain = newVM;
            */
        }

        internal void DeleteChain()
        {
            if (_selectedChain != null)
            {
                var result = MessageBox.Show(
                    $"Delete production chain '{_selectedChain.Name}'?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    Database.Instance.ProductionChains.Remove(_selectedChain.Chain);
                    ProductionChains.Remove(_selectedChain);
                    SelectedChain = null;
                }
            }
        }
    }
}
