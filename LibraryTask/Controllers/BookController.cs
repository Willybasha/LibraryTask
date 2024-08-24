using BusinessLayer.ModelViews;
using BusinessLayer.ModelViews.BookModels;
using BusinessLayer.paging;
using BusinessLayer.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTask.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public BookController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;  // Number of books per page
            var books = await _serviceManager.BookService.GetBooksAsync(page, pageSize);

            var model = new BookListViewModel
            {
                Books = books.Items,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = books.TotalCount
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookDetails(int id)
        {
            var book = await _serviceManager.BookService.GetBookByIdAsync(id); // Assuming you have a method in your service to get a book by ID
            if (book == null)
            {
                return NotFound();
            }
            return Json(book); // Return book details as JSON
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.BookService.AddBookAsync(model);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            var result = await _serviceManager.BookService.UpdateBookAsync(model);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Invalid ID." });
            }

            var result = await _serviceManager.BookService.DeleteBookAsync(id);
            return Json(new { success = result });
        }

    }
}
