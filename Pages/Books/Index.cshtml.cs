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
    public class IndexModel : PageModel
    {
        private readonly Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context _context;

        public IndexModel(Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context context)
        {
            _context = context;
            BookD = new BookData();
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            // Load the book data
            BookD.Books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .Where(i => i.ID == id.Value)
                    .SingleOrDefault();

                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
                }
            }
            else
            {
                // Fallback if no specific book is selected
                Book = await _context.Book
                    .Include(b => b.Publisher)
                    .Include(b => b.Author)
                    .ToListAsync();
            }
        }
    }
}
