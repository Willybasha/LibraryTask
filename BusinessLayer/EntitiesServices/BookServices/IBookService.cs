using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelViews.BookModels;
using RepositoryLayer.Entities;

namespace BusinessLayer.EntitiesServices.BookServices
{
    public interface IBookService 
    {
        Task  AddBookAsync(BookRequestModel model);
        Task<bool> UpdateBookAsync(BookUpdateModel model);
        Task<bool> DeleteBookAsync(int id);
        Task<(IEnumerable<Book> Items, int TotalCount)> GetBooksAsync(int page, int pageSize);

        Task<Book> GetBookByIdAsync(int id);
    }
}
