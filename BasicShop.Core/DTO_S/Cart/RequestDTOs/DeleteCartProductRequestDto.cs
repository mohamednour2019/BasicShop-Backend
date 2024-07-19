using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.Cart.RequestDTOs
{
    public class DeleteCartProductRequestDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
