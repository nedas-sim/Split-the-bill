using Domain.Common.Results;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
	private readonly object _responseForUnimplementedResult = new { Message = "Api result not handled" };
	private readonly string JwtCookieKey = "Token";

    protected readonly ISender sender;

	public BaseController(ISender sender)
	{
		this.sender = sender;
	}

	protected ActionResult<T> FromResult<T>(BaseResult<T> result) where T : class
    {
		return result switch
		{
			SuccessResult<T> success => Ok(success.Item),
			ValidationErrorResult<T> validationErrorResult => BadRequest(validationErrorResult),
			NotFoundResult<T> notFoundResult => NotFound(notFoundResult),
			_ => StatusCode(500, _responseForUnimplementedResult),
		};
	}

	protected IActionResult ToNoContent<T>(BaseResult<T> result)
	{
        return result switch
		{
			SuccessResult<T> or NoContentResult<T> => NoContent(),
			ValidationErrorResult<T> validationErrorResult => BadRequest(validationErrorResult),
            NotFoundResult<T> notFoundResult => NotFound(notFoundResult),
            _ => StatusCode(500, _responseForUnimplementedResult),
		};
	}

	protected void SetJwt(string jwt)
	{
		CookieOptions cookieOptions = new()
		{
			HttpOnly = true,
		};

		Response.Cookies.Append(JwtCookieKey, jwt, cookieOptions);
	}
}
