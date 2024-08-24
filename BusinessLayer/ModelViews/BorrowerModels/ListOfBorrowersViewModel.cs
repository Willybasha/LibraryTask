using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.paging;
using RepositoryLayer.Entities;

namespace BusinessLayer.ModelViews.BorrowerModels
{
    public class ListOfBorrowersViewModel
    {
        public IEnumerable<Borrower> Borrowers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
