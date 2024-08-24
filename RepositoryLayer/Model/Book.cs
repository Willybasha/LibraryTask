using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxCopies { get; set; }
        public int? AvailableCopies { get; set; }
        public string? ImageUrl { get; set; }
    }
}
