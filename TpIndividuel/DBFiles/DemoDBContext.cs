using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TpIndividuel.Model;

namespace EFDemos
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // création de la classe de configuration pour l'entité "Article"
            var articleEntity = modelBuilder.Entity<Article>();
            
            articleEntity.HasKey(m => m.Id); 
            articleEntity.Property(m => m.Libelle).HasMaxLength(256).IsRequired();
            articleEntity.Property(m => m.SKU).HasMaxLength(256).IsRequired();
            articleEntity.Property(m => m.DateSortie).IsRequired();
            articleEntity.Property(m => m.Poids).IsRequired();
            articleEntity.Property(m => m.PrixInitial).IsRequired(); 
            articleEntity // Un article a ...
             .HasMany(e => e.EtagereIds) // plusieurs étageres ...
             .WithOne(a => a.Article) // qui le connaissent ...
             .HasForeignKey(a => a.IdArticle); // par son id 

            // création de la classe de configuration pour l'entité "Etagere"
            var etagereEntity = modelBuilder.Entity<Etagere>();

            etagereEntity.HasKey(m => m.Id);
            etagereEntity.Property(m => m.PoidsMaximum).IsRequired();
            etagereEntity // Une étagère a ...
             .HasMany(a => a.ArticleIds) // plusieurs articles ...
             .WithOne(e => e.Etagere) // qui la connaissent ...
             .HasForeignKey(e => e.IdEtagere); // par son id 

            etagereEntity // Plusieurs étagères ont ...
             .HasOne(a => a.ReferentSecteur) // un secteur...
             .WithMany(e => e.ReferalsEtagere) // qui les connait ...
             .HasForeignKey(e => e.Id); // par leur id 

            // création de la classe de configuration pour l'entité "PositionMagasin"
            var positionEntity = modelBuilder.Entity<PositionMagasin>();
            positionEntity.HasKey(m => new { m.IdArticle, m.IdEtagere }); // définition d'une clé composée
            positionEntity.Property(m => m.Quantite).IsRequired();

            // création de la classe de configuration pour l'entité "Secteur"
            var secteurEntity = modelBuilder.Entity<Secteur>();
            // définition du mapping
            secteurEntity.HasKey(m => m.Id);
            secteurEntity.Property(m => m.Nom).HasMaxLength(256).IsRequired();
            secteurEntity // Un secteur a ...
             .HasMany(a => a.ReferalsEtagere) // plusieurs etageres ...
             .WithOne(e => e.ReferentSecteur) // qui le connaissent ...
             .HasForeignKey(e => e.IdSecteur); // par son id 

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // définition de la base de données à utiliser ainsi que de la chaine de connexion
            optionsBuilder.UseSqlite("Filename=test.db");

            base.OnConfiguring(optionsBuilder);
        }

    }
}
