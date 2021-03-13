using System;
using System.Collections.Generic;
using NhaTroKH.Model;
using NhaTroKH.Model2.DataPovincialModel;
using NhaTroKH.Service;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace NhaTroKH.DB
{
    public class Data_Tinh
    {
        //config firebase
        private Iconfig iconfig = new Iconfig();
        private IFirebaseClient client;

        static FirebaseResponse provincialRespone, districtRespone, wardRespone;
        

        // property
        private List<TinhModel> arrayTinh = new List<TinhModel>();
        private int idTinh = 0;
        public static List<string> mangtinh = new List<string>()
            {
                "Thành phố Hà Nội",
                "Tỉnh Hà Giang",
                "Tỉnh Cao Bằng",
                "Tỉnh Bắc Cạn",
                "Tỉnh Tuyên Quang",
                "Tỉnh Lào Cai",
                "Tỉnh Điện Biên",
                "Tỉnh Lai Châu",
                "Tỉnh Sơn La",
                "Tỉnh Yên Bái",
                "Tỉnh Hoà Bình",
                "Tỉnh Thái Nguyên",
                "Tỉnh Lạng Sơn",
                "Tỉnh Quảng Ninh",
                "Tỉnh Bắc Giang",
                "Tỉnh Phú Thọ",
                "Tỉnh Vĩnh Phúc",
                "Tỉnh Bắc Ninh",
                "Tỉnh Hải Dương",
                "Thành phố Hải Phòng",
                "Tỉnh Hưng Yên",
                "Tỉnh Thái Bình",
                "Tỉnh Hà Nam",
                "Tỉnh Nam Định",
                "Tỉnh Ninh Bình",
                "Tỉnh Thanh Hóa",
                "Tỉnh Nghệ An",
                "Tỉnh Hà Tĩnh",
                "Tỉnh Quảng Bình",
                 "Tỉnh Quảng Trị",
                "Tỉnh Thừa Thiên Huế",
                "Thành Phố Đà Nẵng",
                "Tỉnh Quảng Nam",
                "Tỉnh Quảng Ngãi",
                "Tỉnh Bình Định",
                 "Tỉnh Phú Yên",
                "Tỉnh Khánh Hòa",
                "Tỉnh Ninh Thuận",
                "Tỉnh Bình Thuận",
                "Tỉnh Kon Tum",
                "Tỉnh Gia Lai",
                "Tỉnh Đắk Lắk",
                "Tỉnh Đắk Nông",
                "Tỉnh Lâm Đồng",
                "Tỉnh Bình Phước",
                "Tỉnh Tây Ninh",
                "Tỉnh Bình Dương",
                 "Tỉnh Đồng Nai",
                 "Tỉnh Bà Rịa - Vũng Tàu",
                "Thành phố Hồ Chí Minh",
                "Tỉnh Long An",
                "Tỉnh Tiền Giang",
                "Tỉnh Bến Tre",
                "Tỉnh Trà Vinh",
                "Tỉnh Đồng Tháp",
                "Tỉnh Vĩnh Long",
                "Tỉnh An Giang",
                "Tỉnh Kiên Giang",
                "Thành phố Cần Thơ",
                 "Tỉnh Hậu Giang",
                "Tỉnh Sóc Trăng",
                "Tỉnh Bạc Liêu",
                "Tỉnh Cà Mau"
            };


        // pakage add list data 
        List<ProvincialModel> provincialListRespone = new List<ProvincialModel>();
        List<DistrictModel> districtListRespone = new List<DistrictModel>();
        List<WardModel> wardListRespone = new List<WardModel>();

        // resutl from firebase 
        List<Tinh> tinhResult = new List<Tinh>();
        List<QuanHuyen> quanhuyenResult = new List<QuanHuyen>();
        List<PhuongXa> phuongxaResult = new List<PhuongXa>();
       
       

        public Data_Tinh()
        {
            client = new FireSharp.FirebaseClient(iconfig.config);
            //addDSTinh();
            addListProvincial();
        }

        private void addDSTinh()
        {
            foreach (var item in mangtinh)
            {
                arrayTinh.Add(new TinhModel(idTinh++, item));
            }
        }

        public List<TinhModel> getArrayTinh
        {
            get { return this.arrayTinh; }
            set { this.arrayTinh = value; }
        }

        // getIndex Tỉnh ở firebase
        public static int getIndexProvincial(string valueProvincial, List<string> provincialList)
        {
            int i;
            for (i = 0; i < provincialList.Count; i++)
            {
                if (valueProvincial == provincialList[i].ToString())
                {
                    break;
                }
            }
            return i + 1;
        }



        //----------------------------------
        //------Test Get Data FireBase------
        //----------------------------------
        // Hàm add object tỉnh vào mảng tỉnh
        int indexProvincial = 1;
        private async void addListProvincial()
        {
            bool checkError = true;

            while (checkError)
            {
                // get data frokm firebase
                provincialRespone = await this.client.GetAsync("TinhThanh/" + 1 + "/");
                Tinh tinh = provincialRespone.ResultAs<Tinh>();
                // check null from firebase
                if (provincialRespone.Body == "null")
                {
                    checkError = false;
                }
                // if not null ->  add into list
                else
                {
                    // Lặp lại toàn bộ tỉnh -> đến hết danh sách tỉnh
                    // ví dụ tinh 1 -> vào quận lấy
                    // ví dụ quạn 1 -> vào xã lấy hết

                    
                    // 1. Index tỉnh
                    provincialListRespone.Add(new ProvincialModel(
                        IndexProvincial: "1",
                        IDProvincial: tinh.MaTinh,
                        NameProvincial: tinh.TenTinh)
                        );
                    // truyền  index tỉnh vào hàm add quận
                    addListDistrict(
                        indexProvincial: indexProvincial.ToString(),
                        idProvincial: tinh.MaTinh.ToString()
                        );
                }

            }
        }

        // Hàm add object huyen vào mang huyện
        int indexDistrict = 1;
        private async void addListDistrict(string indexProvincial, string idProvincial)
        {
            bool checkError = true;
            while (checkError)
            {
                districtRespone = await this.client.GetAsync("TinhThanh/" + indexProvincial +"/QuanHuyen/" + indexDistrict.ToString() + "/");
                QuanHuyen quanHuyen = districtRespone.ResultAs<QuanHuyen>();

                if(districtRespone.Body == "null")
                {
                    checkError = false;
                }
                else
                {
                    // lấy mảng xã add vào  chỗ null
                    // 1. Index Quận/ Huyện
                    // 2. Index Tinh
                    // 2. Mã tỉnh
                    districtListRespone.Add(new DistrictModel(
                        IndexDistrict: indexDistrict.ToString(),
                        IndexProvincial: indexProvincial,
                        IDProvincial: "1",
                        IDDistrict: quanHuyen.MaQuanHuyen.ToString(),
                        NameDistrict: quanHuyen.TenQuanHuyen)
                        );
                    this.addListWard(
                        indexProvincial: indexProvincial,
                        indexDistrict: indexDistrict.ToString(),
                        IDDistrict: quanHuyen.MaQuanHuyen.ToString()
                        );
                }
            }
        }


        // Hàm add object xã vào mảng xã
        int indexWard = 1;
        private async void addListWard(string indexProvincial,string indexDistrict, string IDDistrict)
        {
            bool checkError = true;
            while (checkError)
            {
                wardRespone = await this.client.GetAsync("TinhThanh/1/" + "QuanHuyen/1/" + 1 + "/");
                PhuongXa phuongxa = wardRespone.ResultAs<PhuongXa>();

                if(wardRespone.Body == "null")
                {
                    checkError = false;
                }
                else
                {
                    //hoàn thanh add phường xã
                    // 1.Index Phuong xa
                    // 2.Index Quan huyen
                    // 3.Index Tinh/ Thanh Pho
                    // 3.Mã Quận/ Huyện
                    wardListRespone.Add(new WardModel(
                        IndexWard: indexWard.ToString(),
                        IndexProvincial: indexProvincial,
                        IndexDistrict: indexDistrict,
                        IDDistrict: IDDistrict,
                        IdWard: phuongxa.maPhuongXa.ToString(),
                        NameWard: phuongxa.TenXaPhuong)
                        );
                }
            }
        }

    }
}
