using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace photoservice.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            //wyczy�� sesj�
            HttpContext.Session.Clear();

            //przekieruj na stron� g��wn�
            return RedirectToPage("/Index");
        }
    }
}
