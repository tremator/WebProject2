using GraphQL;
using GraphQL.Types;
using System.Linq;
using WebProjectll.GraphQL.Types;
using WebProjectll.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using CompanyApi.Repositories;

namespace WebProjectll.GraphQL.Types
{
    public class ProjectMutation: ObjectGraphType
    {
        private readonly DatabaseContext _context;

        public ProjectMutation(DatabaseContext context){
            _context = context;
        }
        public Project Create(Project project){
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }
    }
}