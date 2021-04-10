using GraphQL.Types;
using WebProjectll.Models;
using WebProjectll.Repositories;



namespace WebProjectll.GraphQL.Types
{
    public class ProjectType: ObjectGraphType<Project>
    {
        public ProjectType(ProjectRepository repository){
            Name = "Project";
            Field(x => x.id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field<ListGraphType<UserType>>(
                "users",
                resolve: context => {
                    return repository.getUsers(context.Source.id);
                }
            );
            Field<ListGraphType<TimeReportType>>(
                "reports",
                resolve: context => {
                    return repository.getReports(context.Source.id);
                }
            );

        }
        
    }
}