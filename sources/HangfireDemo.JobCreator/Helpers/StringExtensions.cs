namespace DustInTheWind.HangfireDemoSimple.JobCreator.Helpers;

internal static class StringExtensions
{
    public static bool IsNullOrEmpty(this string s)
    {
        return string.IsNullOrEmpty(s);
    }

    public static bool IsNotNullOrEmpty(this string s)
    {
        return !string.IsNullOrEmpty(s);
    }
}