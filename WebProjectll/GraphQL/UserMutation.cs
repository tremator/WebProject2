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
    public class UserMutation: ObjectGraphType
    {
        public UserMutation(UserRepository repository){
            Field<UserType>("createUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "input" }),
                resolve: context => repository.Create(context.GetArgument<User>("input"))
            );
        }
    }
}