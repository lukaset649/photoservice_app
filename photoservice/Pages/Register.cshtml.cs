using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using photoservice.Data;
using photoservice.Models;
using photoservice.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AuthenticationService _authService;
        private readonly PhotoserviceContext _context;

        public RegisterModel(PhotoserviceContext context, AuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Sprawdzenie, czy u¿ytkownik z takim e-mailem ju¿ istnieje
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == RegisterRequest.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "U¿ytkownik z tym e-mailem ju¿ istnieje.");
                return Page();
            }

            // Haszowanie has³a
            string hashedPassword = _authService.HashPassword(RegisterRequest.Password);

            // Tworzenie nowego u¿ytkownika
            var newUser = new User
            {
                FName = RegisterRequest.FirstName,
                LName = RegisterRequest.LastName,
                Email = RegisterRequest.Email,
                PhoneNumber = RegisterRequest.PhoneNumber,
                PasswordHash = hashedPassword,
                IsDeleted = false,
                RegistrationDate = DateTime.Now
            };

            // Dodanie u¿ytkownika do bazy danych
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            //Przypisanie domyœlnie u¿ytkownikowi roli 4-klient
            var userRole = new UserRole
            {
                UserId = newUser.IdUser,
                RoleId = 4
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();


            return RedirectToPage("/Index"); // Mo¿na przekierowaæ na stronê powitaln¹
        }
    }
}

