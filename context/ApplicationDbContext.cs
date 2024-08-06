using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WanderMateBackend.Models;

namespace WanderMateBackend.context
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        DbSet<Hotel> hotels {get; set;}
    }
}