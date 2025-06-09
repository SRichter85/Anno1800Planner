using System.Collections.ObjectModel;

namespace Anno1800Planner.Outdated
{
    //public class SyncedCollection<T> : NotifyPropertyChangedBase
    //{
    //    private List<T>? _src;
    //    private ObservableCollection<T> _observable = new();
    //    public SyncedCollection()
    //    {
    //    }

    //    public SyncedCollection(List<T> src)
    //    {
    //        ChangeSource(src);
    //    }

    //    public ObservableCollection<T> Observable
    //    {
    //        get => _observable;
    //        private set => Set(ref _observable, value);
    //    }

    //    public bool MarkDatabaseOnchange { get; set; } = true;

    //    public IEnumerable<T> Source { get => _src ?? new List<T>(); }

    //    public void ChangeSource(List<T>? src)
    //    {
    //        _src = src;
    //        if (src == null)
    //        {
    //            ClearObservable();
    //        }
    //        else
    //        {
    //            CreateObservable(src);
    //        }
    //    }


    //    public void AddSync(T item)
    //    {
    //        var src = _src;
    //        if (src == null)
    //        {
    //            return;
    //        }

    //        src.Add(item);
    //        _observable.Add(item);
    //        MarkDirty();
    //    }

    //    public void RemoveSync(T item)
    //    {
    //        var src = _src;
    //        if (src == null)
    //        {
    //            return;
    //        }

    //        src.Remove(item);
    //        _observable.Remove(item);
    //        MarkDirty();
    //    }
    //    public void ResetSync(IEnumerable<T> items)
    //    {
    //        _src?.Clear();
    //        _observable.Clear();

    //        foreach (var item in items)
    //        {
    //            _src?.Add(item);
    //            _observable.Add(item);
    //        }

    //        MarkDirty();
    //    }

    //    private void MarkDirty()
    //    {
    //        if (MarkDatabaseOnchange) Database.Instance.MarkDirty();
    //    }

    //    private void CreateObservable(IEnumerable<T> initialValues)
    //    {
    //        _observable.CollectionChanged -= Observable_CollectionChanged;
    //        Observable = new ObservableCollection<T>(initialValues);
    //        _observable.CollectionChanged += Observable_CollectionChanged;
    //    }

    //    private void ClearObservable()
    //    {
    //        _observable.Clear();
    //        _observable.CollectionChanged -= Observable_CollectionChanged;
    //    }

    //    private void Observable_CollectionChanged(object? s, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    //    {
    //        if (_src == null) return;

    //        if (e.NewItems != null)
    //            foreach (T item in e.NewItems)
    //                if (!_src.Contains(item)) _src.Add(item);

    //        if (e.OldItems != null)
    //            foreach (T item in e.OldItems)
    //                _src.Remove(item);

    //        MarkDirty();
    //    }
    //}


}
