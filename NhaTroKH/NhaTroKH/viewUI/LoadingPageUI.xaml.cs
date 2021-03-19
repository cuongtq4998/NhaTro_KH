using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NhaTroKH.Database;
using NhaTroKH.DB;
using NhaTroKH.DB.SqlLite;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class LoadingPageUI : ContentPage
    {
        public static List<Province> provinces1 = new List<Province>();
        public static List<Distrist> provinces2 = new List<Distrist>();
        public static List<Ward> provinces3 = new List<Ward>();

        public LoadingPageUI()
        {
            InitializeComponent();
            activityIndicatorLoadingPage.IsRunning = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            

            string jsonProvinceFileName = "tinhthanhpho.json";
            string jsonDistristFileName = "quanhuyen.json";
            string jsonWardFileName = "xaphuong.json";

            var assembly = typeof(LoginPageUI).GetTypeInfo().Assembly;
            Stream streamProvince = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonProvinceFileName}");
            Stream streamDistrist = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonDistristFileName}");
            Stream streamward = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonWardFileName}");


            /// danh sach tinh
            using (var reader = new StreamReader(streamProvince))
            {
                var jsonString = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<JObject>(jsonString);
                IList<JToken> values = data.Properties().Select(x => x.First).ToList();

                foreach (var item1 in values)
                {
                    Province pro = new Province();
                    pro = item1.ToObject<Province>();
                    provinces1.Add(pro);
                }
                Console.WriteLine("done!");
            }

            /// danh sach huyen
            using (var reader = new StreamReader(streamDistrist))
            {
                var jsonString = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<JObject>(jsonString);
                IList<JToken> values = data.Properties().Select(x => x.First).ToList();

                foreach (var item2 in values)
                {
                    Distrist pro = new Distrist();
                    pro = item2.ToObject<Distrist>();
                    provinces2.Add(pro);
                }
                Console.WriteLine("done!");
            }
             
            /// danh sach xa 
            using (var reader = new StreamReader(streamward))
            {
                var jsonString = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<JObject>(jsonString);
                IList<JToken> values = data.Properties().Select(x => x.First).ToList();

                foreach (var item3 in values)
                {
                    Ward pro = new Ward();
                    pro = item3.ToObject<Ward>();
                    provinces3.Add(pro);
                }
                Console.WriteLine("done!");
            }
            if (provinces3.Count > 0)
            {
                _ = Navigation.PushAsync(new LoginPageUI());
            }
        } 
    }
}

public partial class Province
{
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("slug")]
    public string slug { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("name_with_type")]
    public string name_with_type { get; set; }
    [JsonProperty("code")]
    public string code { get; set; }
}
public class Distrist
{
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("slug")]
    public string slug { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("name_with_type")]
    public string name_with_type { get; set; }
    [JsonProperty("path")]
    public string path { get; set; }
    [JsonProperty("path_with_type")]
    public string path_with_type { get; set; }
    [JsonProperty("code")]
    public string code { get; set; }
    [JsonProperty("parent_code")]
    public string parent_code { get; set; }
}
public class Ward
{
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("slug")]
    public string slug { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("name_with_type")]
    public string name_with_type { get; set; }
    [JsonProperty("path")]
    public string path { get; set; }
    [JsonProperty("path_with_type")]
    public string path_with_type { get; set; }
    [JsonProperty("code")]
    public string code { get; set; }
    [JsonProperty("parent_code")]
    public string parent_code { get; set; }
}
