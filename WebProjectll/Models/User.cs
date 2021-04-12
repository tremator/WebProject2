using System.Collections.Generic;


namespace WebProjectll.Models
{
    public class User
    {
        
        public long id {get; set;}
        public string token { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Project> Projects { get; set; }

        public List<TimeReport> TimeReports { get; set; }
    }
}