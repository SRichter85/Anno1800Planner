using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public class SyncedCollection<TModel, TViewModel> where TViewModel : ModelDataVM<TModel>
    {
        public SelectableCollection<TViewModel> ViewModels { get; }

        private readonly IList<TModel> _modelList;

        public SyncedCollection(
            IList<TModel> modelList,
            Func<TModel, TViewModel> factory)
        {
            _modelList = modelList;

            ViewModels = [.. modelList.Select(factory)];

            ViewModels.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (TViewModel vm in e.NewItems!)
                            _modelList.Add(vm.Model);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (TViewModel vm in e.OldItems!)
                            _modelList.Remove(vm.Model);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        _modelList.Clear();
                        break;
                }

                Database.Instance.MarkDirty();
            };
        }
    }
}
