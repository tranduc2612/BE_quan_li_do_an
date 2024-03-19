using GP.Business.IService;
using GP.Business.Service;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    //options =>
    //{
    //    options.Filters.Add(typeof(CustomExceptionFilter));
    //}
).AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddDbContext<ManagementGraduationProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizletDb"));
});

// Turn off default Validate
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});

// Add service
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IClassificationService, ClassificationService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<IClassificationService, ClassificationService>();
builder.Services.AddScoped<IScheduleSemesterService, ScheduleSemesterService>();
builder.Services.AddScoped<IGroupReviewOutlineService, GroupReviewOutlineService>();
builder.Services.AddScoped<ICommentService, CommentService>();



// Add repository 
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IClassificationRepository, ClassificationRepository>();
builder.Services.AddScoped<IProjectOutlineRepository, ProjectOutlineRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IClassificationRepository, ClassificationRepository>();
builder.Services.AddScoped<IScheduleSemesterRepository, ScheduleSemesterRepository>();
builder.Services.AddScoped<IGroupReviewOutlineRepository, GroupReviewOutlineRepository>();
builder.Services.AddScoped<ITeachingRepository, TeachingRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();








// Add healper
builder.Services.AddScoped<MappingProfile>();
builder.Services.AddScoped<AuthHelper>();
builder.Services.AddScoped<HelperCommon>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//builder.Services.AddDbContext<ManagementGraduationProjectContext>(options =>
//{
//    options.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=ManagementGraduationProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("Access-Control-Allow-Origin:*",
                                "http://localhost:5173").AllowAnyOrigin()
                                                         .AllowAnyMethod()
                                                         .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
