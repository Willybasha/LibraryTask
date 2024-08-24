using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelViews.BorrowTransactionModels;
using RepositoryLayer.Model;

namespace BusinessLayer.EntitiesServices.BorrowTransactionServices
{
    public interface IBorrowTransactionService
    {
        Task<(bool IsSuccess, string Message)> BorrowBookAsync(BorrowTransactionRequestModel model);
        Task<(bool IsSuccess, string Message)> ReturnBookAsync(int transactionId);
        Task<(IEnumerable<BorrowTransaction> Items, int TotalCount)> GetBorrowTransactionsAsync(int page, int pageSize);
      

    }
}
