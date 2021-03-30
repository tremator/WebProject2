using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;



namespace WebProjectll.Repositories
{
    public class UserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext  context){
            _context = context;
        }

        public IEnumerable<Project> getProjects(long id){
            var results = from userProjects in _context.Projectusers select userProjects;
            results = results.Where(up => up.UserId == id);
            List<Project> projectResults = new List<Project>();
            results.ForEachAsync((userProject) => projectResults.Add(_context.Projects.Find(userProject.ProjectId)));

            return projectResults;

        }
        
    }
}