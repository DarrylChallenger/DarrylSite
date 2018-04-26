namespace DarrylSite.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SecurityContext : DbContext
    {
        public SecurityContext()
            : base("name=SecurityContext")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<UserCompany> UserCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
