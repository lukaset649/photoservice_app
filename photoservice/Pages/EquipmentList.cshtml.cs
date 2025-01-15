using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using photoservice.Data;
using photoservice.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photoservice.Pages
{
    public class EquipmentListModel : PageModel
    {
        private readonly PhotoserviceContext _context;

        public List<Equipment> Equipments { get; set; } = new List<Equipment>();

        public EquipmentListModel(PhotoserviceContext context)
        {
            _context = context;
        }

        // Funkcja wywo³ywana przy ¿¹daniu GET
        public async Task<IActionResult> OnGetAsync()
        {
            Equipments = await _context.Equipment
                .Include(e => e.EqType)           // Pobranie powi¹zanego typu sprzêtu
                .Include(e => e.EqManufacturer)   // Pobranie powi¹zanego producenta sprzêtu
                .ToListAsync();

            return Page();
        }

        //wywo³ywana przy ¿¹daniu POST do usuniêcia sprzêtu
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var equipment = await _context.Equipment
                .Include(e => e.EquipmentCompabilityEqs)  // Wczytanie powi¹zanych rekordów
                .Include(e => e.EquipmentCompabilityCompatibleWiths)
                .FirstOrDefaultAsync(e => e.IdEq == id);

            if (equipment == null)
            {
                return NotFound();
            }

            // Usuwanie powi¹zanych rekordów z tabeli equipment_compability
            _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityEqs);
            _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityCompatibleWiths);

            // Usuwanie sprzêtu z bazy danych
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            // Po usuniêciu przekierowanie do listy sprzêtu
            return RedirectToPage();
        }

        // Funkcja do usuwania wszystkich sprzêtów
        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            // Pobranie wszystkich sprzêtów i powi¹zane rekordy
            var allEquipments = await _context.Equipment
                .Include(e => e.EquipmentCompabilityEqs)
                .Include(e => e.EquipmentCompabilityCompatibleWiths)
                .ToListAsync();

            // Usuwanie powi¹zanych rekordów dla wszystkich sprzêtów
            foreach (var equipment in allEquipments)
            {
                _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityEqs);
                _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityCompatibleWiths);
            }

            // Usuwanie wszystkich sprzêtów
            _context.Equipment.RemoveRange(allEquipments);
            await _context.SaveChangesAsync();

            // Po usuniêciu, przekierowanie z powrotem do listy sprzêtu
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateConditionAsync()
        {
            // Predefiniowana wartoœæ
            string newConditionValue = "Sprawny";

            // pobierz wszystkie rekordy
            var equipments = await _context.Equipment.ToListAsync();

            // Zaktualizowanie kolumny 'condition' w ka¿dym rekordzie
            foreach (var equipment in equipments)
            {
                equipment.Condition = newConditionValue;
            }

            // Zapisanie zmian w bazie
            await _context.SaveChangesAsync();

            //przekierowanie do strony, aby zobaczyæ zmiany
            return RedirectToPage();
        }
    }
}
