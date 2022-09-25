using Domain.Common.Results;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
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
			_ => StatusCode(500, new { Message = "Api result not handled" }),
		};
	}
}
