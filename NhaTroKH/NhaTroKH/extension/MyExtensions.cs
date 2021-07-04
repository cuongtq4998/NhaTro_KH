using System;
namespace NhaTroKH.extension
{
    public static class MyExtensions
    {

        /// <summary>
        /// ******* CONVERT FROM STRING
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        // from string to int
        public static int toInt32(this string value)
        {
            if (!value.Trim().ToLower().Equals(null)) {
                return Int32.Parse(value.Trim().ToLower());
            }
            return 0;
        }

        public static bool isNullOrEmpty(this string value)
        { 
            if(value.Trim() == "" || value == null)
            {
                return true;
            }
            return false;

        }

        // convert to Amount VN
        public static string toAmountVN(this string value, string formart = "{0:0,0} VNĐ")
        {
            if (value != null)
            {
                if (!value.isNullOrEmpty())
                {
                    return string.Format(formart, int.Parse(value));
                }
                return "0";
            }
            return "0";
        }

        /// <summary>
        /// ******* CONVERT FROM DATE
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>

        // format Date to string
        public static string toDateWithFormat(this DateTime dateTime, string format = "yyyy/MM/dd HH:mm:ss")
        { 
            return dateTime.ToString(format);
        }
    }
}
