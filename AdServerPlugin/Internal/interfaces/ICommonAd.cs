using System;

public abstract class ICommonAd<T>
{
    internal const string ADVIEW_LISTENER_PACKAGE = "com.adgrowth.adserver.views.AdView$Listener";
    internal const string INTERSTITIAL_LISTENER_PACKAGE = "com.adgrowth.adserver.InterstitialAd$Listener";
    internal const string REWARDED_LISTENER_PACKAGE = "com.adgrowth.adserver.RewardedAd$Listener";
    protected AndroidBaseListener<T> _listener;
    public event Action<T> OnLoad;
    public event Action<AdRequestException> OnFailedToLoad;
    public event Action OnClicked;
    public event Action<string> OnFailedToShow;
    public event Action OnImpression;
    public event Action OnDismissed;
    public abstract void Destroy();
    public abstract bool IsLoaded();
    public abstract bool IsFailed();

    internal virtual void ConfigureEvents()
    {
        _listener.OnLoad += delegate
       {
           if (OnLoad != null)
               OnLoad((T)(object)this);
       };

        _listener.OnFailedToLoad += delegate (AdRequestException exception)
        {
            if (OnFailedToLoad != null)
                OnFailedToLoad(exception);
        };

        _listener.OnDismissed += delegate
        {
            if (OnDismissed != null)
                OnDismissed();
        };

        _listener.OnClicked += delegate
        {
            if (OnClicked != null)
                OnClicked();
        };

        _listener.OnFailedToShow += delegate (string code)
        {
            if (OnFailedToShow != null)
                OnFailedToShow(code);
        };

        _listener.OnImpression += delegate
        {
            if (OnImpression != null)
                OnImpression();
        };
    }
}
