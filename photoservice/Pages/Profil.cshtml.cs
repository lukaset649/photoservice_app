using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using photoservice.Services;
using photoservice.Models;
using photoservice.Data;

namespace photoservice.Pages
{
    public class ProfilModel : PageModel
    {
        private readonly AuthenticationService _authenticationService;
        private readonly PhotoserviceContext _context;

        [BindProperty]
        public string PhoneNumber { get; set; }

        public User User { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public ProfilModel(AuthenticationService authenticationService, PhotoserviceContext context)
        {
            _authenticationService = authenticationService;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            int id = int.Parse(userId);
            User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return RedirectToPage("/Login");
            }

            // Ustawiamy numer telefonu, aby umo¿liwiæ edycjê
            PhoneNumber = User.PhoneNumber;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            int id = int.Parse(userId);
            User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return RedirectToPage("/Login");
            }

            // Walidacja numeru telefonu
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                ErrorMessage = "Numer telefonu nie mo¿e byæ pusty.";
                return Page();
            }

            // Zaktualizowanie numeru telefonu w bazie danych
            User.PhoneNumber = PhoneNumber;

            try
            {
                _context.Users.Update(User);
                await _context.SaveChangesAsync();
                SuccessMessage = "Numer telefonu zosta³ zaktualizowany pomyœlnie.";
            }
            catch (Exception ex)
            {
                ErrorMessage = "Wyst¹pi³ b³¹d podczas aktualizacji numeru telefonu. Spróbuj ponownie.";
            }

            return Page();
        }
    }
}
