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

        // Zmiana na listê ClientReservationView
        public IList<ClientReservationView> ReservationView { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Pobieranie danych z widoku ClientReservationView
            ReservationView = await _context.ClientReservationViews.ToListAsync();
        }
    }
}
