using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public abstract class ContainsId<TId> where TId : notnull
    {
        protected ContainsId() { }

        [SetsRequiredMembers]
        protected ContainsId(TId id)
        {
            Id = id;
        }

        public required TId Id { get; set; }

        public override string ToString() => Id.ToString() ?? "Unknown ID";
    }
}
