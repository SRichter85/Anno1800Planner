using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anno1800Planner.ViewModels
{
    /// <summary>
    /// A ViewModel that wraps a 'Need' model to prepare its data for display in the UI.
    /// </summary>
    public class NeedVM : WrapperVM<Need>, ICreatable<NeedVM, Need>
    {
        // The constructor is private to enforce creation via the static factory method.
        private NeedVM(Need data) : base(data) { }

        // --- ICreatable Implementation ---
        public static NeedVM Create(Need data) => new NeedVM(data);
        public Need GetDbEntry() => Data;

        // --- GUI-Specific Properties ---

        /// <summary>
        /// Gets the display name for the need, resolving the underlying Resource or Building ID.
        /// </summary>
        public string Name
        {
            get
            {
                if (Data.IsForBuilding && Data.Building.HasValue)
                    return Game.Get(Data.Building.Value).Name;
                if (Data.IsForResource && Data.Resource.HasValue)
                    return Game.Get(Data.Resource.Value).Name;
                return "Unknown";
            }
        }

        public string SuppliedResidentsDisplay
        {
            get
            {
                // Assuming your Need model now has a 'ConsumptionRate' property in t/min
                if (Data.IsForResource && Data.ConsumptionRate > 0)
                {
                    double suppliedResidents = 1.0 / Data.ConsumptionRate;
                    return $"{suppliedResidents:F1} 🏠";
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Controls the visibility of the consumption rate display.
        /// </summary>
        public Visibility IsConsumptionVisible => Data.IsForResource ? Visibility.Visible : Visibility.Collapsed;
    }
}
