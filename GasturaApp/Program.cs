using DotNetEnv;
using GasturaApp.Application.Repositories.Implementations;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load(); // carrega .env só no desenvolvimento
}
string ConvertDatabaseUrlToConnectionString(string databaseUrl)
{
    // Ex: postgresql://usuario:senha@host:porta/banco
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    return $"Host={uri.Host};Port={uri.Port};Username={userInfo[0]};Password={userInfo[1]};Database={uri.AbsolutePath.TrimStart('/')};Ssl Mode=Require;Trust Server Certificate=true;";
}

var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(rawUrl))
    throw new InvalidOperationException("DATABASE_URL não está definido.");

var connectionString = ConvertDatabaseUrlToConnectionString(rawUrl);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IGastoRepository, GastoRepository>();
builder.Services.AddScoped<IGastoService, GastoService>();

builder.Services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
builder.Services.AddScoped<IOrcamentoService, OrcamentoService>();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();