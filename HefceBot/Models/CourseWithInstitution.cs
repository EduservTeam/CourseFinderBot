namespace HefceBot.Models
{
    public class CourseWithInstitution : Course
    {
        public Institution Institution { get; set; }

        public string WebUrl
        {
            get
            {
                string kisMode = string.Empty;
                switch (KisMode)
                {
                    case "FullTime":
                        kisMode = "FT";
                        break;
                    case "PartTime":
                        kisMode = "PT";
                        break;
                }

                return $"http://unistats.ac.uk/Subjects/Overview/{Institution.PUBUKPRN}{kisMode}-{KisCourseId}";
            }
        }

        public CourseWithInstitution(Course course)
        {
            ApiUrl = course.ApiUrl;
            KisCourseId = course.KisCourseId;
            KisMode = course.KisMode;
            Title = course.Title;
            TitleInWelsh = course.TitleInWelsh;
        }
    }
}