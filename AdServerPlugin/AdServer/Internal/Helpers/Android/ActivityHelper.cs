using UnityEngine;

internal class AndroidActivityHelper
{
    public static AndroidJavaObject GetCurrentActivity()
    {
        return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"); ;
    }
}
