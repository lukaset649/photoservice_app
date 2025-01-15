using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using photoservice.Models;
using photoservice.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photoservice.Pages
{
    public class UsersModel : PageModel
    {
        private readonly photoservice.Data.PhotoserviceContext _context;

        public UsersModel(photoservice.Data.PhotoserviceContext context)
        {
            _context = context;
        }

        public List<ActiveUsersWithRole> ActiveUsers { get; set; } = new List<ActiveUsersWithRole>();

        // Parametry
        public string SearchQuery { get; set; } = string.Empty;
        public bool SortByRole { get; set; }

        public async Task OnGetAsync(string searchQuery, bool sortByRole)
        {
            // Przypisanie warto�ci z zapytania do w�a�ciwo�ci
            SearchQuery = searchQuery;
            SortByRole = sortByRole;

            // Pobranie u�ytkownik�w z bazy
            var usersQuery = _context.ActiveUsersWithRoles.AsQueryable();

            // Filtrowanie na podstawie frazy w imieniu lub nazwisku
            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Podziel fraz� na imi� i nazwisko, je�eli zawiera spacj�
                var nameParts = searchQuery.Split(' ');

                if (nameParts.Length == 1)
                {
                    // Je�li tylko jedno s�owo, filtruj na podstawie imienia lub nazwiska
                    usersQuery = usersQuery.Where(u => u.FName.Contains(nameParts[0]) || u.LName.Contains(nameParts[0]));
                }
                else if (nameParts.Length == 2)
                {
                    // Je�li dwa s�owa (imi� i nazwisko), filtruj na podstawie obu
                    usersQuery = usersQuery.Where(u => u.FName.Contains(nameParts[0]) && u.LName.Contains(nameParts[1]) ||
                                                        u.FName.Contains(nameParts[1]) && u.LName.Contains(nameParts[0]));
                }
            }

            // Sortowanie po roli
            if (sortByRole)
            {
                usersQuery = usersQuery.OrderBy(u => u.RoleName);
            }

            // Wykonanie zapytania i przypisanie wynik�w do listy
            ActiveUsers = await usersQuery.ToListAsync();
        }
    }
}
