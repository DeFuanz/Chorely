using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chorely.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Chorley.Data
{
    public class ChorleyContext : IdentityDbContext<ChorelyUser>
    {
        public ChorleyContext (DbContextOptions<ChorleyContext> options)
            : base(options)
        {
        }

        public DbSet<Chorely.Models.Chore> Chore { get; set; } = default!;
    }
}
