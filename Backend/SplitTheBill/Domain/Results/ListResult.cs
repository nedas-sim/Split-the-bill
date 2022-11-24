using Domain.Common;
using Domain.Common.Results;

namespace Domain.Results;

public sealed class ListResult<T> : BaseListResult<T>
{
    public List<T> Items { get; set; }
    public int PageSize { get; set; }
    public int LastPage { get; set; }
    public int TotalCount { get; set; }
    public bool PreviousPage { get; set; }
    public bool NextPage { get; set; }

    public ListResult(List<T> items, int totalCount, IPaging parameters)
    {
        Items = items;
        LastPage = GetLastPage(parameters.Size, totalCount);
        TotalCount = totalCount;
        PreviousPage = parameters.Page > 1;
        NextPage = parameters.Page * parameters.Size < totalCount;
        PageSize = parameters.Size;
    }

    internal static int GetLastPage(int pageSize, int totalCount)
        => totalCount switch
        {
            0 => 0,
            _ => 1 + (totalCount - 1) / pageSize,
        };
}