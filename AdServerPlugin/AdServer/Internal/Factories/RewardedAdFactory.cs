using System;
using UnityEngine;

internal class RewardedAdFactory
{
    internal static IRewardedAd GetRewardedAd(string unitId)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidRewardedAd(unitId);
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new RewardedAdIOS(unitId);
            default:
                throw new Exception("PLATFORM_NOT_SUPPORTED_YET");

        }
    }

}
