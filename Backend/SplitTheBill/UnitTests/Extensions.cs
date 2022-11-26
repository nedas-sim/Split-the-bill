namespace UnitTests;

public static class Extensions
{
    public static BaseMessageResult<T> ShouldContain<T>(this BaseMessageResult<T> @this, string errorMessage)
    {
        Assert.Contains(errorMessage, @this.Message);

        return @this;
    }
}