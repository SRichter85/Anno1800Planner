using Anno1800Planner.GameData;
using Anno1800Planner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ProductionChainView.xaml
    /// </summary>
    public partial class ProductionChainView : UserControl
    {
        private bool _isEditingName = false;

        public ProductionChainView()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
                mw.NavigateToProductionConfigurator();
        }
        private void AllBuildingsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (DataContext is ProductionChainViewVM vm && vm.SelectedBuilding is Building building)
            //{
            //    vm.AddBuilding(building);
            //}
        }

    }
}
