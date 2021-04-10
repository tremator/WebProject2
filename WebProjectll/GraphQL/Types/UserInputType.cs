using GraphQL.Types;
namespace WebProjectll.GraphQL.Types
{
    public class UserInputType: InputObjectGraphType
    {
        public UserInputType(){
            Name = "UserInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}