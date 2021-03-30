using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;


namespace WebProjectll.Repositories
{
    public class ProjectRepository
    {
        private readonly DatabaseContext _context;
        public ProjectRepository(DatabaseContext context){
            _context = context;
        }

        public IEnumerable<Project> filter(ResolveFieldContext<object> graphqlContext){
            var results = from projects in _context.Projects select projects;
            if(graphqlContext.HasArgument("name")){
                var name = graphqlContext.GetArgument<string>("name");
                results = results.Where(p => p.Name.Contains(name));
            }
            if(graphqlContext.HasArgument("description")){
                var name = graphqlContext.GetArgument<string>("description");
                results = results.Where(p => p.Description.Contains(name));
            }
            return results;
        }
       public IEnumerable<User> getUsers(long id){
            var results = from userProjects in _context.Projectusers select userProjects;
            results = results.Where(up => up.ProjectId == id);
            List<User> userResults = new List<User>();
            results.ForEachAsync((userProject) => userResults.Add(_context.Users.Find(userProject.UserId)));

            return userResults;

        }

    }
}