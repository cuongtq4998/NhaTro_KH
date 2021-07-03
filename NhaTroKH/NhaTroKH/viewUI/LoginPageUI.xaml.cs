using System.ComponentModel; 
using NhaTroKH.Common; 
using NhaTroKH.viewmodel; 
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace NhaTroKH.viewUI
{
    [DesignTimeVisible(false)]
    public partial class LoginPageUI : ContentPage
    {
        public LoginPageUI()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = new LoginPageVM(Navigation);
        }  

        private void CMND_login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate.validateCMND(sender);
        }

    } 
} 