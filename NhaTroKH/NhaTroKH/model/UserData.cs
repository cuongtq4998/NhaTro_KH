using System;
namespace NhaTroKH.model
{
    public class UserData
    {
        public static UserData shared = new UserData();

        public string IDCard { get; set; } = "";

        public UserData()
        {
        }
    }
}
