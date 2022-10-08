using Domain.Common;

namespace Infrastructure.Extensions;

public static class PagingExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> @this, PagingParameters pagingParameters)
    {
        @this = @this.Skip(pagingParameters.Skip)
                     .Take(pagingParameters.Take);

        return @this;
    }
}