using Microsoft.EntityFrameworkCore;
using photoservice.Data;
using photoservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Pobierz po��czenie do bazy danych z pliku konfiguracyjnego
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Rejestracja DbContext - PhotoserviceContext
builder.Services.AddDbContext<PhotoserviceContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure())
);

// Rejestracja serwis�w
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<JwtService>();

// Dodanie obs�ugi sesji
builder.Services.AddDistributedMemoryCache(); // Wymagane do u�ycia sesji
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Photoservice.Session"; // Nazwa ciasteczka sesji
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas wyga�ni�cia sesji
    options.Cookie.HttpOnly = true; // Zabezpieczenie przed dost�pem JavaScript
    options.Cookie.IsEssential = true; // Ciasteczko wymagane do dzia�ania aplikacji
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

// Konfiguracja HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // Warto�� HSTS domy�lnie wynosi 30 dni, mo�na j� zmieni� w produkcji
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// U�ycie sesji w pipeline
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
