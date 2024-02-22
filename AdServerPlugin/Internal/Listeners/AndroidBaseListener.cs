using System;
using UnityEngine;


public class AndroidBaseListener<T> : AndroidJavaProxy
{
    private readonly T _parent;
    public event Action<T> OnLoad;
    public event Action<AdRequestException> OnFailedToLoad;
    public event Action OnDismissed;
    public event Action OnClicked;
    public event Action<string> OnFailedToShow;
    public event Action OnImpression;
    public event Action<RewardItem> OnEarnedReward;
    public AndroidBaseListener(string package, T ad) : base(package)
    {
        _parent = ad;
    }
    public void onLoad(AndroidJavaObject _ad) { if (OnLoad != null) OnLoad(_parent); }
    public void onFailedToLoad(AndroidJavaObject exception) { if (OnFailedToLoad != null) OnFailedToLoad(new AdRequestException(exception)); }
    public void onDismissed() { if (OnDismissed != null) OnDismissed(); }
    public void onClicked() { if (OnClicked != null) OnClicked(); }
    public void onFailedToShow(string code) { if (OnFailedToShow != null) OnFailedToShow(code); }
    public void onImpression() { if (OnImpression != null) OnImpression(); }
    public void onEarnedReward(AndroidJavaObject rewardItem) { if (OnLoad != null) OnEarnedReward(new RewardItem(rewardItem)); }
}
