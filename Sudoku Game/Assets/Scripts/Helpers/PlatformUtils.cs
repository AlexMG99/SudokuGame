using UnityEngine;

public class PlatformUtils
{
    public static bool isAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }

    public static bool isApple()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer;
    }

    public static bool isWindows()
    {
        return (Application.platform == RuntimePlatform.WindowsPlayer ||
             Application.platform == RuntimePlatform.WindowsEditor);
    }

    public static bool isMac()
    {
        return (Application.platform == RuntimePlatform.OSXPlayer ||
             Application.platform == RuntimePlatform.OSXEditor);
    }

    public static bool IsEditorPlatform()
    {
        return (Application.platform == RuntimePlatform.WindowsEditor ||
             Application.platform == RuntimePlatform.OSXEditor);
    }

    public static bool isDesktopPlatform()
    {
        return isWindows() || isMac();
    }
}
