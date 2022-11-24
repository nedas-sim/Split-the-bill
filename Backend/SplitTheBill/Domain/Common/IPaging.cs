namespace Domain.Common;

public interface IPaging
{
    public int Page { get; set; }
    public int Size { get; set; }

    internal int Skip => (Page - 1) * Size;
    internal int Take => Size;
}