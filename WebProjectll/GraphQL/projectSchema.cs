using GraphQL;
using GraphQL.Types;


namespace WebProjectll.GraphQL
{
    class ProjectSchema : Schema
    {
        public ProjectSchema(IDependencyResolver resolver) : base(resolver)
        {
            
            Query = resolver.Resolve<ProjectQuery>();
            Mutation = resolver.Resolve<ProjectMutation>();
        }
    }
}