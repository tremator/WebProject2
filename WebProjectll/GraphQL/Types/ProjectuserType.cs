using GraphQL.Types;
using WebProjectll.Models;

namespace WebProjectll.GraphQL.Types
{
    public class ProjectUserType: ObjectGraphType<ProjectUser>
    {
        public ProjectUserType(){
         Name = "ProjectUserType";
         Field(x=> x.Usersid);
         Field(x=> x.Projectsid);

        }
    }
}