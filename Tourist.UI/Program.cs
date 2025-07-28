using Tourist.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<TourService>();
builder.Services.AddHttpClient<AuthService>();

//builder.Services.AddHttpClient<BookingService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7241/");
//});
builder.Services.AddHttpClient<BookingService>();
builder.Services.AddHttpClient<ReviewService>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); 

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
