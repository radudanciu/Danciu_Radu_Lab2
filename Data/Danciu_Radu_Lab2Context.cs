using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Danciu_Radu_Lab2.Models;

namespace Danciu_Radu_Lab2.Data
{
    public class Danciu_Radu_Lab2Context : DbContext
    {
        public Danciu_Radu_Lab2Context (DbContextOptions<Danciu_Radu_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Danciu_Radu_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Danciu_Radu_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Danciu_Radu_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Danciu_Radu_Lab2.Models.Category> Category { get; set; } = default!;
    }
}
