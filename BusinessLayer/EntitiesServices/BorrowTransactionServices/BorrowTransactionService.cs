using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelViews.BorrowTransactionModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Model;
using RepositoryLayer.RepositoryManager;

namespace BusinessLayer.EntitiesServices.BorrowTransactionServices
{
    public class BorrowTransactionService : IBorrowTransactionService
    {
        private readonly IRepositoryManager _repository;
        public BorrowTransactionService(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<(IEnumerable<BorrowTransaction> Items, int TotalCount)> GetBorrowTransactionsAsync(int page, int pageSize)
        {
            try
            {
                var totalCount = _repository.BorrowTransactionRepo.FindAll(trackChanges: false).Count();
                var transactions = await _repository.BorrowTransactionRepo
                    .FindAll(trackChanges: false)
                    .Include(bt => bt.Borrower)
                    .Include(bt => bt.Book)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return (transactions, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong");
            }
        }

        public async Task<(bool IsSuccess, string Message)> BorrowBookAsync(BorrowTransactionRequestModel model)
        {

            try
            {
                var book = await _repository.BooksRepo.FindByCondition(b => b.Id == model.BookId, trackChanges: true).FirstOrDefaultAsync();
                var borrower = await _repository.BorrowersRepo.FindByCondition(b => b.Id == model.BorrowerId, trackChanges: true).FirstOrDefaultAsync();

                if (book == null || borrower == null)
                {
                    return (false, "Borrower or Book not found.");
                }

                // Check if the book has available copies
                if (book.AvailableCopies <= 0)
                {
                    return (false, "No copies left.");
                }

                // Decrease the available copies and save the transaction
                book.AvailableCopies--;

                var borrowTransaction = new BorrowTransaction
                {
                    BorrowerId = model.BorrowerId,
                    BookId = model.BookId,
                    BorrowDate = DateTime.Now
                };

                _repository.BorrowTransactionRepo.Create(borrowTransaction);

                await _repository.SaveAsync();

                return (true, "Borrowing successful.");
            }
            catch (Exception ex) 
            {
                throw new Exception("Something Went Wrong");
            }
        }
        public async Task<(bool IsSuccess, string Message)> ReturnBookAsync(int transactionId)
        {
            var transaction = await _repository.BorrowTransactionRepo.FindByCondition(x => x.Id.Equals(transactionId), true).Include(bk=>bk.Book).FirstOrDefaultAsync();

            if (transaction == null)
            {
                return (false, "Transaction not found.");
            }

            var book = await _repository.BooksRepo.FindByCondition(x=>x.Id ==transaction.BookId,true).FirstOrDefaultAsync();
            if (book == null)
            {
                return (false, "Book not found");  
            }

            // Check if the available copies are at maximum
            if (book.AvailableCopies >= book.MaxCopies)
            {
                _repository.BorrowTransactionRepo.Delete(transaction);
                await _repository.SaveAsync();
                return (false, "Already At Max Copies");
            }

            // Return the book
            book.AvailableCopies += 1;
            _repository.BorrowTransactionRepo.Delete(transaction);
            _repository.BooksRepo.Update(book);
            await _repository.SaveAsync();

            return (true,"Success");
        }
    }
}
