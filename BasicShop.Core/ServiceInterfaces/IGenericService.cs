using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.ServiceInterfaces
{
    public interface IGenericService<TRequestDto, TResponseDto>
    {
        public Task<TResponseDto>perform(TRequestDto? requestDto);
    }
}
