using System;
using UnityEngine;

internal class InterstitialAdFactory
{
    internal static IInterstitialAd GetInterstitialAd(string unitId)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidInterstitialAd(unitId);
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new InterstitialAdIOS(unitId);
            default:
                throw new Exception("PLATFORM_NOT_SUPPORTED_YET");

        }
    }

}
