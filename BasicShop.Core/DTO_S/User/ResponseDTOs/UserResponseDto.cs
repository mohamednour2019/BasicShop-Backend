using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.DTO_S.User.ResponseDTOs
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public Guid CartId {  get; set; }

        public string Role {  get; set; }

    }
}
