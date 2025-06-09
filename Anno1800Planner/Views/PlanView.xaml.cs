using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using Anno1800Planner.ViewModels;

namespace Anno1800Planner.Views
{
    public partial class PlanView : UserControl
    {
        private bool _isEditingName = false;

        public PlanView()
        {
            InitializeComponent();
        }

        public IslandVM? SelectedIsland { get; set; }

        private void AddIsland_Click(object sender, RoutedEventArgs e)
        {
            var vm = ViewModel;
            if (vm != null)
            {
                vm.AddIsland($"Island {vm.Islands.Count + 1}");
            }
        }

        private void RemoveIsland_Click(object sender, RoutedEventArgs e)
        {
            var vm = ViewModel;
            if (vm != null)
            {
                if (sender is Button button && button.Tag is IslandVM islandVM)
                {
                    vm.RemoveIsland(islandVM);
                }
            }
        }
        private void PlanName_Click(object sender, RoutedEventArgs e)
        {
            _isEditingName = true;
            UpdateNameEditState();
        }

        private void NameEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            _isEditingName = false;
            UpdateNameEditState();
        }

        private void NameEdit_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                _isEditingName = false;
                UpdateNameEditState();
            }
        }

        private void UpdateNameEditState()
        {
            PlanNameTextBlock.Visibility = _isEditingName ? Visibility.Collapsed : Visibility.Visible;
            PlanNameTextBox.Visibility = _isEditingName ? Visibility.Visible : Visibility.Collapsed;

            if (_isEditingName)
            {
                PlanNameTextBox.Focus();
                PlanNameTextBox.SelectAll();
            }
        }

        private PlanViewVM? ViewModel { get => DataContext as PlanViewVM; }
    }
}
