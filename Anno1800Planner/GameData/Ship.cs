using Anno1800Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Anno1800Planner.GameData
{
    public class Ship : ContainsId<ShipId>
    {
        public string Name { get; set; }
        public int Maintenance { get; set; }

        public override string ToString() => Name;

        public static void FillDict(Dictionary<ShipId, Ship> dict)
        {
            dict[ShipId.Schooner] = new Ship { Name = "Schooner", Maintenance = 25 };
        }
    }

    public enum ShipId
    {
        Schooner
    }
}
