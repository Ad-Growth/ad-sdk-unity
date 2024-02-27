
using System;

namespace AdGrowth
{
    public class AdServer
    {
        public static bool isInitialized
        {
            get { return AdServerFactory.GetAdServer().isInitialized; }
        }

        public static IClientProfile clientProfile
        {
            get { return AdServerFactory.GetAdServer().clientProfile; }
        }

        public static void Initialize(Action OnInit, Action<SDKInitException> OnFailed)
        {
            AdServerFactory.GetAdServer().Initialize(OnInit, OnFailed);
        }

    }
}
