using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.paging;
using RepositoryLayer.Entities;
using RepositoryLayer.Model;

namespace BusinessLayer.ModelViews.BorrowTransactionModels
{
    public class BorrowTransactionListViewModel
    {
        public IEnumerable<BorrowTransaction> BorrowTransactions { get; set; }
        public IEnumerable<Borrower> Borrowers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Book> AvailableBooks { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
