using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace photoservice.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            //wyczyœæ sesjê
            HttpContext.Session.Clear();

            //przekieruj na stronê g³ówn¹
            return RedirectToPage("/Index");
        }
    }
}
