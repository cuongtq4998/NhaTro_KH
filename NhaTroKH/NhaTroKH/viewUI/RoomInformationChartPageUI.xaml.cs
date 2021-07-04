using System; 
using NhaTroKH.viewmodel;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class RoomInformationChartPageUI : ContentPage
    {
        public RoomInformationChartPageUI()
        {
            InitializeComponent();
            this.BindingContext = new RoomInformationChartPageVM(Navigation); 
        } 
    }
}

 
