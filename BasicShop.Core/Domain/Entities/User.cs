using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.Entities
{
    public class User:IdentityUser<Guid>
    {
        [Required(ErrorMessage ="You Have to Provide First Name")]
        public string FirstName {  get; set; }

        [Required(ErrorMessage = "You Have to Provide Last Name")]
        public string LastName { get; set; }
        public string Role {  get; set; }
        public Cart Cart { get; set; }


    }
}
