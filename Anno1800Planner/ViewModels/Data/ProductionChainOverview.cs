using Anno1800Planner.GameData;
using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.ViewModels
{
    public class ProductionChainOverview
    {
        public int TotalMaintenance { get; }
        public IReadOnlyList<IdCountPair<ResidentTier>> TotalWorkforce { get; }
        public IReadOnlyList<ResourceDeltaEntry> ResourceDeltas { get; }

        public ProductionChainOverview() {
            TotalWorkforce = new List<IdCountPair<ResidentTier>>();
            ResourceDeltas = new List<ResourceDeltaEntry>();
        }
        public ProductionChainOverview(ProductionChainVM chain)
        {
            TotalMaintenance = CalculateMaintenance(chain);
            TotalWorkforce = CalculateWorkforce(chain);
            ResourceDeltas = CalculateResourceDeltas(chain);
        }

        private static int CalculateMaintenance(ProductionChainVM chain) =>
            chain.Buildings.Sum(b => b.Reference.Maintenance * b.Count);

        private static IReadOnlyList<IdCountPair<ResidentTier>> CalculateWorkforce(ProductionChainVM chain) =>
            chain.Buildings
                .GroupBy(b => b.Reference.Tier)
                .Select(g => new IdCountPair<ResidentTier>(
                    Game.TierResolver(g.Key),
                    g.Sum(b => b.Reference.Workforce * b.Count)
                ))
                .ToList();

        private static IReadOnlyList<ResourceDeltaEntry> CalculateResourceDeltas(ProductionChainVM chain)
        {
            var result = new Dictionary<ResourceId, double>();

            foreach (var b in chain.Buildings)
            {
                double factor = b.Count * (60.0 / b.Reference.ProductionTime);

                foreach (var produced in b.Reference.Produces)
                    result.TryAdd(produced, 0);
                foreach (var required in b.Reference.Requires)
                    result.TryAdd(required, 0);

                foreach (var produced in b.Reference.Produces)
                    result[produced] += factor;
                foreach (var required in b.Reference.Requires)
                    result[required] -= factor;
            }

            return result
                .Select(kvp => new ResourceDeltaEntry
                {
                    Resource = Game.ResourceResolver(kvp.Key),
                    Delta = kvp.Value
                })
                .OrderBy(r => r.Resource.Name)
                .ToList();
        }
        public class ResourceDeltaEntry
        {
            public Resource Resource { get; set; } = null!;
            public double Delta { get; set; }
        }
    }
}
