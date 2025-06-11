using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public class IdCountPair<T> where T: notnull
    {
        public T Id { get; set; }
        public int Count { get; set; }

        public IdCountPair(T item, int count = 0)
        {
            Id = item;
            Count = count;
        }
    }


    public class IdCountPairVM<TId, TChildViewModel> : WrapperVM<IdCountPair<TId>>, ICreatable<IdCountPairVM<TId, TChildViewModel>, IdCountPair<TId>>
        where TId : notnull
        where TChildViewModel : WrapperVM<TId>, ICreatable<TChildViewModel, TId>
    {

        public IdCountPairVM(IdCountPair<TId> data) : base(data)
        {
            // It creates its child ViewModel by calling the static Create method. No factory needed!
            this.ChildViewModel = TChildViewModel.Create(data.Id);
        }

        public TChildViewModel ChildViewModel { get; }

        public int Count
        {
            get => Data.Count;
            set => Set(() => Data.Count, value);
        }

        public static IdCountPairVM<TId, TChildViewModel> Create(IdCountPair<TId> data) => new IdCountPairVM<TId, TChildViewModel>(data);

        public IdCountPair<TId> GetDbEntry() => Data;
    }
}
