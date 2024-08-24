using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.ModelViews.BookModels
{
    public class BookUpdateModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxCopies { get; set; }
        public IFormFile BookImage { get; set; }
    }
}
