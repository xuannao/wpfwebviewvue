namespace ba.config.Utility;

internal static class UtilExtensions
{
    public static bool IsNullOrEmpty(this string? s)
    {
        return string.IsNullOrEmpty(s);
    }
}