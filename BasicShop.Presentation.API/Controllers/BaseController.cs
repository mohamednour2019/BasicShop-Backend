
using BasicShop.Presentation.API.ControllerPresenter;
using Microsoft.AspNetCore.Mvc;

namespace BasicShop.Presentation.API.Controllers
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        protected Presenter presenter;
        public BaseController()
        {
            presenter = new Presenter();
        }

    }
}
