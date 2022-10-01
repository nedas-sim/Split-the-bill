namespace Domain.Extensions;

public static class ListExtensions
{
    public static List<string> AddIfFalse(this List<string> @this, bool isValid, string message)
    {
        if (isValid is false)
        {
            @this.Add(message);
        }

        return @this;
    }

    public static string BuildErrorMessage(this List<string> @this, string messagePrefix)
    {
        if (@this.Any() is false)
        {
            return string.Empty;
        }

        @this.Insert(0, messagePrefix);

        string message = $"{string.Join(". ", @this)}.";
        return message;
    }
}