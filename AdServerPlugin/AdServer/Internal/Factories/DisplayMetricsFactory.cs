using UnityEngine;

public class DisplayMetricsFactory
{
    public static IDisplayMetrics GetDisplayMetrics()
    {

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidDisplayMetrics();
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new DisplayMetricsIOS();
            default:
                throw new System.Exception("PLATFORM_NOT_SUPPORTED_YET");
        }
    }

    public interface IDisplayMetrics
    {
        public int GetScreenWidthPixels();

        public int GetScreenHeightPixels();

        public float GetScreenDensity();

        public float GetScreenXDpi();

        public float GetScreenYDpi();

        public int GetInPixels(float dp);

        public EdgeInsets GetSafeAreaInsets(AndroidJavaObject activity);

    }
    
    public class EdgeInsets
    {
        public EdgeInsets(int top, int right, int bottom, int left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }

        public int top { get; }
        public int right { get; }
        public int bottom { get; }
        public int left { get; }

        public override string ToString()
        {
            return "top: " + top +
                    "right: " + right +
                    "bottom: " + bottom +
                    "left: " + left;
        }
    }
}
