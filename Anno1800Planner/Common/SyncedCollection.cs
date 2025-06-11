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
        where TDataVM : /*WrapperVM<TDbEntry>,*/ ICreatable<TDataVM, TDbEntry>, INotifyPropertyChanged
    {
        private readonly IList<TDbEntry> _sourceModelList;

        /// <summary>
        /// Occurs when the collection is modified (items added/removed) OR when a property on an item changes.
        /// The 'sender' will be the collection itself for collection changes, or the specific item for item property changes.
        /// </summary>
        public event PropertyChangedEventHandler? ModelDataChanged;

        public SyncedCollection(IList<TDbEntry> sourceModelList) : base(sourceModelList.Select(dbEntry => TDataVM.Create(dbEntry)))
        {
            _sourceModelList = sourceModelList;

            // 2. Now that the initial items exist, subscribe to their events.
            foreach (var vm in this)
            {
                vm.PropertyChanged += HandleItemPropertyChanged;
            }

            // Now that construction is complete, hook up the events.
            this.CollectionChanged += HandleCollectionChanged;
        }

        /// <summary>
        /// A convenience method that removes the currently selected item from the collection.
        /// </summary>
        public void RemoveSelectedItem()
        {
            var selectedItem = SelectedItem;
            if (selectedItem != null)
            {
                SelectedItem = default(TDataVM);
                this.Remove(selectedItem);
            }
        }
        /// <summary>
        /// Creates and adds a ViewModel for the given data entry. If a ViewModel for this
        /// entry already exists, it returns the existing instance.
        /// </summary>
        /// <returns>The new or existing ViewModel for the data entry.</returns>
        public TDataVM AddItem(TDbEntry data)
        {
            // First, check if a VM for this data already exists to avoid duplicates.
            //var existingVm = this.FirstOrDefault(vm => EqualityComparer<TDbEntry>.Default.Equals(vm.Data, data));
            var existingVm = this.FirstOrDefault(vm => EqualityComparer<TDbEntry>.Default.Equals(vm.GetDbEntry(), data));
            if (existingVm != null)
            {
                return existingVm;
            }

            // If not, create it using the static factory method.
            var newVm = TDataVM.Create(data);

            // Add the new VM to this collection. Our overridden InsertItem method will automatically
            // handle adding the 'data' to the _sourceModelList and hooking up events.
            this.Add(newVm);

            return newVm;
        }

        /// <summary>
        /// Finds the ViewModel corresponding to the given data entry and removes it.
        /// </summary>
        public void RemoveItem(TDbEntry data)
        {
            // Find the corresponding ViewModel by searching for the one that wraps this data.
            //var vmToRemove = this.FirstOrDefault(vm => EqualityComparer<TDbEntry>.Default.Equals(vm.Data, data));
            var vmToRemove = this.FirstOrDefault(vm => EqualityComparer<TDbEntry>.Default.Equals(vm.GetDbEntry(), data));

            if (vmToRemove != null)
            {
                // Our overridden RemoveItem method will automatically handle removing the
                // data from the _sourceModelList and unhooking events.
                this.Remove(vmToRemove);
            }
        }

        protected override void InsertItem(int index, TDataVM viewModel)
        {
            // Subscribe to the new item's property changes.
            viewModel.PropertyChanged += HandleItemPropertyChanged;

            // Sync the underlying model list.
            //_sourceModelList.Insert(index, viewModel.Data);
            _sourceModelList.Insert(index, viewModel.GetDbEntry());
            base.InsertItem(index, viewModel);
        }

        protected override void RemoveItem(int index)
        {
            TDataVM viewModelToRemove = this[index];

            // Unsubscribe to prevent memory leaks.
            viewModelToRemove.PropertyChanged -= HandleItemPropertyChanged;

            // Sync the underlying model list.
            //_sourceModelList.Remove(viewModelToRemove.Data);
            _sourceModelList.Remove(viewModelToRemove.GetDbEntry());
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
            DB.MarkDirty(); // Centralize marking the database as dirty.
        }
    }

    // helper class for wrappers specifically around IdCountPair
    public class SyncedCountPairCollection<TDbEntry, TDataVM> : SyncedCollection<IdCountPair<TDbEntry>, IdCountPairVM<TDbEntry, TDataVM>>
        where TDbEntry : notnull
        // The constraint is the key: it ensures TDataVM has a static Create factory method.
        where TDataVM : WrapperVM<TDbEntry>, ICreatable<TDataVM, TDbEntry>
    {
        public SyncedCountPairCollection(IList<IdCountPair<TDbEntry>> sourceModelList) : base(sourceModelList)
        {
        }
    }
}
