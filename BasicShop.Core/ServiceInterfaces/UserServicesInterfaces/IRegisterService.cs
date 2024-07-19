using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.User.RequestDTOs;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.ServiceInterfaces.UserServicesInterfaces
{
    public interface IRegisterService:IGenericService<RegisterRequestDto,ResponseModel<UserResponseDto>>
    {
    }
}
