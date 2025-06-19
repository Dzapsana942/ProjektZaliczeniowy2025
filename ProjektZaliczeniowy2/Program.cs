using Microsoft.EntityFrameworkCore;
using GraphQL.Types;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.SystemTextJson;
using GraphQL.Server; // UWAGA! GraphQL.Server.Transports.AspNetCore + GraphQL.Server.Ui.Playground
using ProjektZaliczeniowy2.GraphQL;
using ProjektZaliczeniowy2.Data;
using ProjektZaliczeniowy2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger + JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Wpisz token JWT poniżej: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("super_secret_key_1234567890_9876543210_secret!"))
    };
});

// InMemory DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

// JwtService
builder.Services.AddScoped<JwtService>();
builder.Services.AddHttpContextAccessor();

// GraphQL
builder.Services.AddScoped<UserType>();
builder.Services.AddScoped<UserQuery>();
builder.Services.AddScoped<UserMutation>(); // dodane!
builder.Services.AddScoped<ISchema, UserSchema>();

builder.Services.AddGraphQL(builder => builder
    .AddSystemTextJson()
    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
    .AddGraphTypes(typeof(UserSchema).Assembly)
    .AddSchema<UserSchema>()
);

// Build app
var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseMiddleware<ProjektZaliczeniowy2.Middleware.HeaderLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontrolerów
app.MapControllers();

// GraphQL endpoint
app.UseGraphQL<ISchema>();
app.UseGraphQLPlayground(); // Dostęp pod /ui/playground

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjektZaliczeniowy2.Data.AppDbContext>();

    if (!db.Users.Any(u => u.Name == "TestUser" && u.Email == "testuser@example.com"))
    {
        db.Users.Add(new ProjektZaliczeniowy2.Models.User
        {
            Name = "testuser",
            Email = "test@test.com"
        });

        db.SaveChanges();
    }
}
app.Run();
