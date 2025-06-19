using GraphQL.Resolvers;
using GraphQL.Types;
using ProjektZaliczeniowy2.Data;

namespace ProjektZaliczeniowy2.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(AppDbContext dbContext)
        {
            Name = "Query";

            AddField(new FieldType
            {
                Name = "users",
                Type = typeof(ListGraphType<UserType>),
                Resolver = new FuncFieldResolver<object>(context => dbContext.Users.ToList())
            });
        }
    }
}
