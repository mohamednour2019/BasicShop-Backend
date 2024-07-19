using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.Cart.ResponseDTOs
{
    public class ProductCartResponsDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set;}
        public decimal CartTotalPrice {  get; set; }
    }
}
