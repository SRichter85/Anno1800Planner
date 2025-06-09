using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using Anno1800Planner.Common;

namespace Anno1800Planner.ViewModels
{
    public class IslandBuildingVM : ModelDataVM<IslandBuildingData>
    {
        public IslandBuildingVM(IslandBuildingData model) : base(model) { }

        public BuildingId Id => Model.Id;

        public int Count
        {
            get => Model.Count;
            set => Set(() => Model.Count, value);
        }

        public int WithTractor
        {
            get => Model.WithTractor;
            set => Set(() => Model.WithTractor, value);
        }

        public int WithSilo
        {
            get => Model.WithSilo;
            set => Set(() => Model.WithSilo, value);
        }

        public int WithFertilizer
        {
            get => Model.WithFertilizer;
            set => Set(() => Model.WithFertilizer, value);
        }

        public bool WithElectricity
        {
            get => Model.WithElectricity;
            set => Set(() => Model.WithElectricity, value);
        }
    }
}
