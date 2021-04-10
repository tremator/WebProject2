using GraphQL.Types;
namespace WebProjectll.GraphQL.Types
{
    public class TimeReportInputType: InputObjectGraphType
    {
        public TimeReportInputType(){
            Name = "TimeReportInput";
            Field<NonNullGraphType<FloatGraphType>>("hours");
            Field<NonNullGraphType<IntGraphType>>("userId");
            Field<NonNullGraphType<IntGraphType>>("proyectId");
        }
    }
}