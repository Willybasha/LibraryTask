using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelViews.BorrowerModels;
using RepositoryLayer.Entities;

namespace BusinessLayer.EntitiesServices.BorrowerServices
{
    public interface IBorrowerService
    {
        Task<(IEnumerable<Borrower> Items, int TotalCount)> GetAllBorrowersAsync(int page, int pageSize);
        Task AddBorrowerAsync(BorrowerRequestModel model);
        Task<bool> DeleteBorrowerAsync(int id);
        Task<Borrower> GetBorrowerByIdAsync(int id);
    }
}
