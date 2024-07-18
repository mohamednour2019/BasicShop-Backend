using System.ComponentModel.DataAnnotations;

namespace BasicShop.Core.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }    
        public User User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

        [Required(ErrorMessage = "Total Price Shoud Be Priveded")]
        [Range(0,Double.MaxValue,ErrorMessage ="Ivalid Price")]
        public decimal TotalPrice {  get; set; }
    }
}
