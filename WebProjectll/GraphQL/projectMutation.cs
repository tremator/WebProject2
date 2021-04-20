using GraphQL;
using GraphQL.Types;
using System.Linq;
using WebProjectll.GraphQL.Types;
using WebProjectll.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebProjectll.Repositories;
using Microsoft.AspNetCore.Http;
//using CompanyApi.Repositories;

namespace WebProjectll.GraphQL
{
    public class ProjectMutation: ObjectGraphType
    {
       

        public ProjectMutation(ProjectRepository project, UserRepository repository, ProjectUserRepository userProjectRepositorie, TimeReportRepository timeReportRepository, IHttpContextAccessor accessor){
           Field<ProjectType>("createProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectInputType>> { Name = "input" }),
                resolve: context => project.Create(context.GetArgument<Project>("input"),accessor.HttpContext)
            );
            Field<UserType>("createUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "input" }),
                resolve: context => repository.Create(context.GetArgument<User>("input"),accessor.HttpContext)
            );
            Field<ProjectUserType>("createProjectUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectUserInputType>> { Name = "input" }),
                resolve: context => userProjectRepositorie.Create(context.GetArgument<ProjectUser>("input"),accessor.HttpContext)
            );
            Field<ProjectType>("addUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectUserInputType>> { Name = "input" }),
                resolve: context => project.addUser(context.GetArgument<ProjectUser>("input"),accessor.HttpContext)
            );
            Field<TimeReportType>("createTimeReport",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TimeReportInputType>> { Name = "input" }),
                resolve: context => timeReportRepository.Create(context.GetArgument<TimeReport>("input"),accessor.HttpContext)
            );
            Field<ProjectType>("updateProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectInputType>> { Name = "input"},new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id"} ),
                resolve: context => project.update(context.GetArgument<int>("id"),context.GetArgument<Project>("input"),accessor.HttpContext)
            );
            Field<ProjectType>("deleteUserFromProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectUserInputType>> { Name = "input"}),
                resolve: context => project.deleteUser(context.GetArgument<ProjectUser>("input"),accessor.HttpContext)
            );
            Field<UserType>("deleteUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "input" }),
                resolve: context => repository.deleteUser(context.GetArgument<int>("input"),accessor.HttpContext)
            );
            Field<UserType>("deleteProject",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "input" }),
                resolve: context => project.deleteProject(context.GetArgument<int>("input"),accessor.HttpContext)
            );

        }
        
    }
}