using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.Product.RequestDTOs
{
    public class ChangeProductQuantityRequestDto
    {
        [Required(ErrorMessage ="Product Id is Required")]
        public Guid ProductId {  get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity should be between {0} and {1}.")]
        public int Quantity {  get; set; }
    }
}
