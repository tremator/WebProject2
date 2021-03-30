
using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;

namespace WebProjectll.Repositories
{
    public class TimeReportRepository
    {
        private readonly DatabaseContext _context;
        public TimeReportRepository(DatabaseContext context){
            _context = context;
        }


    }
}