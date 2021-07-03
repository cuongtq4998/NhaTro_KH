using System;
using System.IO;
using NhaTroKH.DB;
using NhaTroKH.viewUI;
using Xamarin.Forms; 

namespace NhaTroKH
{
    public partial class App : Application
    {

        /// <summary>
        /// create DB
        /// </summary>
        static DatabaseSQLite database;

        public static DatabaseSQLite Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseSQLite(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "account.db3"));
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
            var navigationPage = new NavigationPage(new LoadingPageUI());
            navigationPage.BarBackgroundColor = Color.SlateBlue;
            navigationPage.BarTextColor = Color.White;
            MainPage = navigationPage;
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
