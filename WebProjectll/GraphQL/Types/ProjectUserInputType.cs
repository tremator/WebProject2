
namespace GraphQL.Types
{
    public class ProjectUserInputType: InputObjectGraphType
    {
        public ProjectUserInputType(){
            Name = "ProjectUserInput";
            Field<NonNullGraphType<IntGraphType>>("usersid");
            Field<NonNullGraphType<IntGraphType>>("projectsid");
        }
    }
}