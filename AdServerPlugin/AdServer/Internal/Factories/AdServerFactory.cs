using System;
using UnityEngine;


internal class AdServerFactory
{
    internal static IAdServer GetAdServer()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return new AndroidAdServer();
            case RuntimePlatform.IPhonePlayer:
            // TODO: return new AdServerIOS();
            default:
                throw new Exception("PLATFORM_NOT_SUPPORTED_YET");

        }
    }

}
