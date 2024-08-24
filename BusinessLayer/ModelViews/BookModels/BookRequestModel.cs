using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.ModelViews.BookModels
{
    public class BookRequestModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int MaxCopies { get; set; }
        public int? AvailableCopies { get; set; }
        public IFormFile? BookImage { get; set; }

    }
}
