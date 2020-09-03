using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Core.Encoding
{
    public static class Base64Encoding
    {
        public static string Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
