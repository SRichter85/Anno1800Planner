using Anno1800Planner.Models;
using Anno1800Planner.ViewModels;
using Anno1800Planner.Views;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace Anno1800Planner
{
    public partial class MainWindow : Window
    {

        private MainVM _mainVM;

        public MainWindow()
        {
            InitializeComponent();
            _mainVM = new MainVM();
            DataContext = _mainVM;
        }
    }
}