using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainVM : WrapperVM<Guid, ProductionChain>, ICreatable<ProductionChainVM, Guid>, ICreatable<ProductionChainVM, ProductionChain>
    {
        //public ProductionChainVM(Guid data) : base(data, Database.ProductionChainResolver)
        //{
        //    Buildings = new SyncedCountPairCollection<BuildingId, BuildingVM>(Reference.Buildings);
        //    Buildings.ModelDataChanged += (_, _) => RefreshCalculated();
        //    RemoveBuildingCommand = new RelayCommand(
        //        execute: Buildings.RemoveSelectedItem,
        //        canExecute: () => Buildings.SelectedItem != null
        //    );
        //}

        public ProductionChainVM(ProductionChain model) : base(model)
        {
            Buildings = new SyncedCountPairCollection<BuildingId, BuildingVM>(Reference.Buildings);
            Buildings.ModelDataChanged += (_, _) => RefreshCalculated();
            RemoveBuildingCommand = new RelayCommand(
                execute: Buildings.RemoveSelectedItem,
                canExecute: () => Buildings.SelectedItem != null
            );
        }

        private ProductionChainCalculation _overview = new();

        public ProductionChainCalculation Overview
        {
            get => _overview;
            private set => Set(ref _overview, value);
        }

        public string Name
        {
            get => Reference.Name;
            set => Set(() => Reference.Name, value);
        }

        public ICommand RemoveBuildingCommand;

        public SyncedCountPairCollection<BuildingId, BuildingVM> Buildings { get; }

        public void RefreshCalculated()
        {
            Overview = new ProductionChainCalculation(this);
        }


        public static ProductionChainVM Create(Guid data) => new ProductionChainVM(DB.ProductionChainResolver(data));

        public static ProductionChainVM Create(ProductionChain data) => new ProductionChainVM(data);

        Guid ICreatable<ProductionChainVM, Guid>.GetDbEntry() => Data;

        ProductionChain ICreatable<ProductionChainVM, ProductionChain>.GetDbEntry() => Reference;
    }
}
