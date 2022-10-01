using Domain.Common.Results;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
	private readonly object _responseForUnimplementedResult = new { Message = "Api result not handled" };

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

	protected IActionResult ToNoContent(BaseResult<Unit> result)
	{
        return result switch
		{
			NoContentResult<Unit> => NoContent(),
			ValidationErrorResult<Unit> validationErrorResult => BadRequest(validationErrorResult),
			_ => StatusCode(500, _responseForUnimplementedResult),
		};
	}
}
