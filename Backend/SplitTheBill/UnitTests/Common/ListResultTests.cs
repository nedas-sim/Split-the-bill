using Domain.Results;

namespace UnitTests.Common;

public class ListResultTests
{
    [Theory]
    [InlineData(20, 50, 3)]
    [InlineData(20, 40, 2)]
    [InlineData(20, 20, 1)]
    [InlineData(20, 21, 2)]
    [InlineData(20, 0, 0)]
    [InlineData(20, 1, 1)]
    [InlineData(1, 1, 1)]
    public void LastPageTests(int pageSize, int totalCount, int expected)
    {
        // Act:
        int lastPage = ListResult<object>.GetLastPage(pageSize, totalCount);

        // Assert:
        Assert.Equal(expected, lastPage);
    }
}