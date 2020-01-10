using EFDemos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpIndividuel.Interfaces;
using TpIndividuel.Model;

namespace TpIndividuel.Repositories
{
	class ArticleRepository : IArticle
    {
        private readonly DemoDbContext context;

        public ArticleRepository(DemoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Article FindById(int id)
        {
            using (var ctx = new DemoDbContext())
            {
                var article = ctx.Articles.Where(m => m.Id == id).First();
                return article;
            }
        }

        public IEnumerable<Article> GetAll()
        {
            using (var ctx = new DemoDbContext())
            {
                var article = ctx.Articles.ToList();
                return article;
            }
        }
        public IEnumerable<Article> GetAllByEtagere(PositionMagasin pm, Etagere etagere)
        {
            using (var ctx = new DemoDbContext())
            {
                var article = ctx.Articles.Where(m=>m.Id == pm.IdArticle && pm.IdEtagere == etagere.Id).ToList();
                return article;
            }
        }

        public IEnumerable<Article> GetAllBySecteur(PositionMagasin pm, Etagere etagere, Secteur secteur)
        {
            using (var ctx = new DemoDbContext())
            {
                var article = ctx.Articles.Where(m => m.Id == pm.IdArticle && pm.IdEtagere == etagere.Id && secteur.Id == etagere.IdSecteur).ToList();
                return article;
            }
        }

        public void Insert(Article article, PositionMagasin pm, Etagere etagere)
        {
            var totalWeight = 0.0;
            using (var ctx = new DemoDbContext())
            {
                var articlesEtagere = GetAllByEtagere(pm, etagere);
                foreach(Article art in articlesEtagere)
                {
                    totalWeight += art.Poids;
                }
                totalWeight += article.Poids;
                if(totalWeight > etagere.PoidsMaximum)
                {
                    Console.WriteLine("L'étagère contient trop d'article");
                }
                else
                {
                    ctx.Articles.Add(article);
                    ctx.SaveChanges();
                }
            }
        }



        public void Remove(Article article)
        {
            using (var ctx = new DemoDbContext())
            {
                ctx.Articles.Remove(article);
            }
        }


        public void Save()
        {
            using (var ctx = new DemoDbContext())
            {
                ctx.SaveChanges();
            }
        }

        public void Update(Article article)
        {
            using (var ctx = new DemoDbContext())
            {
                var result = ctx.Articles.SingleOrDefault(a => a.Id == article.Id);
                if (result != null)
                {
                    result = article;
                    ctx.SaveChanges();
                }
            }
        }
        public double GetAveragePrice(PositionMagasin pm, Etagere etagere, Secteur secteur)
        {
            using (var ctx = new DemoDbContext())
            {
                var articles = GetAllBySecteur(pm, etagere, secteur);
                var articlesTotal = 0.0;
                foreach(Article article in articles)
                {
                    articlesTotal += article.PrixInitial;
                }
                var avg = articlesTotal / articles.Count();
                return avg;
            }
        }
    }

}
