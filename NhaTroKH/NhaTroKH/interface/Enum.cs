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

        // Enum type Page
        public enum ETypePageAddAddress
        {
            EInforCustomer,
            EFamilyRelative,
            EProcessUser,
            EDefaultAddress
        }
    }
}
