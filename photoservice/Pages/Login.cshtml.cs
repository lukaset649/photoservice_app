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
            // Je�li chcesz, aby strona logowania by�a pusta pocz�tkowo, to OnGet b�dzie mog�a by� pusta.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Prosz� poda� zar�wno e-mail, jak i has�o.";
                return Page();
            }

            var user = await _authenticationService.LoginAsync(new LoginRequest
            {
                Email = Email,
                Password = Password
            });

            if (user == null)
            {
                ErrorMessage = "Nieprawid�owy e-mail lub has�o.";
                return Page();
            }

            // W przypadku udanego logowania przekieruj do strony g��wnej lub dashboardu
            return RedirectToPage("/Index"); // Lub inna strona, na kt�r� ma zosta� przekierowany u�ytkownik po udanym logowaniu.
        }
    }
}
