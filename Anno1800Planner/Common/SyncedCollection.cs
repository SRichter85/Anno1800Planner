using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    /// <summary>
    /// An ObservableCollection of ViewModels that stays in sync with a source list of Models.
    /// This class is the single source of truth for all modifications to ensure both collections are synchronized.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the ViewModel, which must wrap a Model.</typeparam>
    /// <typeparam name="TModel">The type of the source Model.</typeparam>
    public class SyncedCollection<TModel, TViewModel> : SelectableCollection<TViewModel>
        where TViewModel : ModelDataVM<TModel> // Your existing constraint is perfect
    {
        private readonly IList<TModel> _sourceModelList;
        private readonly Func<TModel, TViewModel> _viewModelFactory;

        // A dictionary for fast lookups from a Model to its corresponding ViewModel
        private readonly Dictionary<TModel, TViewModel> _modelToViewModelMap = new();

        public SyncedCollection(
            IList<TModel> sourceModelList,
            Func<TModel, TViewModel> viewModelFactory)
            // Pass the initially populated list to the base ObservableCollection constructor
            : base(sourceModelList.Select(viewModelFactory))
        {
            _sourceModelList = sourceModelList;
            _viewModelFactory = viewModelFactory;

            // Populate the lookup dictionary for fast access
            foreach (var vm in this)
            {
                _modelToViewModelMap[vm.Model] = vm;
            }
        }

        // Override the base class methods to inject our sync logic

        protected override void InsertItem(int index, TViewModel viewModel)
        {
            // Add the model to the source list first
            _sourceModelList.Insert(index, viewModel.Model);
            _modelToViewModelMap[viewModel.Model] = viewModel;

            // Now call the base implementation, which will raise the CollectionChanged event
            base.InsertItem(index, viewModel);

            Database.Instance.MarkDirty();
        }

        protected override void RemoveItem(int index)
        {
            // Get the ViewModel we are about to remove
            TViewModel viewModelToRemove = this[index];

            // Remove the corresponding model from the source list
            _sourceModelList.Remove(viewModelToRemove.Model);
            _modelToViewModelMap.Remove(viewModelToRemove.Model);

            // Now call the base implementation
            base.RemoveItem(index);

            Database.Instance.MarkDirty();
        }

        protected override void ClearItems()
        {
            // Clear the underlying model list and map first
            _sourceModelList.Clear();
            _modelToViewModelMap.Clear();

            // Now call the base implementation
            base.ClearItems();

            Database.Instance.MarkDirty();
        }

        // New public methods to handle changes originating from the model side

        /// <summary>
        /// Adds a new model to the source list and creates/adds the corresponding ViewModel to this collection.
        /// </summary>
        public TViewModel AddItem(TModel model)
        {
            var viewModel = _viewModelFactory(model);
            this.Add(viewModel); // This will call our overridden InsertItem method
            return viewModel;
        }

        /// <summary>
        /// Removes a model from the source list and removes the corresponding ViewModel from this collection.
        /// </summary>
        public void RemoveItem(TModel modelToRemove)
        {
            if (_modelToViewModelMap.TryGetValue(modelToRemove, out TViewModel viewModelToRemove))
            {
                this.Remove(viewModelToRemove); // This will call our overridden RemoveItem method
            }
        }
    }
}
