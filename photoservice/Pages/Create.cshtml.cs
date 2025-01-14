using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using photoservice.Data;
using photoservice.Models;

namespace photoservice.Pages
{
    public class CreateModel : PageModel
    {
        private readonly photoservice.Data.PhotoserviceContext _context;

        public CreateModel(photoservice.Data.PhotoserviceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientId"] = new SelectList(_context.Users, "IdUser", "IdUser");
        ViewData["ServiceId"] = new SelectList(_context.ServiceTypes, "IdService", "IdService");
        ViewData["StatusId"] = new SelectList(_context.Statuses, "IdStatus", "IdStatus");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
