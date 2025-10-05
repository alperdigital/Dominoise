using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class TopBar : MonoBehaviour
    {
        [Header("Top Bar References")]
        [SerializeField] private Image logoImage;

        public void Initialize()
        {
            // Initialize top bar components
            if (logoImage != null)
            {
                // Set logo image properties
                logoImage.preserveAspect = true;
            }
        }

        public void SetLogo(Sprite logo)
        {
            if (logoImage != null)
                logoImage.sprite = logo;
        }
    }
}
