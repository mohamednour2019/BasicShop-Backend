using BasicShop.Core.Domain.Entities;
using BasicShop.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BasicShop.Presentation.API.ControllerPresenter
{
    public class Presenter
    {
        private ContentResult contentResult;
        public Presenter()
        {
            contentResult = new ContentResult()
            {
                ContentType = "appliction/json"
            };
        }
        public async Task<IActionResult> Handle<TRequestDto, TResponseDto>(TRequestDto? requestDto, IGenericService<TRequestDto, TResponseDto> service)
        {
            TResponseDto response = await service.perform(requestDto);
            contentResult.Content = JsonSerializer.Serialize(response);
            contentResult.StatusCode = (int)HttpStatusCode.OK;
            return contentResult;
        }
    }
}
