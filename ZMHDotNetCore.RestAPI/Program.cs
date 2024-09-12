using Microsoft.EntityFrameworkCore;
using RestAPI.Db;
using ZMHDotNetCore.Shared;

var builder = WebApplication.CreateBuilder(args);

//to enable CORS in blazor web app
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("https://localhost:7230",
                                            "http://localhost:5095")
                             .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                             .AllowAnyHeader();
                    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;

//builder.Services.AddScoped<AdoDotNetServices>(n => new AdoDotNetServices(connectionString));
//builder.Services.AddScoped<DapperServices>(n => new DapperServices(connectionString));

builder.Services.AddScoped(n => new AdoDotNetServices(connectionString));
builder.Services.AddScoped(n => new DapperServices(connectionString));

builder.Services.AddDbContext<AppDBContext>(opt =>
{
  opt.UseSqlServer(connectionString);
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//for CORS enable
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
