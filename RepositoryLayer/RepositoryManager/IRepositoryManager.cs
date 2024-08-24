using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.EntitiesRepos.BooksRepo;
using RepositoryLayer.EntitiesRepos.BorrowesRepo;
using RepositoryLayer.EntitiesRepos.BorrowTransactionRepo;

namespace RepositoryLayer.RepositoryManager
{
    public interface IRepositoryManager
    {
        IBooksRepo BooksRepo { get; }
        IBorrowersRepo BorrowersRepo { get; }
        IBorrowTransactionRepo BorrowTransactionRepo { get; }
        Task SaveAsync();
    }
}
