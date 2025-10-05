using System;

namespace Game.Services
{
    public interface ITimerService
    {
        /// <summary>
        /// Belirtilen süre sonunda bir eylemi çalıştırır.
        /// </summary>
        /// <param name="duration">Saniye cinsinden bekleme süresi.</param>
        /// <param name="onFinished">Süre bittiğinde çalıştırılacak eylem.</param>
        void Delay(float duration, Action onFinished);
    }
}
