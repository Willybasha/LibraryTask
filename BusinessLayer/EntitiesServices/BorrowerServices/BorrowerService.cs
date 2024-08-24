using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelViews.BorrowerModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using RepositoryLayer.RepositoryManager;

namespace BusinessLayer.EntitiesServices.BorrowerServices
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IRepositoryManager _repository;
        public BorrowerService(IRepositoryManager repositoryManager)
        {
            _repository = repositoryManager;
            
        }

        public async Task<(IEnumerable<Borrower> Items, int TotalCount)> GetAllBorrowersAsync(int page, int pageSize)
        {
            try
            {
                var totalCount = _repository.BorrowersRepo.FindAll(trackChanges: false).Count();
                var books = await _repository.BorrowersRepo
                    .FindAll(trackChanges: false)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (books, totalCount);
            }
            catch (Exception ex) 
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task<Borrower> GetBorrowerByIdAsync(int id)
        {
            try
            {
                var borrower = await _repository.BorrowersRepo.FindByCondition(x => x.Id.Equals(id), false).FirstOrDefaultAsync();
                return (borrower);
            }
            catch (Exception ex) 
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task AddBorrowerAsync(BorrowerRequestModel model)
        {
            try
            {
                var borrower = new Borrower
                {
                    Name = model.Name,
                    Code = model.Code
                };
                _repository.BorrowersRepo.Create(borrower);
                await _repository.SaveAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception("Something Went Wrong");
            }
        }

        public async Task<bool> DeleteBorrowerAsync(int id)
        {
            try
            {
                var borrower = await _repository.BorrowersRepo.FindByCondition(x => x.Id == id, false).FirstOrDefaultAsync();
                if (borrower == null)
                    return false;

                // Check if the borrower has any associated borrow transactions
                var hasTransactions =await _repository.BorrowTransactionRepo.AnyAsync(br => br.BorrowerId == borrower.Id);

                if (hasTransactions)
                {
                    // Return a specific result indicating that deletion is not allowed
                    throw new InvalidOperationException("Cannot delete borrower with active transactions.");
                }

                _repository.BorrowersRepo.Delete(borrower);
                await _repository.SaveAsync();

                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
