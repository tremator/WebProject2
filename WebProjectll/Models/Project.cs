using System.Collections.Generic;
namespace WebProjectll.Models
{
    public class Project
    {
     public long id { get; set; }   
     public string Name { get; set; }
     public string Description { get; set; }
     public List<User> Users { get; set; }

     public List<TimeReport> reports { get; set; }
     
    }
}