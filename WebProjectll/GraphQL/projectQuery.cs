using GraphQL;
using GraphQL.Types;
using System.Linq;
using WebProjectll.GraphQL.Types;
using WebProjectll.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebProjectll.Repositories;



namespace WebProjectll.GraphQL
{
    public class ProjectQuery: ObjectGraphType
    {
        public ProjectQuery(ProjectRepository projectRepository, UserRepository userRepository, TimeReportRepository timeReportRepository){
             Field<ListGraphType<ProjectType>>("projects",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "email" },
                    new QueryArgument<StringGraphType> { Name = "name" }
                ),
                resolve: context => projectRepository.filter(context)
            );
             Field<ListGraphType<UserType>>("users",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context => userRepository.filter(context)
            );
            Field<ListGraphType<TimeReportType>>("timeReports",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context => timeReportRepository.filter(context)
            );
            Field<GraphType>("projectReport",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "inicialDate" },
                    new QueryArgument<StringGraphType> { Name = "endDate" }
                ),
                resolve: context => projectRepository.CSV(context)
            );
            
        }
    }
}