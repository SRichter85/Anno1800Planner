using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.GameData
{
    public class Need : ContainsId<string>
    {
        public ResourceId? Resource { get; set; }
        public BuildingId? Building { get; set; }

        // Only relevant if Resource is set
        public double ConsumptionRate { get; set; }

        public NeedCategory Category { get; set; }

        public int WorkforceBonus { get; set; } = 0;
        public double IncomeBonus { get; set; } = 0;

        public bool IsForResource => Resource != null;
        public bool IsForBuilding => Building != null;

        public static string GetId(TierId tier, Need need)
        {
            string? name = need.Resource?.ToString() ?? need.Building?.ToString();

            if (name == null)
                       throw new InvalidOperationException("Need must have either a Resource or Building.");

            var id = $"{tier}/{name}";
            return id;
        }

        public override string ToString()
        {
            if (Resource != null)
                return $"{Resource} every {ConsumptionRate} min ({Category})";
            if (Building != null)
                return $"{Building} Access ({Category})";
            return $"[Unknown Need]";
        }
    }

    public enum NeedCategory
    {
        Basic,
        Luxury,
        Lifestyle
    }
}
