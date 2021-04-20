using GraphQL;
using GraphQL.Types;
using System.Linq;
using WebProjectll.GraphQL.Types;
using WebProjectll.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebProjectll.Repositories;
using Microsoft.AspNetCore.Http;

namespace WebProjectll.GraphQL
{
    public class ProjectQuery: ObjectGraphType
    {
        public ProjectQuery(ProjectRepository projectRepository, UserRepository userRepository, TimeReportRepository timeReportRepository, IHttpContextAccessor accessor){
             Field<ListGraphType<ProjectType>>("projects",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "description" },
                    new QueryArgument<StringGraphType> { Name = "name" }
                ),
                resolve: context => projectRepository.filter(context,accessor.HttpContext)
            );
             Field<ListGraphType<UserType>>("users",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context => userRepository.filter(context,accessor.HttpContext)
            );
            Field<ListGraphType<TimeReportType>>("timeReports",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context => timeReportRepository.filter(context,accessor.HttpContext)
            );
            Field<StringGraphType>("projectReport",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "initialDate" },
                    new QueryArgument<StringGraphType> { Name = "endDate" }
                ),
                resolve: context => projectRepository.CSV(context,accessor.HttpContext)
            );
            Field<StringGraphType>("projectReportPDF",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "initialDate" },
                    new QueryArgument<StringGraphType> { Name = "endDate" }
                ),
                resolve: context => projectRepository.PDF(context, accessor.HttpContext)
            );
            Field<StringGraphType>("userReport",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "initialDate" },
                    new QueryArgument<StringGraphType> { Name = "endDate" }
                ),
                resolve: context => userRepository.CSV(context, accessor.HttpContext)
            );
            Field<StringGraphType>("userReportPDF",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "inicialDate" },
                    new QueryArgument<StringGraphType> { Name = "endDate" }
                ),
                resolve: context => userRepository.PDF(context, accessor.HttpContext)
            );
            Field<UserType>("login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }
                ),
                resolve: context => userRepository.login(context.GetArgument<string>("name"),context.GetArgument<string>("password"))
            );

            
        }
    }
}