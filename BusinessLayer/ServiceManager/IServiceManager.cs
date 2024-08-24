using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.EntitiesServices.BookServices;
using BusinessLayer.EntitiesServices.BorrowerServices;
using BusinessLayer.EntitiesServices.BorrowTransactionServices;
using RepositoryLayer.EntitiesRepos.BorrowTransactionRepo;

namespace BusinessLayer.ServiceManager
{
    public interface IServiceManager
    {
        IBookService BookService { get; }
        IBorrowerService BorrowerService { get; }
        IBorrowTransactionService BorrowTransactionService { get; }
    }
}
