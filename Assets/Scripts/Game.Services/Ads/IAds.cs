using System;

namespace Game.Services.Ads
{
    public interface IAds
    {
        bool Ready(string placement = "default");
        void ShowInterstitial(Action onClosed);
        void ShowRewarded(Action onRewarded, Action onFailed = null);
    }
}