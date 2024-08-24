using BusinessLayer.ModelViews.BorrowTransactionModels;
using BusinessLayer.paging;
using BusinessLayer.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Model;

namespace LibraryTask.Controllers
{
    [Authorize]
    public class BorrowTransactionController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public BorrowTransactionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 5;  // Define the page size
            var borrowTransactions = await _serviceManager.BorrowTransactionService.GetBorrowTransactionsAsync(page, pageSize);
            var borrowers = await _serviceManager.BorrowerService.GetAllBorrowersAsync(page,pageSize);
            var books= await _serviceManager.BookService.GetBooksAsync(page,pageSize);

            var model = new BorrowTransactionListViewModel
            {
                BorrowTransactions = borrowTransactions.Items,
                Borrowers= borrowers.Items,
                Books=books.Items,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = borrowTransactions.TotalCount
                }
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBorrowTransaction([FromBody] BorrowTransactionRequestModel model)
        {
            
            var result = await _serviceManager.BorrowTransactionService.BorrowBookAsync(model);
            if (result.IsSuccess)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { sucess = false , message=result.Message});
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookTransaction request)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceManager.BorrowTransactionService.ReturnBookAsync(request.TransactionId);
                return Json(new { success = true, message = result.Message });
            }
            return Json(new { success = false, message = "Invalid data." });
        }
    }
}
