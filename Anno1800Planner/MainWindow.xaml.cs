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
        // Cached views
        private readonly ResourcesView _gameDataView = new ResourcesView();
        private readonly BuildingsView _buildingDataView = new BuildingsView();
        private readonly ResidentTiersView _residentTiersView = new ResidentTiersView(); 
        private readonly ProductionChainConfigurationView _chainListView = new();
        private readonly ProductionChainView _chainDetailView = new();
        private readonly PlanView _planView = new PlanView();
        private readonly IslandView _islandView = new IslandView();

        private readonly List<PlanViewVM> _planVMs = new();

        private MainVM _mainVM;

        public MainWindow()
        {
            InitializeComponent();
            _mainVM = new MainVM();
            DataContext = _mainVM;
            // Optionally show default view
            /*MainContent.Content = _gameDataView; 
            
            DatabaseIO.Instance.HasUnsavedChangesChanged += (s, hasChanges) =>
            {
                SaveButton.Content = hasChanges ? "Save *" : "Save";
            };


            DatabaseIO.Instance.Load();

            _planVMs.Clear();
            foreach (var model in Database.Plans)
                _planVMs.Add(new PlanVM(model));

            PlanListBox.ItemsSource = null;
            PlanListBox.ItemsSource = _planVMs;
            _chainListView.RefreshData();

            // Optional: select the first plan
            if (_planVMs.Count > 0)
                PlanListBox.SelectedItem = _planVMs[0];*/
        }

        private void GameData_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _gameDataView;
        }

        private void BuildingData_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _buildingDataView;
        }
        private void ResidentTiers_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _residentTiersView;
        }

        private void ProductionChains_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _chainListView;
        }

        public void NavigateToProductionConfigurator()
        {
            //MainContent.Content = _chainListView;
        }

        public void NavigateToProductionChain(ProductionChainViewVM chainVm)
        {
            _chainDetailView.DataContext = chainVm;
            //MainContent.Content = _chainDetailView;
        }

        public void NavigateToPlan(PlanViewVM planVM)
        {
            _planView.DataContext = planVM;
            //MainContent.Content = _planView;
        }

        public void NavigateToIsland(IslandVM island)
        {
            _islandView.DataContext = island;
            //MainContent.Content = _islandView;
        }

        private void AddPlan_Click(object sender, RoutedEventArgs e)
        {
            /*
            var plan = new Plan { Name = $"New Plan {_planVMs.Count + 1}" };
            var planVM = new PlanVM(base.MainVM, plan);

            _planVMs.Add(planVM);

            PlanListBox.ItemsSource = null;
            PlanListBox.ItemsSource = _planVMs;


            Database.Plans.Add(plan);
            DatabaseIO.Instance.MarkDirty();

            // Select the new plan
            PlanListBox.SelectedItem = planVM;
            */
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Database.Instance.Save();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (DatabaseIO.Instance.HasUnsavedChanges)
            {
                var result = MessageBox.Show("You have unsaved changes. Continue and lose changes?", "Warning", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes) return;
            }

            DatabaseIO.Instance.Load();

            _planVMs.Clear();
            foreach (var model in Database.Plans)
                _planVMs.Add(new PlanVM(model));

            PlanListBox.ItemsSource = null;
            PlanListBox.ItemsSource = _planVMs;
            _chainListView.RefreshData();

            // Optional: select the first plan
            if (_planVMs.Count > 0)
                PlanListBox.SelectedItem = _planVMs[0];
            */
        }

        private void PlanSelected(object sender, SelectionChangedEventArgs e)
        {
            if (PlanListBox.SelectedItem is PlanViewVM selectedPlan)
            {
                NavigateToPlan(selectedPlan);
            }
        }
    }
}