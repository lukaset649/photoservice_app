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

            // Przechowywanie danych u¿ytkownika w sesji
            HttpContext.Session.SetString("UserId", user.IdUser.ToString());
            HttpContext.Session.SetString("UserFName", user.FName);
            HttpContext.Session.SetString("UserLName", user.LName);

            // Przekierowanie do spersonalizowanej strony
            return RedirectToPage("/Dashboard");
        }
    }
}
