using GraphQL.Types;
using WebProjectll.Models;
using WebProjectll.Repositories;

namespace WebProjectll.GraphQL.Types
{
    public class TimeReportType: ObjectGraphType<TimeReport> 
    {
        public TimeReportType(TimeReportRepository repository){
            Name = "TimeReport";
            Field(x => x.Id);
            Field(x => x.Hours);
            Field(x => x.date);
            Field<UserType>(
                "user",
                resolve: context => repository.User(context.Source.UserId)
            );
            Field<ProjectType>(
                "project",
                resolve: context => repository.Project(context.Source.ProyectId)
            );
        }
    }
}