using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using photoservice.Data;
using photoservice.Models;

namespace photoservice.Pages
{
    public class EditModel : PageModel
    {
        private readonly photoservice.Data.PhotoserviceContext _context;

        public EditModel(photoservice.Data.PhotoserviceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation =  await _context.Reservations.FirstOrDefaultAsync(m => m.IdRes == id);
            if (reservation == null)
            {
                return NotFound();
            }
            Reservation = reservation;
           ViewData["ClientId"] = new SelectList(_context.Users, "IdUser", "IdUser");
           ViewData["ServiceId"] = new SelectList(_context.ServiceTypes, "IdService", "IdService");
           ViewData["StatusId"] = new SelectList(_context.Statuses, "IdStatus", "IdStatus");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.IdRes))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.IdRes == id);
        }
    }
}
