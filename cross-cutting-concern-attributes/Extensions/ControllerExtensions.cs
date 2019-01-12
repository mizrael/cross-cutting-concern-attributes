using Microsoft.AspNetCore.Mvc;

namespace cross_cutting_concern_attributes.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound<T>(this ControllerBase controller, T data)
        {
            if(null == data)
                return new NotFoundResult();
            return new OkObjectResult(data);
        }
    }
}