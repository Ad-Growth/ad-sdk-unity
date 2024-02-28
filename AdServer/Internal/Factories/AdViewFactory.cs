using System;
using UnityEngine;

internal class AdViewFactory
{
    internal static IAdView GetAdView(string unitId, AdSize adSize, AdOrientation orientation, int x, int y)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidAdView(unitId, adSize, orientation, x, y);
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new AdViewIOS(unitId, OnLoad);
            default:
                throw new Exception("PLATFORM_NOT_SUPPORTED_YET");

        }
    }
    internal static IAdView GetAdView(string unitId, AdSize adSize, AdOrientation orientation, AdPosition adPosition)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidAdView(unitId, adSize, orientation, adPosition);
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new AdViewIOS(unitId, OnLoad);
            default:
                throw new Exception("PLATFORM_NOT_SUPPORTED_YET");

        }
    }

}
