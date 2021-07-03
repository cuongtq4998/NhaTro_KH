using System; 
using NhaTroKH.viewmodel;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class FeedbackPageUI : ContentPage
    {
        public FeedbackPageUI()
        {
            InitializeComponent();
            this.BindingContext = new FeedbackPageVM(Navigation);
        }  
    }
}
