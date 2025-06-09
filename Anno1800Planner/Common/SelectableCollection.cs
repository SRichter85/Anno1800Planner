using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public class SelectableCollection<T> : ObservableCollection<T>, INotifyPropertyChanged
    {
        public SelectableCollection() { }

        public SelectableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        private T? _selectedItem;
        public T? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (!EqualityComparer<T?>.Default.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
                }
            }
        }
    }
}
