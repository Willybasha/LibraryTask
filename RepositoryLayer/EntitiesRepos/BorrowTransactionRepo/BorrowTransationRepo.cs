using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.Model;
using RepositoryLayer.RepositoryBase;

namespace RepositoryLayer.EntitiesRepos.BorrowTransactionRepo
{
    public class BorrowTransationRepo : RepositoryBase<BorrowTransaction> , IBorrowTransactionRepo
    {
        public BorrowTransationRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
