using System;
using System.Collections.Generic;
using System.IO;
using NhaTroKH.Database;
using NhaTroKH.DB;
using NhaTroKH.viewUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NhaTroKH
{
    public partial class App : Application
    {

        static AccommodationDBsqlLite database;
        // Create the database connection as a singleton.
        public static AccommodationDBsqlLite Database
        {
            get
            {
                if (database == null)
                {
                    database = new AccommodationDBsqlLite(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "DataTinhModel.db3"
                        ));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();  
        }

        protected override void OnStart()
        {
            Console.WriteLine("Start");
            MainPage = new NavigationPage(new LoadingPageUI());
        }

        protected override void OnSleep()
        {
            Console.WriteLine("Sleep");
        }

        protected override void OnResume()
        {
            Console.WriteLine("Resume");
        }
         
    }
}
