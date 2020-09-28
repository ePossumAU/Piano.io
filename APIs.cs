using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Xml;

namespace PianoFileMerge2
{

    /*Convert Json to C# https://json2csharp.com/Convert any JSON object to a C# class online. 
     * Check out the help panel below to view details on how to use this converter.*/

    public class PianoAPIs : System.Web.UI.Page
    {

        /*Common headers are considered restricted and are 
         * either exposed directly by the API(such as Content-Type) 
         * or protected by the system and cannot be changed.*/

        private static string[] RestrictedHeaders = new string[] {
                "Accept",
                "Connection",
                "Content-Length",
                "Content-Type",
                "Date",
                "Expect",
                "Host",
                "If-Modified-Since",
                "Keep-Alive",
                "Proxy-Connection",
                "Range",
                "Referer",
                "Transfer-Encoding",
                "User-Agent"
        };

        /*Test restricted headers before processing*/
        private bool IsRestrictedHeader(string customHeader)
        {
            var retValue = false;
            foreach (string header in RestrictedHeaders)
            {
                if (customHeader == header)
                    retValue = true;
            }

            return retValue;
        }

        public class UserSearch
        {
            [Newtonsoft.Json.JsonProperty("code")]
            public int Code { get; set; }

            [Newtonsoft.Json.JsonProperty("ts")]
            public int Ts { get; set; }

            [Newtonsoft.Json.JsonProperty("limit")]
            public int Limit { get; set; }

            [Newtonsoft.Json.JsonProperty("offset")]
            public int Offset { get; set; }

            [Newtonsoft.Json.JsonProperty("total")]
            public int Total { get; set; }

            [Newtonsoft.Json.JsonProperty("count")]
            public int Count { get; set; }

            [Newtonsoft.Json.JsonProperty("users")]
            public List<UserDetails> Users { get; set; }
        }

        public class UserDetails
        {
            [Newtonsoft.Json.JsonProperty("first_name")]
            public string FirstName { get; set; }

            [Newtonsoft.Json.JsonProperty("last_name")]
            public string LastName { get; set; }

            [Newtonsoft.Json.JsonProperty("personal_name")]
            public string PersonalName { get; set; }

            [Newtonsoft.Json.JsonProperty("email")]
            public string Email { get; set; }

            [Newtonsoft.Json.JsonProperty("uid")]
            public string Uid { get; set; }

            [Newtonsoft.Json.JsonProperty("image1")]
            public object Image1 { get; set; }

            [Newtonsoft.Json.JsonProperty("create_date")]
            public int CreateDate { get; set; }

            [Newtonsoft.Json.JsonProperty("reset_password_email_sent")]
            public bool ResetPasswordEmailSent { get; set; }

            [Newtonsoft.Json.JsonProperty("custom_fields")]
            public List<object> CustomFields { get; set; }
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string[] GetUserDetails(string searchEmail)
        {
            string[] retValue = new string[2] { string.Empty, string.Empty };

            //URI builder for user/search
            var appDomain = "https://sandbox.tinypass.com";
            var appId = "o1sRRZSLlw";
            var appToken = "zziNT81wShznajW2BD5eLA4VCkmNJ88Guye7Sw4D";
            var appFolder = "user/search";
            var maxRecords = 1;
            var apiString = $"{appDomain}/api/v3/publisher/{appFolder}?" +
                 $"aid={appId}&" +
                 $"api_token={appToken}&" +
                 $"email={searchEmail}&" +
                 $"limit={maxRecords}&offset=0";

            var userData = Get(apiString);

            UserSearch jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<UserSearch>(userData);

            if (jsonResult.Users.Count > 0)
            {
                string systemEmail = jsonResult.Users[0].Email;
                string[] wordSplit = systemEmail.Split('@');

                //Email address without Host portion.
                retValue[0] = wordSplit[0];
                retValue[1] = jsonResult.Users[0].Uid; //To be used to replace csv UID.

                return retValue;
            }
            else
                return retValue;
        }
    }

}