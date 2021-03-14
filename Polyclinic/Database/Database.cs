using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Database1;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Cost> Costs { set; get; }
        public virtual DbSet<CostInspection> CostInspections { set; get; }
        public virtual DbSet<Inspection> Inspections { set; get; }
        public virtual DbSet<InspectionDoctor> InspectionDoctors { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
        public virtual DbSet<Visit> Visis { set; get; }
    }
}
