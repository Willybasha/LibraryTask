using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.EntitiesRepos.BooksRepo;
using RepositoryLayer.EntitiesRepos.BorrowesRepo;
using RepositoryLayer.EntitiesRepos.BorrowTransactionRepo;

namespace RepositoryLayer.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IBorrowersRepo> _borrowerRepo;
        private readonly Lazy<IBooksRepo> _booksRepo;
        private readonly Lazy<IBorrowTransactionRepo> _borrowTransactionRepo;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
            _booksRepo = new Lazy<IBooksRepo>(() => new Booksrepo(context));
            _borrowerRepo = new Lazy<IBorrowersRepo>(()=> new BorrowerRepo(context));
            _borrowTransactionRepo = new Lazy<IBorrowTransactionRepo>(() => new BorrowTransationRepo(context));
        }
        public IBooksRepo BooksRepo => _booksRepo.Value;
        public IBorrowersRepo BorrowersRepo => _borrowerRepo.Value;

        public IBorrowTransactionRepo BorrowTransactionRepo => _borrowTransactionRepo.Value;

        public async Task SaveAsync()=>await _context.SaveChangesAsync();
    }
}
