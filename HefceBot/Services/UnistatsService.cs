using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Chronic;
using HefceBot.Models;

namespace HefceBot.Services
{
    public class UnistatsService : IDisposable
    {
        private HttpClient _client;

        public UnistatsService()
        {
            var unistatsEndpoint = ConfigurationManager.AppSettings["UnistatsEndpoint"];
            var unistatsAuthKey = ConfigurationManager.AppSettings["UnistatsAuthKey"];

            var auth = $"{unistatsAuthKey}:";
            var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(auth));

            _client = new HttpClient {BaseAddress = new Uri(unistatsEndpoint)};
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public IEnumerable<Institution> GetInstitutions()
        {
            var response = _client.GetAsync("Institutions?pageSize=1000").Result;

            return response.IsSuccessStatusCode 
                ? response.Content.ReadAsAsync<IEnumerable<Institution>>().Result 
                : null;
        }

        public Institution GetInstitution(string pubukprn)
        {
            var response = _client.GetAsync($"Institution/{pubukprn}").Result;

            return response.IsSuccessStatusCode
                ? response.Content.ReadAsAsync<Institution>().Result
                : null;
        }

        public IEnumerable<Course> GetCoursesForInstitution(string pubukprn)
        {
            var response = _client.GetAsync($"Institution/{pubukprn}/Courses?pageSize=1000").Result;

            return response.IsSuccessStatusCode
                ? response.Content.ReadAsAsync<IEnumerable<Course>>().Result
                : null;
        }

        public IEnumerable<CourseWithInstitution> GetAllCourses()
        {
            var courses = new List<CourseWithInstitution>();

            var institutions = GetInstitutions();
            if (institutions == null) return null;

            institutions.ForEach(i =>
            {
                var institutionCourses = GetCoursesForInstitution(i.PUBUKPRN);
                if (institutionCourses != null)
                {
                    courses.AddRange(institutionCourses.Select(c => new CourseWithInstitution(c) {Institution = i}));
                }
            });

            return courses;
        }

        public void Dispose()
        {
            _client = null;
        }
    }
}