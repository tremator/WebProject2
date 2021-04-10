using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;



namespace WebProjectll.Repositories
{
    public class ProjectUserRepository
    {
        private readonly DatabaseContext _context;

        public ProjectUserRepository(DatabaseContext  context){
            _context = context;
        }

        
        public ProjectUser Create(ProjectUser projectUser){
            _context.Project_user.Add(projectUser);
            _context.SaveChanges();
            return projectUser;
        }
    }
}