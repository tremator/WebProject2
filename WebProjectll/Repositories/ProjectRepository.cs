using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Microsoft.AspNetCore.Razor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;


namespace WebProjectll.Repositories
{
    public class ProjectRepository: ControllerBase
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
            var results = _context.Project_user.Where(u => u.Projectsid == id).ToList();
            List<User> userResults = new List<User>();
            foreach (var projectuser in results)
            {
                var user = _context.Users.Find(projectuser.Usersid);
                userResults.Add(user);
            }

            return userResults;

        }
        public Project Create(Project project){
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }
        public Project addUser(ProjectUser info){
            var project = _context.Projects.Find(info.Projectsid);
            var user = _context.Users.Find(info.Usersid);
            project.Users = new List<User>();
            project.Users.Add(user);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return project;
            
        }
        public Project deleteUser(ProjectUser info){
            var project = _context.Projects.Find(info.Projectsid);
            var user = _context.Users.Find(info.Usersid);
            project.Users = getUsers(project.id).ToList();
            project.Users.Remove(user);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return project;
        }
        public Project update(long id, Project project){
            if(id != project.id){
                return null;
            }
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return project;
        }
        public IEnumerable<TimeReport> getReports(long id){
            var results = from timeReports in _context.TimeReports select timeReports;
            return results.Where(t => t.ProyectId == id);
        }

        public IActionResult CSV(ResolveFieldContext<object> graphqlContext){
            var results = from timeReports in _context.TimeReports select timeReports;
            var id = graphqlContext.GetArgument<int>("id");
            results = results.Where((tr => tr.ProyectId == id));
            if(graphqlContext.HasArgument("inicialDate") && graphqlContext.HasArgument("endDate")){
                var initialDateString = graphqlContext.GetArgument<string>("inicialDate");
                var initialDate = DateTime.Parse(initialDateString);
                var endDateString = graphqlContext.GetArgument<string>("endDate");
                var endDate = DateTime.Parse(endDateString);
                results = results.Where(tr => tr.date > initialDate && tr.date < endDate);
                
            }
            var builder = new StringBuilder();
            builder.AppendLine("hours,date");
            foreach (var item in results)
            {
                builder.AppendLine($"{item.Hours},{item.date}");
            }
            var file = File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "projectReport.csv");
            return file;
            
        }

    }
}