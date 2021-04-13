using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace WebProjectll.Repositories
{
    public class ProjectUserRepository
    {
        private readonly DatabaseContext _context;

        public ProjectUserRepository(DatabaseContext  context){
            _context = context;
        }

        
        public ProjectUser Create(ProjectUser projectUser, HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }

            _context.Project_user.Add(projectUser);
            _context.SaveChanges();
            return projectUser;
        }
        private bool validateToken(HttpContext accesor){

            var headers = accesor.Request.Headers.ToList();
            var accesToken = headers.Where(h => h.Key == "Authorization").Single();
            var users = _context.Users.ToList();
            User user = users.Find(u => u.token == accesToken.Value.ToString());
            if(user == null){
                return false;
            }else{
                return true;
            }
        }
    }
}