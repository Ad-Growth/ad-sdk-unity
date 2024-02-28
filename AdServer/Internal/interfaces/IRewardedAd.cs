using System;

public abstract class IRewardedAd : ICommonAd<IRewardedAd>
{

    public IRewardedAd(string unitId) { }

    public abstract void Show(Action<RewardItem> OnEarnedReward);
    public abstract void Load(Action<IRewardedAd> OnLoad);

}
