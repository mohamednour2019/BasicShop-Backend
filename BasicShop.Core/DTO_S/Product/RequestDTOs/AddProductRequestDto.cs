using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.Product.RequestDTOs
{
    public class AddProductRequestDto
    {
        [Required(ErrorMessage ="Product Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity should be between {0} and {1}.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [Range(0,double.MaxValue,ErrorMessage = "Quantity should be between {0} and {1}.")]
        public int QuantityInStock { get; set; }
    }
}
