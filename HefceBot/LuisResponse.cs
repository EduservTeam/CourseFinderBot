using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HefceBot.Models;
using Newtonsoft.Json;

namespace HefceBot
{
    public class LuisResponse
    {
        public static async Task<UniLuis> ParseUserInput(string strInput)
        {
            string strRet = string.Empty;
            string strEscaped = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                var luisID = System.Configuration.ConfigurationManager.AppSettings["LuisId"];
                var luisKey = System.Configuration.ConfigurationManager.AppSettings["LuisKey"];
                string uri =
                    $"https://api.projectoxford.ai/luis/v1/application?id={luisID}&subscription-key={luisKey}&q={strEscaped}";
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<UniLuis>(jsonResponse);
                    return _Data;
                }
            }
            return null;
        }
    }
}