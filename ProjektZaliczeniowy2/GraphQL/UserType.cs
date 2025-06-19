using GraphQL.Types;
using ProjektZaliczeniowy2.Models;

namespace ProjektZaliczeniowy2.GraphQL
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id).Description("Id użytkownika");
            Field(x => x.Name).Description("Imię użytkownika");
            Field(x => x.Email).Description("Email użytkownika");
        }
    }
}
