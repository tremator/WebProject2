using GraphQL;
using GraphQL.Types;
using System.Linq;
using WebProjectll.GraphQL.Types;
using WebProjectll.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebProjectll.Repositories;



namespace WebProjectll.GraphQL.Types
{
    public class ProjectQuery: ObjectGraphType
    {
        /*public ProjectQuery(ProjectRepository projectRepository){
             Field<ListGraphType<Project>>("companies",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "email" },
                    new QueryArgument<StringGraphType> { Name = "name" }
                ),
                resolve: context => companyRepository.Filter(context)
            );
        }*/
    }
}