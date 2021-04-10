namespace WebProjectll.Models
{
    public class ProjectUser
    {
        public long Usersid { get; set; }
        public User User { get; set; }
        public long Projectsid { get; set; }
        public Project Project { get; set; }
    }
}