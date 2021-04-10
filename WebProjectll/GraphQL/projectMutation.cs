using GraphQL;
using GraphQL.Types;
using System.Linq;
using WebProjectll.GraphQL.Types;
using WebProjectll.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebProjectll.Repositories;
//using CompanyApi.Repositories;

namespace WebProjectll.GraphQL
{
    public class ProjectMutation: ObjectGraphType
    {
       

        public ProjectMutation(ProjectRepository project, UserRepository repository, ProjectUserRepository userProjectRepositorie, TimeReportRepository timeReportRepository){
           Field<ProjectType>("createProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectInputType>> { Name = "input" }),
                resolve: context => project.Create(context.GetArgument<Project>("input"))
            );
            Field<UserType>("createUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "input" }),
                resolve: context => repository.Create(context.GetArgument<User>("input"))
            );
            Field<ProjectUserType>("createProjectUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectUserInputType>> { Name = "input" }),
                resolve: context => userProjectRepositorie.Create(context.GetArgument<ProjectUser>("input"))
            );
            Field<ProjectType>("addUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectUserInputType>> { Name = "input" }),
                resolve: context => project.addUser(context.GetArgument<ProjectUser>("input"))
            );
            Field<TimeReportType>("createTimeReport",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TimeReportInputType>> { Name = "input" }),
                resolve: context => timeReportRepository.Create(context.GetArgument<TimeReport>("input"))
            );
            Field<ProjectType>("updateProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectInputType>> { Name = "input"},new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id"} ),
                resolve: context => project.update(context.GetArgument<int>("id"),context.GetArgument<Project>("input"))
            );
            Field<ProjectType>("deleteUserFromProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectUserInputType>> { Name = "input"}),
                resolve: context => project.deleteUser(context.GetArgument<ProjectUser>("input"))
            );
             Field<UserType>("deleteUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "input" }),
                resolve: context => repository.deleteUser(context.GetArgument<int>("input"))
            );

        }
        
    }
}