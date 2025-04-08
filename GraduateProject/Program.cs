using GP.Business.IService;
using GP.Business.Service;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Model;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;
using Hangfire.MySql;

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
    options.UseMySql(builder.Configuration.GetConnectionString("DB"),new MySqlServerVersion(new Version(8,0,30)));
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
builder.Services.AddScoped<IScheduleWeekService, ScheduleWeekService>();
builder.Services.AddScoped<ICouncilService, CouncilService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IJobService, JobService>();




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
builder.Services.AddScoped<IScheduleWeekRepository, ScheduleWeekRepository>();
builder.Services.AddScoped<ICouncilRepository, CouncilRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();








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
//    options.UseMySql(builder.Configuration.GetConnectionString("DB"));
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
                                "http://qlda-utc.site/").AllowAnyOrigin()
                                                         .AllowAnyMethod()
                                                         .AllowAnyHeader();
        });
});

//builder.Services.AddHangfire(configuration =>
//                                configuration
//                                .UseSimpleAssemblyNameTypeSerializer()
//                                .UseRecommendedSerializerSettings()
//                                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DB")));
//builder.Services.AddHangfireServer();

// Thêm dịch vụ Hangfire với cấu hình MySQL
builder.Services.AddHangfire(configuration =>
    configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseStorage(new MySqlStorage(builder.Configuration.GetConnectionString("DB"),new MySqlStorageOptions()))); // Sử dụng MySQLStorage thay vì SqlServerStorage

builder.Services.AddHangfireServer();

var app = builder.Build();

//Configure the HTTP request pipeline.
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

app.UseHangfireDashboard();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IJobService>(x => x.JobExcuteScheduleSemester(), Cron.Minutely);

app.MapControllers();

// app.Run($"http://*:5000");
app.Run();
Console.WriteLine($"🚀 App is running on port: {builder.Configuration["ASPNETCORE_URLS"] ?? "default"}");

