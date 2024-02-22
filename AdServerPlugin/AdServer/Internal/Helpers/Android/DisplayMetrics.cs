using UnityEngine;
using static DisplayMetricsFactory;

internal class AndroidDisplayMetrics : IDisplayMetrics
{
    private AndroidJavaObject _displayMetrics;
    private AndroidJavaClass _resources;

    internal AndroidDisplayMetrics()
    {
        _resources = new AndroidJavaClass("android.content.res.Resources");
        _displayMetrics = _resources.CallStatic<AndroidJavaObject>("getSystem").Call<AndroidJavaObject>("getDisplayMetrics");
    }

    public int GetScreenWidthPixels()
    {
        return _displayMetrics.Get<int>("widthPixels");
    }

    public int GetScreenHeightPixels()
    {
        return _displayMetrics.Get<int>("heightPixels");
    }

    public float GetScreenDensity()
    {
        return _displayMetrics.Get<float>("density");
    }

    public float GetScreenXDpi()
    {
        return _displayMetrics.Get<float>("xdpi");
    }

    public float GetScreenYDpi()
    {
        return _displayMetrics.Get<float>("ydpi");
    }

    public int GetInPixels(float dp)
    {
        float density = GetScreenDensity();
        return Mathf.RoundToInt(dp * density);
    }

    public EdgeInsets GetSafeAreaInsets(AndroidJavaObject activity)
    {
        AndroidJavaObject window = activity.Call<AndroidJavaObject>("getWindow");

        AndroidJavaObject lp = window.Call<AndroidJavaObject>("getAttributes");

        int LAYOUT_IN_DISPLAY_CUTOUT_MODE_SHORT_EDGES = 1;

        lp.Set("layoutInDisplayCutoutMode", LAYOUT_IN_DISPLAY_CUTOUT_MODE_SHORT_EDGES);

        AndroidJavaObject decorView = window.Call<AndroidJavaObject>("getDecorView");

        AndroidJavaObject rootView = decorView.Call<AndroidJavaObject>("getRootView");

        AndroidJavaObject rootWindowInsets = rootView.Call<AndroidJavaObject>("getRootWindowInsets");

        AndroidJavaObject displayCutout = rootWindowInsets.Call<AndroidJavaObject>("getDisplayCutout");

        int topInset = displayCutout.Call<int>("getSafeInsetTop");
        int bottomInset = displayCutout.Call<int>("getSafeInsetBottom");
        int leftInset = displayCutout.Call<int>("getSafeInsetLeft");
        int rightInset = displayCutout.Call<int>("getSafeInsetRight");

        return new EdgeInsets(topInset, rightInset, bottomInset, leftInset);
    }
}
