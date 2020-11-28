using System;
using Covid.Server.Entities;
using Covid.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Covid.Server.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 设置联合主键
            modelBuilder.Entity<DailyHealth>()
                        .HasKey(x => new
                        {
                            x.EmployeeId,
                            x.Date
                        });

            // 设置一对多的外键关系
            // 员工与部门之间是一对多的关系
            modelBuilder.Entity<Employee>()
                        .HasOne<Department>()
                        .WithMany()
                        .HasForeignKey(x => x.DepartmentId)
                        .OnDelete(DeleteBehavior.Restrict);

            // 设置一对多的外键关系
            // 员工与每日健康之间是一对多的关系
            modelBuilder.Entity<DailyHealth>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // 初始化种子数据
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "研发部" },
                new Department { Id = 2, Name = "销售部" },
                new Department { Id = 3, Name = "采购部" });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    DepartmentId = 1,
                    No = "A01",
                    Name = "Nick Carter",
                    PictureUrl = "",
                    BirthDate = new DateTime(1980, 1, 1),
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = 2,
                    DepartmentId = 1,
                    No = "A02",
                    Name = "Mike Seaver",
                    PictureUrl = "",
                    BirthDate = new DateTime(1975, 4, 5),
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = 3,
                    DepartmentId = 2,
                    No = "B01",
                    Name = "Sarah Jackson",
                    PictureUrl = "",
                    BirthDate = new DateTime(1989, 4, 7),
                    Gender = Gender.Female
                },
                new Employee
                {
                    Id = 4,
                    DepartmentId = 2,
                    No = "B02",
                    Name = "Mary Bloody",
                    PictureUrl = "",
                    BirthDate = new DateTime(1995, 11, 13),
                    Gender = Gender.Female
                },
                new Employee
                {
                    Id = 5,
                    DepartmentId = 3,
                    No = "C01",
                    Name = "Micheal Jackson",
                    PictureUrl = "",
                    BirthDate = new DateTime(1975, 12, 23),
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = 6,
                    DepartmentId = 3,
                    No = "C02",
                    Name = "Jack Sparrow",
                    PictureUrl = "",
                    BirthDate = new DateTime(1775, 3, 24),
                    Gender = Gender.Male
                });
        }

        public DbSet<DailyHealth> DailyHealths { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}