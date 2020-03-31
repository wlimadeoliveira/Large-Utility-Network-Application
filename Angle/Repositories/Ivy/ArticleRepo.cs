using Angle.Interfaces.Ivy;
using Angle.Models.Models.Ivy;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Repositories.Ivy
{
    public class ArticleRepo : IArticle
    {
        private readonly ProjectDataContext _context;

        public ArticleRepo(ProjectDataContext context)
        {
            _context = context;
        }


        public void Delete(Article article)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetAll()
        {
            return _context.Article.ToList();
        }

        public Article GetById(long Id)
        {
            return _context.Article.FirstOrDefault(x => x.ID == Id);
        }

        public Article GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Article article)
        {
            _context.Article.Add(article);
        }

        public void Update(Article article)
        {
            _context.Article.Update(article);
        }
    }
}
