using backend;
using backend.models;
using backend.services;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();

// Add Auth Service
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Controllers
builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"] ?? "backend-api",
        ValidAudience = builder.Configuration["JwtSettings:Audience"] ?? "backend-client",
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not found in configuration."))
        )
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(c => new OpenApiSecurityRequirement()
    {
        [new OpenApiSecuritySchemeReference("Bearer", c)] = []
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TeacherDbContext>(options =>
    options.UseNpgsql(connectionString)); 
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TeacherDbContext>();
    dbContext.Database.Migrate();
    var teachers = dbContext.Teachers.ToAsyncEnumerable();
    Console.WriteLine("Checking for existing teachers...");
    var teacher = await teachers.FirstOrDefaultAsync();
    if (teacher == null)
    {
        Console.WriteLine("No teachers found. Seeding default teacher...");
        dbContext.Teachers.Add(new Teacher()
        {
            Id = 0,
            Name = "Projeto-I-Token",
            Email = "projeto1@gmail.com",
            Password = new AuthService().HashPassword(builder.Configuration["Migrate_Default_Passwords"] ?? "admin123")
        });
        dbContext.SaveChanges();
    }
    else
    {
        Console.WriteLine("Teachers already exist. Skipping seeding.");
    }
}

app.Run();
