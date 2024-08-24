using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.RepositoryBase;

namespace RepositoryLayer.EntitiesRepos.BooksRepo
{
    public class Booksrepo : RepositoryBase<Book>, IBooksRepo
    {
        public Booksrepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
