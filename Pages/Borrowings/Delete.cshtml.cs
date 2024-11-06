using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danciu_Radu_Lab2.Data;
using Danciu_Radu_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Danciu_Radu_Lab2.Pages.Borrowings
{
    public class DeleteModel : PageModel
    {
        private readonly Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context _context;

        public DeleteModel(Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

            public async Task<IActionResult> OnGetAsync(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var borrowing = await _context.Borrowing
                    .Include(b => b.Book)
                        .ThenInclude(b => b.Author)
                    .Include(b => b.Member)
                    .FirstOrDefaultAsync(m => m.ID == id);

                if (borrowing == null)
                {
                    return NotFound();
                }
                Borrowing = borrowing;
                ViewData["BookID"] = new SelectList(_context.Book.Select(b => new { ID = b.ID, Details = b.Title + " - " + b.Author.FullName }), "ID", "Details");
                ViewData["MemberID"] = new SelectList(_context.Member.Select(m => new { ID = m.ID, FullName = m.FullName }), "ID", "FullName");
                return Page();
            }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing != null)
            {
                Borrowing = borrowing;
                _context.Borrowing.Remove(Borrowing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
