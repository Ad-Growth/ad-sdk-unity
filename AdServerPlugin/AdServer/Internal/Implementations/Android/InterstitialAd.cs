using System;
using UnityEngine;

internal class AndroidInterstitialAd : IInterstitialAd
{
    private const string CLASS_NAME = "com.adgrowth.adserver.InterstitialAd";
    private AndroidJavaObject _ad;
    private readonly AndroidJavaObject _activity;

    internal AndroidInterstitialAd(string unitId) : base(unitId)
    {
        _activity = AndroidActivityHelper.GetCurrentActivity();
        _listener = new AndroidBaseListener<IInterstitialAd>(INTERSTITIAL_LISTENER_PACKAGE, this);
        _ad = new AndroidJavaObject(CLASS_NAME, unitId);
        _ad.Call("setListener", _listener);
    }

    public override void Load(Action<IInterstitialAd> OnLoad)
    {
        _listener.OnLoad += OnLoad;
        _ad?.Call("load", _activity);
    }

    public override void Show()
    {
        _ad?.Call("show", _activity);
    }

    public override bool IsLoaded()
    {
        if (_ad == null) return false;

        return _ad.Call<bool>("isLoaded");
    }

    public override bool IsFailed()
    {
        if (_ad == null) return true;

        return _ad.Call<bool>("isFailed");
    }

    public override void Destroy()
    {
        _ad = null;
    }

}