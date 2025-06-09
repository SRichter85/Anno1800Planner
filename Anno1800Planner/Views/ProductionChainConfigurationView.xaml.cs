using Anno1800Planner.Models;
using Anno1800Planner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anno1800Planner.Views
{

    /// <summary>
    /// Interaction logic for ProductionChainsView.xaml
    /// </summary>
    public partial class ProductionChainConfigurationView : UserControl
    {
        private bool _isEditingName = false;

        public ProductionChainConfigurationView()
        {
            InitializeComponent();
            //ViewModel = new ProductionChainsVM();
            //DataContext = ViewModel;
        }

        public ProductionChainConfigurationViewVM ViewModel { get; }

        public void RefreshData()
        {
            ViewModel.RefreshData();
        }
        private void AddChain_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddChain();
        }

        private void DeleteChain_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteChain();
        }

        private void EditChain_Click(object sender, RoutedEventArgs e)
        {
            var selectedChain = ViewModel.SelectedChain;
            if (selectedChain != null)
            {
                if (Application.Current.MainWindow is MainWindow mw)
                    mw.NavigateToProductionChain(selectedChain);
            }
        }

        
    }
}
