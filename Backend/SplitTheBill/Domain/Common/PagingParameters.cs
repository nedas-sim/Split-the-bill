namespace Domain.Common;

public abstract class PagingParameters : IPaging
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}