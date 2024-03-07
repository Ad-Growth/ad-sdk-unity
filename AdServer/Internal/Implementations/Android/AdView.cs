using System;
using UnityEngine;

internal class AndroidAdView : IAdView
{
    private const string CLASS_NAME = "com.adgrowth.adserver.views.AdView";
    AndroidJavaClass _javaAdOrientationEnum = new AndroidJavaClass("com.adgrowth.adserver.enums.AdOrientation");
    AndroidJavaClass _javaAdSizeEnum = new AndroidJavaClass("com.adgrowth.adserver.enums.AdSize");
    private AndroidJavaObject _ad;
    private AndroidJavaObject _activity;
    private AndroidJavaObject _layoutParams;
    private int _viewWidth;
    private int _viewHeight;
    private DisplayMetricsFactory.IDisplayMetrics _displayMetrics;
    private DisplayMetricsFactory.EdgeInsets _edgeInsets;
    private int _gravity;
    private bool _useSafeAreaInsets = false;
    private AndroidJavaObject _unityView;
    private string _unitId;
    private AdSize _adSize;
    private AdOrientation _orientation;

    internal AndroidAdView(string unitId, AdSize adSize, AdOrientation orientation, int x, int y)
    {
        _unitId = unitId;
        _adSize = adSize;
        _orientation = orientation;
        SetAd();
        ConfigureEvents();
        SetSize();
        SetLayoutParams();
        SetPosition(x, y);
    }

    public AndroidAdView(string unitId, AdSize adSize, AdOrientation orientation, AdPosition adPosition)
    {
        _unitId = unitId;
        _adSize = adSize;
        _orientation = orientation;
        SetAd();
        ConfigureEvents();
        SetSize();
        SetLayoutParams();
        SetPosition(adPosition);
    }


    private void SetSize()
    {
        _displayMetrics = DisplayMetricsFactory.GetDisplayMetrics();
        _edgeInsets = _displayMetrics.GetSafeAreaInsets(_activity);
        switch (_adSize)
        {
            case AdSize.FULL_BANNER:
                _viewWidth = LayoutHelper.FULL_BANNER_WIDTH;
                _viewHeight = LayoutHelper.FULL_BANNER_HEIGHT;
                break;
            case AdSize.LARGE_BANNER:
                _viewWidth = LayoutHelper.LARGE_BANNER_WIDTH;
                _viewHeight = LayoutHelper.LARGE_BANNER_HEIGHT;
                break;
            case AdSize.LEADERBOARD:
                _viewWidth = LayoutHelper.LEADERBOARD_WIDTH;
                _viewHeight = LayoutHelper.LEADERBOARD_HEIGHT;
                break;
            case AdSize.MEDIUM_RECTANGLE:
                _viewWidth = LayoutHelper.MEDIUM_RECTANGLE_WIDTH;
                _viewHeight = LayoutHelper.MEDIUM_RECTANGLE_HEIGHT;
                break;
            case AdSize.BANNER:
            default:
                _viewWidth = LayoutHelper.BANNER_WIDTH;
                _viewHeight = LayoutHelper.BANNER_HEIGHT;
                break;
        }

    }

    private void SetLayoutParams()
    {
        if (_orientation == AdOrientation.PORTRAIT)
        {
            _layoutParams = new AndroidJavaObject("android.widget.FrameLayout$LayoutParams", _displayMetrics.GetInPixels(_viewHeight), _displayMetrics.GetInPixels(_viewWidth));
            return;
        }
        _layoutParams = new AndroidJavaObject("android.widget.FrameLayout$LayoutParams", _displayMetrics.GetInPixels(_viewWidth), _displayMetrics.GetInPixels(_viewHeight));
    }

    private void SetAd()
    {
        _activity = AndroidActivityHelper.GetCurrentActivity();
        _ad = new AndroidJavaObject(
            CLASS_NAME,
            _activity,
            _unitId,
            _javaAdSizeEnum.CallStatic<AndroidJavaObject>("valueOf", Enum.GetName(typeof(AdSize), _adSize)),
            _javaAdOrientationEnum.CallStatic<AndroidJavaObject>("valueOf", Enum.GetName(typeof(AdOrientation), _orientation))
        );
        _listener = new AndroidBaseListener<IAdView>(ADVIEW_LISTENER_PACKAGE, this);
        _ad.Call("setListener", _listener);
    }

    public void SetPosition(AdPosition adPosition)
    {
        AndroidJavaClass Gravity = new AndroidJavaClass("android.view.Gravity");
        // use gravity for position
        switch (adPosition)
        {
            case AdPosition.TOP_LEFT:
                _gravity = Gravity.GetStatic<int>("LEFT");
                break;
            case AdPosition.TOP_CENTER:
                _gravity = Gravity.GetStatic<int>("CENTER_HORIZONTAL");
                break;
            case AdPosition.TOP_RIGHT:
                _gravity = Gravity.GetStatic<int>("RIGHT");
                break;
            case AdPosition.CENTER_LEFT:
                _gravity = Gravity.GetStatic<int>("LEFT") | Gravity.GetStatic<int>("CENTER_VERTICAL");
                break;
            case AdPosition.CENTER:
                _gravity = Gravity.GetStatic<int>("CENTER");
                break;
            case AdPosition.CENTER_RIGHT:
                _gravity = Gravity.GetStatic<int>("RIGHT") | Gravity.GetStatic<int>("CENTER_VERTICAL");
                break;
            case AdPosition.BOTTOM_LEFT:
                _gravity = Gravity.GetStatic<int>("LEFT") | Gravity.GetStatic<int>("BOTTOM");
                break;
            case AdPosition.BOTTOM_CENTER:
                _gravity = Gravity.GetStatic<int>("CENTER") | Gravity.GetStatic<int>("BOTTOM");
                break;
            case AdPosition.BOTTOM_RIGHT:
                _gravity = Gravity.GetStatic<int>("RIGHT") | Gravity.GetStatic<int>("BOTTOM");
                break;
        }

        _layoutParams.Set("gravity", _gravity);
    }


    public void SetPosition(int x, int y)
    {
        // use x and y for position
        if (_useSafeAreaInsets)
        {
            _layoutParams.Set("leftMargin", x + _edgeInsets.left);
            _layoutParams.Set("topMargin", y + _edgeInsets.top);

            _layoutParams.Set("rightMargin", _edgeInsets.right);
            _layoutParams.Set("bottomMargin", _edgeInsets.bottom);
        }
        else
        {
            _layoutParams.Set("leftMargin", x);
            _layoutParams.Set("topMargin", y);
        }
    }

    public void EnableSafeArea(bool enable)
    {
        _useSafeAreaInsets = enable;
    }

    public override void Destroy()
    {
        _unityView?.Call("removeView", _ad);
    }

    public override bool Reload()
    {
        return _ad.Call<bool>("reload");
    }
    public override bool IsLoaded()
    {
        return _ad.Call<bool>("isLoaded");
    }

    public override bool IsFailed()
    {
        return _ad.Call<bool>("isFailed");
    }

    public override void Load()
    {

        AndroidActivityHelper.RunOnUIThread(() =>
        {
            try
            {
                _unityView = _activity.Call<AndroidJavaObject>("getWindow").Call<AndroidJavaObject>("getDecorView");
                _ad.Call("setLayoutParams", _layoutParams);
                _unityView.Call("addView", _ad);
            }
            catch (Exception e)
            { Debug.LogError(e); }

        });

    }


}
