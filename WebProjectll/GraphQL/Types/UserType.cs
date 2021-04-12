using GraphQL.Types;
using WebProjectll.Models;
using WebProjectll.Repositories;


namespace WebProjectll.GraphQL.Types
{
    public class UserType: ObjectGraphType<User>
    {
        public UserType(UserRepository repository)
        {
            Name = "User";
            Field(x => x.id);
            Field(x => x.Name);
            Field(x => x.Password);
            Field(x => x.token);
            Field<ListGraphType<ProjectType>>(
                "projects",
                resolve: context => {
                    return repository.getProjects(context.Source.id);
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