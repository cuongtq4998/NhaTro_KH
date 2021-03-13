using System;
using System.Collections.Generic;
using System.Text;
using NhaTroKH.Model;

namespace NhaTroKH.DB
{
    class DummyDB
    {
        public List<TabbarModel> TabbarModels = new List<TabbarModel>()
        {
            new TabbarModel()
            {
                animalID = 1,
                name = "Asia",
                continentOrigin = "Ant"
            },
            new TabbarModel()
            {
                animalID = 2,
                name = "Europe",
                continentOrigin = "Frog"
            },
            new TabbarModel()
            {
                animalID = 3,
                name = "Asia",
                continentOrigin = "Asian Elephant"
            },
            new TabbarModel()
            {
                animalID = 4,
                name = "Europe",
                continentOrigin = "Grasshopper"
            },
            new TabbarModel()
            {
                animalID = 5,
                name = "Europe",
                continentOrigin = "Mouse"
            },
            new TabbarModel()
            {
                animalID = 6,
                name = "Europe",
                continentOrigin = "Mosquito"
            },
            new TabbarModel()
            {
                animalID = 7,
                name = "Africa",
                continentOrigin = "Antelope"
            },
            new TabbarModel()
            {
                animalID = 8,
                name = "Africa",
                continentOrigin = "Camel"
            },
            new TabbarModel()
            {
                animalID = 9,
                name = "Africa",
                continentOrigin = "Egret"
            }
        };
    }
}
