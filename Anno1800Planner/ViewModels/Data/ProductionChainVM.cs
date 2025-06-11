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
        
        /// <summary>
        /// Adds a building to the chain, or increments its count if it already exists.
        /// This logic is now properly encapsulated within this class.
        /// </summary>
        public void AddBuilding(BuildingVM buildingToAdd)
        {
            var existing = Buildings.FirstOrDefault(b => b.ChildViewModel == buildingToAdd);
            if (existing != null)
            {
                existing.Count++;
            }
            else
            {
                var model = new IdCountPair<BuildingId>(buildingToAdd.Data, 1);
                Buildings.AddItem(model);
            }
        }


        public static ProductionChainVM Create(Guid data) => new ProductionChainVM(DB.ProductionChainResolver(data));

        public static ProductionChainVM Create(ProductionChain data) => new ProductionChainVM(data);

        Guid ICreatable<ProductionChainVM, Guid>.GetDbEntry() => Data;

        ProductionChain ICreatable<ProductionChainVM, ProductionChain>.GetDbEntry() => Reference;
    }
}
