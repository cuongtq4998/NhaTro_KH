using System;
namespace NhaTroKH.@interface
{
    public class Enum
    {
        public enum EInforCustomer: int
        {
            Hometown = 0,
            JobSite = 1,
            CurrentSite = 2,
            ResidentSite = 3
        }

        public enum EFamilyRelative: int
        {
            Hometown = 0
        }

        //  enum  detail qua trinh bản than
        public enum EProcessUser: int
        {
            Hometown = 0,
            JobSite = 1
        }


        // enum setting
        public enum ESetting: int
        {
            ResidentSetting = 0, // Địa chỉ thường trú
            HometownSetting = 1, // Địa chỉ quê quán
            DefaultAddress = 2 //  Địa chỉ nhà trọ
        }


        // Enum type Page
        public enum ETypePageAddAddress
        {
            EInforCustomer,
            EFamilyRelative,
            EProcessUser,
            EDefaultAddress, // Phần Setting Địa chỉ nhà trọ 
        }


        /// <summary>
        ///  enum position isAddress
        /// </summary>
        public enum ValueIsAddress
        {
            isResident,
            isAccomodation,
            isCurrentSite ,
            isJobSite
        }
    }
}
