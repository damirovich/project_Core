using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SUBDCORE.Models
{
    public partial class ProzivContext : DbContext
    {
        public ProzivContext()
        {
        }

        public ProzivContext(DbContextOptions<ProzivContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budget { get; set; }
        public virtual DbSet<Credits> Credits { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<FinishedProducts> FinishedProducts { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<LoanRepayment> LoanRepayment { get; set; }
        public virtual DbSet<Manufacture> Manufacture { get; set; }
        public virtual DbSet<Month> Month { get; set; }
        public virtual DbSet<Payroll> Payroll { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        public virtual DbSet<PurchaseOfCheese> PurchaseOfCheese { get; set; }
        public virtual DbSet<RawMaterials> RawMaterials { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public virtual DbSet<Years> Years { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-L449UJT;Database=Proziv;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasKey(e => e.IdBudget);

                entity.Property(e => e.IdBudget).HasColumnName("id_budget");

                entity.Property(e => e.BudgetSum).HasColumnName("budget_sum");
            });

            modelBuilder.Entity<Credits>(entity =>
            {
                entity.HasKey(e => e.IdCredits);

                entity.Property(e => e.IdCredits).HasColumnName("id_Credits");

                entity.Property(e => e.CreaditAmount).HasColumnType("money");

                entity.Property(e => e.DateOfIssue).HasColumnType("smalldatetime");

                entity.Property(e => e.Fine).HasColumnType("money");

                entity.Property(e => e.MonthlyPayment)
                    .HasColumnName("monthlyPayment")
                    .HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.IdEmployees);

                entity.HasIndex(e => e.Position);

                entity.Property(e => e.IdEmployees).HasColumnName("id_employees");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasColumnName("Full_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Position)
                    .HasConstraintName("FK_Employees_Position");
            });

            modelBuilder.Entity<FinishedProducts>(entity =>
            {
                entity.HasKey(e => e.IdFinishedProducts);

                entity.ToTable("Finished_Products");

                entity.HasIndex(e => e.UnitOfMeasure);

                entity.Property(e => e.IdFinishedProducts).HasColumnName("id_finished_Products");

                entity.Property(e => e.Names)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summ).HasColumnType("money");

                entity.Property(e => e.UnitOfMeasure).HasColumnName("Unit_Of_Measure");

                entity.HasOne(d => d.UnitOfMeasureNavigation)
                    .WithMany(p => p.FinishedProducts)
                    .HasForeignKey(d => d.UnitOfMeasure)
                    .HasConstraintName("FK_Finished_Products_Unit_Of_Measure");
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => e.IdIngredients);

                entity.HasIndex(e => e.Manufacturing);

                entity.HasIndex(e => e.RawMaterials);

                entity.Property(e => e.IdIngredients).HasColumnName("id_ingredients");

                entity.Property(e => e.RawMaterials).HasColumnName("Raw_Materials");

                entity.HasOne(d => d.ManufacturingNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.Manufacturing)
                    .HasConstraintName("FK_Ingredients_Finished_Products");

                entity.HasOne(d => d.RawMaterialsNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.RawMaterials)
                    .HasConstraintName("FK_Ingredients_Raw_Materials");
            });

            modelBuilder.Entity<LoanRepayment>(entity =>
            {
                entity.HasKey(e => e.IdLoanRepayment);

                entity.Property(e => e.IdLoanRepayment).HasColumnName("Id_loanRepayment");

                entity.Property(e => e.CreaditId).HasColumnName("Creadit_Id");

                entity.Property(e => e.DateOfPay).HasColumnType("smalldatetime");

                entity.Property(e => e.ExpiredDays).HasColumnName("expiredDays");

                entity.Property(e => e.Fine).HasColumnType("money");

                entity.Property(e => e.MonthlyPayment)
                    .HasColumnName("monthlyPayment")
                    .HasColumnType("money");

                entity.Property(e => e.TotalPayment).HasColumnType("money");

                entity.HasOne(d => d.Creadit)
                    .WithMany(p => p.LoanRepayment)
                    .HasForeignKey(d => d.CreaditId)
                    .HasConstraintName("FK_LoanRepayment_Credits");

                entity.HasOne(d => d.MonthForPayNavigation)
                    .WithMany(p => p.LoanRepayment)
                    .HasForeignKey(d => d.MonthForPay)
                    .HasConstraintName("FK_LoanRepayment_Month");
            });

            modelBuilder.Entity<Manufacture>(entity =>
            {
                entity.HasKey(e => e.IdManufacture);

                entity.HasIndex(e => e.Employees);

                entity.HasIndex(e => e.Production);

                entity.Property(e => e.IdManufacture).HasColumnName("Id_Manufacture");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.HasOne(d => d.EmployeesNavigation)
                    .WithMany(p => p.Manufacture)
                    .HasForeignKey(d => d.Employees)
                    .HasConstraintName("FK_Manufacture_Employees");

                entity.HasOne(d => d.ProductionNavigation)
                    .WithMany(p => p.Manufacture)
                    .HasForeignKey(d => d.Production)
                    .HasConstraintName("FK_Manufacture_Finished_Products");
            });

            modelBuilder.Entity<Month>(entity =>
            {
                entity.HasKey(e => e.IdMonth);

                entity.Property(e => e.IdMonth)
                    .HasColumnName("id_Month")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MonthName)
                    .HasColumnName("Month_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.HasKey(e => e.IdSalary);

                entity.HasIndex(e => e.IdEmployee);

                entity.HasIndex(e => e.Month);

                entity.HasIndex(e => e.Years);

                entity.Property(e => e.IdSalary).HasColumnName("id_Salary");

                entity.Property(e => e.DataPayroll).HasColumnType("smalldatetime");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.InitalProsent)
                    .HasColumnName("initalProsent")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InitalSalary)
                    .HasColumnName("initalSalary")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProsentZp)
                    .HasColumnName("ProsentZP")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SumSalary).HasColumnType("money");

                entity.Property(e => e.SummaSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WagePrem).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WorkPerformed).HasColumnName("Work_performed");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK_Payroll_Employees");

                entity.HasOne(d => d.MonthNavigation)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.Month)
                    .HasConstraintName("FK_Payroll_Month");

                entity.HasOne(d => d.YearsNavigation)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.Years)
                    .HasConstraintName("FK_Payroll_Years");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition)
                    .HasName("PK_Position_1");

                entity.Property(e => e.IdPosition)
                    .HasColumnName("id_position")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Position1)
                    .HasColumnName("Position")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSales>(entity =>
            {
                entity.HasKey(e => e.IdProductSales);

                entity.ToTable("Product_Sales");

                entity.HasIndex(e => e.Employees);

                entity.HasIndex(e => e.FinishedProduct);

                entity.Property(e => e.IdProductSales).HasColumnName("id_ProductSales");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.FinishedProduct).HasColumnName("Finished_Product");

                entity.Property(e => e.Summ).HasColumnType("money");

                entity.HasOne(d => d.EmployeesNavigation)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.Employees)
                    .HasConstraintName("FK_Product_Sales_Employees");

                entity.HasOne(d => d.FinishedProductNavigation)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.FinishedProduct)
                    .HasConstraintName("FK_Product_Sales_Finished_Products");
            });

            modelBuilder.Entity<PurchaseOfCheese>(entity =>
            {
                entity.HasKey(e => e.IdPurchaseOfcheese);

                entity.ToTable("Purchase_Of_Cheese");

                entity.HasIndex(e => e.Employees);

                entity.HasIndex(e => e.RawMaterials);

                entity.Property(e => e.IdPurchaseOfcheese).HasColumnName("id_PurchaseOfcheese");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.RawMaterials).HasColumnName("Raw_materials");

                entity.Property(e => e.Summ).HasColumnType("money");

                entity.HasOne(d => d.EmployeesNavigation)
                    .WithMany(p => p.PurchaseOfCheese)
                    .HasForeignKey(d => d.Employees)
                    .HasConstraintName("FK_Purchase_Of_Cheese_Employees");

                entity.HasOne(d => d.RawMaterialsNavigation)
                    .WithMany(p => p.PurchaseOfCheese)
                    .HasForeignKey(d => d.RawMaterials)
                    .HasConstraintName("FK_Purchase_Of_Cheese_Raw_Materials");
            });

            modelBuilder.Entity<RawMaterials>(entity =>
            {
                entity.HasKey(e => e.IdRawMaterials);

                entity.ToTable("Raw_Materials");

                entity.HasIndex(e => e.UnitOfMeasure);

                entity.Property(e => e.IdRawMaterials).HasColumnName("id_RawMaterials");

                entity.Property(e => e.Names)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summ).HasColumnType("money");

                entity.Property(e => e.UnitOfMeasure).HasColumnName("Unit_Of_Measure");

                entity.HasOne(d => d.UnitOfMeasureNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.UnitOfMeasure)
                    .HasConstraintName("FK_Raw_Materials_Unit_Of_Measure");
            });

            modelBuilder.Entity<UnitOfMeasure>(entity =>
            {
                entity.HasKey(e => e.IdUnitOfmeasure);

                entity.ToTable("Unit_Of_Measure");

                entity.Property(e => e.IdUnitOfmeasure).HasColumnName("id_UnitOfmeasure");

                entity.Property(e => e.Names)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Years>(entity =>
            {
                entity.HasKey(e => e.IdYears);

                entity.Property(e => e.IdYears).HasColumnName("id_Years");

                entity.Property(e => e.YearsName).HasColumnName("Years_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
