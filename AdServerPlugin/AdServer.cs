
using System;

namespace AdGrowth
{
    public class AdServer
    {
        public static void initialize(Action OnInit, Action<SDKInitException> OnFailed)
        {
            AdServerFactory.GetAdServer().initialize(OnInit, OnFailed);
        }
    }
}
