using System;

public abstract class IInterstitialAd : ICommonAd<IInterstitialAd>
{

    public IInterstitialAd(string unitId) { }

    public abstract void Show();
    public abstract void Load(Action<IInterstitialAd> OnLoad);

}
