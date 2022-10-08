namespace Domain.Common;

public abstract class PagingParameters
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;

    internal int Skip => (Page - 1) * Size;
    internal int Take => Size;
}