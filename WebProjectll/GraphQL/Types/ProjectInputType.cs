using GraphQL.Types;
namespace WebProjectll.GraphQL.Types
{
    public class ProjectInputType: InputObjectGraphType
    {
        public ProjectInputType(){
            Name = "ProjectInput";
            Field<IntGraphType>("id");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("description");
        }
    }
}   