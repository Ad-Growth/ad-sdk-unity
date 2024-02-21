using System;
using UnityEngine;

internal class AndroidAdServer : IAdServer
{
    public void initialize(Action OnInit, Action<SDKInitException> OnFailed)
    {
        AndroidJavaClass AdServer = new AndroidJavaClass("com.adgrowth.adserver.AdServer");

        AndroidJavaObject currentActivity = AndroidActivityHelper.GetCurrentActivity();

        AdServer.CallStatic("initialize", currentActivity, new AndroidAdServerListener(OnInit, OnFailed));
    }

}