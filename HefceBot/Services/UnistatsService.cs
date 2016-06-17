using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HefceBot.Models;

namespace HefceBot.Services
{
    public class UnistatsService
    {
        private readonly string _unistatsEndpoint;
        private readonly string _unistatsAuthKey;

        public UnistatsService()
        {
            _unistatsEndpoint = ConfigurationManager.AppSettings["UnistatsEndpoint"];
            _unistatsAuthKey = ConfigurationManager.AppSettings["UnistatsAuthKey"];
        }

        public IEnumerable<Institution> GetInstitutions()
        {
            string auth = $"{_unistatsAuthKey}:";
            var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(auth));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_unistatsEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("Institutions").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Institution>>().Result;
                }
            }

            return null;
        }
    }
}