using HandCrafter.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace HandCrafter
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AddressDB> Addesses { get; set; }
        public DbSet<RoleDB> Roles { get; set; }
        public DbSet<UserDB> Users { get; set; }
        public DbSet<ContactDB> Contacts { get; set; }
        public DbSet<BasketDB> Baskets { get; set; }
        public DbSet<ProductDB> Products { get; set; }
        public DbSet<ProductColorDB> ProductsColors { get; set; }
        public DbSet<ColorDB> Colors { get; set; }
        public DbSet<ProductCompositionDB> ProductsCompositions { get; set; }
        public DbSet<CompositionDB> Compositions { get; set; }
        public DbSet<ProductCategoryDB> ProductsCategories { get; set; }
        public DbSet<CategoryDB> Categories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Связь между продуктами и категориями, через смежную таблицу
            modelBuilder.Entity<ProductDB>()
                .HasMany(e => e.ProductCategory)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.IdProduct).IsRequired();

            modelBuilder.Entity<CategoryDB>()
                .HasMany(e => e.ProductCategory)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.IdCategory).IsRequired();

            //Связь между продуктами и цветами, через смежную таблицу
            modelBuilder.Entity<ProductDB>()
                .HasMany(e => e.ProductColor)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.IdProduct).IsRequired();

            modelBuilder.Entity<ColorDB>()
                .HasMany(e => e.ProductColor)
                .WithOne(e => e.Color)
                .HasForeignKey(e => e.IdColor).IsRequired();

            //Связь между продуктами и свойствами, через смежную таблицу
            modelBuilder.Entity<ProductDB>()
                .HasMany(e => e.ProductComposition)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.IdProduct).IsRequired();

            modelBuilder.Entity<CompositionDB>()
                .HasMany(e => e.ProductComposition)
                .WithOne(e => e.Composition)
                .HasForeignKey(e => e.IdComposition).IsRequired();

            //Связь между юзером и продуктами, через смежную таблицу - корзину
            modelBuilder.Entity<ProductDB>()
                .HasMany(e => e.Basket)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.IdProduct).IsRequired();

            modelBuilder.Entity<UserDB>()
                .HasMany(e => e.Basket)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.IdUser).IsRequired();

            modelBuilder.Entity<RoleDB>()
                .HasMany(e => e.Contact) 
                .WithOne(e => e.Role) 
                .HasForeignKey(e => e.IdRole)
                .IsRequired();


            //Связь между юзером и контактами, 1 к 1
            modelBuilder.Entity<UserDB>()
                .HasOne(e => e.Contact)
                .WithOne(e => e.User)
                .HasForeignKey<ContactDB>(contact => contact.IdUser)
                .IsRequired();

            //Связь между юзером и адрессами, 1 ко многим
            modelBuilder.Entity<UserDB>()
                .HasMany(e => e.Address)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.IdUser).IsRequired();
        }
    }
}
