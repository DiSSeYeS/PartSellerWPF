﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PartSellerWPF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        private static Entities _context;
        public Entities()
            : base("name=Entities")
        {
        }
        public static Entities GetContext()
        {
            return _context == null ? _context = new Entities() : _context;
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Case> Case { get; set; }
        public virtual DbSet<Chipset> Chipset { get; set; }
        public virtual DbSet<CoolerType> CoolerType { get; set; }
        public virtual DbSet<Cooling> Cooling { get; set; }
        public virtual DbSet<CPU> CPU { get; set; }
        public virtual DbSet<Disk> Disk { get; set; }
        public virtual DbSet<DiskType> DiskType { get; set; }
        public virtual DbSet<FormFactor> FormFactor { get; set; }
        public virtual DbSet<GPU> GPU { get; set; }
        public virtual DbSet<Motherboard> Motherboard { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Part> Part { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<RAM> RAM { get; set; }
        public virtual DbSet<RAMType> RAMType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Socket> Socket { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Supply> Supply { get; set; }
        public virtual DbSet<SupportedFormFactorMotherboard> SupportedFormFactorMotherboard { get; set; }
        public virtual DbSet<SupportedFormFactorSupply> SupportedFormFactorSupply { get; set; }
        public virtual DbSet<SupportedSockets> SupportedSockets { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
