using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using photoservice.Data;
using photoservice.Models;

namespace photoservice.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly photoservice.Data.PhotoserviceContext _context;

        public DetailsModel(photoservice.Data.PhotoserviceContext context)
        {
            _context = context;
        }

        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FirstOrDefaultAsync(m => m.IdRes == id);
            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                Reservation = reservation;
            }
            return Page();
        }
    }
}
