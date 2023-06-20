using Microsoft.AspNetCore.Mvc;

namespace BookerApi.Lib;

public class ResultController : ControllerBase
{
    [NonAction]
    public IActionResult Data<T>(T data) => Ok(new DataResult<T>(data));

    [NonAction]
    public IActionResult Error(string message, int statusCode)
    {
        Response.StatusCode = statusCode;
        return new JsonResult(new ErrorResult(message));
    }

    [NonAction]
    public IActionResult Error(Error error, int statusCode)
    {
        Response.StatusCode = statusCode;
        return new JsonResult(new ErrorResult(error));
    }

    [NonAction]
    public IActionResult Success() => Ok(new EmptyResult());
}