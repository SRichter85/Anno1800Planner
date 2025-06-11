using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            Buildings = new BuildingsViewVM(this);
            ResidentTiers = new ResidentTiersViewVM(this);
            ProductionChainConfiguration = new ProductionChainConfigurationViewVM(this);
            ProductionChain = new ProductionChainViewVM(this);
            DB.Load();
        }

        public MainContentViewModelBase? CurrentContent
        {
            get => _currentContent;
            set => Set(ref _currentContent, value);
        }

        // Exposed sub-view models


        public ResourcesViewVM Resources { get; }
        public BuildingsViewVM Buildings { get; }
        public ResidentTiersViewVM ResidentTiers { get; }
        public ProductionChainConfigurationViewVM ProductionChainConfiguration { get; }

        public ProductionChainViewVM ProductionChain { get; }

        public ICommand NavigateToResources => new RelayCommand(() => NavigateTo(Resources));
        public ICommand NavigateToBuildings => new RelayCommand(() => NavigateTo(Buildings));
        public ICommand NavigateToResidentTiers => new RelayCommand(() => NavigateTo(ResidentTiers));
        public ICommand NavigateToProductionChainConfiguration => new RelayCommand(() =>
        {
            ProductionChainConfiguration.ProductionChains = new SyncedCollection<Models.ProductionChain, ProductionChainVM>(DB.ProductionChains);
            NavigateTo(ProductionChainConfiguration);
        });

        public ICommand Save => new RelayCommand(() => DB.Save());
        public ICommand Load => new RelayCommand(() =>
        {
            try
            {
                DB.Load();
                // Maybe show a success message
            }
            catch (Exception ex)
            {
                // This is where you show the MessageBox to the user
                MessageBox.Show($"Error loading data: {ex.Message}", "Load Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

        public ICommand OpenSavePath => new RelayCommand(() =>
        {
            try
            {
                string? directoryPath = Path.GetDirectoryName(DB.GetSavePath());
                if (directoryPath != null && Directory.Exists(directoryPath))
                {
                    // Use Process.Start with a ProcessStartInfo object for robustness.
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = directoryPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
            }
            catch (Exception)
            {
                // Handle exceptions if the folder cannot be opened for any reason.
                // This could be a permission issue. For now, we fail silently.
            }
        });


        public void NavigateTo(MainContentViewModelBase vm)
        {
            CurrentContent = vm;
        }
    }
}
