using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;

namespace Anno1800Planner.ViewModels
{
    public class ListProductionChainVM : WrapperVM<List<ProductionChain>>
    {
        public ListProductionChainVM(List<ProductionChain> data) : base(data)
        {
        }
    }
}
