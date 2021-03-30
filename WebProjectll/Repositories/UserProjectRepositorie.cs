using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;


namespace WebProjectll.Repositories
{
    
    public class UserProjectRepositorie
    {
        private readonly DatabaseContext _context;
        public UserProjectRepositorie(DatabaseContext context){
            _context = context;
        }

        
    }
}