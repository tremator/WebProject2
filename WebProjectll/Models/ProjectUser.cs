namespace WebProjectll.Models
{
    public class ProjectUser
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}