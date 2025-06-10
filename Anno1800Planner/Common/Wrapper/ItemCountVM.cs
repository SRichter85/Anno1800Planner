using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    //public class ItemCountVM<T> : WrapperVM<IdCountPair<T>>
    //{
    //    private readonly IdCountPair<T> _model;

    //    public T Item => _model.Id;


    //    public int Count
    //    {
    //        get => _model.Count;
    //        set => Set(() => _model.Count, value);
    //    }

    //    public ItemCountVM(IdCountPair<T> model) : base(model)
    //    {
    //        _model = model;
    //    }
    //}

    //public class IdCountPairVM<TId, TRef> : ItemCountVM<TId>
    //{
    //    private readonly Func<TId, TRef> _resolver;

    //    public IdCountPairVM(IdCountPair<TId> model, Func<TId, TRef> resolver)
    //        : base(model)
    //    {
    //        _resolver = resolver;
    //    }

    //    public TRef Reference => _resolver(Item);

    //    public string DisplayName => Reference?.ToString() ?? Item?.ToString() ?? "";
    //}


    //public static class IdCountPairVM
    //{
    //    public static IdCountPairVM<TId, TRef, TViewModel> Create<TId, TRef, TViewModel>(
    //        TViewModel referenceVM
    //    ) where TViewModel : WrapperVM<TId, TRef>
    //    {
    //        var model = new IdCountPair<TId>(referenceVM.Data);
    //        return new IdCountPairVM<TId, TRef, TViewModel>(model, referenceVM);
    //    }
    //}

    //public class IdCountPairVM<TId, TModel, TViewModel> : NotifyPropertyChangedBase
    //    where TViewModel : WrapperVM<TId, TModel>
    //{
    //    public IdCountPair<TId> Model { get; }

    //    public int Count
    //    {
    //        get => Model.Count;
    //        set => Set(() => Model.Count, value);
    //    }

    //    public TId Id => Model.Id;
    //    public TModel Reference => ReferenceVM.Reference;

    //    public TViewModel ReferenceVM { get; }

    //    public IdCountPairVM(IdCountPair<TId> model, TViewModel referenceVM)
    //    {
    //        Model = model;
    //        ReferenceVM = referenceVM;
    //    }
    //}
}
