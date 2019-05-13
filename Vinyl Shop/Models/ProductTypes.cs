using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vinyl_Shop.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }

        [Required] // Обязательно для заполнения
        public string Name { get; set; }
    }
}
