using System;
using UnityEngine;

internal class AndroidAdServer : IAdServer
{
    public const string CLASS_NAME = "com.adgrowth.adserver.AdServer";
    private AndroidJavaClass _javaAdServer;
    private AndroidJavaObject _javaCurrentActivity;

    public AndroidAdServer()
    {
        _javaAdServer = GetAndroidJavaClass();
        _javaCurrentActivity = AndroidActivityHelper.GetCurrentActivity();
    }

    public static AndroidJavaClass GetAndroidJavaClass()
    {
        return new AndroidJavaClass(CLASS_NAME);
    }

    public override void Initialize(Action OnInit, Action<SDKInitException> OnFailed)
    {
        _javaAdServer.CallStatic("initialize", _javaCurrentActivity, new AndroidAdServerListener(OnInit, OnFailed));
    }


    public override IClientProfile clientProfile
    {
        get { return new AndroidClientProfile(); }
    }

    public override bool isInitialized
    {
        get { return _javaAdServer.CallStatic<bool>("isInitialized"); }
    }

}
