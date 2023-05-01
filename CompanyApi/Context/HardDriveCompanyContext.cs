using System;
using System.Collections.Generic;
using CompanyApi.Models.Posgres;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Context;

public partial class HardDriveCompanyContext : DbContext
{
    public HardDriveCompanyContext()
    {
    }

    public HardDriveCompanyContext(DbContextOptions<HardDriveCompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConnectionInterfaceType> ConnectionInterfaceTypes { get; set; }

    public virtual DbSet<ContactPerson> ContactPeople { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractClassifier> ContractClassifiers { get; set; }

    public virtual DbSet<DriveTypeP> DriveTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesPosition> EmployeesPositions { get; set; }

    public virtual DbSet<HardDrive> HardDrives { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }

    public virtual DbSet<PriorityTask> PriorityTasks { get; set; }

    public virtual DbSet<SelectStatistic> SelectStatistics { get; set; }

    public virtual DbSet<TaskP> Tasks { get; set; }

    public virtual DbSet<TaskReceiptType> TaskReceiptTypes { get; set; }

    public virtual DbSet<TaskStatusP> TaskStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=hard_drive_company;User Id=adef;Password=199as55");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<ConnectionInterfaceType>(entity =>
        {
            entity.HasKey(e => e.ConnectionInterfaceId).HasName("connection_interface_type_pkey");

            entity.ToTable("connection_interface_type");

            entity.Property(e => e.ConnectionInterfaceId).HasColumnName("connection_interface_id");
            entity.Property(e => e.InterfaceName)
                .HasMaxLength(20)
                .HasColumnName("interface_name");
            entity.Property(e => e.TransferRate).HasColumnName("transfer_rate");
        });

        modelBuilder.Entity<ContactPerson>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("contact_person_pkey");

            entity.ToTable("contact_person");

            entity.HasIndex(e => e.Address, "contact_person_address_index");

            entity.HasIndex(e => e.Email, "contact_person_email_index");

            entity.HasIndex(e => e.FullName, "contact_person_full_name_index");

            entity.HasIndex(e => e.PhoneNumber, "contact_person_phone_index");

            entity.HasIndex(e => e.PostalCode, "contact_person_postal_code_index");

            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .HasColumnName("postal_code");

            entity.HasOne(d => d.Organization).WithMany(p => p.ContactPeople)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("contact_person_organization_id_fkey");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("contract_pkey");

            entity.ToTable("contract");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.Info).HasColumnName("info");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("contract_person_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("contract_type_id_fkey");
        });

        modelBuilder.Entity<ContractClassifier>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("contract_classifier_pkey");

            entity.ToTable("contract_classifier");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<DriveTypeP>(entity =>
        {
            entity.HasKey(e => e.DriveTypeId).HasName("drive_type_pkey");

            entity.ToTable("drive_type");

            entity.Property(e => e.DriveTypeId).HasColumnName("drive_type_id");
            entity.Property(e => e.DriveTypeName)
                .HasMaxLength(10)
                .HasColumnName("drive_type_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.HasIndex(e => e.EmployeeLogin, "employee_employee_login_key").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeLogin).HasColumnName("employee_login");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .HasColumnName("phone_number");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employee_position_id_fkey");
        });

        modelBuilder.Entity<EmployeesPosition>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("employees_position_pkey");

            entity.ToTable("employees_position");

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.PositionName)
                .HasMaxLength(15)
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<HardDrive>(entity =>
        {
            entity.HasKey(e => e.SerialNumber).HasName("hard_drive_pkey");

            entity.ToTable("hard_drive");

            entity.Property(e => e.SerialNumber).HasColumnName("serial_number");
            entity.Property(e => e.ConnectionInterfaceId).HasColumnName("connection_interface_id");
            entity.Property(e => e.DriveName)
                .HasMaxLength(100)
                .HasColumnName("drive_name");
            entity.Property(e => e.DriveSize).HasColumnName("drive_size");
            entity.Property(e => e.DriveTypeId).HasColumnName("drive_type_id");

            entity.HasOne(d => d.ConnectionInterface).WithMany(p => p.HardDrives)
                .HasForeignKey(d => d.ConnectionInterfaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("hard_drive_connection_interface_id_fkey");

            entity.HasOne(d => d.DriveTypeP).WithMany(p => p.HardDrives)
                .HasForeignKey(d => d.DriveTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("hard_drive_drive_type_id_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("organization_pkey");

            entity.ToTable("organization");

            entity.HasIndex(e => e.Address, "organization_address_index");

            entity.HasIndex(e => e.Email, "organization_email_index");

            entity.HasIndex(e => e.OrganizationName, "organization_name_index");

            entity.HasIndex(e => e.PhoneNumber, "organization_phone_index");

            entity.HasIndex(e => e.PostalCode, "organization_postal_code_index");

            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(100)
                .HasColumnName("organization_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .HasColumnName("postal_code");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("organization_type_id_fkey");
        });

        modelBuilder.Entity<OrganizationType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("organization_type_pkey");

            entity.ToTable("organization_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(13)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<PriorityTask>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("priority_task_pkey");

            entity.ToTable("priority_task");

            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(7)
                .HasColumnName("priority_name");
        });

        modelBuilder.Entity<SelectStatistic>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("select_statistic");

            entity.Property(e => e.AllTasksCnt).HasColumnName("all_tasks_cnt");
            entity.Property(e => e.TasksCmpltOnTimeCnt).HasColumnName("tasks_cmplt_on_time_cnt");
            entity.Property(e => e.TasksCmpltOutTimeCnt).HasColumnName("tasks_cmplt_out_time_cnt");
            entity.Property(e => e.TasksNotCmpltOnTimeCnt).HasColumnName("tasks_not_cmplt_on_time_cnt");
            entity.Property(e => e.TasksNotCmpltOutTimeCnt).HasColumnName("tasks_not_cmplt_out_time_cnt");
        });

        modelBuilder.Entity<TaskP>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("task_pkey");

            entity.ToTable("task");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CompletionDate).HasColumnName("completion_date");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.CreateDate).HasColumnName("create_date");
            entity.Property(e => e.ExecutionPeriod).HasColumnName("execution_period");
            entity.Property(e => e.ExecutorId).HasColumnName("executor_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.SerialNumber).HasColumnName("serial_number");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Author).WithMany(p => p.TaskAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_author_id_fkey");

            entity.HasOne(d => d.Contract).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_contract_id_fkey");

            entity.HasOne(d => d.Executor).WithMany(p => p.TaskExecutors)
                .HasForeignKey(d => d.ExecutorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_executor_id_fkey");

            entity.HasOne(d => d.Person).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_person_id_fkey");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_priority_id_fkey");

            entity.HasOne(d => d.Receipt).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_receipt_id_fkey");

            entity.HasOne(d => d.SerialNumberNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.SerialNumber)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_serial_number_fkey");

            entity.HasOne(d => d.StatusP).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("task_status_id_fkey");
        });

        modelBuilder.Entity<TaskReceiptType>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("task_receipt_type_pkey");

            entity.ToTable("task_receipt_type");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.ReceiptName)
                .HasMaxLength(50)
                .HasColumnName("receipt_name");
        });

        modelBuilder.Entity<TaskStatusP>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("task_status_pkey");

            entity.ToTable("task_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(12)
                .HasColumnName("status_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
