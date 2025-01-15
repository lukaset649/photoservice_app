using Microsoft.EntityFrameworkCore;
using photoservice.Data;
using photoservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Pobierz po³¹czenie do bazy danych z pliku konfiguracyjnego
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Rejestracja DbContext - PhotoserviceContext
builder.Services.AddDbContext<PhotoserviceContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure())
);

// Rejestracja serwisów
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<JwtService>();

// Dodanie obs³ugi sesji
builder.Services.AddDistributedMemoryCache(); // Wymagane do u¿ycia sesji
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Photoservice.Session"; // Nazwa ciasteczka sesji
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas wygaœniêcia sesji
    options.Cookie.HttpOnly = true; // Zabezpieczenie przed dostêpem JavaScript
    options.Cookie.IsEssential = true; // Ciasteczko wymagane do dzia³ania aplikacji
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

// Konfiguracja HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // Wartoœæ HSTS domyœlnie wynosi 30 dni, mo¿na j¹ zmieniæ w produkcji
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// U¿ycie sesji w pipeline
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
