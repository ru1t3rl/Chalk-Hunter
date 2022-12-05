using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ru1t3rl.ChalkHunter.UI
{
    public class HealthIconsUI : BaseHealthUI
    {
        [SerializeField] private GameObject healthIconPrefab;
        [SerializeField] private GameObject lostHealthIconPrefab;
        [SerializeField] private Transform healthIconsParent;
        [SerializeField] private int numberOfIcons = 3;
        private int active = 0;

        private GameObject[] healthIcons;

        protected override void Awake()
        {
            base.Awake();

            healthIcons = new GameObject[numberOfIcons];

            for (int iIcon = 0; iIcon < numberOfIcons; iIcon++)
            {
                healthIcons[iIcon] = Instantiate(healthIconPrefab, healthIconsParent);
            }
        }

        private void Start()
        {
            active = numberOfIcons;
            UpdateUI();
        }

        public override void UpdateUI()
        {
            for (int i = 0; i < numberOfIcons; i++)
            {
                healthIcons[i].SetActive(i / numberOfIcons < health.CurrentHealth / health.MaxHealth);
            }
        }
    }
}