using System;
using System.ComponentModel;

namespace NhaTroKH.DB
{ 
    static class KeyCustomerViewEnumeration
    {
        // Key Lưu thông tin khách thuê
        public const string CustomerInforCurrentSite = "CustomerInforCurrentSite";
        public const string CustomerInforHometown = "CustomerInforHometown";
        public const string CustomerInforJobSite = "CustomerInforJobSite";
        public const string CustomerInforResidentSite = "CustomerInforResidentSite";

        public const string CustomerInforName = "CustomerInforName";
        public const string CustomerInforCMNDCreateDate = "CustomerInforCMNDCreateDate";
        public const string CustomerInforBirthday = "CustomerInforBirthday";
        public const string CustomerInforSex = "CustomerInforSex";
        public const string CustomerInforPhone = "CustomerInforPhone";
        public const string CustomerInforDanToc = "CustomerInforDanToc";
        public const string CustomerInforTonGiao = "CustomerInforTonGiao";
        public const string CustomerInforJob = "CustomerInforJob";
        public const string CustomerInforCompany = "CustomerInforCompany";
        public const string CustomerInforEmail = "CustomerInforEmail";
        public const string CustomerInforCartMoto = "CustomerInforCartMoto";
        public const string CustomerInforTypeMoto = "CustomerInforTypeMoto";
        public const string CustomerInforEdu = "CustomerInforEdu";
        public const string CustomerInforProfection = "CustomerInforProfection";

        // Key lưu thông tin quán trình bản thân
        public const string CustomerProcessUserCurrentSite = "CustomerProcessUserCurrentSite";
        public const string CustomerProcessUserJobSite = "CustomerProcessUserJobSite";
        public const string CustomerProcessDBJob = "CustomerProcessDBJob";
        public const string CustomerProcessDBDateTuNgay = "CustomerProcessDBDateTuNgay";
        public const string CustomerProcessDBDateDenNgay = "CustomerProcessDBDateDenNgay";

        // Key lưu thông tin người thân, gia đình
        public const string CustomerFamilyDBgioitinh = "CustomerFamilyDBgioitinh";
        public const string CustomerFamilyDBNAME_ = "CustomerFamilyDBNAME_";
        public const string CustomerFamilyDBDateNgaySinh = "CustomerFamilyDBDateNgaySinh";
        public const string CustomerFamilyDBquanhe = "CustomerFamilyDBquanhe";
        public const string CustomerFamilyCustomerFamily = "CustomerFamilyCustomerFamily";

        // ##Key placeholder địa chỉ thông tin khách hàng
        // Nơi làm việc
        public const string CustomerInforPlaceholder_JobSite = "Nhập nơi làm việc...->"; 
        // Quê quán
        public const string CustomerInforPlaceholder_Hometown = "Nhập quê quán...->";
        // Chỗ ở hiện nay
        public const string CustomerInforPlaceholder_CurentSite = "254/98/19K Âu Cơ, p.9, q.Tân Bình";
        // Thường trú
        public const string CustomerInforPlaceholder_Resident = "Nhập nơi thường trú...->";

        // ## Key placeholder địa chỉ thông tin quá trình bản thân
        // Chỗ ở
        public const string CustomerProcessPlaceholder_Current = "Nhập chỗ ở...->";
        // Nơi làm việc
        public const string CustomerProcessPlaceholder_JobSite = "Nhập nơi làm việc...->";

        // ##Key placeholder địa chỉ người thân gia đình
        // Chỗ ở
        public const string CustomerFamilyPlaceholder_Current = "Nhập chỗ ở...->";

        public const string DefaultAddress = "DefaultAddress";
    }
}
