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

        public ClientReservationView ReservationView { get; set; } = default!;
        public IList<ReservationDetail> ReservationDetails { get; set; } = default!;
        public DetailsWedding WeddingDetails { get; set; } = default!;
        public DetailsPhotoshoot PhotoshootDetails { get; set; } = default!;
        public DetailsOther OtherDetails { get; set; } = default!;
        public DetailsBaptism BaptismDetails { get; set; } = default!;
        public int ReservationId { get; set; }

        public async Task OnGetAsync(int id)
        {
            ReservationId = id;

            // Fetch reservation data from ClientReservationView
            ReservationView = await _context.ClientReservationViews
                .FirstOrDefaultAsync(r => r.IdRes == id);

            // Fetch reservation details
            ReservationDetails = await _context.ReservationDetails
                .Include(r => r.Type)
                .Where(r => r.ReservationId == id)
                .ToListAsync();

            var detail = ReservationDetails.FirstOrDefault();
            if (detail != null)
            {
                if (detail.TypeId == 1)  // Wedding
                {
                    WeddingDetails = await _context.DetailsWeddings
                        .FirstOrDefaultAsync(d => d.IdDetWed == detail.DetailsId);
                }
                else if (detail.TypeId == 2)  // Photoshoot
                {
                    PhotoshootDetails = await _context.DetailsPhotoshoots
                        .FirstOrDefaultAsync(d => d.IdDetPs == detail.DetailsId);
                }
                else if (detail.TypeId == 4)  // Other
                {
                    OtherDetails = await _context.DetailsOthers
                        .FirstOrDefaultAsync(d => d.IdDetOth == detail.DetailsId);
                }
                else if (detail.TypeId == 3)  // Baptism
                {
                    BaptismDetails = await _context.DetailsBaptisms
                        .FirstOrDefaultAsync(d => d.IdDetBap == detail.DetailsId);
                }
            }
        }
    }
}
