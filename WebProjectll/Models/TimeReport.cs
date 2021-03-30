using System;
namespace WebProjectll.Models

{
    public class TimeReport
    {
        
        public long Id { get; set; }
        public double Hours { get; set; }
        public long UserId { get; set; }
        public DateTime date { get; set; }
        public User User { get; set; }
        public long ProyectId { get; set; }
        public Project Proyect { get; set; }
    }
}