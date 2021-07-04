using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.Common;
using NhaTroKH.Models;
using NhaTroKH.Service;
using NhaTroKH.viewmodel;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class CreateAccountPageUI : ContentPage
    {
        public CreateAccountPageUI()
        {
            InitializeComponent();
            this.BindingContext = new CreateAccountPageVM(Navigation); 
        } 
    }
}
