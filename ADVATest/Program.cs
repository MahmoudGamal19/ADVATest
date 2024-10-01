using AutoMapper;
using Domin.Interfaces;
using Domin.Interfaces.Repository;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Mapper;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(connString);
});

// Add services to the container.
builder.Services.AddControllersWithViews();


//----GiveConfigrationToRepository-----

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();



//----GiveConfigrationToService-----

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

//-----Auto Mapper

builder.Services.AddAutoMapper(typeof(MapperProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=GetAll}");

app.Run();



