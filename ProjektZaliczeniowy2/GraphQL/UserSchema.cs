using GraphQL.Types;

namespace ProjektZaliczeniowy2.GraphQL
{
    public class UserSchema : Schema
    {
        public UserSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<UserQuery>();
            Mutation = provider.GetRequiredService<UserMutation>(); // <--- TO MUSI BYĆ!
        }
    }
}
