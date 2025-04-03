using System.Reflection;

namespace MyLuck.WebApp.Features.Shared.Helpers;

internal static class AppHelper
{
    internal static string GetAppVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version?.ToString();
        return version ?? string.Empty;
    }
}