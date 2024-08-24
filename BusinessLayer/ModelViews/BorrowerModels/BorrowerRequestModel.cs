using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelViews.BorrowerModels;
using BusinessLayer.paging;
using RepositoryLayer.Entities;

namespace BusinessLayer.ModelViews.BorrowerModels
{
    public class BorrowerRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}

   