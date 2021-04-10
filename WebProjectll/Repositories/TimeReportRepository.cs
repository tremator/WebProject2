
using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using System;

namespace WebProjectll.Repositories
{
    public class TimeReportRepository
    {
        private readonly DatabaseContext _context;
        public TimeReportRepository(DatabaseContext context){
            _context = context;
        }


        public IEnumerable<TimeReport> filter(ResolveFieldContext<object> graphqlContext){
            var results = from timeReports  in _context.TimeReports select timeReports;
            if(graphqlContext.HasArgument("id")){
                var id = graphqlContext.GetArgument<int>("id");
                results = results.Where(p => p.Id == id);
            }
            return results;
        }

        public TimeReport Create(TimeReport timeReport){
            timeReport.date = DateTime.Now;
            _context.TimeReports.Add(timeReport);
            _context.SaveChanges();
            return timeReport;
        }
        public User User(long userId){
            return _context.Users.Find(userId);
        }
        public Project Project(long projectId){
            return _context.Projects.Find(projectId);
        }
        

    }
}