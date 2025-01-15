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
builder.Services.AddScoped<AuthenticationService>(); // Rejestracja serwisu autentykacji
builder.Services.AddScoped<JwtService>(); // Rejestracja serwisu Jwt, je¿eli go u¿ywasz

// Dodaj inne niezbêdne us³ugi, np. kontrolery, jeœli ich u¿ywasz
builder.Services.AddControllers(); // Rejestracja kontrolerów

// Rejestracja Razor Pages, jeœli s¹ potrzebne
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
app.UseAuthorization();

// Mapowanie Razor Pages
app.MapRazorPages();
// Mapowanie kontrolerów (API)
app.MapControllers();

app.Run();
