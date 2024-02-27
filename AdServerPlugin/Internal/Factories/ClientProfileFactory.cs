using System;
using UnityEngine;

public class ClientProfileFactory
{
    public static IClientProfile GetClientProfile()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidClientProfile();
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new IOSClientProfile();
            default:
                throw new Exception("PLATFORM_NOT_SUPPORTED_YET");

        }
    }
}