using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.model
{
    public class AddressSettingModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool isSelected;
        public bool IsSelected
        {
            set
            {
                isSelected = value;
                onPropertyChanged();
            }
            get => isSelected;
        }

        string titleAddress;
        public string TitleAddress
        {
            set
            {
                titleAddress = value;
            }
            get => titleAddress;
        }

        ValueIsAddress valueIsAddress;
        public ValueIsAddress ValueIsAddress
        {
            set
            {
                valueIsAddress = value;
            }
            get => valueIsAddress;
        }

    }
}
