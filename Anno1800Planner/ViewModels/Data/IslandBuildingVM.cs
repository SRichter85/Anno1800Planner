using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1800Planner.GameData;
using Anno1800Planner.Models;
using Anno1800Planner.Common;
using System.Net;

namespace Anno1800Planner.ViewModels
{
    public class IslandBuildingVM : WrapperVM<IslandBuildingData>, ICreatable<IslandBuildingVM, IslandBuildingData>
    {
        public IslandBuildingVM(IslandBuildingData model) : base(model) { }

        public BuildingId Id => Data.Id;

        public int Count
        {
            get => Data.Count;
            set => Set(() => Data.Count, value);
        }

        public int WithTractor
        {
            get => Data.WithTractor;
            set => Set(() => Data.WithTractor, value);
        }

        public int WithSilo
        {
            get => Data.WithSilo;
            set => Set(() => Data.WithSilo, value);
        }

        public int WithFertilizer
        {
            get => Data.WithFertilizer;
            set => Set(() => Data.WithFertilizer, value);
        }

        public bool WithElectricity
        {
            get => Data.WithElectricity;
            set => Set(() => Data.WithElectricity, value);
        }

        public static IslandBuildingVM Create(IslandBuildingData data) => new IslandBuildingVM(data);
    }
}
