using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.User.RequestDTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage ="First Name is Required")]
        [Length(3,50,ErrorMessage ="First Name Length should be between 3 and 30 Characters")]
        public string FristName {  get; set; }


        [Required(ErrorMessage = "Last Name is Required")]
        [Length(3, 50, ErrorMessage = "Last Name Length should be between 3 and 30 Characters")]
        public string LastName {  get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email {  get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [MinLength(8,ErrorMessage ="Password Shouldn't be less than 8 Charachters")]
        public string Password { get; set; }
        public string PhoneNumber {  get; set; }

    }
}
