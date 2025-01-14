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
    public class ReservationModel : PageModel
    {
        private readonly photoservice.Data.PhotoserviceContext _context;

        public ReservationModel(photoservice.Data.PhotoserviceContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Service)
                .Include(r => r.Status).ToListAsync();
        }
    }
}
