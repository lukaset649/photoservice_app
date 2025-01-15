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
builder.Services.AddScoped<AuthenticationService>(); // Rejestracja serwisu autentykacji
builder.Services.AddScoped<JwtService>(); // Rejestracja serwisu Jwt, je�eli go u�ywasz

// Dodaj inne niezb�dne us�ugi, np. kontrolery, je�li ich u�ywasz
builder.Services.AddControllers(); // Rejestracja kontroler�w

// Rejestracja Razor Pages, je�li s� potrzebne
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
app.UseAuthorization();

// Mapowanie Razor Pages
app.MapRazorPages();
// Mapowanie kontroler�w (API)
app.MapControllers();

app.Run();
