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

        // Funkcja wywo�ywana przy ��daniu GET
        public async Task<IActionResult> OnGetAsync()
        {
            Equipments = await _context.Equipment
                .Include(e => e.EqType)           // Pobranie powi�zanego typu sprz�tu
                .Include(e => e.EqManufacturer)   // Pobranie powi�zanego producenta sprz�tu
                .ToListAsync();

            return Page();
        }

        //wywo�ywana przy ��daniu POST do usuni�cia sprz�tu
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var equipment = await _context.Equipment
                .Include(e => e.EquipmentCompabilityEqs)  // Wczytanie powi�zanych rekord�w
                .Include(e => e.EquipmentCompabilityCompatibleWiths)
                .FirstOrDefaultAsync(e => e.IdEq == id);

            if (equipment == null)
            {
                return NotFound();
            }

            // Usuwanie powi�zanych rekord�w z tabeli equipment_compability
            _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityEqs);
            _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityCompatibleWiths);

            // Usuwanie sprz�tu z bazy danych
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            // Po usuni�ciu przekierowanie do listy sprz�tu
            return RedirectToPage();
        }

        // Funkcja do usuwania wszystkich sprz�t�w
        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            // Pobranie wszystkich sprz�t�w i powi�zane rekordy
            var allEquipments = await _context.Equipment
                .Include(e => e.EquipmentCompabilityEqs)
                .Include(e => e.EquipmentCompabilityCompatibleWiths)
                .ToListAsync();

            // Usuwanie powi�zanych rekord�w dla wszystkich sprz�t�w
            foreach (var equipment in allEquipments)
            {
                _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityEqs);
                _context.EquipmentCompabilities.RemoveRange(equipment.EquipmentCompabilityCompatibleWiths);
            }

            // Usuwanie wszystkich sprz�t�w
            _context.Equipment.RemoveRange(allEquipments);
            await _context.SaveChangesAsync();

            // Po usuni�ciu, przekierowanie z powrotem do listy sprz�tu
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateConditionAsync()
        {
            // Predefiniowana warto��
            string newConditionValue = "Sprawny";

            // pobierz wszystkie rekordy
            var equipments = await _context.Equipment.ToListAsync();

            // Zaktualizowanie kolumny 'condition' w ka�dym rekordzie
            foreach (var equipment in equipments)
            {
                equipment.Condition = newConditionValue;
            }

            // Zapisanie zmian w bazie
            await _context.SaveChangesAsync();

            //przekierowanie do strony, aby zobaczy� zmiany
            return RedirectToPage();
        }
    }
}
