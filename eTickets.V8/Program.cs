using Microsoft.EntityFrameworkCore;
using eTickets.V8.Data;
using eTickets.V8.Data.Services;
using eTickets.V8.Data.Cart;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext configuration

//untuk menggunakan UseNpgsql ada package yang harus di installed terlebih dahulu
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//dibawah ini untuk memanggil fungsi yang ada di Data/Cart/ShoppingCart
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed Database
AppDbInitializer.Seed(app);

app.Run();
