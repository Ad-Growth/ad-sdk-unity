using System;

public abstract class IRewardedAd : ICommonAd<IRewardedAd>
{
    public event Action<RewardItem> OnEarnedReward;
    public IRewardedAd(string unitId) { }

    public abstract void Show(Action<RewardItem> OnEarnedReward);
    public abstract void Load(Action<IRewardedAd> OnLoad);
    internal override void ConfigureEvents()
    {
        base.ConfigureEvents();

        _listener.OnEarnedReward += delegate (RewardItem rewardItem)
        {
            if (OnEarnedReward != null)
                OnEarnedReward(rewardItem);
        };
    }
}
