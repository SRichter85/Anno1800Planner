using Anno1800Planner.Common;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Anno1800Planner.Common
{
    /// <summary>
    /// A powerful, self-managing collection that stays in sync with a source list of data.
    /// It automatically creates and manages ViewModel items using the ICreatable contract.
    /// It also provides a unified 'ModelDataChanged' event for all collection or item modifications.
    /// </summary>
    /// <typeparam name="TDbEntry">The type of the raw data entry from the database/source.</typeparam>
    /// <typeparam name="TDataVM">The type of the ViewModel, which must be creatable from the database entry.</typeparam>
    public class SyncedCollection<TDbEntry, TDataVM> : SelectableCollection<TDataVM>
        // The constraint is the key: it ensures TDataVM has a static Create factory method.
        where TDataVM : WrapperVM<TDbEntry>, ICreatable<TDataVM, TDbEntry>
    {
        private readonly IList<TDbEntry> _sourceModelList;

        /// <summary>
        /// Occurs when the collection is modified (items added/removed) OR when a property on an item changes.
        /// The 'sender' will be the collection itself for collection changes, or the specific item for item property changes.
        /// </summary>
        public event PropertyChangedEventHandler? ModelDataChanged;

        public SyncedCollection(IList<TDbEntry> sourceModelList)
        {
            _sourceModelList = sourceModelList;

            // Populate the initial items by calling the static Create method on the ViewModel type.
            // No factory delegate is needed from the outside.
            var viewModels = sourceModelList.Select(dbEntry => TDataVM.Create(dbEntry));
            foreach (var vm in viewModels)
            {
                this.Add(vm); // Use base Add during construction to avoid firing events yet.
            }

            // Now that construction is complete, hook up the events.
            this.CollectionChanged += HandleCollectionChanged;
        }

        protected override void InsertItem(int index, TDataVM viewModel)
        {
            // Subscribe to the new item's property changes.
            viewModel.PropertyChanged += HandleItemPropertyChanged;

            // Sync the underlying model list.
            _sourceModelList.Insert(index, viewModel.Data);
            base.InsertItem(index, viewModel);
        }

        protected override void RemoveItem(int index)
        {
            TDataVM viewModelToRemove = this[index];

            // Unsubscribe to prevent memory leaks.
            viewModelToRemove.PropertyChanged -= HandleItemPropertyChanged;

            // Sync the underlying model list.
            _sourceModelList.Remove(viewModelToRemove.Data);
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var vm in this)
            {
                vm.PropertyChanged -= HandleItemPropertyChanged;
            }
            _sourceModelList.Clear();
            base.ClearItems();
        }

        private void HandleItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Forward the event from the item as a ModelDataChanged event.
            OnModelDataChanged(sender, e);
        }

        private void HandleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            // When the collection changes, raise the unified event with the collection as the sender.
            OnModelDataChanged(this, new PropertyChangedEventArgs("Item[]"));
        }

        protected virtual void OnModelDataChanged(object? sender, PropertyChangedEventArgs e)
        {
            ModelDataChanged?.Invoke(sender, e);
            Database.Instance.MarkDirty(); // Centralize marking the database as dirty.
        }
    }

    // helper class for wrappers specifically around IdCountPair
    public class SyncedCountPairCollection<TDbEntry, TDataVM> : SyncedCollection<IdCountPair<TDbEntry>, IdCountPairVM<TDbEntry, TDataVM>>
        // The constraint is the key: it ensures TDataVM has a static Create factory method.
        where TDataVM : WrapperVM<TDbEntry>, ICreatable<TDataVM, TDbEntry>
    {
        public SyncedCountPairCollection(IList<IdCountPair<TDbEntry>> sourceModelList) : base(sourceModelList)
        {
        }
    }
}
