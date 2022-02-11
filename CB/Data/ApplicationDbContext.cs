using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CB.Models.Service> Service { get; set; }
        public DbSet<CB.Models.SubService> SubService { get; set; }
        public DbSet<CB.Models.Patient> Patient { get; set; }
        public DbSet<CB.Models.Appointment> Appointment { get; set; }
        public DbSet<CB.Models.FileOnDatabaseModel> FilesOnDatabase { get; set; }

    }
}
