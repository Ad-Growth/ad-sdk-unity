using System;
using UnityEngine;

internal class AndroidRewardedAd : IRewardedAd
{
    private const string CLASS_NAME = "com.adgrowth.adserver.RewardedAd";
    private AndroidJavaObject _ad;
    private readonly AndroidJavaObject _activity;

    internal AndroidRewardedAd(string unitId) : base(unitId)
    {
        _activity = AndroidActivityHelper.GetCurrentActivity();
        _listener = new AndroidBaseListener<IRewardedAd>(REWARDED_LISTENER_PACKAGE, this);
        _ad = new AndroidJavaObject(CLASS_NAME, unitId);
        _ad.Call("setListener", _listener);
        ConfigureEvents();
    }

    public override void Load(Action<IRewardedAd> OnLoad)
    {
        _listener.OnLoad += OnLoad;
        _ad?.Call("load", _activity);
    }

    public override void Show(Action<RewardItem> OnEarnedReward)
    {
        _listener.OnEarnedReward += OnEarnedReward;
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
