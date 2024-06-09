using DevFreela.API.Extensions;
using DevFreela.API.Filters;
using DevFreela.Application.Auth;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using DevFreela.Infrastructure.Services;
using DevFreela.Infrastructure.Services.MessageBus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDoc();

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddHttpClient();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IMessageBusService, MessageBusService>();

builder.Services.AddConfigFluentValidation();
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand));
});

var app = builder.Build();

app.UseSwaggerDoc();

app.UseHttpsRedirection();

app.UseAuth();

app.MapControllers();

app.Run();
