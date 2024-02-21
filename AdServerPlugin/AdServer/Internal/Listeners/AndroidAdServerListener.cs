using System;
using UnityEngine;

public class AndroidAdServerListener : AndroidJavaProxy
{
    private readonly Action _onInit;

    private readonly Action<SDKInitException> _onFailed;

    public AndroidAdServerListener(Action OnInit, Action<SDKInitException> OnFailed) : base("com.adgrowth.adserver.AdServer$Listener")
    {
        _onInit = OnInit;
        _onFailed = OnFailed;
    }
    public void onInit() { _onInit(); }
    public void onFailed(AndroidJavaObject exception) { _onFailed(new SDKInitException(exception)); }
}