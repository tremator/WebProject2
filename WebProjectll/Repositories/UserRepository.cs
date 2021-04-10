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

        public IEnumerable<User> filter(ResolveFieldContext<object> graphqlContext){
            var results = from users in _context.Users select users;
            if(graphqlContext.HasArgument("id")){
                var id = graphqlContext.GetArgument<string>("id");
                results = results.Where(p => p.Name.Contains(id));
            }
            return results;
        }
        public IEnumerable<Project> getProjects(long id){
            var results = _context.Project_user.Where(u => u.Usersid == id).ToList();
            List<Project> projectResults = new List<Project>();
            foreach (var projectuser in results)
            {
                var project = _context.Projects.Find(projectuser.Projectsid);
                projectResults.Add(project);
            }

            return projectResults;

        }
        
        public User deleteUser(long id){
            
            var user = _context.Users.Find(id);
            if (user == null) {
                return null;
            }
            var relations = from projectUser in _context.Project_user select projectUser;
            foreach (var relation in relations)
            {
               if(relation.Usersid == id){
                   _context.Project_user.Remove(relation);
               } 
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        
        public User Create(User user){
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public IEnumerable<TimeReport> getReports(long id){
            var results = from timeReports in _context.TimeReports select timeReports;
            return results.Where(t => t.ProyectId == id);
        }
    }
}