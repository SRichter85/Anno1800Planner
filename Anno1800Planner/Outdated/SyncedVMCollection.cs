using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Outdated
{


    //public class SyncedVMCollection<TModel, TViewModel> : NotifyPropertyChangedBase
    //    where TViewModel : class, IModelWrapper<TModel>
    //{
    //    private List<TModel>? _source;
    //    private ObservableCollection<TViewModel> _observable = new();

    //    public SyncedVMCollection() { }

    //    public SyncedVMCollection(List<TModel> source)
    //    {
    //        ChangeSource(source);
    //    }

    //    public ObservableCollection<TViewModel> Observable
    //    {
    //        get => _observable;
    //        private set => Set(ref _observable, value);
    //    }

    //    public bool MarkDatabaseOnChange { get; set; } = true;

    //    public IEnumerable<TModel> Source => _source ?? Enumerable.Empty<TModel>();

    //    public void ChangeSource(List<TModel>? source)
    //    {
    //        _source = source;

    //        if (source == null)
    //        {
    //            Observable = new ObservableCollection<TViewModel>();
    //            return;
    //        }

    //        Observable = new ObservableCollection<TViewModel>(source.Select(model => (TViewModel)TViewModel.Wrap(model)));

    //        Observable.CollectionChanged += Observable_CollectionChanged;
    //    }

    //    public void AddSync(TViewModel vm)
    //    {
    //        if (_source == null) return;

    //        _source.Add(vm.Model);
    //        _observable.Add(vm);
    //        MarkDirty();
    //    }

    //    public void RemoveSync(TViewModel vm)
    //    {
    //        if (_source == null) return;

    //        _source.Remove(vm.Model);
    //        _observable.Remove(vm);
    //        MarkDirty();
    //    }

    //    private void Observable_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    //    {
    //        if (_source == null) return;

    //        if (e.NewItems != null)
    //            foreach (TViewModel vm in e.NewItems)
    //                if (!_source.Contains(vm.Model))
    //                    _source.Add(vm.Model);

    //        if (e.OldItems != null)
    //            foreach (TViewModel vm in e.OldItems)
    //                _source.Remove(vm.Model);

    //        MarkDirty();
    //    }

    //    private void MarkDirty()
    //    {
    //        if (MarkDatabaseOnChange)
    //            Database.Instance.MarkDirty();
    //    }
    //}
}
