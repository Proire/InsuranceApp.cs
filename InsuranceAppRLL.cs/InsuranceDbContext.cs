using Microsoft.EntityFrameworkCore;
using InsuranceAppRLL.Entities;
using Microsoft.Data.SqlClient;

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

        // Admin methods 
        public async Task RegisterAdminAsync(Admin admin)
        {
            var usernameParam = new SqlParameter("@Username", admin.Username);
            var passwordParam = new SqlParameter("@Password", admin.Password);
            var emailParam = new SqlParameter("@Email", admin.Email);
            var fullNameParam = new SqlParameter("@FullName", admin.FullName);
            var createdAtParam = new SqlParameter("@CreatedAt", admin.CreatedAt);

            // Execute the stored procedure
            await Database.ExecuteSqlRawAsync(
                "EXEC RegisterAdmin @Username, @Password, @Email, @FullName, @CreatedAt",
                usernameParam, passwordParam, emailParam, fullNameParam, createdAtParam
            );
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            var adminIdParam = new SqlParameter("@AdminID", admin.AdminID);
            var usernameParam = new SqlParameter("@Username", admin.Username);
            var passwordParam = new SqlParameter("@Password", admin.Password);
            var emailParam = new SqlParameter("@Email", admin.Email);
            var fullNameParam = new SqlParameter("@FullName", admin.FullName);
            var createdAtParam = new SqlParameter("@CreatedAt", admin.CreatedAt);

            // Execute the stored procedure
            await Database.ExecuteSqlRawAsync(
                "EXEC UpdateAdmin @AdminID, @Username, @Password, @Email, @FullName, @CreatedAt",
                adminIdParam, usernameParam, passwordParam, emailParam, fullNameParam, createdAtParam
            );
        }

        public async Task ExecuteDeleteAdminStoredProcedureAsync(int adminId)
        {
            var adminIdParam = new SqlParameter("@AdminID", adminId);

            // Execute the stored procedure to delete the admin
            await Database.ExecuteSqlRawAsync(
                "EXEC DeleteAdmin @AdminID",
                adminIdParam
            );
        }

        public async Task<Admin> GetAdminByIdAsync(int adminId)
        {
            var adminIdParam = new SqlParameter("@AdminID", adminId);

            var result = await Admins
                .FromSqlRaw("EXEC GetAdminById @AdminID", adminIdParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            var result = await Admins
                .FromSqlRaw("EXEC GetAllAdmins")
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        // Employees
        public async Task RegisterEmployeeAsync(Employee employee)
        {
            var parameters = new[]
            {
            new SqlParameter("@Username", employee.Username),
            new SqlParameter("@Password", employee.Password),
            new SqlParameter("@Email", employee.Email),
            new SqlParameter("@FullName", employee.FullName),
            new SqlParameter("@Role", employee.Role),
            new SqlParameter("@CreatedAt", employee.CreatedAt)
            };

            await Database.ExecuteSqlRawAsync("EXEC RegisterEmployee @Username, @Password, @Email, @FullName, @Role, @CreatedAt", parameters);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var parameters = new[]
            {
            new SqlParameter("@EmployeeID", employee.EmployeeID),
            new SqlParameter("@Username", employee.Username),
            new SqlParameter("@Password", employee.Password),
            new SqlParameter("@Email", employee.Email),
            new SqlParameter("@FullName", employee.FullName),
            new SqlParameter("@Role", employee.Role)
            };

            await Database.ExecuteSqlRawAsync("EXEC UpdateEmployee @EmployeeID, @Username, @Password, @Email, @FullName, @Role", parameters);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var parameter = new SqlParameter("@EmployeeID", employeeId);
            await Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @EmployeeID", parameter);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var parameter = new SqlParameter("@EmployeeID", employeeId);
            var result = await Employees
                .FromSqlRaw("EXEC GetEmployeeById @EmployeeID", parameter)
                .ToListAsync(); // Ensures the query is executed and results are fetched

            return result.FirstOrDefault(); // Returns the first employee or null if no results
        }


        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await Employees.FromSqlRaw("EXEC GetAllEmployees").ToListAsync();
        }

        // customers 
        public async Task RegisterCustomerAsync(string fullName, string email, string password, string phone, DateTime dateOfBirth, int? agentID)
        {
            await Database.ExecuteSqlRawAsync("EXEC RegisterCustomer @p0, @p1, @p2, @p3, @p4, @p5",
                fullName, email, password, phone, dateOfBirth, agentID);
        }

        public async Task UpdateCustomerAsync(int customerID, string fullName, string email, string password, string phone, DateTime dateOfBirth, int? agentID)
        {
            await Database.ExecuteSqlRawAsync("EXEC UpdateCustomer @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                customerID, fullName, email, password, phone, dateOfBirth, agentID);
        }

        public async Task DeleteCustomerAsync(int customerID)
        {
            await Database.ExecuteSqlRawAsync("EXEC DeleteCustomer @p0", customerID);
        }

        // Method to call stored procedure for fetching a customer by ID
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var parameter = new SqlParameter("@CustomerID", customerId);
            var result = await Customers
                .FromSqlRaw("EXEC GetCustomerById @CustomerID", parameter)
                .ToListAsync(); // Ensures the query is executed and results are fetched

            return result.FirstOrDefault(); // Returns the first customer or null if no results
        }


        // Method to call stored procedure for fetching all customers
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await Customers
                .FromSqlRaw("EXEC GetAllCustomers")
                .ToListAsync();
        }

        // Method to call stored procedure for fetching customers by agent ID
        public async Task<IEnumerable<Customer>> GetCustomersByAgentIdAsync(int agentId)
        {
            var parameter = new SqlParameter("@AgentID", agentId);
            var result = await Customers
                .FromSqlRaw("EXEC GetCustomersByAgentId @AgentID", parameter)
                .ToListAsync(); // Ensures the query is executed and results are fetched

            return result; // Returns the list of customers
        }


        // agents 
        public async Task RegisterInsuranceAgentAsync(InsuranceAgent agent)
        {
            var parameters = new[]
            {
            new SqlParameter("@Username", agent.Username),
            new SqlParameter("@Password", agent.Password),
            new SqlParameter("@Email", agent.Email),
            new SqlParameter("@FullName", agent.FullName),
        };

            await Database.ExecuteSqlRawAsync("EXEC RegisterInsuranceAgent @Username, @Password, @Email, @FullName", parameters);
        }

        public async Task DeleteAgentAsync(int agentId)
        {
            var parameter = new SqlParameter("@AgentID", agentId);
            await Database.ExecuteSqlRawAsync("EXEC DeleteInsuranceAgent @AgentID", parameter);
        }

        public async Task UpdateAgentAsync(InsuranceAgent agent)
        {
            var parameters = new[]
            {
            new SqlParameter("@AgentID", agent.AgentID),
            new SqlParameter("@Username", agent.Username),
            new SqlParameter("@Password", agent.Password),
            new SqlParameter("@Email", agent.Email),
            new SqlParameter("@FullName", agent.FullName),
        };

            await Database.ExecuteSqlRawAsync("EXEC UpdateInsuranceAgent @AgentID, @Username, @Password, @Email, @FullName", parameters);
        }

        public async Task<InsuranceAgent> GetAgentByIDAsync(int agentId)
        {
            var agentIdParam = new SqlParameter("@AgentID", agentId);

            var result = await InsuranceAgents
                .FromSqlRaw("EXEC GetInsuranceAgentByID @AgentID", agentIdParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }


        public async Task<IEnumerable<InsuranceAgent>> GetAllAsync()
        {
            return await InsuranceAgents.FromSqlRaw("EXEC GetAllInsuranceAgents").ToListAsync();
        }

        // commission 
        public async Task<double> GetTotalCommissionForAgentAsync(int agentId)
        {
            var parameter = new SqlParameter("@AgentID", agentId);
            var totalCommission = await Database.ExecuteSqlRawAsync("EXEC GetTotalCommissionForAgent @AgentID", parameter);

            if (totalCommission == null)
            {
                return 0;
            }

            return totalCommission;
        }

        // Employee scheme 
        public async Task AddSchemeToEmployeeAsync(int schemeId, int employeeId)
        {
            var parameters = new[]
            {
            new SqlParameter("@SchemeID", schemeId),
            new SqlParameter("@EmployeeID", employeeId)
        };

            await Database.ExecuteSqlRawAsync("EXEC AddSchemeToEmployee @SchemeID, @EmployeeID", parameters);
        }

        public async Task DeleteEmployeeFromSchemeAsync(int employeeId)
        {
            var parameter = new SqlParameter("@EmployeeID", employeeId);
            await Database.ExecuteSqlRawAsync("EXEC DeleteEmployeeFromScheme @EmployeeID", parameter);
        }

        public async Task DeleteSchemeFromEmployeesAsync(int schemeId)
        {
            var parameter = new SqlParameter("@SchemeID", schemeId);
            await Database.ExecuteSqlRawAsync("EXEC DeleteSchemeFromEmployees @SchemeID", parameter);
        }

        // insurance plan 
        public async Task<int> AddInsurancePlanAsync(string planName, string planDetails)
        {
            var planIdParam = new SqlParameter
            {
                ParameterName = "@PlanID",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@PlanName", planName),
                new SqlParameter("@PlanDetails", planDetails),
                new SqlParameter("@CreatedAt", DateTime.UtcNow),
                planIdParam
            };

            await Database.ExecuteSqlRawAsync("EXEC AddInsurancePlan @PlanID OUTPUT, @PlanName, @PlanDetails, @CreatedAt", parameters);

            return (int)planIdParam.Value;
        }


        public async Task DeleteInsurancePlanAsync(int planId)
        {
            var parameter = new SqlParameter("@PlanID", planId);
            await Database.ExecuteSqlRawAsync("EXEC DeleteInsurancePlan @PlanID", parameter);
        }

        public async Task UpdateInsurancePlanAsync(int planId, string planName, string planDetails)
        {
            var parameters = new[]
            {
            new SqlParameter("@PlanID", planId),
            new SqlParameter("@PlanName", planName),
            new SqlParameter("@PlanDetails", planDetails)
        };

            await Database.ExecuteSqlRawAsync("EXEC UpdateInsurancePlan @PlanID, @PlanName, @PlanDetails", parameters);
        }

        public async Task<List<InsurancePlan>> GetAllInsurancePlansAsync()
        {
            return await InsurancePlans.FromSqlRaw("EXEC GetAllInsurancePlans").ToListAsync();
        }

        public async Task<InsurancePlan> GetInsurancePlanByIdAsync(int planId)
        {
            var planIdParam = new SqlParameter("@PlanID", planId);

            var result = await InsurancePlans
                .FromSqlRaw("EXEC GetInsurancePlanById @PlanID", planIdParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }


        // payments 
        public async Task AddPaymentAsync(int customerId, int policyId, double amount)
        {
            var parameters = new[]
            {
            new SqlParameter("@CustomerID", customerId),
            new SqlParameter("@PolicyID", policyId),
            new SqlParameter("@Amount", amount)
            };

            await Database.ExecuteSqlRawAsync("EXEC AddPayment @CustomerID, @PolicyID, @Amount", parameters);
        }

        //  Login 
        // Method to call GetEmployeeByEmail stored procedure
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var emailParam = new SqlParameter("@Email", email);

            var result = await Employees
                .FromSqlRaw("EXEC GetEmployeeByEmail @Email", emailParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        // Method to call GetAdminByEmail stored procedure
        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            var emailParam = new SqlParameter("@Email", email);

            var result = await Admins
                .FromSqlRaw("EXEC GetAdminByEmail @Email", emailParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        // Method to call GetCustomerByEmail stored procedure
        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var emailParam = new SqlParameter("@Email", email);

            var result = await Customers
                .FromSqlRaw("EXEC GetCustomerByEmail @Email", emailParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        // Method to call GetInsuranceAgentByEmail stored procedure
        public async Task<InsuranceAgent> GetInsuranceAgentByEmailAsync(string email)
        {
            var emailParam = new SqlParameter("@Email", email);

            var result = await InsuranceAgents
                .FromSqlRaw("EXEC GetInsuranceAgentByEmail @Email", emailParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
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
