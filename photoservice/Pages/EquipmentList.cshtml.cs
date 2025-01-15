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

        // Konstruktor, kt�ry przyjmuje kontekst bazy danych
        public EquipmentListModel(PhotoserviceContext context)
        {
            _context = context;
        }

        // Funkcja obs�uguj�ca wy�wietlanie strony
        public async Task<IActionResult> OnGetAsync()
        {
            // Pobieranie wszystkich sprz�t�w z bazy danych wraz z powi�zanymi danymi typu i producenta
            Equipments = await _context.Equipment
                .Include(e => e.EqType)           // Pobranie typu sprz�tu
                .Include(e => e.EqManufacturer)   // Pobranie producenta sprz�tu
                .ToListAsync();

            return Page();
        }
    }
}
