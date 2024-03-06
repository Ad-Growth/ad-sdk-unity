using System;
using UnityEngine;


internal class AndroidActivityHelper
{
    private static AndroidJavaClass _javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject GetCurrentActivity()
    {
        return _javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public static void RunOnUIThread(Action runnable)
    {
        try
        {
            GetCurrentActivity().Call("runOnUiThread", new AndroidJavaRunnable(runnable));
        }
        catch (Exception e)
        { Debug.LogError(e); }
    }
}
