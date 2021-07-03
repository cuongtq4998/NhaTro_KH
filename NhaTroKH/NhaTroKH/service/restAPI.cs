using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.extension;
using NhaTroKH.Models;
using NhaTroKH.Service;

namespace NhaTroKH.service
{
    public class restAPI
    {
        private Iconfig iconfig = new Iconfig();
        private FirebaseResponse response;
        private SetResponse setResponse;
        private IFirebaseClient client;
        public static restAPI shared = new restAPI();
        IFirebaseConfig _config;
        HttpClient _httpClient;

        public restAPI()
        {
            _config = iconfig.config;
            client = new FireSharp.FirebaseClient(_config);
            _httpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true });
            var basePath = _config.BasePath.EndsWith("/") ? _config.BasePath : _config.BasePath + "/";
            _httpClient.BaseAddress = new Uri(basePath);

            if (_config.RequestTimeout.HasValue)
            {
                _httpClient.Timeout = _config.RequestTimeout.Value;
            }
        }

   

        public async Task<LOGIN> LoginAPI(string IDCard)
        { 
            return await Task.Run(async () =>
            { 
                response = await client.GetAsync("taikhoan/" + IDCard.ToString());
                LOGIN loginModel = response.ResultAs<LOGIN>(); 
                return loginModel;
            }); 
        }

        // insert water
        public async Task<bool> InsertWaterInfor(
            string pagramElectric,
            string pagramWater,
            string pagramMotobikeNumber,
            string pagramMotobike,
            string years,
            string month,
            string room
            )
        { 
            var data = new DIENNUOC
            {
                SO_DIEN = pagramElectric,
                SO_NUOC = pagramWater,
                SO_XE_SO_KHACH = pagramMotobikeNumber,
                SO_XE_TAY_GA_KHACH = pagramMotobike
            };
            return await Task.Run(async () => {
                setResponse = await client.SetAsync("diennuoc/" + years + "/" + month + "/" + room + "/", data);
                if(setResponse.Body != "null")
                {
                    return true;
                }
                return false;
            });
        }

        // get infor water, electric
        public async Task<DIENNUOC> getWaterInfor(string years, string month, string room)
        {
            return await Task.Run(async () => {
                response = await client.GetAsync("diennuoc/" + years + "/" + month + "/" + room);
                DIENNUOC electricWater = response.ResultAs<DIENNUOC>(); 
                return electricWater;
            });
        }

        private async Task<List<FEEDBACK>> getInforFeedback(string numRoom)
        { 
            return await Task.Run(async () => { 
                response = await client.GetAsync("ThongTinPhanHoi/" + numRoom + "/");
                if (response.Body == "null") return new List<FEEDBACK>();
                return response.ResultAs<List<FEEDBACK>>(); 
            }); 
        }

        public async Task<bool> insertFeedback(string numRoom, FEEDBACK feedbackInput)
        {
            var index = 1;
            SetResponse response;
            var inforFeedback = await getInforFeedback(numRoom);
            var listSort = inforFeedback
                .Where(item => item != null)
                .OrderBy(item => item.STT).ToList();
            if(listSort.Count > 0)
            {
                // has item with room current
                var itemLast = listSort.LastOrDefault();
                var numIndexLast = itemLast.STT.toInt32();
                feedbackInput.STT = (numIndexLast + index).ToString();
                response = await client.SetAsync("ThongTinPhanHoi/" + numRoom + "/" + (numIndexLast + index).ToString(), feedbackInput);

                if (response.StatusCode == System.Net.HttpStatusCode.OK) { return true; }
            }
            else
            {
                // none item with room current
                feedbackInput.STT = index.ToString();
                response = await client.SetAsync("ThongTinPhanHoi/" + numRoom + "/" + index.ToString(), feedbackInput);
                if (response.StatusCode == System.Net.HttpStatusCode.OK) { return true; }
            }
            return false;
        }

    }
}
