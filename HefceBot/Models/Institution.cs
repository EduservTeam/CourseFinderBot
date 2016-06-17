namespace HefceBot.Models
{
    public class Institution
    {
        public string ApiUrl { get; set; }
        public string Country { get; set; }
        public int? NSSQuestion24 { get; set; }
        public int? NSSQuestion24Population { get; set; }
        public string Name { get; set; }
        public int NumberOfCourses { get; set; }
        public string PUBUKPRN { get; set; }
        public string QAAReportUrl { get; set; }
        public string SortableName { get; set; }
        public string StudentUnionUrl { get; set; }
        public string StudentUnionUrlWales { get; set; }
        public string UKPRN { get; set; }
    }
}