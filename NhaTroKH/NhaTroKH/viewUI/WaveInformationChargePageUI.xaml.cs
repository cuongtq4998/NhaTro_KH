using NhaTroKH.viewmodel;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class WaveInformationChargePageUI : ContentPage
    {
        public WaveInformationChargePageUI()
        {
            InitializeComponent();
            this.BindingContext = new WaveInformationChargePageVM(Navigation); 
        } 
    }
}
