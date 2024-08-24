using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.RepositoryBase;

namespace RepositoryLayer.EntitiesRepos.BorrowesRepo
{
    public class BorrowerRepo : RepositoryBase<Borrower> , IBorrowersRepo
    {
        public BorrowerRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
