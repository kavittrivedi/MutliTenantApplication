using Microsoft.EntityFrameworkCore;
using MutiTenantApplicationApi;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor
builder.Services.AddScoped<PostService>(); // Register PostService
// Register ApplicationDbContext with dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5049/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseTenantMiddleware();
// Use CORS policy
app.UseCors();
app.UseAuthorization();
//app.UseMiddleware<TenantMiddleware>();
app.MapControllers();
app.Run();