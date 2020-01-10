using System;
using System.Collections.Generic;
using System.Text;
using TpIndividuel.Model;

namespace TpIndividuel.Interfaces
{
    public interface IArticle
    {
        void Insert(Article article, Etagere etagere);
        void Update(Article article);
        void Remove(Article article);
        void Save();
        IEnumerable<Article> GetAll();
        IEnumerable<Article> GetAllByEtagere(PositionMagasin pm, Etagere etagere);
        IEnumerable<Article> GetAllBySecteur(PositionMagasin pm, Etagere etagere, Secteur secteur);
        double GetAveragePrice(PositionMagasin pm, Etagere etagere, Secteur secteur);
        Article FindById(int id);
    }
}
