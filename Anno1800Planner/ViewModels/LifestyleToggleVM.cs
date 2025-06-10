using Anno1800Planner.Common;
using Anno1800Planner.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.ViewModels
{
    public class LifestyleToggleVM : WrapperVM<Need>
    {
        private readonly List<string> _modelList;

        public LifestyleToggleVM(Need need, List<string> modelList)
            : base(need)
        {
            _modelList = modelList;
        }

        public string DisplayName => Game.ResourceResolver(Data.Resource.Value).Name;

        private string Id => Data.Id;

        public bool IsEnabled
        {
            get => _modelList.Contains(Id);
            set
            {
                if (value)
                {
                    if (!_modelList.Contains(Id))
                        _modelList.Add(Id);
                }
                else
                {
                    _modelList.Remove(Id);
                }
                OnPropertyChanged();
            }
        }
    }
}
