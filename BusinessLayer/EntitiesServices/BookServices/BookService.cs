using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entities;
using RepositoryLayer.EntitiesRepos.BooksRepo;
using RepositoryLayer.RepositoryManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.ModelViews.BookModels;

namespace BusinessLayer.EntitiesServices.BookServices
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _repository;

        private readonly IHostingEnvironment _hostingEnvironment;
        public BookService(IRepositoryManager repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task AddBookAsync(BookRequestModel model)
        {
            string imagePath = null;
            if (model.BookImage != null && model.BookImage.Length > 0)
            {
                // Define the path where the image will be saved
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.BookImage.FileName);  // Generate a unique name for the file
                imagePath = Path.Combine(uploadDir, fileName);

                // Ensure the directory exists
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Save the image to the defined path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await model.BookImage.CopyToAsync(fileStream);
                }

                // This is the relative URL path for storing in the database
                imagePath = $"/images/{fileName}";
            }
            var book = new Book
            {
                Title = model.Title,
                MaxCopies = model.MaxCopies,
                AvailableCopies = model.MaxCopies,
                ImageUrl = imagePath // Handle the image logic here
            };

            _repository.BooksRepo.Create(book);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<Book> Items, int TotalCount)> GetBooksAsync(int page, int pageSize)
        {
            var totalCount = _repository.BooksRepo.FindAll(trackChanges: false).Count();
            var books = await _repository.BooksRepo
                .FindAll(trackChanges: false)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalCount);
        }


        public async Task<Book> GetBookByIdAsync(int id)
        {
            try
            {
                var book = await _repository.BooksRepo.FindByCondition(x => x.Id.Equals(id), false).FirstOrDefaultAsync();
                return (book);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task<bool> UpdateBookAsync(BookUpdateModel model)
        {
            var book = await _repository.BooksRepo.FindByCondition(x => x.Id == model.Id, false).FirstOrDefaultAsync();
            if (book == null)
                return false;

            book.Title = model.Title;
            book.MaxCopies = model.MaxCopies;
            if (model.BookImage != null)
            {
                // Save the image and update the book's image URL
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.BookImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.BookImage.CopyToAsync(stream);
                }
                book.ImageUrl = $"/images/{model.BookImage.FileName}";
            }

            _repository.BooksRepo.Update(book);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _repository.BooksRepo.FindByCondition(x => x.Id == id,false).FirstOrDefaultAsync();
            if (book == null)
                return false;

            _repository.BooksRepo.Delete(book);
            await _repository.SaveAsync();

            return true;
        }
    }

}
