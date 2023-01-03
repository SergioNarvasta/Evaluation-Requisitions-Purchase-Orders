using HDProjectWeb.Models.Detalles;
using HDProjectWeb.Models.Helps;
using HDProjectWeb.Models;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectWeb_DRA.Data;
using ProjectWeb_DRA.Services;
using ProjectWeb_DRA.Models.DetallesOCompra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//  Agregamos los servicios creados
builder.Services.AddTransient<IRepositorioRQCompra, RepositorioRQCompra>();
builder.Services.AddTransient<IRepositorioOrdenCompra,RepositorioOrdenCompra>();
builder.Services.AddTransient<IServicioEstandar, ServicioEstandar>(); 
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<IServicioUsuario, ServicioUsuario>();
builder.Services.AddTransient<IUserStore<_Login>, UsuarioStore>();
builder.Services.AddTransient<IDetalleReqService, DetalleReqService>();
//Servicios para mostrar Ayudas
builder.Services.AddTransient<ICentroCostoService, CentroCostoService>();
builder.Services.AddTransient<IDisciplinaService, DisciplinaService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IAdjuntosService, AdjuntosService>();

//Servicios de OCC 
builder.Services.AddTransient<IDetallePrdService,DetallePrdService>();

//Configuracion de Servicios para leer Cookies
/*
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme);*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
