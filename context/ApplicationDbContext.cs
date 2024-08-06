using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WanderMateBackend.context
{
    public class ApplicationDbContext : DbContext
    {
          public ApplicationDbContext(DbContextOptions options) 
        : base (options)
        {    

            
        }
    }
}