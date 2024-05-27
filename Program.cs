using frontendnet.Middelwares;
using frontendnet.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//agregamos los servicios
builder.Services.AddControllersWithViews();

//soporte para consultar el API
var UrlWebApi = builder.Configuration["UrlWebAPI"];
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<EnviaBearerDelegatingHandler>();
builder.Services.AddTransient<RefrescaTokenDelegatingHandler>();
builder.Services.AddHttpClient<AuthClientService>(HttpClient => {HttpClient.BaseAddress = new Uri(UrlWebApi!);});
builder.Services.AddHttpClient<CategoriasClientService>(HttpClient => {HttpClient.BaseAddress= new Uri(UrlWebApi!);})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>().AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();
builder.Services.AddHttpClient<UsuariosClientService>(HttpClient => {HttpClient.BaseAddress= new Uri(UrlWebApi!);})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();
builder.Services.AddHttpClient<RolesClientService>(HttpClient => {HttpClient.BaseAddress= new Uri(UrlWebApi!);})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();
builder.Services.AddHttpClient<PerfilClientService> (HttpClient => {HttpClient.BaseAddress= new Uri(UrlWebApi!);})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

//soporte para cookie auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.Cookie.Name=".frontendnet";
    options.AccessDeniedPath="/Home/AccessDenied";
    options.LoginPath="/Auth";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
var app = builder.Build();
 
 //agregamos un middleware para el manejo de errores
 app.UseExceptionHandler("/Home/Error");

 app.UseStaticFiles();
 app.UseRouting();

 app.UseAuthentication();
 app.UseAuthorization();

 app.MapControllerRoute(
    name : "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );
     app.Run();
     

