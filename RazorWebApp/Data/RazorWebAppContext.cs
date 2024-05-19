using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorWebApp.Models;

namespace RazorWebApp.Data
{
    public class RazorWebAppContext : DbContext
    {
        public RazorWebAppContext (DbContextOptions<RazorWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<RazorWebApp.Models.IceCream> IceCream { get; set; } = default!;
    }
}
