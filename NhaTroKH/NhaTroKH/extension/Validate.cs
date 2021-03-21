using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NhaTroKH.Common
{
    class Validate
    {
        //validate CMND digit 9 number
        public static void validateCMND(object sender)
        {
            var textChange = (Entry)sender;
            if (textChange.Text.Length > 9)
            {
                string entryMax = textChange.Text;

                entryMax = entryMax.Remove(entryMax.Length - 1);

                textChange.Text = entryMax;
            }
        }
    }
}
