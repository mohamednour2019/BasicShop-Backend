using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.User.RequestDTOs
{
    public class SignInRequestDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email {  get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [MinLength(8, ErrorMessage = "Password Shouldn't be less than 8 Charachters")]
        public string Password { get; set; }
    }
}
