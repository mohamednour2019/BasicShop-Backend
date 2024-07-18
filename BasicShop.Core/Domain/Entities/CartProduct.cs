using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.Entities
{
    public class CartProduct
    {
        public Cart Cart { get; set; }
        public Guid CartId {  get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        [Required(ErrorMessage ="Unit Price Should Be Provided")]
        public int Quantity {  get; set; }

        [Required(ErrorMessage = "Unit Price Shoud Be Priveded")]
        [Range(0, Double.MaxValue, ErrorMessage = "Ivalid Price")]
        public decimal UnitPrice {  get; set; }
    }
}
