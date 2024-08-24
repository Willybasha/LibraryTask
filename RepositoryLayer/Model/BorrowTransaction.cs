using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Model
{
    public class BorrowTransaction
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual Borrower Borrower { get; set; }
    }
}
