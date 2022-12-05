using UnityEngine;

namespace Ru1t3rl.ChalkHunter.UI
{
    public class HealthBarUI : BaseHealthUI
    {
        [SerializeField] private Transform healthBarForeground;
        private float startWidth;
        private Vector3 scale;

        private void Start()
        {
            scale = healthBarForeground.localScale;
            startWidth = healthBarForeground.localScale.x;

            UpdateUI();
        }

        public override void UpdateUI()
        {
            scale.x = startWidth * (health.CurrentHealth / health.MaxHealth);
            healthBarForeground.localScale = scale;
        }
    }
}