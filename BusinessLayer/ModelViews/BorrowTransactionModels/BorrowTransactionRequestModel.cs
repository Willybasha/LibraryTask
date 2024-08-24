using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ModelViews.BorrowTransactionModels
{
    public class BorrowTransactionRequestModel
    {
        public int BorrowerId { get; set; }
        public int BookId { get; set; }
    }
}
