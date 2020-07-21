using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models;
using Angle.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Angle.Models.ViewModels.AccountViewModel;
using Angle.Models.Models.Ivy;

namespace LUNA.Models.Models
{
    public class ProjectDataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<ProjectArticle> ProjectArticles { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Attribute> Attribute { get; set; }
        public DbSet<PType> PType { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<StockInformation> StockInformation { get; set; }
        // public DbSet<QuickAdventure> QuickAdventure { get; set; }
        public DbSet<Action_QR> Action_QR { get; set; }
        public DbSet<Controller_QR> Controller_QR { get; set; }
        public DbSet<Index_QR> Index_QR { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public ProjectDataContext(DbContextOptions<ProjectDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProjectPerson>()
                  .HasKey(pp => new { pp.ProjectID, pp.PersonID });
            builder.Entity<ProjectPerson>()
                .HasOne(pp => pp.Project)
                .WithMany(p => p.ProjectPersons)
                .HasForeignKey(pp => pp.ProjectID);
            builder.Entity<ProjectPerson>()
                .HasOne(pp => pp.Person)
                .WithMany(p => p.ProjectPersons)
                .HasForeignKey(p => p.PersonID);

            builder.Entity<ProductAttribute>()
                .HasKey(pa => new { pa.ProductID, pa.AttributeID });
            builder.Entity<ProductAttribute>()
              .HasOne(pa => pa.Product)
              .WithMany(a => a.ProductAttributes)
              .HasForeignKey(a => a.ProductID);
            builder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Attribute)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(p => p.AttributeID);










            builder.Entity<ProductHistory>()
            .HasKey(ph => new { ph.ProductID, ph.HistoryID, ph.UserID, ph.Date });
            builder.Entity<ProductHistory>()
              .HasOne(ph => ph.Product)
              .WithMany(a => a.ProductHistories)
              .HasForeignKey(a => a.ProductID);
            builder.Entity<ProductHistory>()
                .HasOne(ph => ph.History)
                .WithMany(p => p.ProductHistories)
                .HasForeignKey(p => p.HistoryID);
            builder.Entity<ProductHistory>()
                  .HasOne(ph => ph.User)
                  .WithMany(a => a.ProductHistories)
                  .HasForeignKey(a => a.UserID);

            builder.Entity<ProductType>()
            .HasKey(pf => new { pf.ProductID, pf.DeviceTypeID });
            builder.Entity<ProductType>()
              .HasOne(pf => pf.Product)
              .WithMany(p => p.ProductTypes)
              .HasForeignKey(a => a.ProductID);
            builder.Entity<ProductType>()
                .HasOne(pf => pf.DeviceType)
                .WithMany(p => p.ProductTypes)
                .HasForeignKey(p => p.DeviceTypeID);

            builder.Entity<TypeAttribute>()
            .HasKey(pf => new { pf.DeviceTypeID, pf.AttributeID });
            builder.Entity<TypeAttribute>()
              .HasOne(pf => pf.DeviceType)
              .WithMany(p => p.TypeAttributes)
              .HasForeignKey(a => a.DeviceTypeID);
            builder.Entity<TypeAttribute>()
                .HasOne(pf => pf.Attribute)
                .WithMany(p => p.TypeAttributes)
                .HasForeignKey(p => p.AttributeID);

            builder.Entity<TypeFeature>()
 .HasKey(pf => new { pf.DeviceTypeID, pf.FeatureID });
            builder.Entity<TypeFeature>()
              .HasOne(pf => pf.DeviceType)
              .WithMany(p => p.TypeFeatures)
              .HasForeignKey(a => a.DeviceTypeID);
            builder.Entity<TypeFeature>()
                .HasOne(pf => pf.Feature)
                .WithMany(p => p.TypeFeature)
                .HasForeignKey(p => p.FeatureID);
            builder.Entity<TypeChild>()
 .HasKey(pf => new { pf.TypeID, pf.ChildID });
            builder.Entity<TypeChild>()
              .HasOne(pf => pf.Type)
              .WithMany(p => p.Childs)
              .HasForeignKey(a => a.TypeID);
            builder.Entity<Product>()
                .HasOne(p => p.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<ProjectArticle>()
.HasKey(pf => new { pf.ProjectID, pf.ArticleID });
            builder.Entity<ProjectArticle>()
              .HasOne(pf => pf.Project)
              .WithMany(p => p.ProjectArticles)
              .HasForeignKey(a => a.ProjectID);
            builder.Entity<ProjectArticle>()
                .HasOne(pf => pf.Article)
                .WithMany(p => p.ProjectArticles)
                .HasForeignKey(p => p.ArticleID);
            /*       builder.Entity<QuickAdventure>()
       .HasKey(qa => new { qa.ProductID, qa.UserID });
                   builder.Entity<QuickAdventure>()
                     .HasOne(qa => qa.Product)
                     .WithMany(qa => qa.QuickAdventures )
                     .HasForeignKey(qa => qa.ProductID);
                   builder.Entity<QuickAdventure>()
                       .HasOne(qa => qa.User)
                       .WithMany(qa => qa.QuickAdventures)
                       .HasForeignKey(qa => qa.UserID);*/



            // Defining Composite Keys for Index_QR, Composite of ActionID, ControllerID and ProductID
            builder.Entity<Index_QR>()
             .HasKey(qr => new { qr.Id });
            builder.Entity<Index_QR>()
               .HasOne(qr => qr.Action_QR)
               .WithMany(qr => qr.Index_QRs)
               .HasForeignKey(qr => qr.ActionID);
            builder.Entity<Index_QR>()
                .HasOne(qr => qr.Controller_QR)
                .WithMany(qr => qr.Index_QRs)
                .HasForeignKey(qr => qr.ControllerID);
            builder.Entity<Index_QR>()
                .HasOne(qr => qr.Product)
                .WithMany(qr => qr.Index_QRs)
                .HasForeignKey(qr => qr.ProductID);
            builder.Entity<Index_QR>()
                .HasOne(qr => qr.User)
                .WithMany(qr => qr.Index_QRs)
                .HasForeignKey(qr => qr.UserID)
                    .HasConstraintName("UserID")
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
