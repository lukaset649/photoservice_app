using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using photoservice.Services;
using photoservice.Models;

namespace photoservice.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthenticationService _authenticationService;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void OnGet()
        {
            // Jeœli chcesz, aby strona logowania by³a pusta pocz¹tkowo, to OnGet bêdzie mog³a byæ pusta.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Proszê podaæ zarówno e-mail, jak i has³o.";
                return Page();
            }

            var user = await _authenticationService.LoginAsync(new LoginRequest
            {
                Email = Email,
                Password = Password
            });

            if (user == null)
            {
                ErrorMessage = "Nieprawid³owy e-mail lub has³o.";
                return Page();
            }

            // W przypadku udanego logowania przekieruj do strony g³ównej lub dashboardu
            return RedirectToPage("/Index"); // Lub inna strona, na któr¹ ma zostaæ przekierowany u¿ytkownik po udanym logowaniu.
        }
    }
}
