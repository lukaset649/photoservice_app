using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using photoservice.Data;
using photoservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace photoservice.Pages
{
    public class EquipmentListModel : PageModel
    {
        private readonly PhotoserviceContext _context;

        public List<Equipment> Equipments { get; set; } = new List<Equipment>();

        // Konstruktor, który przyjmuje kontekst bazy danych
        public EquipmentListModel(PhotoserviceContext context)
        {
            _context = context;
        }

        // Funkcja obs³uguj¹ca wyœwietlanie strony
        public async Task<IActionResult> OnGetAsync()
        {
            // Pobieranie wszystkich sprzêtów z bazy danych wraz z powi¹zanymi danymi typu i producenta
            Equipments = await _context.Equipment
                .Include(e => e.EqType)           // Pobranie typu sprzêtu
                .Include(e => e.EqManufacturer)   // Pobranie producenta sprzêtu
                .ToListAsync();

            return Page();
        }
    }
}
