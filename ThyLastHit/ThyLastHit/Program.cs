using System;
using EloBuddy.SDK.Events;


namespace ThyLastHit
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            LastMenu.Initialize();
            new LastHit().Initialize();
        }
    }
}
