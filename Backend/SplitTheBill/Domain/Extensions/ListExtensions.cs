using System.Text;

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

        StringBuilder sb = new();
        sb.Append(messagePrefix);
        sb.Append(": ");
        sb.Append(string.Join("; ", @this));
        sb.Append('.');

        return sb.ToString();
    }
}