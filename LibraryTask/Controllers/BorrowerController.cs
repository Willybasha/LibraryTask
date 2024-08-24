using BusinessLayer.ModelViews.BookModels;
using BusinessLayer.ModelViews.BorrowerModels;
using BusinessLayer.paging;
using BusinessLayer.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTask.Controllers
{
    [Authorize]
    public class BorrowerController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public BorrowerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 5;
            var borrowers = await _serviceManager.BorrowerService.GetAllBorrowersAsync(page, pageSize);
            var model = new ListOfBorrowersViewModel
            {
                Borrowers = borrowers.Items,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = borrowers.TotalCount
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrower(BorrowerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.BorrowerService.AddBorrowerAsync(model);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            try
            {
                var result = await _serviceManager.BorrowerService.DeleteBorrowerAsync(id);
                return Json(new { success = true });
            }
            catch (InvalidOperationException ex)
            {
                // Return a specific error message if the borrower cannot be deleted
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                return Json(new { success = false, message =ex.Message  });
            }
        }

    }
}
