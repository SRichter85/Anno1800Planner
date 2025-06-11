using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Anno1800Planner.GameData
{
    public class Ship : ContainsId<ShipId>
    {
        public Ship()
        {
        }

        [SetsRequiredMembers]
        public Ship(ShipId id, string name, int maintenance) : base(id)
        {
            Name = name;
            Maintenance = maintenance;
        }

        public string Name { get; set; } = string.Empty;
        public int Maintenance { get; set; }

        public override string ToString() => Name;

        public static IEnumerable<Ship> CreateDefault()
        {
            yield return new Ship(ShipId.Schooner, "Schooner", 25);
        }
    }

    public enum ShipId
    {
        Schooner
    }
}
