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
    public class IndexModel : PageModel
    {
        private readonly photoservice.Data.PhotoserviceContext _context;

        public IndexModel(photoservice.Data.PhotoserviceContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }
    }
}
