﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    /// <summary>
    /// Defines the contract for a ViewModel that can be created from a raw data source entry.
    /// This is the core of the automatic factory pattern.
    /// </summary>
    /// <typeparam name="TSelf">The implementing ViewModel type itself.</typeparam>
    /// <typeparam name="TData">The type of the raw data used for creation.</typeparam>
    public interface ICreatable<TSelf, TData> where TSelf : ICreatable<TSelf, TData>
    {
        /// <summary>
        /// Creates an instance of the ViewModel from a raw data entry.
        /// </summary>
        static abstract TSelf Create(TData data);

        TData GetDbEntry();
    }

    public abstract class WrapperVM<TData> : NotifyPropertyChangedBase where TData: notnull
    {
        /// <summary>
        /// Note, based on TModel it can be a class or an id
        /// </summary>
        public TData Data { get; }

        protected WrapperVM(TData data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// An abstract base class for ViewModels that wrap a a database entry which maps to ContainsId<TId>.
    /// It automatically exposes the reference.
    /// </summary>
    public abstract class WrapperVM<TData, TRef> : WrapperVM<TData> where TData: notnull
        where TRef : ContainsId<TData> // Generic constraint ensures the model has an .Id property
    {
        protected WrapperVM(TRef reference) : base(reference.Id)
        {
            Reference = reference;
        }

        /// <summary>
        /// Gets the ID of the underlying model.
        /// </summary>
        public TRef Reference { get; private set; }
    }
}
