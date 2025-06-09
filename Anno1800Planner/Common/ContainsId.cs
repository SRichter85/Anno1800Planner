using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public abstract class ContainsId<TId>
    {
        public TId Id { get; set; } = default!;

        public override string ToString() => Id?.ToString() ?? base.ToString();
    }
}
