namespace HefceBot.Models
{
    public class CourseWithInstitution : Course
    {
        public Institution Institution { get; set; }

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