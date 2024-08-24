using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.EntitiesServices.BookServices;
using BusinessLayer.EntitiesServices.BorrowerServices;
using BusinessLayer.EntitiesServices.BorrowTransactionServices;
using Microsoft.AspNetCore.Hosting;
using RepositoryLayer.EntitiesRepos.BorrowTransactionRepo;
using RepositoryLayer.RepositoryManager;

namespace BusinessLayer.ServiceManager
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IBorrowerService> _borrowerService;
        private readonly Lazy<IBorrowTransactionService> _borrowTransactionService;

        public ServiceManager(IRepositoryManager repositoryManager,IHostingEnvironment hostingenviroment)
        {
            _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager, hostingenviroment));
            _borrowerService = new Lazy<IBorrowerService>(() =>new BorrowerService(repositoryManager));
            _borrowTransactionService = new Lazy<IBorrowTransactionService>(() => new BorrowTransactionService(repositoryManager));
        }
        public IBookService BookService =>_bookService.Value;
        public IBorrowerService BorrowerService =>_borrowerService.Value;
        public IBorrowTransactionService BorrowTransactionService =>_borrowTransactionService.Value;
    }
}
