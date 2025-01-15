using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using photoservice.Models;
using photoservice.Services;
using System.Collections.Generic;
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

        public async Task OnGetAsync()
        {
            ActiveUsers = await _context.ActiveUsersWithRoles.ToListAsync();
        }
    }
}
