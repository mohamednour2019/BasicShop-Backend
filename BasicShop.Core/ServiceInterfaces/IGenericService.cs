using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.ServiceInterfaces
{
    public interface IGenericService<TRequestDto, TResponseDto>where TRequestDto:class where TResponseDto:class
    {
        public Task<TResponseDto>perform(TRequestDto requestDto);
    }
}
