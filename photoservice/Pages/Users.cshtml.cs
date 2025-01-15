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
            // Przypisanie wartoœci z zapytania do w³aœciwoœci
            SearchQuery = searchQuery;
            SortByRole = sortByRole;

            // Pobranie u¿ytkowników z bazy
            var usersQuery = _context.ActiveUsersWithRoles.AsQueryable();

            // Filtrowanie na podstawie frazy w imieniu lub nazwisku
            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Podziel frazê na imiê i nazwisko, je¿eli zawiera spacjê
                var nameParts = searchQuery.Split(' ');

                if (nameParts.Length == 1)
                {
                    // Jeœli tylko jedno s³owo, filtruj na podstawie imienia lub nazwiska
                    usersQuery = usersQuery.Where(u => u.FName.Contains(nameParts[0]) || u.LName.Contains(nameParts[0]));
                }
                else if (nameParts.Length == 2)
                {
                    // Jeœli dwa s³owa (imiê i nazwisko), filtruj na podstawie obu
                    usersQuery = usersQuery.Where(u => u.FName.Contains(nameParts[0]) && u.LName.Contains(nameParts[1]) ||
                                                        u.FName.Contains(nameParts[1]) && u.LName.Contains(nameParts[0]));
                }
            }

            // Sortowanie po roli
            if (sortByRole)
            {
                usersQuery = usersQuery.OrderBy(u => u.RoleName);
            }

            // Wykonanie zapytania i przypisanie wyników do listy
            ActiveUsers = await usersQuery.ToListAsync();
        }
    }
}
