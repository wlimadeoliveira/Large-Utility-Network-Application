using Angle.Models.Models.Ivy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces.Ivy
{
    public interface IArticle
    {
        List<Article> GetAll();
        Article GetById(long Id);
        Article GetByName(string name);
        void Insert(Article article);
        void Update(Article article);
        void Delete(Article article);
       
    }
}
