using NhaTroKH.DB;
using NhaTroKH.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NhaTroKH.ViewModel
{
    class TabbarModelViewModel
    {
        private ObservableCollection<TabbarModel> _animals;
        public ObservableCollection<TabbarModel> animal
        {
            get { return _animals; }
            set { _animals = value; }
        }

        public TabbarModelViewModel()
        {
            animal = new ObservableCollection<TabbarModel>();

            DummyDB db = new DummyDB();

            foreach(var item in db.TabbarModels)
            {
                animal.Add(item);
            }
        }
    }
}
