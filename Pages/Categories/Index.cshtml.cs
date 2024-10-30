using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danciu_Radu_Lab2.Data;
using Danciu_Radu_Lab2.Models;
using Danciu_Radu_Lab2.Models.ViewModels;

namespace Danciu_Radu_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context _context;

        public IndexModel(Danciu_Radu_Lab2.Data.Danciu_Radu_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
                .Include(i => i.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(i => i.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                var selectedCategory = CategoryData.Categories
                    .Where(c => c.ID == id.Value)
                    .SingleOrDefault();

                // Selectează cărțile din categoria selectată
                if (selectedCategory != null)
                {
                    CategoryData.Books = selectedCategory.BookCategories
                        .Select(bc => bc.Book)
                        .ToList();
                }
            }
        }
    }
}
