namespace UnitTests;

public static class Extensions
{
    public static BaseMessageResult<T> ShouldContain<T>(this BaseMessageResult<T> @this, string errorMessage)
    {
        Assert.Contains(errorMessage, @this.Message);

        return @this;
    }

    public static BaseMessageResult<T> ShouldNotContain<T>(this BaseMessageResult<T> @this, string errorMessage)
    {
        Assert.DoesNotContain(errorMessage, @this.Message);

        return @this;
    }
}