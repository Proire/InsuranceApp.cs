using Microsoft.EntityFrameworkCore;
using InsuranceAppRLL.Entities;

namespace InsuranceAppRLL
{
    public class InsuranceDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InsuranceAgent> InsuranceAgents { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<EmployeeScheme> EmployeeSchemes {  get; set; }

        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer - InsuranceAgent (many-to-one)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.InsuranceAgent)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.AgentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Policy - Customer (many-to-one)
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Policy - Scheme (many-to-one)
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Scheme)
                .WithMany(s => s.Policies)
                .HasForeignKey(p => p.SchemeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment - Customer (many-to-one)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment - Policy (many-to-one)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Policy)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.PolicyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Commission - InsuranceAgent (many-to-one)
            modelBuilder.Entity<Commission>()
                .HasOne(c => c.InsuranceAgent)
                .WithMany(a => a.Commissions)
                .HasForeignKey(c => c.AgentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Commission - Policy (many-to-one)
            modelBuilder.Entity<Commission>()
                .HasOne(c => c.Policy)
                .WithMany(p => p.Commissions)
                .HasForeignKey(c => c.PolicyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Scheme - InsurancePlan (many-to-one)
            modelBuilder.Entity<Scheme>()
                .HasOne(s => s.InsurancePlan)
                .WithMany(p => p.Schemes)
                .HasForeignKey(s => s.PlanID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
