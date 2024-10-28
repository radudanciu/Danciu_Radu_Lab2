using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danciu_Radu_Lab2.Data;
using Danciu_Radu_Lab2.Models;

namespace Danciu_Radu_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context _context;

        public DetailsModel(Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookCategories)    
                .ThenInclude(bc => bc.Category)      
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            // Extract categories from the BookCategories navigation property
            Categories = Book.BookCategories.Select(bc => bc.Category);

            return Page();
        }
    }
}
