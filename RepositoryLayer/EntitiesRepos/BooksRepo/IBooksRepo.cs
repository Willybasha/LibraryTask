using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entities;
using RepositoryLayer.RepositoryBase;

namespace RepositoryLayer.EntitiesRepos.BooksRepo
{
    public interface IBooksRepo : IRepositoryBase<Book>
    {
    }
}
