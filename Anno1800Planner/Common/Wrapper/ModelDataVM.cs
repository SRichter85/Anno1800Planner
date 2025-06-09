using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{

    public abstract class ModelDataVM<TModel> : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Note, based on TModel it can be a class or an id
        /// </summary>
        public TModel Model { get; }

        protected ModelDataVM(TModel model)
        {
            Model = model;
        }
    }

    public abstract class ModelDataVM<TId, TRef> : ModelDataVM<TId>
    {

        public Func<TId, TRef> Resolver { get; }

        protected ModelDataVM(TId id, Func<TId, TRef> resolver) : base(id)
        {
            Resolver = resolver;
        }

        public TRef Reference => Resolver(Model);
    }
}
