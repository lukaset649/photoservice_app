using Microsoft.AspNetCore.Mvc.RazorPages;

namespace photoservice.Pages
{
    public class DashboardModel : PageModel
    {
        public string UserName { get; set; }

        public void OnGet()
        {
            //Sprawdza sesjê i przekierowuje niezalogowanych u¿ytkowników
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                Response.Redirect("/Login");
            }
            // Pobierz dane u¿ytkownika z sesji
            UserName = HttpContext.Session.GetString("UserFName") ?? "Goœæ";
        }
    }
}
