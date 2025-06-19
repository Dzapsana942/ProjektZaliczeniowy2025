using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using ProjektZaliczeniowy2.Data;
using ProjektZaliczeniowy2.Models;

namespace ProjektZaliczeniowy2.GraphQL
{
    public class UserMutation : ObjectGraphType
    {
        public UserMutation(AppDbContext dbContext)
        {
            Name = "Mutation";

            AddField(new FieldType
            {
                Name = "createUser",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
                ),
                Type = typeof(UserType),
                Resolver = new FuncFieldResolver<User>(context =>
                {
                    var userInput = context.GetArgument<User>("user");
                    dbContext.Users.Add(userInput);
                    dbContext.SaveChanges();
                    return userInput;
                })
            });
        }
    }

    public class UserInputType : InputObjectGraphType<User>
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field(x => x.Name);
            Field(x => x.Email);
        }
    }
}
